using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TopohitEscritorio.Modelos;

namespace TopohitEscritorio.Servicios
{
    // Toda la comunicación con la Web API pasa por aquí (como HttpClientCicle en clase).
    // Es static, igual que la clase BD: hay un solo HttpClient para toda la aplicación.
    public static class Api
    {
        // Dirección de la API. Es el perfil "http" de launchSettings.json.
        // Cuando subáis la API al servidor, solo hay que cambiar ESTA línea.
        private const string DIRECCION = "http://localhost:5110/";

        private static HttpClient client = CrearCliente();

        // Se rellena al hacer login. Hace falta para los endpoints con [Authorize].
        public static string Token = null;

        // Último mensaje de error que ha devuelto la API (para enseñarlo en un MessageBox)
        public static string UltimoError = "";

        // Código HTTP del último error (401, 403, 404, 500...)
        public static int UltimoCodigo = 0;

        private static HttpClient CrearCliente()
        {
            HttpClient c = new HttpClient();
            c.BaseAddress = new Uri(DIRECCION);
            c.Timeout = TimeSpan.FromSeconds(15);
            c.DefaultRequestHeaders.Accept.Clear();
            c.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return c;
        }

        // Lee el { "mensaje": "..." } de una respuesta de error y lo guarda en UltimoError
        private static async Task GuardarError(HttpResponseMessage response)
        {
            UltimoCodigo = (int)response.StatusCode;
            UltimoError = "Error " + (int)response.StatusCode + " (" + response.StatusCode + ")";

            // 401 y 403 sin mensaje: los traducimos nosotros
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                UltimoError = "La sesión ha caducado. Cierra sesión y vuelve a entrar.";
            }
            else if (response.StatusCode == HttpStatusCode.Forbidden)
            {
                UltimoError = "No tienes permiso para hacer esto. Solo puede hacerlo un administrador.";
            }

            try
            {
                MensajeApi m = await response.Content.ReadFromJsonAsync<MensajeApi>();
                if (m != null && !string.IsNullOrEmpty(m.Mensaje))
                {
                    UltimoError = m.Mensaje;
                }
            }
            catch (Exception)
            {
                // La respuesta no traía JSON: nos quedamos con el mensaje de arriba
            }
        }

        // ---------- Comprobar conexión ----------

        // GET api/modos: lo usamos para saber si la API está encendida
        public static async Task<bool> EstaConectada()
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync("api/modos");
                return response.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // ---------- Autenticación ----------

        // POST api/auth/login. Si va bien guarda el token y devuelve los datos del usuario (con su rol).
        // Si va mal devuelve null y deja el motivo en UltimoError.
        // CAMBIO 18/11 (API nueva): la contraseña NO viaja. El escritorio la cifra con bcrypt
        // ($2a$, coste 10, sal fija del proyecto) y envía el hash; la API lo compara con el de la BD.
        public static async Task<LoginRespuesta> Login(string usuario, string contrasena)
        {
            LoginPeticion peticion = new LoginPeticion();
            peticion.Username = usuario;
            peticion.PasswordHash = Cifrado.Hash(contrasena);

            HttpResponseMessage response = await client.PostAsJsonAsync("api/auth/login", peticion);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                await GuardarError(response);
                return null;
            }

            LoginRespuesta datos = await response.Content.ReadFromJsonAsync<LoginRespuesta>();
            Token = datos.Token;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
            return datos;
        }

        // PUT api/auth/password: el usuario que ha entrado cambia SU contraseña. true si va bien (200)
        // CAMBIO 18/11: se envían los hashes bcrypt (sal fija) de la actual y de la nueva, nunca las contraseñas.
        // Si la actual no es correcta, la API responde 400 y el mensaje queda en UltimoError.
        public static async Task<bool> CambiarContrasena(string actual, string nueva)
        {
            CambioContrasena peticion = new CambioContrasena();
            peticion.PasswordHashActual = Cifrado.Hash(actual);
            peticion.PasswordHashNueva = Cifrado.Hash(nueva);
            HttpResponseMessage response = await client.PutAsJsonAsync("api/auth/password", peticion);
            return await ComprobarOk(response);
        }

        // Al cerrar sesión borramos el token
        public static void CerrarSesion()
        {
            Token = null;
            client.DefaultRequestHeaders.Authorization = null;
        }

        // ---------- Jugadores (usuarios) ----------

        // GET api/usuarios: todos los usuarios (cualquier rol). Devuelve null si hay error.
        // CAMBIO 18/11: en la API nueva api/jugadores solo trae a los niños; la gestión de usuarios es api/usuarios.
        public static async Task<List<JugadorApi>> GetJugadores()
        {
            HttpResponseMessage response = await client.GetAsync("api/usuarios");
            if (response.StatusCode != HttpStatusCode.OK)
            {
                await GuardarError(response);
                return null;
            }
            return await response.Content.ReadFromJsonAsync<List<JugadorApi>>();
        }

        // Prepara lo que se envía en el POST y el PUT de un usuario
        private static UsuarioPeticion UsuarioAPeticion(Usuario u, string contrasena)
        {
            UsuarioPeticion p = new UsuarioPeticion();
            p.Username = u.NombreUsuario;
            p.Rol = u.Rol;
            p.Tematica = u.Tematica;
            p.AvatarId = u.AvatarId;                  // CAMBIO 19/11
            p.Observaciones = u.Observaciones;
            p.Activo = u.Activo;
            // CAMBIO 18/11: la contraseña no viaja; si han escrito una, enviamos su hash bcrypt (sal fija)
            if (!string.IsNullOrEmpty(contrasena))
            {
                p.PasswordHash = Cifrado.Hash(contrasena);
            }
            return p;
        }

        // POST api/usuarios (solo admin). 201 = creado, 409 = ya existe, 403 = no eres admin
        public static async Task<HttpStatusCode> CrearUsuario(Usuario u, string contrasena)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync("api/usuarios", UsuarioAPeticion(u, contrasena));
            if (response.StatusCode != HttpStatusCode.Created)
            {
                await GuardarError(response);
            }
            return response.StatusCode;
        }

        // PUT api/usuarios/{id} (solo admin). 200 = modificado, 409 = nombre repetido
        // Si han escrito una contraseña nueva, va su hash en el mismo PUT (vacía = no se cambia).
        public static async Task<HttpStatusCode> ModificarUsuario(Usuario u, string contrasena)
        {
            HttpResponseMessage response = await client.PutAsJsonAsync("api/usuarios/" + u.Id, UsuarioAPeticion(u, contrasena));
            if (response.StatusCode != HttpStatusCode.OK)
            {
                await GuardarError(response);
            }
            return response.StatusCode;
        }

        // DELETE api/usuarios/{id} (solo admin). 200 = eliminado, 400 = tiene partidas
        public static async Task<HttpStatusCode> EliminarUsuario(int id)
        {
            HttpResponseMessage response = await client.DeleteAsync("api/usuarios/" + id);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                await GuardarError(response);
            }
            return response.StatusCode;
        }

        // ---------- Partidas ----------

        // GET api/sesiones/todas: todas las partidas de todos los niños en UNA sola petición
        public static async Task<List<SesionApi>> GetTodasSesiones()
        {
            HttpResponseMessage response = await client.GetAsync("api/sesiones/todas");
            if (response.StatusCode != HttpStatusCode.OK)
            {
                await GuardarError(response);
                return null;
            }
            return await response.Content.ReadFromJsonAsync<List<SesionApi>>();
        }

        // ---------- Configuración de los modos ----------

        public static async Task<List<NivelHistoria>> GetNiveles()
        {
            HttpResponseMessage response = await client.GetAsync("api/modos/historia");
            if (response.StatusCode != HttpStatusCode.OK)
            {
                await GuardarError(response);
                return null;
            }
            return await response.Content.ReadFromJsonAsync<List<NivelHistoria>>();
        }

        public static async Task<List<DificultadContrarreloj>> GetContrarreloj()
        {
            HttpResponseMessage response = await client.GetAsync("api/modos/contrarreloj");
            if (response.StatusCode != HttpStatusCode.OK)
            {
                await GuardarError(response);
                return null;
            }
            return await response.Content.ReadFromJsonAsync<List<DificultadContrarreloj>>();
        }

        public static async Task<List<DificultadInfinito>> GetInfinito()
        {
            HttpResponseMessage response = await client.GetAsync("api/modos/infinito");
            if (response.StatusCode != HttpStatusCode.OK)
            {
                await GuardarError(response);
                return null;
            }
            return await response.Content.ReadFromJsonAsync<List<DificultadInfinito>>();
        }

        // CAMBIO 19/11: GET api/modos/avatares, los 12 avatares (tabla AVATAR). null si hay error.
        public static async Task<List<AvatarApi>> GetAvatares()
        {
            HttpResponseMessage response = await client.GetAsync("api/modos/avatares");
            if (response.StatusCode != HttpStatusCode.OK)
            {
                await GuardarError(response);
                return null;
            }
            return await response.Content.ReadFromJsonAsync<List<AvatarApi>>();
        }

        // PUT api/modos/historia/{numero}: activar o desactivar un nivel. true si se ha guardado (200)
        public static async Task<bool> GuardarNivel(NivelHistoria n)
        {
            HttpResponseMessage response = await client.PutAsJsonAsync("api/modos/historia/" + n.Numero, n);
            return await ComprobarOk(response);
        }

        // ---------- CAMBIO 24/11: dificultades de Infinito y bloqueos ----------
        // Los nombres llevan tilde ("Fácil"), así que en la ruta van con Uri.EscapeDataString.

        // PUT api/modos/infinito/{nombre}: vidas, aceleración y puntos para desbloquear. true si va bien (200)
        public static async Task<bool> GuardarInfinito(DificultadInfinito d)
        {
            string ruta = "api/modos/infinito/" + Uri.EscapeDataString(d.Nombre);
            HttpResponseMessage response = await client.PutAsJsonAsync(ruta, d);
            return await ComprobarOk(response);
        }

        // GET api/jugadores/{id}/bloqueos: cómo están las 3 dificultades para ese jugador. null si hay error.
        public static async Task<List<EstadoDificultad>> GetBloqueos(int idUsuario)
        {
            HttpResponseMessage response = await client.GetAsync("api/jugadores/" + idUsuario + "/bloqueos");
            if (response.StatusCode != HttpStatusCode.OK)
            {
                await GuardarError(response);
                return null;
            }
            return await response.Content.ReadFromJsonAsync<List<EstadoDificultad>>();
        }

        // PUT api/jugadores/{id}/bloqueos/{dificultad}: bloquea (o cambia el motivo). true si va bien (200)
        public static async Task<bool> Bloquear(int idUsuario, string dificultad, string motivo)
        {
            BloqueoPeticion peticion = new BloqueoPeticion();
            peticion.Motivo = motivo;
            string ruta = "api/jugadores/" + idUsuario + "/bloqueos/" + Uri.EscapeDataString(dificultad);
            HttpResponseMessage response = await client.PutAsJsonAsync(ruta, peticion);
            return await ComprobarOk(response);
        }

        // DELETE api/jugadores/{id}/bloqueos/{dificultad}: quita el bloqueo. true si va bien (200)
        public static async Task<bool> Desbloquear(int idUsuario, string dificultad)
        {
            string ruta = "api/jugadores/" + idUsuario + "/bloqueos/" + Uri.EscapeDataString(dificultad);
            HttpResponseMessage response = await client.DeleteAsync(ruta);
            return await ComprobarOk(response);
        }

        // GET api/modos/criterios: los criterios de aviso de Seguimiento. null si hay error.
        public static async Task<CriteriosAviso> GetCriterios()
        {
            HttpResponseMessage response = await client.GetAsync("api/modos/criterios");
            if (response.StatusCode != HttpStatusCode.OK)
            {
                await GuardarError(response);
                return null;
            }
            return await response.Content.ReadFromJsonAsync<CriteriosAviso>();
        }

        // PUT api/modos/criterios (solo admin). true si se ha guardado (200)
        public static async Task<bool> GuardarCriterios(CriteriosAviso c)
        {
            HttpResponseMessage response = await client.PutAsJsonAsync("api/modos/criterios", c);
            return await ComprobarOk(response);
        }

        private static async Task<bool> ComprobarOk(HttpResponseMessage response)
        {
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return true;
            }
            await GuardarError(response);
            return false;
        }
    }
}
