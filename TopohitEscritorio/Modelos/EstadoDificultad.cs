namespace TopohitEscritorio.Modelos
{
    // CAMBIO 24/11: cómo está una dificultad de Infinito para un jugador
    // (GET api/jugadores/{id}/bloqueos). Mismos nombres que el JSON de la API.
    public class EstadoDificultad
    {
        public string Nombre { get; set; }
        public int Orden { get; set; }
        public int PuntosDesbloqueo { get; set; }
        public string DificultadAnterior { get; set; }   // null en la primera
        public int RecordAnterior { get; set; }
        public bool Abierta { get; set; }
        public bool BloqueadaTutor { get; set; }
        public string Motivo { get; set; }
        public int? Record { get; set; }                 // null = todavía no la ha jugado
        public int Partidas { get; set; }

        public EstadoDificultad()
        {
        }
    }
}
