using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopohitEscritorio.Modelos
{
    public class DificultadContrarreloj
    {
        public string Nombre { get; set; }          // Fácil, Medio o Difícil
        public int Agujeros { get; set; }
        public int Tiempo { get; set; }             // segundos de partida
        public int Objetivo { get; set; }
        public int TiempoVisibleMs { get; set; }    // cuánto se ve cada topo (milisegundos)
        public int SaleCadaMs { get; set; }         // cada cuánto sale uno (milisegundos)
        public int ToposALaVez { get; set; }

        // Constructor vacío: lo necesita ReadFromJsonAsync para crear el objeto desde el JSON
        public DificultadContrarreloj()
        {
        }

        public DificultadContrarreloj(string nombre, int agujeros, int tiempo, int objetivo,
                                      int tiempoVisibleMs, int saleCadaMs, int toposALaVez)
        {
            this.Nombre = nombre;
            this.Agujeros = agujeros;
            this.Tiempo = tiempo;
            this.Objetivo = objetivo;
            this.TiempoVisibleMs = tiempoVisibleMs;
            this.SaleCadaMs = saleCadaMs;
            this.ToposALaVez = toposALaVez;
        }
    }
}
