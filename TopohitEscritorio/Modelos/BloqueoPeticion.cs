namespace TopohitEscritorio.Modelos
{
    // CAMBIO 24/11: cuerpo de PUT api/jugadores/{id}/bloqueos/{dificultad}
    public class BloqueoPeticion
    {
        public string Motivo { get; set; }      // lo que verá el niño (null = mensaje por defecto)

        public BloqueoPeticion()
        {
        }
    }
}
