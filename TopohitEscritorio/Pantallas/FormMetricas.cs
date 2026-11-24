using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using TopohitEscritorio.Modelos;
using TopohitEscritorio.Servicios;

namespace TopohitEscritorio.Pantallas
{
    // CAMBIO 24/11: pantalla "Métricas" (reto "Mètriques dels jocs").
    // Las métricas de TODOS los jugadores (GET api/sesiones/todas) y el modelo de abandono, calculados
    // con Python (Servicios/Metricas.cs). Arriba, las tarjetas con los números; debajo, las gráficas.
    // Sin textos de conclusiones, como pide el PDF. Las métricas de cada niño están en su tablet.
    public partial class FormMetricas : Form
    {
        public FormMetricas()
        {
            InitializeComponent();
        }

        private void FormMetricas_Load(object sender, EventArgs e)
        {
            Ui.BotonPrincipal(btnCalcular);
            Calcular();
        }

        private void btnCalcular_Click(object sender, EventArgs e)
        {
            Calcular();
        }

        private async void Calcular()
        {
            if (Datos.ModoDemo)
            {
                lblEstado.Text = "En el modo demo no hay métricas: se calculan con las partidas de la API.";
                btnCalcular.Enabled = false;
                return;
            }

            btnCalcular.Enabled = false;
            lblEstado.Visible = true;
            lblEstado.ForeColor = Ui.Marron;
            lblEstado.Text = "Calculando las métricas con Python… (la primera vez tarda unos segundos)";
            VaciarPaneles();

            try
            {
                ResultadoMetricas resultado = await Metricas.Calcular();
                lblEstado.Visible = false;
                PintarTarjetas(resultado);
                PintarGraficas(resultado);
            }
            catch (Exception ex)
            {
                lblEstado.ForeColor = Ui.Rojo;
                lblEstado.Text = "No se han podido calcular las métricas. " + ex.Message;
            }
            btnCalcular.Enabled = true;
        }

        // Quita las tarjetas y gráficas anteriores (y libera sus imágenes)
        private void VaciarPaneles()
        {
            while (flpTarjetas.Controls.Count > 0)
            {
                flpTarjetas.Controls[0].Dispose();
            }
            while (flpGraficas.Controls.Count > 0)
            {
                // Cada caja tiene el título (0) y la imagen (1): la imagen se libera aparte
                PictureBox imagen = (PictureBox)flpGraficas.Controls[0].Controls[1];
                if (imagen.Image != null)
                {
                    imagen.Image.Dispose();
                }
                flpGraficas.Controls[0].Dispose();
            }
        }

        // Una tarjeta por número: el valor grande y el título debajo
        private void PintarTarjetas(ResultadoMetricas resultado)
        {
            foreach (TarjetaMetrica t in resultado.Tarjetas)
            {
                Panel tarjeta = new Panel();
                tarjeta.Size = new Size(212, 58);
                tarjeta.Margin = new Padding(0, 0, 10, 8);
                tarjeta.BackColor = Color.White;
                tarjeta.BorderStyle = BorderStyle.FixedSingle;

                Label valor = new Label();
                valor.Text = t.Valor;
                valor.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
                valor.ForeColor = Ui.Marron;
                valor.Location = new Point(8, 4);
                valor.Size = new Size(196, 26);

                Label titulo = new Label();
                titulo.Text = t.Titulo;
                titulo.Font = new Font("Segoe UI", 8.5F);
                titulo.ForeColor = Ui.Gris;
                titulo.Location = new Point(8, 32);
                titulo.Size = new Size(196, 18);

                tarjeta.Controls.Add(valor);
                tarjeta.Controls.Add(titulo);
                flpTarjetas.Controls.Add(tarjeta);
            }
        }

        // Dos gráficas por fila. Al hacer clic se abre en grande (el árbol no se lee en pequeño)
        private void PintarGraficas(ResultadoMetricas resultado)
        {
            foreach (GraficaMetrica g in resultado.Graficas)
            {
                Panel caja = new Panel();
                caja.Size = new Size(540, 380);
                caja.Margin = new Padding(0, 0, 14, 14);
                caja.BackColor = Color.White;
                caja.BorderStyle = BorderStyle.FixedSingle;

                Label titulo = new Label();
                titulo.Text = g.Titulo;
                titulo.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
                titulo.ForeColor = Ui.Marron;
                titulo.Location = new Point(10, 8);
                titulo.Size = new Size(518, 22);

                PictureBox imagen = new PictureBox();
                imagen.Image = CargarImagen(Path.Combine(Metricas.Carpeta, g.Archivo));
                imagen.SizeMode = PictureBoxSizeMode.Zoom;
                imagen.Location = new Point(10, 34);
                imagen.Size = new Size(518, 336);
                imagen.Cursor = Cursors.Hand;
                imagen.Tag = g.Titulo;
                imagen.Click += new EventHandler(imagen_Click);

                caja.Controls.Add(titulo);
                caja.Controls.Add(imagen);
                flpGraficas.Controls.Add(caja);
            }
        }

        // Se copia la imagen para no dejar el PNG bloqueado (así se puede borrar al calcular otra vez)
        private Image CargarImagen(string ruta)
        {
            Image copia;
            using (Image original = Image.FromFile(ruta))
            {
                copia = new Bitmap(original);
            }
            return copia;
        }

        // Abre la gráfica en una ventana grande (modal, como las demás ventanas con ShowDialog)
        private void imagen_Click(object sender, EventArgs e)
        {
            PictureBox pequena = (PictureBox)sender;

            Form ventana = new Form();
            ventana.Text = "Métricas · " + pequena.Tag;
            ventana.Size = new Size(1300, 860);
            ventana.StartPosition = FormStartPosition.CenterParent;
            ventana.BackColor = Color.White;

            PictureBox grande = new PictureBox();
            grande.Image = pequena.Image;
            grande.SizeMode = PictureBoxSizeMode.Zoom;
            grande.Dock = DockStyle.Fill;
            ventana.Controls.Add(grande);

            ventana.ShowDialog();
            grande.Image = null;          // la imagen es de la gráfica pequeña: no se libera aquí
            ventana.Dispose();
        }
    }
}
