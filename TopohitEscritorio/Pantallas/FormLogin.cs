using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TopohitEscritorio.Modelos;
using TopohitEscritorio.Servicios;

namespace TopohitEscritorio
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        private async void FormLogin_Load(object sender, EventArgs e)
        {
            lblError.Visible = false;
            await ComprobarApi();
        }

        // Pregunta a la API si está encendida y lo enseña en la etiqueta de estado
        private async Task ComprobarApi()
        {
            lblEstadoApi.Text = "Estado de la API: comprobando...";
            bool conectada = await Api.EstaConectada();
            if (conectada)
            {
                lblEstadoApi.Text = "Estado de la API: ● Conectado";
                lblEstadoApi.ForeColor = Color.Green;
            }
            else
            {
                lblEstadoApi.Text = "Estado de la API: ● Sin conexión";
                lblEstadoApi.ForeColor = Color.Red;
            }
        }

        // async: dentro usamos await para llamar a la API sin congelar la ventana
        private async void btnEntrar_Click(object sender, EventArgs e)
        {
            string nombre = txtUsuario.Text.Trim();
            string contrasena = txtContrasena.Text;

            if (nombre == "" || contrasena == "")
            {
                MostrarError("Escribe el usuario y la contraseña");
                return;
            }

            btnEntrar.Enabled = false;      // así no pulsan dos veces mientras esperamos
            this.Cursor = Cursors.WaitCursor;
            try
            {
                // 1. Login en la API
                LoginRespuesta respuesta = await Api.Login(nombre, contrasena);
                if (respuesta == null)
                {
                    MostrarError(Api.UltimoError);   // "Usuario o contraseña incorrectos"
                    // 401 = contraseña mal, 403 = usuario desactivado (CAMBIO 18/11, API nueva).
                    // Otro código = falla la API o la BD: entonces ofrecemos el modo demo.
                    if (Api.UltimoCodigo != 401 && Api.UltimoCodigo != 403)
                    {
                        PreguntarModoDemo();
                    }
                    return;
                }

                // 2. Solo pueden entrar educadores y admins (los niños solo juegan)
                if (respuesta.Rol != "educador" && respuesta.Rol != "admin")
                {
                    Api.CerrarSesion();
                    MostrarError("Solo pueden entrar educadores o administradores");
                    return;
                }

                // 3. Guardamos quién ha entrado
                Usuario actual = new Usuario();
                actual.Id = respuesta.IdUsuario;
                actual.NombreUsuario = respuesta.Username;
                actual.Rol = respuesta.Rol;
                Datos.UsuarioActual = actual;
                Datos.ModoDemo = false;

                // 4. Descargamos jugadores, partidas y configuración de los modos
                await Datos.CargarDesdeApi();
                lblError.Visible = false;

                // 5. Abrimos la ventana principal
                this.Cursor = Cursors.Default;
                FormPrincipal f = new FormPrincipal();
                f.ShowDialog();          // modal: el código espera aquí hasta que se cierre
                txtContrasena.Text = "";  // al cerrar la principal volvemos al login (= cerrar sesión)
                Api.CerrarSesion();
                Datos.UsuarioActual = null;
            }
            catch (HttpRequestException)
            {
                MostrarError("No se puede conectar con la API. ¿Está en marcha?");
                await ComprobarApi();
                PreguntarModoDemo();
            }
            catch (TaskCanceledException)
            {
                MostrarError("La API tarda demasiado en responder.");
                PreguntarModoDemo();
            }
            catch (Exception ex)
            {
                Api.CerrarSesion();
                Datos.UsuarioActual = null;
                MostrarError(ex.Message);
            }
            finally
            {
                btnEntrar.Enabled = true;
                this.Cursor = Cursors.Default;
            }
        }

        // Si no hay API o base de datos, ofrecemos entrar con los datos de ejemplo
        // para poder seguir probando las pantallas.
        private void PreguntarModoDemo()
        {
            DialogResult r = MessageBox.Show("No se pueden cargar los datos reales.\n\n" +
                                             "¿Quieres entrar en MODO DEMO con datos de ejemplo?\n" +
                                             "(Lo que hagas no se guardará)",
                                             "Modo demo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (r == DialogResult.Yes)
            {
                AbrirModoDemo();
            }
        }

        private void AbrirModoDemo()
        {
            Datos.CargarEjemplo();
            Datos.ModoDemo = true;
            Datos.UsuarioActual = new Usuario(0, "demo", "", "admin", "Normal");
            lblError.Visible = false;

            FormPrincipal f = new FormPrincipal();
            f.Text = f.Text + "  [MODO DEMO]";
            f.ShowDialog();
            txtContrasena.Text = "";
            Datos.UsuarioActual = null;
            Datos.ModoDemo = false;
        }

        private void MostrarError(string mensaje)
        {
            lblError.Text = mensaje;
            lblError.Visible = true;
        }
    }
}
