using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using TopohitEscritorio.Modelos;
using TopohitEscritorio.Servicios;

namespace TopohitEscritorio.Pantallas
{
    // Pantalla "Modos y niveles" (reto "Protecció dels usuaris": administrar los juegos).
    // La educadora solo puede activar o desactivar los niveles del modo Historia.
    // El resto de datos (agujeros, objetivo, tiempo...) se ven pero no se cambian:
    // son el diseño del juego y no los piden los retos.
    public partial class FormModos : Form
    {
        // Cómo estaba "Activo" en cada nivel la última vez que coincidía con la base de datos.
        // Así al guardar solo enviamos a la API los niveles que han cambiado (y no los 10 cada vez).
        private List<bool> activosGuardados = new List<bool>();

        public FormModos()
        {
            InitializeComponent();
        }

        private void FormModos_Load(object sender, EventArgs e)
        {
            Ui.EstiloTabla(dgvHistoria);
            RefrescarTabla();
        }

        // Vuelve a enlazar la tabla con la lista de niveles y apunta cómo está cada uno
        private void RefrescarTabla()
        {
            dgvHistoria.DataSource = null;
            dgvHistoria.DataSource = Datos.NivelesHistoria;   // al marcar "Activo" se cambia el objeto de la lista

            activosGuardados.Clear();
            foreach (NivelHistoria n in Datos.NivelesHistoria)
            {
                activosGuardados.Add(n.Activo);
            }
        }

        // Si algo no se ha guardado, volvemos a pedir los niveles a la API
        // para que la tabla enseñe lo que hay de verdad en la base de datos.
        private async Task RecargarNiveles()
        {
            try
            {
                List<NivelHistoria> niveles = await Api.GetNiveles();
                if (niveles != null)
                {
                    Datos.NivelesHistoria.Clear();
                    Datos.NivelesHistoria.AddRange(niveles);
                }
            }
            catch (Exception)
            {
                // Sin conexión: nos quedamos con lo que hay
            }
            RefrescarTabla();
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            dgvHistoria.EndEdit();     // termina la edición de la casilla que esté marcada

            // En modo demo solo se queda en memoria
            if (Datos.ModoDemo)
            {
                MessageBox.Show("Cambios guardados.", "Guardado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            btnGuardar.Enabled = false;
            string error = "";
            int cambiados = 0;
            try
            {
                for (int i = 0; i < Datos.NivelesHistoria.Count; i++)
                {
                    NivelHistoria n = Datos.NivelesHistoria[i];
                    if (n.Activo == activosGuardados[i])
                    {
                        continue;                       // no ha cambiado: no hace falta enviarlo
                    }
                    cambiados++;
                    if (await Api.GuardarNivel(n))      // PUT api/modos/historia/{numero}
                    {
                        activosGuardados[i] = n.Activo;  // ya está igual que en la base de datos
                    }
                    else
                    {
                        error = error + "Nivel " + n.Numero + ": " + Api.UltimoError + "\n";
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
            catch (Exception ex)
            {
                error = ex.Message;
            }
            finally
            {
                btnGuardar.Enabled = true;
            }

            if (error == "" && cambiados == 0)
            {
                MessageBox.Show("No has cambiado ningún nivel.", "Sin cambios", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (error == "")
            {
                MessageBox.Show("Cambios guardados (" + cambiados + " niveles).", "Guardado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Algunos niveles no se han guardado:\n" + error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                await RecargarNiveles();
            }
        }
    }
}
