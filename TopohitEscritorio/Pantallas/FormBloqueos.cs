using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using TopohitEscritorio.Modelos;
using TopohitEscritorio.Servicios;

namespace TopohitEscritorio.Pantallas
{
    // CAMBIO 24/11: el tutor bloquea para siempre una o varias dificultades de Infinito a un jugador.
    // Se abre con ShowDialog desde la ficha del jugador (como los formularios de clase).
    // Mientras una dificultad está bloqueada, la tablet la enseña con el candado y el motivo
    // (en lugar de los puntos que faltan) y la API no deja guardar partidas de esa dificultad.
    public partial class FormBloqueos : Form
    {
        private Usuario jugador;
        private List<EstadoDificultad> estados = new List<EstadoDificultad>();

        // Los controles de cada fila, en el orden de la cadena (Fácil, Normal, Difícil)
        private CheckBox[] casillas;
        private TextBox[] motivos;
        private Label[] textosEstado;

        public FormBloqueos(Usuario jugador)
        {
            InitializeComponent();
            this.jugador = jugador;
            casillas = new CheckBox[] { chkBloqueo1, chkBloqueo2, chkBloqueo3 };
            motivos = new TextBox[] { txtMotivo1, txtMotivo2, txtMotivo3 };
            textosEstado = new Label[] { lblEstado1, lblEstado2, lblEstado3 };
        }

        private async void FormBloqueos_Load(object sender, EventArgs e)
        {
            lblTitulo.Text = "Bloquear dificultades · " + jugador.NombreUsuario;
            Ui.BotonPrincipal(btnGuardar);
            Ui.BotonSecundario(btnCancelar);
            ActivarFilas(false);

            if (Datos.ModoDemo)
            {
                lblCargando.Text = "En el modo demo no se pueden bloquear dificultades.";
                return;
            }

            try
            {
                List<EstadoDificultad> lista = await Api.GetBloqueos(jugador.Id);
                if (lista == null)
                {
                    lblCargando.Text = Api.UltimoError;
                }
                else
                {
                    estados = lista;
                    Pintar();
                }
            }
            catch (HttpRequestException)
            {
                lblCargando.Text = "No se puede conectar con la API.";
            }
            catch (TaskCanceledException)
            {
                lblCargando.Text = "La API tarda demasiado en responder.";
            }
        }

        // Rellena cada fila con su dificultad
        private void Pintar()
        {
            lblCargando.Visible = false;
            for (int i = 0; i < casillas.Length; i++)
            {
                if (i < estados.Count)
                {
                    EstadoDificultad d = estados[i];
                    casillas[i].Text = "Bloquear " + d.Nombre;
                    casillas[i].Checked = d.BloqueadaTutor;
                    motivos[i].Text = d.Motivo;
                    textosEstado[i].Text = TextoEstado(d);
                    casillas[i].Enabled = true;
                    motivos[i].Enabled = d.BloqueadaTutor;
                }
                else
                {
                    casillas[i].Visible = false;
                    motivos[i].Visible = false;
                    textosEstado[i].Visible = false;
                }
            }
            btnGuardar.Enabled = true;
        }

        // Qué ve ahora el niño en esta dificultad
        private string TextoEstado(EstadoDificultad d)
        {
            string record = "sin jugar";
            if (d.Record != null)
            {
                record = "récord " + d.Record.Value;
            }

            string texto;
            if (d.BloqueadaTutor)
            {
                texto = "Ahora: bloqueada por el tutor";
            }
            else if (d.Abierta)
            {
                texto = "Ahora: abierta";
            }
            else
            {
                texto = "Ahora: cerrada · le faltan " + (d.PuntosDesbloqueo - d.RecordAnterior) +
                        " puntos en " + d.DificultadAnterior;
            }
            return texto + " · " + record;
        }

        private void ActivarFilas(bool activas)
        {
            for (int i = 0; i < casillas.Length; i++)
            {
                casillas[i].Enabled = activas;
                motivos[i].Enabled = activas;
            }
            btnGuardar.Enabled = activas;
        }

        // El motivo solo se puede escribir si la casilla está marcada
        private void chkBloqueo_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < casillas.Length; i++)
            {
                motivos[i].Enabled = casillas[i].Checked;
            }
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            btnGuardar.Enabled = false;
            string error = "";
            int cambios = 0;
            try
            {
                for (int i = 0; i < estados.Count; i++)
                {
                    EstadoDificultad d = estados[i];
                    string motivo = motivos[i].Text.Trim();
                    string motivoAntes = d.Motivo;
                    if (motivoAntes == null)
                    {
                        motivoAntes = "";
                    }

                    bool correcto = true;
                    if (casillas[i].Checked && (!d.BloqueadaTutor || motivo != motivoAntes))
                    {
                        // Nueva o con otro motivo: PUT api/jugadores/{id}/bloqueos/{dificultad}
                        string enviar = null;
                        if (motivo != "")
                        {
                            enviar = motivo;
                        }
                        correcto = await Api.Bloquear(jugador.Id, d.Nombre, enviar);
                        cambios++;
                    }
                    else if (!casillas[i].Checked && d.BloqueadaTutor)
                    {
                        // Se quita: DELETE api/jugadores/{id}/bloqueos/{dificultad}
                        correcto = await Api.Desbloquear(jugador.Id, d.Nombre);
                        cambios++;
                    }

                    if (!correcto)
                    {
                        error = error + d.Nombre + ": " + Api.UltimoError + "\n";
                    }
                }
            }
            catch (HttpRequestException)
            {
                error = "No se puede conectar con la API.";
            }
            catch (TaskCanceledException)
            {
                error = "La API tarda demasiado en responder.";
            }
            finally
            {
                btnGuardar.Enabled = true;
            }

            if (error != "")
            {
                MessageBox.Show("No se ha podido guardar todo:\n" + error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (cambios == 0)
            {
                MessageBox.Show("No has cambiado nada.", "Sin cambios", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Bloqueos guardados. La tablet los verá la próxima vez que abra la pantalla de dificultades.",
                    "Guardado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
