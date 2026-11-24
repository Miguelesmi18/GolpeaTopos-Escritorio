using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using TopohitEscritorio.Modelos;
using TopohitEscritorio.Servicios;

namespace TopohitEscritorio.Pantallas
{
    // Pantalla "Dificultades" (reto "Protecció dels usuaris": administrar los juegos).
    // CAMBIO 24/11: el juego solo tiene el modo Infinito. Aquí la educadora cambia, de cada dificultad,
    // las vidas, cuánto acelera y los PUNTOS que hay que conseguir en la dificultad anterior para abrirla.
    // Los bloqueos de un jugador concreto se hacen en su ficha (botón "Bloquear dificultades").
    public partial class FormModos : Form
    {
        // Cómo estaba cada dificultad la última vez que coincidía con la base de datos
        // (vidas, incremento y puntos), para enviar a la API solo las que han cambiado.
        private List<string> guardadas = new List<string>();

        public FormModos()
        {
            InitializeComponent();
        }

        private void FormModos_Load(object sender, EventArgs e)
        {
            Ui.EstiloTabla(dgvInfinito);
            RefrescarTabla();
        }

        // Texto que resume una dificultad, para saber si ha cambiado
        private string Huella(DificultadInfinito d)
        {
            return d.Vidas + "|" + d.Incremento + "|" + d.PuntosDesbloqueo;
        }

        // Pone el texto de la columna "Se abre con" a partir de la dificultad anterior de la lista
        private void CalcularRequisitos()
        {
            for (int i = 0; i < Datos.Infinito.Count; i++)
            {
                DificultadInfinito d = Datos.Infinito[i];
                if (i == 0 || d.PuntosDesbloqueo == 0)
                {
                    d.Requisito = "Siempre abierta";
                }
                else
                {
                    d.Requisito = d.PuntosDesbloqueo + " puntos en " + Datos.Infinito[i - 1].Nombre;
                }
            }
        }

        // Vuelve a enlazar la tabla con la lista y apunta cómo está cada dificultad
        private void RefrescarTabla()
        {
            CalcularRequisitos();
            dgvInfinito.DataSource = null;
            dgvInfinito.DataSource = Datos.Infinito;

            guardadas.Clear();
            for (int i = 0; i < Datos.Infinito.Count; i++)
            {
                guardadas.Add(Huella(Datos.Infinito[i]));
            }
        }

        // La primera dificultad siempre está abierta: sus puntos no se pueden cambiar
        private void dgvInfinito_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.RowIndex == 0 && dgvInfinito.Columns[e.ColumnIndex].Name == "colPuntos")
            {
                e.Cancel = true;
            }
        }

        // Si se escribe algo que no es un número, no se deja salir de la celda
        private void dgvInfinito_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("Escribe un número entero.", "Dato no válido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            e.Cancel = true;
        }

        // Al cambiar los puntos se actualiza la columna "Se abre con"
        private void dgvInfinito_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            CalcularRequisitos();
            dgvInfinito.Refresh();
        }

        // Comprueba los valores antes de enviarlos. Devuelve "" si están bien.
        private string Comprobar()
        {
            string error = "";
            int i = 0;
            while (i < Datos.Infinito.Count && error == "")
            {
                DificultadInfinito d = Datos.Infinito[i];
                if (d.Vidas <= 0)
                {
                    error = d.Nombre + ": las vidas tienen que ser 1 o más.";
                }
                else if (d.Incremento < 0)
                {
                    error = d.Nombre + ": la aceleración no puede ser negativa.";
                }
                else if (d.PuntosDesbloqueo < 0)
                {
                    error = d.Nombre + ": los puntos no pueden ser negativos.";
                }
                i++;
            }
            return error;
        }

        // Si algo no se ha guardado, volvemos a pedir las dificultades a la API
        // para que la tabla enseñe lo que hay de verdad en la base de datos.
        private async Task RecargarDificultades()
        {
            try
            {
                List<DificultadInfinito> lista = await Api.GetInfinito();
                if (lista != null)
                {
                    Datos.Infinito.Clear();
                    Datos.Infinito.AddRange(lista);
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
            dgvInfinito.EndEdit();     // termina la edición de la celda que se esté escribiendo

            string incorrecto = Comprobar();
            if (incorrecto != "")
            {
                MessageBox.Show(incorrecto, "Dato no válido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // En modo demo solo se queda en memoria
            if (Datos.ModoDemo)
            {
                RefrescarTabla();
                MessageBox.Show("Cambios guardados.", "Guardado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            btnGuardar.Enabled = false;
            string error = "";
            int cambiadas = 0;
            try
            {
                for (int i = 0; i < Datos.Infinito.Count; i++)
                {
                    DificultadInfinito d = Datos.Infinito[i];
                    if (Huella(d) != guardadas[i])
                    {
                        cambiadas++;
                        if (await Api.GuardarInfinito(d))      // PUT api/modos/infinito/{nombre}
                        {
                            guardadas[i] = Huella(d);
                        }
                        else
                        {
                            error = error + d.Nombre + ": " + Api.UltimoError + "\n";
                        }
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

            if (error == "" && cambiadas == 0)
            {
                MessageBox.Show("No has cambiado ninguna dificultad.", "Sin cambios", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (error == "")
            {
                MessageBox.Show("Cambios guardados (" + cambiadas + " dificultades).", "Guardado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Algunas dificultades no se han guardado:\n" + error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                await RecargarDificultades();
            }
        }
    }
}
