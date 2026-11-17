using System;
using System.Net.Http;
using System.Windows.Forms;
using TopohitEscritorio.Modelos;
using TopohitEscritorio.Servicios;

namespace TopohitEscritorio
{
    // Ventana para que el usuario que ha entrado (educador o admin) cambie SU contraseña.
    // Se abre de forma modal (ShowDialog) desde "Cambiar contraseña" de la barra lateral.
    public partial class FormCambiarContrasena : Form
    {
        // Igual que en la API (AuthController.MINIMO_CONTRASENA)
        private const int MINIMO_CONTRASENA = 4;

        public FormCambiarContrasena()
        {
            InitializeComponent();
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            // 1. Comprobaciones
            if (txtActual.Text == "" || txtNueva.Text == "" || txtRepetir.Text == "")
            {
                MessageBox.Show("Rellena las tres contraseñas.", "Faltan datos",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (txtNueva.Text.Length < MINIMO_CONTRASENA)
            {
                MessageBox.Show("La contraseña nueva debe tener al menos " + MINIMO_CONTRASENA + " caracteres.",
                                "Contraseña corta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (txtNueva.Text != txtRepetir.Text)
            {
                MessageBox.Show("Las dos contraseñas nuevas no coinciden.", "No coinciden",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 2. Modo demo: no hay API, así que no se guarda nada
            if (Datos.ModoDemo)
            {
                MessageBox.Show("En el modo demo no se guardan las contraseñas.", "Modo demo",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
                return;
            }

            // 3. Se lo pedimos a la API (ella comprueba la actual y cifra la nueva)
            btnGuardar.Enabled = false;
            try
            {
                bool cambiada = await Api.CambiarContrasena(txtActual.Text, txtNueva.Text);
                if (cambiada)
                {
                    MessageBox.Show("Contraseña cambiada. La próxima vez entra con la nueva.", "Hecho",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    // Por ejemplo: "La contraseña actual no es correcta"
                    MessageBox.Show(Api.UltimoError, "No se ha cambiado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
