using System.Collections.Generic;

namespace TopohitEscritorio.Modelos
{
    // Lo que deja el script de Python (Python/metricas_escritorio.py) en resultado.json:
    // las tarjetas con los números y la lista de gráficas (PNG en la misma carpeta).
    public class ResultadoMetricas
    {
        public List<TarjetaMetrica> Tarjetas { get; set; }
        public List<GraficaMetrica> Graficas { get; set; }
    }

    // Una tarjeta de arriba: "Jugadores 8", "Modelo · accuracy 0.913"...
    public class TarjetaMetrica
    {
        public string Titulo { get; set; }
        public string Valor { get; set; }
    }

    // Una gráfica: su título y el nombre del PNG
    public class GraficaMetrica
    {
        public string Titulo { get; set; }
        public string Archivo { get; set; }
    }
}
