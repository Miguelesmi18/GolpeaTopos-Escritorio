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

        // Constructor vacío: lo necesita ReadFromJsonAsync para crear el objeto desde el JSON
        public DificultadInfinito()
        {
        }

        public DificultadInfinito(string nombre, int agujeros, int vidas, int incremento)
        {
            this.Nombre = nombre;
            this.Agujeros = agujeros;
            this.Vidas = vidas;
            this.Incremento = incremento;
        }
    }
}
