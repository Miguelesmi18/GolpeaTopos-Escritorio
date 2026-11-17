using System;
using System.Collections.Generic;

namespace TopohitEscritorio.Modelos
{
    // Clases "espejo" del JSON que envía y recibe la API.
    // Los nombres de las propiedades tienen que coincidir con las claves del JSON
    // (da igual mayúsculas o minúsculas: ReadFromJsonAsync no las distingue).
    // NivelHistoria, DificultadContrarreloj y DificultadInfinito no están aquí:
    // la API usa los mismos nombres, así que reutilizamos nuestras clases.

    // Lo que enviamos a POST api/auth/login
    public class LoginPeticion
    {
        public string Username { get; set; }
        public string Password { get; set; }          // la API la comprueba con el hash de la BD
    }

    // Lo que devuelve la API cuando el login va bien
    public class LoginRespuesta
    {
        public string Token { get; set; }
        public DateTime Expira { get; set; }
        public int IdUsuario { get; set; }
        public string Username { get; set; }
        public string Rol { get; set; }               // admin, educador o jugador
    }

    // Lo que enviamos a POST api/jugadores y PUT api/jugadores/{id}
    public class UsuarioPeticion
    {
        public string Username { get; set; }
        public string Password { get; set; }          // vacía en el PUT = no cambiar la contraseña
        public string Rol { get; set; }
        public string Tematica { get; set; }
        public string Observaciones { get; set; }
        public bool Activo { get; set; }
    }

    // Un elemento de GET api/jugadores
    public class JugadorApi
    {
        public int IdUsuario { get; set; }
        public string Username { get; set; }
        public string Rol { get; set; }
        public string Tematica { get; set; }
        public string Observaciones { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaRegistro { get; set; }
    }

    // Un elemento de GET api/sesiones/todas
    public class SesionApi
    {
        public int SessionId { get; set; }
        public int IdUsuario { get; set; }
        public string Username { get; set; }
        public int ModoId { get; set; }
        public string Modo { get; set; }
        public DateTime DateTime { get; set; }
        public int SessionLength { get; set; }
        public int ErrorsMade { get; set; }
        public int FinalScore { get; set; }
        public string Difficulty { get; set; }
        public int? LevelReached { get; set; }
        public int NAttempts { get; set; }
        public int MolesEscaped { get; set; }
        public int Hits { get; set; }
        public decimal? Accuracy { get; set; }
        public int MaxCombo { get; set; }
        public int GoldenHits { get; set; }
        public bool Abandoned { get; set; }
        public int? AvgReactionMs { get; set; }       // tiempo de reacción medio (puede faltar)
    }

    // Lo que enviamos a PUT api/auth/contrasena
    public class CambioContrasena
    {
        public string PasswordActual { get; set; }
        public string PasswordNueva { get; set; }
    }

    // GET y PUT api/modos/criterios: cuándo avisa la pestaña Seguimiento
    public class CriteriosAviso
    {
        public int ReaccionLenta { get; set; }      // % más lenta que la media del grupo
        public int PrecisionBaja { get; set; }      // % de precisión mínimo
        public int AbandonosAltos { get; set; }     // partidas abandonadas
        public int IntentosAtascado { get; set; }   // intentos sin superar un nivel de Historia
        public int DiasSinJugar { get; set; }       // días sin ninguna partida
    }

    // Cuando algo va mal, la API responde { "mensaje": "..." }
    public class MensajeApi
    {
        public string Mensaje { get; set; }
    }
}
