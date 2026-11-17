using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopohitEscritorio.Modelos
{
    public class Partida
    {
        public string SessionId { get; set; }      // session_id
        public string NombreUsuario { get; set; }  // username
        public DateTime Fecha { get; set; }        // date_time
        public string Modo { get; set; }           // historia, contrarreloj o infinito
        public string Nivel { get; set; }          // "4" en historia, "fácil" en los otros
        public int Duracion { get; set; }          // session_length (segundos)
        public int Puntos { get; set; }            // final_score
        public int Aciertos { get; set; }          // hits
        public int Errores { get; set; }           // errors_made
        public int Escapados { get; set; }         // moles_escaped
        public int ReaccionMs { get; set; }        // avg_reaction_ms
        public bool Abandonada { get; set; }       // abandoned
        public int Precision { get; set; }         // accuracy en %, se calcula sola

        public Partida()
        {
        }

        public Partida(string sessionId, string nombreUsuario, DateTime fecha, string modo, string nivel,
                       int duracion, int puntos, int aciertos, int errores, int escapados,
                       int reaccionMs, bool abandonada)
        {
            this.SessionId = sessionId;
            this.NombreUsuario = nombreUsuario;
            this.Fecha = fecha;
            this.Modo = modo;
            this.Nivel = nivel;
            this.Duracion = duracion;
            this.Puntos = puntos;
            this.Aciertos = aciertos;
            this.Errores = errores;
            this.Escapados = escapados;
            this.ReaccionMs = reaccionMs;
            this.Abandonada = abandonada;
            this.Precision = CalcularPrecision();   // la calculamos al crearla
        }

        // ---- Textos para las tablas (propiedades calculadas: solo tienen get) ----

        public string FechaTexto
        {
            get { return Fecha.ToString("dd/MM HH:mm"); }
        }

        // "Historia · 4" o "Contrarreloj · Fácil"
        public string ModoNivelTexto
        {
            get
            {
                string modo = Modo;
                if (modo.Length > 0)
                {
                    modo = modo.Substring(0, 1).ToUpper() + modo.Substring(1);   // primera letra en mayúscula
                }
                if (Nivel == "")
                {
                    return modo;
                }
                return modo + " · " + Nivel;
            }
        }

        public string ReaccionTexto
        {
            get
            {
                if (ReaccionMs == 0)
                {
                    return "—";
                }
                return ReaccionMs + " ms";
            }
        }

        public string DuracionTexto
        {
            get { return Duracion + " s"; }
        }

        public string EstadoTexto
        {
            get
            {
                if (Abandonada)
                {
                    return "Abandonada";
                }
                return "Completa";
            }
        }

        // Número del nivel de Historia (0 si no es Historia)
        public int NivelNumero
        {
            get
            {
                int numero;
                if (Modo == "historia" && int.TryParse(Nivel, out numero))
                {
                    return numero;
                }
                return 0;
            }
        }

        // Método de la clase: aciertos * 100 / (aciertos + errores)
        public int CalcularPrecision()
        {
            int total = this.Aciertos + this.Errores;
            if (total == 0)
            {
                return 0;   // así no dividimos entre cero
            }
            return this.Aciertos * 100 / total;
        }
    }
}
