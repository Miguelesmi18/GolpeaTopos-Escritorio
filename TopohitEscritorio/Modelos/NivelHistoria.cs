using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopohitEscritorio.Modelos
{
    public class NivelHistoria
    {
        public int Numero { get; set; }
        public string Zona { get; set; }       // Campo, Huerto o Mina
        public int Agujeros { get; set; }
        public int Objetivo { get; set; }      // puntos para pasar (0 = nivel jefe)
        public int Tiempo { get; set; }        // segundos
        public string Novedad { get; set; }
        public bool Activo { get; set; }

        // Constructor vacío: lo necesita ReadFromJsonAsync para crear el objeto desde el JSON
        public NivelHistoria()
        {
        }

        public NivelHistoria(int numero, string zona, int agujeros, int objetivo, int tiempo, string novedad)
        {
            this.Numero = numero;
            this.Zona = zona;
            this.Agujeros = agujeros;
            this.Objetivo = objetivo;
            this.Tiempo = tiempo;
            this.Novedad = novedad;
            this.Activo = true;
        }
    }
}
