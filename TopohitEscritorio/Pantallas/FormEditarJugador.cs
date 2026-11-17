using System;
using System.Net;
using System.Net.Http;
using System.Windows.Forms;
using TopohitEscritorio.Modelos;
using TopohitEscritorio.Pantallas;
using TopohitEscritorio.Servicios;

namespace TopohitEscritorio
{
    // Ventana para crear o editar un usuario. Solo la abre el rol admin.
    // Si todo va bien se cierra con DialogResult.OK para que la lista se refresque.
    public partial class FormEditarJugador : Form
    {
        // Igual que en la API (AuthController.MINIMO_CONTRASENA)
        private const int MINIMO_CONTRASENA = 4;

        // Si vale null, estamos creando un usuario NUEVO.
        private Usuario usuarioEditado;

        public FormEditarJugador(Usuario usuario)
        {
            InitializeComponent();
            this.usuarioEditado = usuario;
        }

        private void FormEditarJugador_Load(object sender, EventArgs e)
        {
            cmbRol.Items.Add("jugador");
            cmbRol.Items.Add("educador");
            cmbRol.Items.Add("admin");

            if (usuarioEditado == null)
            {
                lblTitulo.Text = "Nuevo usuario";
                cmbRol.SelectedItem = "jugador";
                chkActivo.Checked = true;
                btnEliminar.Visible = false;       // no se puede borrar algo que aún no existe
                btnRestablecer.Visible = false;    // un usuario nuevo escribe la contraseña directamente
                txtContrasena.Visible = true;
            }
            else
            {
                lblTitulo.Text = "Editar usuario";
                txtUsuario.Text = usuarioEditado.NombreUsuario;
                cmbRol.SelectedItem = usuarioEditado.Rol;
                chkActivo.Checked = usuarioEditado.Activo;
                txtContrasena.Visible = false;     // aparece al pulsar "Restablecer contraseña"
            }
        }

        // Muestra la caja para escribir la contraseña temporal
        private void btnRestablecer_Click(object sender, EventArgs e)
        {
            btnRestablecer.Visible = false;
            txtContrasena.Visible = true;
            txtContrasena.Focus();
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            // 1. Comprobaciones
            string nombre = txtUsuario.Text.Trim();
            if (nombre.Length < 3 || nombre.Length > 50 || cmbRol.SelectedItem == null)
            {
                MessageBox.Show("El usuario debe tener entre 3 y 50 caracteres, y hay que elegir un rol.",
                                "Faltan datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (usuarioEditado == null && txtContrasena.Text == "")
            {
                MessageBox.Show("Un usuario nuevo necesita una contraseña temporal.", "Faltan datos",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (txtContrasena.Text != "" && txtContrasena.Text.Length < MINIMO_CONTRASENA)
            {
                MessageBox.Show("La contraseña debe tener al menos " + MINIMO_CONTRASENA + " caracteres.", "Contraseña corta",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            foreach (Usuario u in Datos.Usuarios)
            {
                if (u.NombreUsuario == nombre && u != usuarioEditado)
                {
                    MessageBox.Show("Ya existe un usuario con ese nombre.", "Duplicado",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            // 2. Copiamos lo escrito a un objeto nuevo (el original no se toca hasta que la API diga OK).
            //    La temática y las observaciones no se editan aquí: se mantienen las que tenía.
            Usuario datos = new Usuario();
            datos.Tematica = "Normal";
            datos.Observaciones = "";
            if (usuarioEditado != null)
            {
                datos.Id = usuarioEditado.Id;
                datos.Tematica = usuarioEditado.Tematica;
                datos.Observaciones = usuarioEditado.Observaciones;
            }
            datos.NombreUsuario = nombre;
            datos.Rol = cmbRol.SelectedItem.ToString();
            datos.Activo = chkActivo.Checked;

            // 3. Modo demo: solo en memoria
            if (Datos.ModoDemo)
            {
                GuardarEnMemoria(datos);
                this.DialogResult = DialogResult.OK;
                this.Close();
                return;
            }

            // 4. Lo enviamos a la API: POST si es nuevo, PUT si ya existía
            btnGuardar.Enabled = false;
            try
            {
                HttpStatusCode codigo;
                HttpStatusCode esperado;
                if (usuarioEditado == null)
                {
                    codigo = await Api.CrearUsuario(datos, txtContrasena.Text);
                    esperado = HttpStatusCode.Created;      // 201
                }
                else
                {
                    codigo = await Api.ModificarUsuario(datos, txtContrasena.Text);
                    esperado = HttpStatusCode.OK;           // 200
                }

                if (codigo == esperado)
                {
                    await Datos.CargarDesdeApi();           // recargamos para que se vea el cambio
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else if (codigo == HttpStatusCode.Conflict) // 409
                {
                    MessageBox.Show("Ya existe un usuario con ese nombre.", "Duplicado",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else                                         // 400, 403, 404...
                {
                    MessageBox.Show(Api.UltimoError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (HttpRequestException)
            {
                MessageBox.Show("No se puede conectar con la API.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnGuardar.Enabled = true;
            }
        }

        // Modo demo: lo mismo que hacía la aplicación antes de conectarla a la API
        private void GuardarEnMemoria(Usuario datos)
        {
            if (usuarioEditado == null)
            {
                usuarioEditado = new Usuario();
                usuarioEditado.Id = Datos.SiguienteId();
                usuarioEditado.FechaRegistro = DateTime.Now;
                usuarioEditado.UltimaPartidaTexto = "—";
                Datos.Usuarios.Add(usuarioEditado);
            }
            usuarioEditado.NombreUsuario = datos.NombreUsuario;
            usuarioEditado.Rol = datos.Rol;
            usuarioEditado.Tematica = datos.Tematica;
            usuarioEditado.Observaciones = datos.Observaciones;
            usuarioEditado.Activo = datos.Activo;
        }

        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Seguro que quieres eliminar a " + usuarioEditado.NombreUsuario + "?", "Eliminar",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            if (Datos.ModoDemo)
            {
                Datos.Usuarios.Remove(usuarioEditado);
                this.DialogResult = DialogResult.OK;
                this.Close();
                return;
            }

            btnEliminar.Enabled = false;
            try
            {
                HttpStatusCode codigo = await Api.EliminarUsuario(usuarioEditado.Id);
                if (codigo == HttpStatusCode.OK)
                {
                    await Datos.CargarDesdeApi();
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    // Por ejemplo: "No se puede eliminar: tiene partidas guardadas. Desactívalo en su lugar."
                    MessageBox.Show(Api.UltimoError, "No se ha eliminado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (HttpRequestException)
            {
                MessageBox.Show("No se puede conectar con la API.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnEliminar.Enabled = true;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
