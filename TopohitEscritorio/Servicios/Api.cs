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
        // La contraseña se envía tal cual: la API la comprueba con el hash bcrypt de la BD.
        public static async Task<LoginRespuesta> Login(string usuario, string contrasena)
        {
            LoginPeticion peticion = new LoginPeticion();
            peticion.Username = usuario;
            peticion.Password = contrasena;

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

        // PUT api/auth/contrasena: el usuario que ha entrado cambia SU contraseña. true si va bien (200)
        public static async Task<bool> CambiarContrasena(string actual, string nueva)
        {
            CambioContrasena peticion = new CambioContrasena();
            peticion.PasswordActual = actual;
            peticion.PasswordNueva = nueva;
            HttpResponseMessage response = await client.PutAsJsonAsync("api/auth/contrasena", peticion);
            return await ComprobarOk(response);
        }

        // Al cerrar sesión borramos el token
        public static void CerrarSesion()
        {
            Token = null;
            client.DefaultRequestHeaders.Authorization = null;
        }

        // ---------- Jugadores (usuarios) ----------

        // GET api/jugadores. Devuelve null si hay error.
        public static async Task<List<JugadorApi>> GetJugadores()
        {
            HttpResponseMessage response = await client.GetAsync("api/jugadores");
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
            p.Observaciones = u.Observaciones;
            p.Activo = u.Activo;
            p.Password = contrasena;                  // vacía = no cambiar la contraseña (la API la cifra)
            return p;
        }

        // POST api/jugadores (solo admin). 201 = creado, 409 = ya existe, 403 = no eres admin
        public static async Task<HttpStatusCode> CrearUsuario(Usuario u, string contrasena)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync("api/jugadores", UsuarioAPeticion(u, contrasena));
            if (response.StatusCode != HttpStatusCode.Created)
            {
                await GuardarError(response);
            }
            return response.StatusCode;
        }

        // PUT api/jugadores/{id} (solo admin). 200 = modificado
        public static async Task<HttpStatusCode> ModificarUsuario(Usuario u, string contrasena)
        {
            HttpResponseMessage response = await client.PutAsJsonAsync("api/jugadores/" + u.Id, UsuarioAPeticion(u, contrasena));
            if (response.StatusCode != HttpStatusCode.OK)
            {
                await GuardarError(response);
            }
            return response.StatusCode;
        }

        // DELETE api/jugadores/{id} (solo admin). 200 = eliminado, 400 = tiene partidas
        public static async Task<HttpStatusCode> EliminarUsuario(int id)
        {
            HttpResponseMessage response = await client.DeleteAsync("api/jugadores/" + id);
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

        // PUT api/modos/historia/{numero}: activar o desactivar un nivel. true si se ha guardado (200)
        public static async Task<bool> GuardarNivel(NivelHistoria n)
        {
            HttpResponseMessage response = await client.PutAsJsonAsync("api/modos/historia/" + n.Numero, n);
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
