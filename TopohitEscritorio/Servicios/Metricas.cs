using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using TopohitEscritorio.Modelos;

namespace TopohitEscritorio.Servicios
{
    // CAMBIO 24/11: pantalla Métricas (reto "Mètriques dels jocs").
    // 1. Pide TODAS las partidas a la API (GET api/sesiones/todas) y las guarda en un JSON.
    // 2. Ejecuta Python/metricas_escritorio.py (pandas, matplotlib y scikit-learn), que deja
    //    las gráficas en PNG y resultado.json en una carpeta temporal.
    // 3. Lee resultado.json para que FormMetricas lo pinte.
    // Hace falta Python con las librerías de Python/requirements.txt (pip install -r requirements.txt).
    public static class Metricas
    {
        // Cómo se llama Python en el ordenador. Si en el vuestro es "py" o "python3", cambiad ESTA línea.
        private const string PYTHON = "python";

        // Tiempo máximo que se espera a Python (la primera vez tarda más: carga las librerías)
        private const int ESPERA_MAXIMA_MS = 180000;

        // Carpeta temporal donde Python deja las gráficas (se borra y se vuelve a crear cada vez)
        public static string Carpeta = Path.Combine(Path.GetTempPath(), "TopohitMetricas");

        // Si algo falla lanza una excepción con el motivo (para enseñarlo en la pantalla)
        public static async Task<ResultadoMetricas> Calcular()
        {
            string json = await Api.GetTodasSesionesJson();
            if (json == null)
            {
                throw new Exception(Api.UltimoError);
            }

            if (Directory.Exists(Carpeta))
            {
                Directory.Delete(Carpeta, true);
            }
            Directory.CreateDirectory(Carpeta);
            string rutaJson = Path.Combine(Carpeta, "partidas.json");
            File.WriteAllText(rutaJson, json, new UTF8Encoding(false));

            string script = Path.Combine(Application.StartupPath, "Python", "metricas_escritorio.py");
            if (!File.Exists(script))
            {
                throw new Exception("No se encuentra " + script);
            }

            // Python tarda unos segundos: se ejecuta fuera del hilo de la pantalla para que no se congele
            await Task.Run(() => EjecutarPython(script, rutaJson));

            string texto = File.ReadAllText(Path.Combine(Carpeta, "resultado.json"), Encoding.UTF8);
            JsonSerializerOptions opciones = new JsonSerializerOptions();
            opciones.PropertyNameCaseInsensitive = true;      // en el JSON las claves van en minúscula
            return JsonSerializer.Deserialize<ResultadoMetricas>(texto, opciones);
        }

        // Esta parte es de información externa que no sale en los vídeos
        // (Process: ejecutar el script de Python desde C# y leer sus errores)
        private static void EjecutarPython(string script, string rutaJson)
        {
            ProcessStartInfo info = new ProcessStartInfo();
            info.FileName = PYTHON;
            info.Arguments = "\"" + script + "\" \"" + rutaJson + "\" \"" + Carpeta + "\"";
            info.WorkingDirectory = Path.GetDirectoryName(script);
            info.UseShellExecute = false;
            info.CreateNoWindow = true;
            info.RedirectStandardError = true;
            info.StandardErrorEncoding = Encoding.UTF8;
            info.EnvironmentVariables["PYTHONIOENCODING"] = "utf-8";

            Process proceso = null;
            try
            {
                proceso = Process.Start(info);
                string errores = proceso.StandardError.ReadToEnd();
                bool terminado = proceso.WaitForExit(ESPERA_MAXIMA_MS);

                if (!terminado)
                {
                    proceso.Kill();
                    throw new Exception("Python ha tardado demasiado en calcular las métricas.");
                }
                if (proceso.ExitCode != 0)
                {
                    throw new Exception(MotivoError(errores));
                }
            }
            catch (Win32Exception)
            {
                // No se ha podido ni arrancar: Python no está instalado o no se llama así
                throw new Exception("No se encuentra Python en este ordenador. Instalad Python y las librerías de " +
                                    "Python/requirements.txt, o cambiad PYTHON en Servicios/Metricas.cs.");
            }
            finally
            {
                if (proceso != null)
                {
                    proceso.Dispose();
                }
            }
        }

        // La última línea del error de Python es la que dice qué ha pasado
        private static string MotivoError(string errores)
        {
            string[] lineas = errores.Trim().Split('\n');
            string ultima = lineas[lineas.Length - 1].Trim();
            if (ultima.Contains("No module named"))
            {
                return "Faltan librerías de Python (" + ultima + "). Ejecutad: pip install -r requirements.txt";
            }
            return "Error de Python: " + ultima;
        }
    }
}
