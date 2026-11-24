using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopohitEscritorio.Servicios;

namespace TopohitEscritorio.Modelos
{
    // Datos de la aplicación (como la clase BD de clase: static, una sola para toda la app).
    // Al hacer login se rellenan con CargarDesdeApi(). En modo demo, con CargarEjemplo().
    public static class Datos
    {
        public static List<Usuario> Usuarios = new List<Usuario>();
        public static List<Partida> Partidas = new List<Partida>();
        public static List<NivelHistoria> NivelesHistoria = new List<NivelHistoria>();
        public static List<DificultadContrarreloj> Contrarreloj = new List<DificultadContrarreloj>();
        public static List<DificultadInfinito> Infinito = new List<DificultadInfinito>();
        public static List<AvatarApi> Avatares = new List<AvatarApi>();   // CAMBIO 19/11: los 12 avatares
        public static Usuario UsuarioActual = null;   // el educador o admin que ha iniciado sesión
        public static bool ModoDemo = false;           // true = datos de ejemplo, no se guarda nada en la API

        // ¿El usuario que ha entrado puede crear, modificar y eliminar usuarios?
        public static bool EsAdmin()
        {
            return ModoDemo || (UsuarioActual != null && UsuarioActual.Rol == "admin");
        }

        private static void VaciarListas()
        {
            Usuarios.Clear();
            Partidas.Clear();
            NivelesHistoria.Clear();
            Contrarreloj.Clear();
            Infinito.Clear();
            Avatares.Clear();
        }

        // CAMBIO 19/11: los mismos 12 avatares que la tabla AVATAR (06_avatares.sql), en el mismo orden.
        // Se usan en el modo demo y si la API todavía no tiene api/modos/avatares.
        private static void AvataresPorDefecto()
        {
            Avatares.Clear();
            Avatares.Add(new AvatarApi(1, "topo_normal_03_feliz"));
            Avatares.Add(new AvatarApi(2, "topo_normal_05_enfadado"));
            Avatares.Add(new AvatarApi(3, "topo_normal_08_dorado"));
            Avatares.Add(new AvatarApi(4, "topo_normal_07_burlon"));
            Avatares.Add(new AvatarApi(5, "topo_halloween_01_feliz_bruja"));
            Avatares.Add(new AvatarApi(6, "topo_halloween_03_enfadado_vampiro"));
            Avatares.Add(new AvatarApi(7, "topo_halloween_06_dorado_bruja"));
            Avatares.Add(new AvatarApi(8, "topo_halloween_05_burlon_fantasma"));
            Avatares.Add(new AvatarApi(9, "topo_navidad_01_feliz_papanoel"));
            Avatares.Add(new AvatarApi(10, "topo_navidad_03_enfadado_reno"));
            Avatares.Add(new AvatarApi(11, "topo_navidad_06_dorado_navidad"));
            Avatares.Add(new AvatarApi(12, "topo_navidad_05_burlon_bufanda"));
        }

        // ---------- Datos REALES de la API ----------
        // Si algo falla lanza una excepción con el motivo.
        public static async Task CargarDesdeApi()
        {
            // 1. Pedimos todo a la API (si algo devuelve null, paramos)
            List<JugadorApi> jugadores = await Api.GetJugadores();
            if (jugadores == null)
            {
                throw new Exception(Api.UltimoError);
            }

            List<SesionApi> sesiones = await Api.GetTodasSesiones();
            if (sesiones == null)
            {
                throw new Exception(Api.UltimoError);
            }

            List<NivelHistoria> niveles = await Api.GetNiveles();
            if (niveles == null)
            {
                throw new Exception(Api.UltimoError);
            }

            List<DificultadContrarreloj> contrarreloj = await Api.GetContrarreloj();
            if (contrarreloj == null)
            {
                throw new Exception(Api.UltimoError);
            }

            List<DificultadInfinito> infinito = await Api.GetInfinito();
            if (infinito == null)
            {
                throw new Exception(Api.UltimoError);
            }

            // Si falla (por ejemplo, aún no se ha ejecutado el SQL de CRITERIOS_AVISO)
            // no paramos: se usan los valores por defecto de Calculos.
            CriteriosAviso criterios = await Api.GetCriterios();

            // CAMBIO 19/11: si falla (API o BD sin la tabla AVATAR), no paramos: se usan los 12 de siempre
            List<AvatarApi> avatares = await Api.GetAvatares();


            // 2. Lo pasamos a nuestras listas
            VaciarListas();

            foreach (JugadorApi j in jugadores)
            {
                Usuario u = new Usuario();
                u.Id = j.IdUsuario;
                u.NombreUsuario = j.Username;
                u.Rol = j.Rol;
                u.Tematica = j.Tematica;
                u.AvatarId = j.AvatarId;
                if (u.AvatarId == 0)
                {
                    u.AvatarId = 1;     // la API todavía no lo envía (falta 06_avatares.sql)
                }
                u.Observaciones = j.Observaciones;
                u.Activo = j.Activo;
                u.FechaRegistro = j.FechaRegistro;
                Usuarios.Add(u);
            }

            foreach (SesionApi s in sesiones)
            {
                Partidas.Add(SesionAPartida(s));
            }

            NivelesHistoria.AddRange(niveles);
            Contrarreloj.AddRange(contrarreloj);
            Infinito.AddRange(infinito);

            if (avatares != null && avatares.Count > 0)
            {
                Avatares.AddRange(avatares);
            }
            else
            {
                AvataresPorDefecto();
            }

            if (criterios != null)
            {
                Calculos.ReaccionLenta = criterios.ReaccionLenta;
                Calculos.PrecisionBaja = criterios.PrecisionBaja;
                Calculos.AbandonosAltos = criterios.AbandonosAltos;
                Calculos.IntentosAtascado = criterios.IntentosAtascado;
                Calculos.DiasSinJugar = criterios.DiasSinJugar;
            }

            CompletarUsuarios();
        }

        // Calcula para cada usuario su número de partidas y la fecha de la última
        private static void CompletarUsuarios()
        {
            foreach (Usuario u in Usuarios)
            {
                u.NumPartidas = 0;
                u.UltimaPartida = null;
                foreach (Partida p in Partidas)
                {
                    if (p.NombreUsuario == u.NombreUsuario)
                    {
                        u.NumPartidas++;
                        if (u.UltimaPartida == null || p.Fecha > u.UltimaPartida.Value)
                        {
                            u.UltimaPartida = p.Fecha;
                        }
                    }
                }
                u.UltimaPartidaTexto = Calculos.TextoFecha(u.UltimaPartida);
            }
        }

        // Convierte una sesión de la API en una Partida de las que usan nuestras tablas
        private static Partida SesionAPartida(SesionApi s)
        {
            Partida p = new Partida();
            p.SessionId = s.SessionId.ToString();
            p.NombreUsuario = s.Username;
            p.Fecha = s.DateTime;
            p.Modo = s.Modo.ToLower();   // "Historia" -> "historia", para que funcionen los filtros
            if (s.LevelReached != null)
            {
                p.Nivel = s.LevelReached.ToString();
            }
            else if (s.Difficulty != null)
            {
                p.Nivel = s.Difficulty;
            }
            else
            {
                p.Nivel = "";
            }
            p.Duracion = s.SessionLength;
            p.Puntos = s.FinalScore;
            p.Aciertos = s.Hits;
            p.Errores = s.ErrorsMade;
            p.Escapados = s.MolesEscaped;
            if (s.AvgReactionMs != null)
            {
                p.ReaccionMs = s.AvgReactionMs.Value;
            }
            else
            {
                p.ReaccionMs = 0;        // partidas antiguas, guardadas antes de existir el campo
            }
            p.Abandonada = s.Abandoned;
            if (s.Accuracy != null)
            {
                p.Precision = (int)Math.Round(s.Accuracy.Value);   // la precisión la calcula la API
            }
            else
            {
                p.Precision = 0;
            }
            return p;
        }

        // ---------- MODO DEMO: datos inventados ----------
        // Para poder ver las pantallas aunque la API o la base de datos no estén disponibles.
        public static void CargarEjemplo()
        {
            VaciarListas();

            // Usuarios: id, usuario, contraseña, rol, temática
            Usuarios.Add(new Usuario(1, "admin", "1234", "admin", "Normal"));
            Usuarios.Add(new Usuario(2, "profe_ana", "1234", "educador", "Normal"));
            Usuarios.Add(new Usuario(3, "martina_r", "1234", "jugador", "Normal"));
            Usuarios.Add(new Usuario(4, "hugo_p", "1234", "jugador", "Halloween"));
            Usuarios.Add(new Usuario(5, "lucia_g", "1234", "jugador", "Navidad"));
            Usuarios.Add(new Usuario(6, "leo_s", "1234", "jugador", "Normal"));
            Usuarios.Add(new Usuario(7, "pablo_m", "1234", "jugador", "Normal"));
            Usuarios.Add(new Usuario(8, "sara_c", "1234", "jugador", "Halloween"));
            Usuarios.Add(new Usuario(9, "mateo_f", "1234", "jugador", "Navidad"));
            Usuarios.Add(new Usuario(10, "aitana_l", "1234", "jugador", "Normal"));

            // CAMBIO 19/11: avatares (ids de la tabla AVATAR)
            AvataresPorDefecto();
            int[] avataresDemo = { 1, 3, 2, 6, 9, 4, 1, 8, 10, 7 };
            for (int i = 0; i < Usuarios.Count; i++)
            {
                Usuarios[i].AvatarId = avataresDemo[i];
            }

            // Partidas: id, usuario, fecha, modo, nivel, duración, puntos, aciertos, errores, escapados, reacción, abandonada
            Partidas.Add(new Partida("a3f9", "hugo_p", new DateTime(2026, 11, 4, 10, 31, 0), "historia", "4", 45, 210, 14, 9, 6, 880, true));
            Partidas.Add(new Partida("a3f8", "martina_r", new DateTime(2026, 11, 4, 10, 28, 0), "contrarreloj", "medio", 60, 520, 48, 3, 2, 450, false));
            Partidas.Add(new Partida("a3f7", "lucia_g", new DateTime(2026, 11, 4, 10, 15, 0), "historia", "5", 45, 330, 31, 12, 7, 610, false));
            Partidas.Add(new Partida("a3f6", "pablo_m", new DateTime(2026, 11, 4, 9, 58, 0), "infinito", "normal", 138, 910, 86, 5, 3, 480, false));
            Partidas.Add(new Partida("a3f5", "leo_s", new DateTime(2026, 11, 3, 18, 2, 0), "historia", "2", 20, 70, 7, 4, 2, 720, true));
            Partidas.Add(new Partida("a3f4", "sara_c", new DateTime(2026, 11, 3, 17, 40, 0), "contrarreloj", "fácil", 60, 310, 31, 2, 1, 530, false));
            Partidas.Add(new Partida("a3f3", "mateo_f", new DateTime(2026, 11, 3, 17, 21, 0), "historia", "6", 45, 470, 44, 8, 5, 560, false));
            Partidas.Add(new Partida("a3f2", "hugo_p", new DateTime(2026, 11, 3, 17, 12, 0), "contrarreloj", "fácil", 60, 260, 26, 7, 4, 820, false));
            Partidas.Add(new Partida("a3f1", "aitana_l", new DateTime(2026, 11, 3, 16, 50, 0), "infinito", "fácil", 52, 190, 19, 6, 5, 650, false));
            Partidas.Add(new Partida("a3f0", "lucia_g", new DateTime(2026, 11, 3, 16, 31, 0), "historia", "4", 33, 150, 15, 10, 6, 700, true));

            // Niveles de Historia: número, zona, agujeros, objetivo, tiempo, novedad
            NivelesHistoria.Add(new NivelHistoria(1, "Campo", 3, 150, 45, "Solo topos normales"));
            NivelesHistoria.Add(new NivelHistoria(2, "Campo", 3, 200, 45, ""));
            NivelesHistoria.Add(new NivelHistoria(3, "Campo", 3, 250, 45, "Aparece el topo dorado"));
            NivelesHistoria.Add(new NivelHistoria(4, "Huerto", 6, 300, 45, "Entra el topo enfadado"));
            NivelesHistoria.Add(new NivelHistoria(5, "Huerto", 6, 400, 45, ""));
            NivelesHistoria.Add(new NivelHistoria(6, "Huerto", 6, 450, 45, "Entran las bombas"));
            NivelesHistoria.Add(new NivelHistoria(7, "Huerto", 6, 500, 50, ""));
            NivelesHistoria.Add(new NivelHistoria(8, "Mina", 9, 600, 50, "9 agujeros"));
            NivelesHistoria.Add(new NivelHistoria(9, "Mina", 9, 700, 50, "Velocidad de Difícil"));
            NivelesHistoria.Add(new NivelHistoria(10, "Mina", 9, 0, 60, "JEFE: topo gigante, 10 golpes"));

            // Contrarreloj: nombre, agujeros, tiempo, objetivo, visible (ms), sale cada (ms), topos a la vez
            Contrarreloj.Add(new DificultadContrarreloj("Fácil", 3, 60, 300, 1500, 1200, 1));
            Contrarreloj.Add(new DificultadContrarreloj("Medio", 6, 60, 500, 1100, 900, 2));
            Contrarreloj.Add(new DificultadContrarreloj("Difícil", 9, 60, 800, 800, 600, 3));

            // Infinito: nombre, agujeros, vidas, incremento, orden y puntos para desbloquear (CAMBIO 24/11)
            Infinito.Add(new DificultadInfinito("Fácil", 3, 5, 1, 1, 0));
            Infinito.Add(new DificultadInfinito("Normal", 6, 3, 2, 2, 500));
            Infinito.Add(new DificultadInfinito("Difícil", 9, 3, 3, 3, 800));

            Partidas.Reverse();     // de la más antigua a la más reciente, igual que las de la API
            CompletarUsuarios();
        }

        // El Id más alto + 1, para dar de alta usuarios nuevos
        public static int SiguienteId()
        {
            int mayor = 0;
            foreach (Usuario u in Usuarios)
            {
                if (u.Id > mayor)
                {
                    mayor = u.Id;
                }
            }
            return mayor + 1;
        }
    }
}
