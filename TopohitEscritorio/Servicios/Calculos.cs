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
        public string ModoHistoria { get; set; }

        // Para pintar en rojo el dato que ha provocado el aviso
        public bool ReaccionEnRojo { get; set; }
        public bool PrecisionEnRojo { get; set; }
        public bool AbandonadasEnRojo { get; set; }
        public bool HistoriaEnRojo { get; set; }
    }

    // Una fila de la tabla "Resumen por modo" (pestaña General)
    public class FilaResumenModo
    {
        public string Modo { get; set; }
        public int Partidas { get; set; }
        public string Accuracy { get; set; }
        public string Errores { get; set; }
        public string Reaccion { get; set; }
        public string Abandonadas { get; set; }   // partidas que el niño dejó a medias
    }

    // Todos los cálculos de las estadísticas. Trabajan con las listas de Datos
    // (que ya vienen de la API) usando foreach, como en clase.
    public static class Calculos
    {
        // Estrellas del modo Historia: % del objetivo del nivel (valores fijos del juego)
        public const int ESTRELLA_1 = 45;
        public const int ESTRELLA_2 = 70;
        public const int ESTRELLA_3 = 100;

        // CAMBIO 19/11: puntos mínimos de una partida en la que se ha vencido al jefe (nivel 10).
        // 10 golpes * 10 + 100 al vencerlo = 200; sin vencerlo no se pasa de 140. Igual que la API
        // (ReglasProgreso.PUNTOS_JEFE_VENCIDO) y la tablet (que da 3 estrellas solo con VICTORIA).
        public const int PUNTOS_JEFE_VENCIDO = 200;

        // Criterios de aviso (pestaña Seguimiento). Ya no son const: el admin los cambia desde
        // el escritorio y se guardan en la tabla CRITERIOS_AVISO. Estos son los valores por defecto,
        // que se usan en el modo demo; al hacer login Datos.CargarDesdeApi() pone los de la base de datos.
        public static int ReaccionLenta = 30;      // 30 % más lenta que la media del grupo
        public static int PrecisionBaja = 60;      // por debajo del 60 %
        public static int AbandonosAltos = 3;      // 3 o más partidas abandonadas
        public static int IntentosAtascado = 10;   // 10 o más intentos sin superar un nivel
        public static int DiasSinJugar = 7;        // ninguna partida en los últimos 7 días

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

        // Partidas de un jugador. modo = null -> todos los modos
        public static List<Partida> PartidasDe(string nombreUsuario, string modo)
        {
            List<Partida> lista = new List<Partida>();
            foreach (Partida p in Datos.Partidas)
            {
                if (p.NombreUsuario == nombreUsuario && (modo == null || p.Modo == modo))
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

        // ---------- Modo Historia: estrellas y niveles ----------

        // Busca la configuración de un nivel (null si no existe)
        public static NivelHistoria BuscarNivel(int numero)
        {
            foreach (NivelHistoria n in Datos.NivelesHistoria)
            {
                if (n.Numero == numero)
                {
                    return n;
                }
            }
            return null;
        }

        // Estrellas (0 a 3) que ha conseguido una partida de Historia
        public static int Estrellas(Partida p)
        {
            NivelHistoria nivel = BuscarNivel(p.NivelNumero);
            if (nivel == null || p.Abandonada)
            {
                return 0;
            }
            if (nivel.Objetivo == 0)
            {
                // CAMBIO 19/11: nivel jefe. Antes daba 3 estrellas solo por no abandonar, aunque se acabara el tiempo
                if (p.Puntos >= PUNTOS_JEFE_VENCIDO)
                {
                    return 3;
                }
                return 0;
            }
            int porcentaje = p.Puntos * 100 / nivel.Objetivo;
            if (porcentaje >= ESTRELLA_3)
            {
                return 3;
            }
            if (porcentaje >= ESTRELLA_2)
            {
                return 2;
            }
            if (porcentaje >= ESTRELLA_1)
            {
                return 1;
            }
            return 0;
        }

        // Mejores estrellas de un jugador en un nivel (0 = no lo ha superado)
        public static int MejoresEstrellas(List<Partida> historia, int numeroNivel)
        {
            int mejor = 0;
            foreach (Partida p in historia)
            {
                if (p.NivelNumero == numeroNivel)
                {
                    int estrellas = Estrellas(p);
                    if (estrellas > mejor)
                    {
                        mejor = estrellas;
                    }
                }
            }
            return mejor;
        }

        // CAMBIO 18/11-B: nivel de Historia por el que va el niño, con la MISMA regla que la API
        // (GET api/sesiones/progreso -> nivelActual): el primer nivel ACTIVO que todavía no ha superado (0★).
        // Los niveles desactivados se saltan, igual que en la tablet. Devuelve 0 si ha completado Historia.
        // En estrellasTotal deja la suma de sus mejores estrellas en los niveles activos.
        public static int NivelActualHistoria(List<Partida> historia, out int estrellasTotal)
        {
            estrellasTotal = 0;
            int nivelActual = 0;
            foreach (NivelHistoria n in Datos.NivelesHistoria)
            {
                if (!n.Activo)
                {
                    continue;      // nivel desactivado por la educadora: no cuenta
                }
                int estrellas = MejoresEstrellas(historia, n.Numero);
                estrellasTotal = estrellasTotal + estrellas;
                if (estrellas == 0 && nivelActual == 0)
                {
                    nivelActual = n.Numero;
                }
            }
            return nivelActual;
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
                List<Partida> historia = PartidasDe(u.NombreUsuario, "historia");

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

                // 3. Modo Historia: nivel actual, estrellas e intentos
                CalcularHistoria(f, historia);

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

        // Rellena la columna "Modo Historia": "Nivel 4 · 6★", "Nivel 2 · 11 intentos" o "Sin empezar"
        private static void CalcularHistoria(FilaSeguimiento f, List<Partida> historia)
        {
            if (historia.Count == 0)
            {
                f.ModoHistoria = "Sin empezar";
                f.HistoriaEnRojo = true;
                return;
            }

            // Nivel actual = el primer nivel activo que todavía no ha superado (con 1★ o más)
            int estrellasTotal;
            int nivelActual = NivelActualHistoria(historia, out estrellasTotal);   // CAMBIO 18/11-B
            if (nivelActual == 0)
            {
                f.ModoHistoria = "Completado · " + estrellasTotal + "★";
                return;
            }

            // ¿Cuántas veces ha intentado el nivel actual?
            int intentos = 0;
            foreach (Partida p in historia)
            {
                if (p.NivelNumero == nivelActual)
                {
                    intentos++;
                }
            }

            if (intentos >= IntentosAtascado)
            {
                f.ModoHistoria = "Nivel " + nivelActual + " · " + intentos + " intentos";
                f.HistoriaEnRojo = true;
                if (f.Aviso == "")
                {
                    f.Aviso = "Atascado en un nivel";
                }
            }
            else
            {
                f.ModoHistoria = "Nivel " + nivelActual + " · " + estrellasTotal + "★";
            }
        }

        // Progreso del grupo: cuántos niños han superado cada nivel y sus estrellas medias.
        // superados[0] es el nivel 1, superados[9] el nivel 10.
        public static void ProgresoHistoria(int[] superados, double[] estrellasMedias)
        {
            for (int nivel = 1; nivel <= 10; nivel++)
            {
                int ninos = 0;
                int sumaEstrellas = 0;
                foreach (Usuario u in Datos.Usuarios)
                {
                    if (SaleEnEstadisticas(u))
                    {
                        int estrellas = MejoresEstrellas(PartidasDe(u.NombreUsuario, "historia"), nivel);
                        if (estrellas > 0)
                        {
                            ninos++;
                            sumaEstrellas = sumaEstrellas + estrellas;
                        }
                    }
                }
                superados[nivel - 1] = ninos;
                if (ninos > 0)
                {
                    estrellasMedias[nivel - 1] = (double)sumaEstrellas / ninos;
                }
                else
                {
                    estrellasMedias[nivel - 1] = 0;
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
