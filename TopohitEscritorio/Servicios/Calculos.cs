using System;
using System.Collections.Generic;
using TopohitEscritorio.Modelos;

namespace TopohitEscritorio.Servicios
{
    // Una fila de la tabla "Niños a revisar" (pestaña Seguimiento)
    public class FilaSeguimiento
    {
        public Usuario Jugador { get; set; }
        public string NombreUsuario { get; set; }
        public string Aviso { get; set; }
        public string Reaccion { get; set; }
        public string Precision { get; set; }
        public string Abandonadas { get; set; }
        public string Dificultad { get; set; }       // CAMBIO 24/11: "Normal · récord 1489" o "Sin empezar" (antes, Modo Historia)

        // Para pintar en rojo el dato que ha provocado el aviso
        public bool ReaccionEnRojo { get; set; }
        public bool PrecisionEnRojo { get; set; }
        public bool AbandonadasEnRojo { get; set; }
        public bool DificultadEnRojo { get; set; }
    }

    // Una fila de la tabla "Resumen por dificultad" (pestaña General). CAMBIO 24/11: antes era por modo
    public class FilaResumenDificultad
    {
        public string Dificultad { get; set; }
        public int Partidas { get; set; }
        public string Accuracy { get; set; }
        public string Errores { get; set; }
        public string Reaccion { get; set; }
        public string Abandonadas { get; set; }   // partidas que el niño dejó a medias
    }

    // Todos los cálculos de las estadísticas. Trabajan con las listas de Datos
    // (que ya vienen de la API) usando foreach, como en clase.
    // CAMBIO 24/11: el juego solo tiene el modo Infinito. Datos solo guarda sus partidas
    // (las antiguas de Historia y Contrarreloj siguen en la base de datos, pero no se enseñan).
    public static class Calculos
    {
        // Criterios de aviso (pestaña Seguimiento). Ya no son const: el admin los cambia desde
        // el escritorio y se guardan en la tabla CRITERIOS_AVISO. Estos son los valores por defecto,
        // que se usan en el modo demo; al hacer login Datos.CargarDesdeApi() pone los de la base de datos.
        public static int ReaccionLenta = 30;      // 30 % más lenta que la media del grupo
        public static int PrecisionBaja = 60;      // por debajo del 60 %
        public static int AbandonosAltos = 3;      // 3 o más partidas abandonadas
        public static int DiasSinJugar = 7;        // ninguna partida en los últimos 7 días

        // CAMBIO 24/11: el aviso "atascado en un nivel de Historia" ya no existe (decisión del equipo).
        // La columna intentos_atascado sigue en CRITERIOS_AVISO y la API la pide al guardar,
        // así que se lee y se vuelve a enviar sin cambiarla.
        public static int IntentosAtascado = 10;

        // ---------- Fechas ----------

        // "Hoy 10:42", "Ayer 17:05", "02/11 18:44" o "—"
        public static string TextoFecha(DateTime? fecha)
        {
            if (fecha == null)
            {
                return "—";
            }
            DateTime f = fecha.Value;
            if (f.Date == DateTime.Today)
            {
                return "Hoy " + f.ToString("HH:mm");
            }
            if (f.Date == DateTime.Today.AddDays(-1))
            {
                return "Ayer " + f.ToString("HH:mm");
            }
            return f.ToString("dd/MM HH:mm");
        }

        // ---------- Partidas ----------

        // ¿Este usuario sale en las estadísticas?
        // Las educadoras y los admins también pueden jugar en la tablet, y sus partidas cuentan
        // igual que las de un niño. Pero si un educador o admin no ha jugado nunca, no lo enseñamos:
        // si no, saldría en "Niños a revisar" como "Sin empezar" sin motivo.
        public static bool SaleEnEstadisticas(Usuario u)
        {
            if (u.Rol == "jugador")
            {
                return true;
            }
            return PartidasDe(u.NombreUsuario, null).Count > 0;
        }

        // Partidas de un jugador. dificultad = null -> todas las dificultades
        public static List<Partida> PartidasDe(string nombreUsuario, string dificultad)
        {
            List<Partida> lista = new List<Partida>();
            foreach (Partida p in Datos.Partidas)
            {
                if (p.NombreUsuario == nombreUsuario && (dificultad == null || p.Nivel == dificultad))
                {
                    lista.Add(p);
                }
            }
            return lista;
        }

        // Partidas desde una fecha (para el período de Seguimiento)
        private static List<Partida> PartidasDesde(List<Partida> partidas, DateTime desde)
        {
            List<Partida> lista = new List<Partida>();
            foreach (Partida p in partidas)
            {
                if (p.Fecha >= desde)
                {
                    lista.Add(p);
                }
            }
            return lista;
        }

        // Reacción media de unas partidas (las que no tienen reacción, 0, no cuentan)
        public static int ReaccionMedia(List<Partida> partidas)
        {
            int suma = 0;
            int cuantas = 0;
            foreach (Partida p in partidas)
            {
                if (p.ReaccionMs > 0)
                {
                    suma = suma + p.ReaccionMs;
                    cuantas++;
                }
            }
            if (cuantas == 0)
            {
                return 0;
            }
            return suma / cuantas;
        }

        public static int PrecisionMedia(List<Partida> partidas)
        {
            if (partidas.Count == 0)
            {
                return 0;
            }
            int suma = 0;
            foreach (Partida p in partidas)
            {
                suma = suma + p.Precision;
            }
            return suma / partidas.Count;
        }

        public static double ErroresMedios(List<Partida> partidas)
        {
            if (partidas.Count == 0)
            {
                return 0;
            }
            int suma = 0;
            foreach (Partida p in partidas)
            {
                suma = suma + p.Errores;
            }
            return (double)suma / partidas.Count;
        }

        public static int ContarAbandonadas(List<Partida> partidas)
        {
            int contador = 0;
            foreach (Partida p in partidas)
            {
                if (p.Abandonada)
                {
                    contador++;
                }
            }
            return contador;
        }

        // ---------- Dificultades (CAMBIO 24/11) ----------

        // Los nombres de las dificultades en su orden (Fácil, Normal, Difícil), como los da la API
        public static List<string> NombresDificultades()
        {
            List<string> nombres = new List<string>();
            foreach (DificultadInfinito d in Datos.Infinito)
            {
                nombres.Add(d.Nombre);
            }
            return nombres;
        }

        // Récord de unas partidas: la mejor puntuación de las que no abandonó (0 si no hay)
        public static int Record(List<Partida> partidas)
        {
            int record = 0;
            foreach (Partida p in partidas)
            {
                if (!p.Abandonada && p.Puntos > record)
                {
                    record = p.Puntos;
                }
            }
            return record;
        }

        // Hasta dónde ha llegado un niño: la dificultad más alta que ha jugado y su récord en ella.
        // Ejemplo: "Normal · récord 1489". Devuelve "" si no ha jugado.
        public static string TextoDificultad(string nombreUsuario)
        {
            string texto = "";
            foreach (string dificultad in NombresDificultades())
            {
                List<Partida> partidas = PartidasDe(nombreUsuario, dificultad);
                if (partidas.Count > 0)
                {
                    texto = dificultad + " · récord " + Record(partidas);    // la última que cumple es la más alta
                }
            }
            return texto;
        }

        // ---------- Pestaña Seguimiento ----------

        // Calcula las filas de "Niños a revisar". diasPeriodo = 0 -> todas las partidas
        public static List<FilaSeguimiento> NinosARevisar(int diasPeriodo)
        {
            DateTime desde = DateTime.MinValue;
            if (diasPeriodo > 0)
            {
                desde = DateTime.Today.AddDays(-diasPeriodo);
            }

            // 1. Reacción media de todo el grupo de niños en el período
            int reaccionGrupo = ReaccionMedia(PartidasDesde(Datos.Partidas, desde));

            List<FilaSeguimiento> filas = new List<FilaSeguimiento>();
            foreach (Usuario u in Datos.Usuarios)
            {
                if (!SaleEnEstadisticas(u) || !u.Activo)
                {
                    continue;    // solo usuarios dados de alta que juegan
                }

                List<Partida> todas = PartidasDe(u.NombreUsuario, null);
                List<Partida> periodo = PartidasDesde(todas, desde);

                FilaSeguimiento f = new FilaSeguimiento();
                f.Jugador = u;
                f.NombreUsuario = u.NombreUsuario;
                f.Aviso = "";

                // 2. Datos del período
                int reaccion = ReaccionMedia(periodo);
                int precision = PrecisionMedia(periodo);
                int abandonadas = ContarAbandonadas(periodo);

                if (periodo.Count == 0)
                {
                    f.Reaccion = "—";
                    f.Precision = "—";
                    f.Abandonadas = "—";
                }
                else
                {
                    f.Reaccion = "—";
                    if (reaccion > 0)
                    {
                        f.Reaccion = reaccion + " ms";
                        if (reaccionGrupo > 0 && reaccion * 100 >= reaccionGrupo * (100 + ReaccionLenta))
                        {
                            int masLenta = (reaccion - reaccionGrupo) * 100 / reaccionGrupo;
                            f.Reaccion = reaccion + " ms (+" + masLenta + " %)";
                            f.ReaccionEnRojo = true;
                            f.Aviso = "Reacción lenta";
                        }
                    }
                    f.Precision = precision + " %";
                    if (precision < PrecisionBaja)
                    {
                        f.PrecisionEnRojo = true;
                        if (f.Aviso == "")
                        {
                            f.Aviso = "Precisión baja";
                        }
                    }
                    f.Abandonadas = abandonadas.ToString();
                    if (abandonadas >= AbandonosAltos)
                    {
                        f.AbandonadasEnRojo = true;
                        if (f.Aviso == "")
                        {
                            f.Aviso = "Abandona partidas";
                        }
                    }
                }

                // 3. CAMBIO 24/11: hasta qué dificultad ha llegado (antes, el nivel de Historia)
                f.Dificultad = TextoDificultad(u.NombreUsuario);
                if (f.Dificultad == "")
                {
                    f.Dificultad = "Sin empezar";
                    f.DificultadEnRojo = true;
                }

                // 4. No ha jugado nunca, o no ha jugado en los últimos días
                if (f.Aviso == "" && u.UltimaPartida == null)
                {
                    f.Aviso = "Sin partidas";
                }
                else if (f.Aviso == "" && u.UltimaPartida.Value < DateTime.Today.AddDays(-DiasSinJugar))
                {
                    f.Aviso = "Sin jugar " + DiasSinJugar + " días";
                }

                if (f.Aviso != "")
                {
                    filas.Add(f);
                }
            }
            return filas;
        }

        // CAMBIO 24/11: progreso del grupo en Infinito (antes, en Historia).
        // Para cada dificultad: cuántos niños la han jugado y la media de sus récords en ella.
        public static void ProgresoDificultades(int[] ninos, int[] recordMedio)
        {
            List<string> nombres = NombresDificultades();
            for (int i = 0; i < nombres.Count; i++)
            {
                int cuantos = 0;
                int sumaRecords = 0;
                foreach (Usuario u in Datos.Usuarios)
                {
                    if (SaleEnEstadisticas(u) && u.Activo)
                    {
                        List<Partida> partidas = PartidasDe(u.NombreUsuario, nombres[i]);
                        if (partidas.Count > 0)
                        {
                            cuantos++;
                            sumaRecords = sumaRecords + Record(partidas);
                        }
                    }
                }
                ninos[i] = cuantos;
                recordMedio[i] = 0;
                if (cuantos > 0)
                {
                    recordMedio[i] = sumaRecords / cuantos;
                }
            }
        }

        public static int ContarJugadores()
        {
            int contador = 0;
            foreach (Usuario u in Datos.Usuarios)
            {
                if (SaleEnEstadisticas(u) && u.Activo)
                {
                    contador++;
                }
            }
            return contador;
        }
    }
}
