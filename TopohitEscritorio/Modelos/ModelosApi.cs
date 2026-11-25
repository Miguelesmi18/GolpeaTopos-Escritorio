using System;
using System.Collections.Generic;

namespace TopohitEscritorio.Modelos
{
    // Clases "espejo" del JSON que envía y recibe la API.
    // Los nombres de las propiedades tienen que coincidir con las claves del JSON
    // (da igual mayúsculas o minúsculas: ReadFromJsonAsync no las distingue).
    // DificultadInfinito no está aquí:
    // la API usa los mismos nombres, así que reutilizamos nuestras clases.

    // Lo que enviamos a POST api/auth/login
    // CAMBIO 18/11 (API nueva): ya no se envía la contraseña, sino su hash bcrypt
    // hecho aquí en el escritorio con Cifrado.Hash (mismo $2a$, coste 10 y sal que la tablet y la BD).
    public class LoginPeticion
    {
        public string Username { get; set; }
        public string PasswordHash { get; set; }
    }

    // Lo que devuelve la API cuando el login va bien
    public class LoginRespuesta
    {
        public string Token { get; set; }
        public DateTime Expira { get; set; }
        public int IdUsuario { get; set; }
        public string Username { get; set; }
        public string Rol { get; set; }               // admin, educador o jugador
        public string Tematica { get; set; }          // CAMBIO 18/11: la API nueva también la devuelve
        public int AvatarId { get; set; }             // CAMBIO 19/11
    }

    // Lo que enviamos a POST api/usuarios y PUT api/usuarios/{id}
    public class UsuarioPeticion
    {
        public string Username { get; set; }
        public string PasswordHash { get; set; }      // CAMBIO 18/11: hash bcrypt hecho en el escritorio (null = no cambiar)
        public string Rol { get; set; }
        public string Tematica { get; set; }
        public int? AvatarId { get; set; }            // CAMBIO 19/11: id de la tabla AVATAR
        public string Observaciones { get; set; }
        public bool Activo { get; set; }
    }

    // Un elemento de GET api/usuarios
    public class JugadorApi
    {
        public int IdUsuario { get; set; }
        public string Username { get; set; }
        public string Rol { get; set; }
        public string Tematica { get; set; }
        public int AvatarId { get; set; }             // CAMBIO 19/11: id de la tabla AVATAR (1 a 12)
        public string Observaciones { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaRegistro { get; set; }
    }

    // CAMBIO 19/11: un elemento de GET api/modos/avatares (tabla AVATAR). No depende de la temática
    // Recurso = nombre del PNG (el mismo que el drawable de Android), que está en Imagenes\Avatares\
    public class AvatarApi
    {
        public int Id { get; set; }
        public string Recurso { get; set; }

        public AvatarApi()
        {
        }

        public AvatarApi(int id, string recurso)
        {
            this.Id = id;
            this.Recurso = recurso;
        }
    }

    // Un elemento de GET api/sesiones/todas
    public class SesionApi
    {
        public int SessionId { get; set; }
        public int UserId { get; set; }               // CAMBIO 18/11: la API nueva lo llama UserId
        public string Username { get; set; }
        public int ModeId { get; set; }               // CAMBIO 18/11: la API nueva lo llama ModeId
        public string Modo { get; set; }
        public DateTime DateTime { get; set; }
        public int SessionLength { get; set; }
        public int ErrorsMade { get; set; }
        public int FinalScore { get; set; }
        public string Difficulty { get; set; }
        public int NAttempts { get; set; }
        public int MolesEscaped { get; set; }
        public int Hits { get; set; }
        public decimal? Accuracy { get; set; }
        public int MaxCombo { get; set; }
        public int GoldenHits { get; set; }
        public bool Abandoned { get; set; }
        public int? AvgReactionMs { get; set; }       // tiempo de reacción medio (puede faltar)
    }

    // CAMBIO 18/11: lo que enviamos a PUT api/auth/password (cambiar la contraseña propia).
    // Van los hashes bcrypt (sal fija), nunca las contraseñas.
    public class CambioContrasena
    {
        public string PasswordHashActual { get; set; }
        public string PasswordHashNueva { get; set; }
    }

    // GET y PUT api/modos/criterios: cuándo avisa la pestaña Seguimiento
    public class CriteriosAviso
    {
        public int ReaccionLenta { get; set; }      // % más lenta que la media del grupo
        public int PrecisionBaja { get; set; }      // % de precisión mínimo
        public int AbandonosAltos { get; set; }     // partidas abandonadas
        public int IntentosAtascado { get; set; }   // CAMBIO 24/11: ya no se usa (era de Historia); se reenvía igual
        public int DiasSinJugar { get; set; }       // días sin ninguna partida
    }

    // Cuando algo va mal, la API responde { "mensaje": "..." }
    public class MensajeApi
    {
        public string Mensaje { get; set; }
    }
}
