using System;
using System.Collections.Generic;
using System.Drawing;         // colores, fuentes, tamaños
using System.Windows.Forms;   // botones, etiquetas, tablas...

namespace TopohitEscritorio.Pantallas
{
    // Colores, imágenes y estilos que se repiten en todas las pantallas (los del Figma).
    // Es static como la clase BD: se usa con Ui.Naranja, Ui.EstiloTabla(...), etc.
    internal static class Ui
    {
        // ---------- Colores ----------
        public static Color Marron = Color.FromArgb(59, 35, 20);         // barra lateral, textos, bordes
        public static Color Sombra = Color.FromArgb(43, 26, 16);         // sombra de tarjetas y botones
        public static Color Naranja = Color.FromArgb(242, 140, 40);      // títulos y botón principal
        public static Color Crema = Color.FromArgb(255, 244, 220);       // fondo de las pantallas
        public static Color Gris = Color.FromArgb(150, 118, 92);         // textos secundarios
        public static Color Linea = Color.FromArgb(242, 228, 200);       // líneas de las tablas
        public static Color Verde = Color.FromArgb(91, 170, 58);         // errores en las gráficas
        public static Color VerdeClaro = Color.FromArgb(140, 198, 110);  // barras de reacción
        public static Color Morado = Color.FromArgb(128, 70, 200);       // zona Mina
        public static Color Rojo = Color.FromArgb(200, 50, 40);          // datos que provocan un aviso
        public static Color Beige = Color.FromArgb(246, 226, 190);       // etiqueta "Completa"

        // ---------- Imágenes ----------
        // Están en Properties > Resources (van dentro del .exe, no hace falta copiar archivos)
        public static Image TopoNormal = Properties.Resources.topo_normal;
        public static Image TopoHalloween = Properties.Resources.topo_halloween;
        public static Image TopoNavidad = Properties.Resources.topo_navidad;

        // El avatar del niño según su temática
        public static Image Avatar(string tematica)
        {
            if (tematica == "Halloween")
            {
                return TopoHalloween;
            }
            if (tematica == "Navidad")
            {
                return TopoNavidad;
            }
            return TopoNormal;
        }

        // ---------- Botones ----------

        // Naranja con borde marrón (por ejemplo "Guardar cambios")
        public static void BotonPrincipal(Button b)
        {
            b.FlatStyle = FlatStyle.Flat;
            b.FlatAppearance.BorderColor = Marron;
            b.FlatAppearance.BorderSize = 2;
            b.BackColor = Naranja;
            b.ForeColor = Color.White;
            b.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            b.Cursor = Cursors.Hand;
        }

        // Blanco con borde marrón (por ejemplo "Cancelar")
        public static void BotonSecundario(Button b)
        {
            b.FlatStyle = FlatStyle.Flat;
            b.FlatAppearance.BorderColor = Marron;
            b.FlatAppearance.BorderSize = 2;
            b.BackColor = Color.White;
            b.ForeColor = Marron;
            b.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            b.Cursor = Cursors.Hand;
        }

        // Pestañas: la activa naranja, las demás blancas
        public static void Pestana(Button b, bool activa)
        {
            if (activa)
            {
                BotonPrincipal(b);
            }
            else
            {
                BotonSecundario(b);
            }
        }

        // ---------- Tablas (DataGridView) ----------

        // Estilo del Figma: cabecera marrón, filas blancas altas y líneas suaves
        public static void EstiloTabla(DataGridView dgv)
        {
            dgv.AutoGenerateColumns = false;
            dgv.BackgroundColor = Color.White;
            dgv.BorderStyle = BorderStyle.None;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.GridColor = Linea;
            dgv.RowHeadersVisible = false;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.AllowUserToResizeRows = false;
            dgv.AllowUserToResizeColumns = false;
            dgv.MultiSelect = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgv.ColumnHeadersHeight = 40;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Marron;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.SelectionBackColor = Marron;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.Padding = new Padding(8, 0, 0, 0);
            dgv.DefaultCellStyle.BackColor = Color.White;
            dgv.DefaultCellStyle.ForeColor = Marron;
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 10F);
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(253, 232, 205);
            dgv.DefaultCellStyle.SelectionForeColor = Marron;
            dgv.DefaultCellStyle.Padding = new Padding(8, 0, 0, 0);
            dgv.RowTemplate.Height = 46;

            // Las columnas de botones ("Editar", "Ver") con el estilo de botón blanco
            foreach (DataGridViewColumn c in dgv.Columns)
            {
                if (c is DataGridViewButtonColumn)
                {
                    DataGridViewButtonColumn boton = (DataGridViewButtonColumn)c;
                    boton.FlatStyle = FlatStyle.Flat;
                    boton.DefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
                    boton.DefaultCellStyle.Padding = new Padding(4, 6, 4, 6);
                    boton.DefaultCellStyle.BackColor = Color.White;
                    boton.DefaultCellStyle.SelectionBackColor = Color.White;
                }
                if (c is DataGridViewImageColumn)
                {
                    ((DataGridViewImageColumn)c).ImageLayout = DataGridViewImageCellLayout.Zoom;
                    c.DefaultCellStyle.NullValue = null;     // sin imagen: hueco vacío en vez de una X roja
                    c.DefaultCellStyle.Padding = new Padding(6);
                }
            }
        }

        // ---------- Gráficas de barras hechas con Panels ----------
        // zona: el Panel donde se dibuja (se vacía cada vez)
        // etiquetas: texto debajo de cada grupo de barras (niveles, dificultades...)
        // series: una lista de valores por cada color (por ejemplo hits y errores)
        // colores: el color de cada serie
        // textosEncima: texto encima del grupo (puede ser null)
        // coloresPorBarra: (opcional) si hay una sola serie, un color distinto para cada barra
        public static void DibujarBarras(Panel zona, string[] etiquetas, List<double[]> series,
                                         Color[] colores, string[] textosEncima, Color[] coloresPorBarra = null)
        {
            zona.Controls.Clear();
            if (etiquetas.Length == 0)
            {
                return;
            }

            int altoEtiqueta = 22;
            int altoTexto = 0;
            if (textosEncima != null)
            {
                altoTexto = 34;
            }
            int altoGrafica = zona.Height - altoEtiqueta - altoTexto;

            // 1. El valor más alto: será la barra más alta
            double maximo = 0;
            foreach (double[] serie in series)
            {
                foreach (double v in serie)
                {
                    if (v > maximo)
                    {
                        maximo = v;
                    }
                }
            }
            if (maximo == 0)
            {
                maximo = 1;    // así no dividimos entre cero
            }

            // 2. Líneas horizontales de fondo
            for (int i = 0; i <= 4; i++)
            {
                Panel linea = new Panel();
                linea.BackColor = Linea;
                linea.Location = new Point(0, altoTexto + altoGrafica * i / 4);
                linea.Size = new Size(zona.Width, 1);
                zona.Controls.Add(linea);
            }

            // 3. Las barras de cada grupo
            int anchoGrupo = zona.Width / etiquetas.Length;
            int anchoBarra = (anchoGrupo - 16) / series.Count;
            if (anchoBarra > 90)
            {
                anchoBarra = 90;
            }
            for (int i = 0; i < etiquetas.Length; i++)
            {
                int anchoBarras = anchoBarra * series.Count;
                int x = i * anchoGrupo + (anchoGrupo - anchoBarras) / 2;

                for (int s = 0; s < series.Count; s++)
                {
                    int alto = (int)(series[s][i] / maximo * (altoGrafica - 4));
                    Panel barra = new Panel();
                    barra.BackColor = colores[s];
                    if (coloresPorBarra != null)
                    {
                        barra.BackColor = coloresPorBarra[i];
                    }
                    barra.Location = new Point(x + s * anchoBarra + 1, altoTexto + altoGrafica - alto);
                    barra.Size = new Size(anchoBarra - 2, alto);
                    zona.Controls.Add(barra);
                    barra.BringToFront();      // por encima de las líneas de fondo
                }

                // Texto encima (valor) y etiqueta debajo
                if (textosEncima != null)
                {
                    double valorMayor = 0;
                    foreach (double[] serie in series)
                    {
                        if (serie[i] > valorMayor)
                        {
                            valorMayor = serie[i];
                        }
                    }
                    int altoMayor = (int)(valorMayor / maximo * (altoGrafica - 4));
                    Label encima = new Label();
                    encima.Text = textosEncima[i];
                    encima.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
                    encima.ForeColor = Marron;
                    encima.TextAlign = ContentAlignment.BottomCenter;
                    encima.Location = new Point(i * anchoGrupo, altoTexto + altoGrafica - altoMayor - altoTexto);
                    encima.Size = new Size(anchoGrupo, altoTexto);
                    zona.Controls.Add(encima);
                }

                Label debajo = new Label();
                debajo.Text = etiquetas[i];
                debajo.Font = new Font("Segoe UI", 8.5F);
                debajo.ForeColor = Gris;
                debajo.TextAlign = ContentAlignment.MiddleCenter;
                debajo.Location = new Point(i * anchoGrupo, altoTexto + altoGrafica + 2);
                debajo.Size = new Size(anchoGrupo, altoEtiqueta - 2);
                zona.Controls.Add(debajo);
            }
        }
    }
}
