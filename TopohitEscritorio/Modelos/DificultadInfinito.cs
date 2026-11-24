using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopohitEscritorio.Modelos
{
    public class DificultadInfinito
    {
        public string Nombre { get; set; }     // Fácil, Normal o Difícil
        public int Agujeros { get; set; }
        public int Vidas { get; set; }
        public int Incremento { get; set; }    // % que acelera cada segundo
        // CAMBIO 24/11: orden de la cadena (1 Fácil, 2 Normal, 3 Difícil) y puntos que hay que
        // conseguir en la dificultad anterior para abrir esta (0 en la primera: siempre abierta)
        public int Orden { get; set; }
        public int PuntosDesbloqueo { get; set; }
        // No viene de la API: es el texto de la columna "Se abre con" ("500 puntos en Fácil")
        public string Requisito { get; set; }

        // Constructor vacío: lo necesita ReadFromJsonAsync para crear el objeto desde el JSON
        public DificultadInfinito()
        {
        }

        public DificultadInfinito(string nombre, int agujeros, int vidas, int incremento, int orden, int puntosDesbloqueo)
        {
            this.Nombre = nombre;
            this.Agujeros = agujeros;
            this.Vidas = vidas;
            this.Incremento = incremento;
            this.Orden = orden;
            this.PuntosDesbloqueo = puntosDesbloqueo;
        }
    }
}
