using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using TopohitEscritorio.Modelos;
using TopohitEscritorio.Servicios;

namespace TopohitEscritorio.Pantallas
{
    // Pantalla "Estadísticas" con dos pestañas:
    //  - General: aciertos/errores y reacción por dificultad, y resumen por dificultad
    //  - Seguimiento: niños a revisar (avisos), progreso del grupo y criterios
    // CAMBIO 24/11: solo el modo Infinito (antes había un desplegable con Historia y Contrarreloj).
    public partial class FormEstadisticas : Form
    {
        private FormPrincipal principal;
        private bool abrirSeguimiento;
        private bool cargando = true;      // para no recalcular mientras rellenamos los combos
        private Font fuenteAviso = new Font("Segoe UI", 9F, FontStyle.Bold);

        public FormEstadisticas(FormPrincipal principal, bool abrirSeguimiento)
        {
            InitializeComponent();
            this.principal = principal;
            this.abrirSeguimiento = abrirSeguimiento;
        }

        private void FormEstadisticas_Load(object sender, EventArgs e)
        {
            Ui.EstiloTabla(dgvResumen);
            Ui.EstiloTabla(dgvRevisar);

            cmbPeriodo.Items.Add("Período: últimos 7 días");
            cmbPeriodo.Items.Add("Período: últimos 30 días");
            cmbPeriodo.Items.Add("Período: todas las partidas");
            cmbPeriodo.SelectedIndex = 1;

            cargando = false;
            RellenarDificultades();
            CargarGeneral();
            CargarSeguimiento();
            MostrarCriterios();

            if (abrirSeguimiento)
            {
                MostrarPestana(btnTabSeguimiento);
            }
            else
            {
                MostrarPestana(btnTabGeneral);
            }
        }

        // ---------- Pestañas ----------
        private void MostrarPestana(Button pestana)
        {
            pnlGeneral.Visible = pestana == btnTabGeneral;
            pnlSeguimiento.Visible = pestana == btnTabSeguimiento;
            Ui.Pestana(btnTabGeneral, pestana == btnTabGeneral);
            Ui.Pestana(btnTabSeguimiento, pestana == btnTabSeguimiento);
            if (pestana == btnTabGeneral)
            {
                lblSubtitulo.Text = "Rendimiento: tiempo de reacción, aciertos y errores";
            }
            else
            {
                lblSubtitulo.Text = "Seguimiento de cada niño: avisos para las educadoras y progreso del grupo";
            }
        }

        private void btnTabGeneral_Click(object sender, EventArgs e)
        {
            MostrarPestana(btnTabGeneral);
        }

        private void btnTabSeguimiento_Click(object sender, EventArgs e)
        {
            MostrarPestana(btnTabSeguimiento);
        }

        // ================= PESTAÑA GENERAL =================

        // El combo de dificultad: todas o una concreta (los nombres vienen de la API)
        private void RellenarDificultades()
        {
            cargando = true;
            cmbDificultad.Items.Clear();
            cmbDificultad.Items.Add("Dificultad: todas");
            foreach (string nombre in Calculos.NombresDificultades())
            {
                cmbDificultad.Items.Add("Dificultad: " + nombre);
            }
            cmbDificultad.SelectedIndex = 0;
            cargando = false;
        }

        private void cmbDificultad_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cargando)
            {
                return;
            }
            CargarGeneral();
        }

        // Partidas de una dificultad ("" = todas).
        // Cuentan todas: también las de educadoras y admins que juegan.
        private List<Partida> PartidasFiltradas(string dificultad)
        {
            List<Partida> lista = new List<Partida>();
            foreach (Partida p in Datos.Partidas)
            {
                if (dificultad == "" || p.Nivel == dificultad)
                {
                    lista.Add(p);
                }
            }
            return lista;
        }

        private void CargarGeneral()
        {
            // Grupos de las gráficas: las tres dificultades o la elegida en el combo
            List<string> grupos = Calculos.NombresDificultades();
            if (cmbDificultad.SelectedIndex > 0)
            {
                string elegida = grupos[cmbDificultad.SelectedIndex - 1];
                grupos = new List<string>();
                grupos.Add(elegida);
            }
            lblGraf1Titulo.Text = "Aciertos y errores por dificultad";
            lblGraf2Titulo.Text = "Tiempo medio de reacción";

            // 1. Gráfica de aciertos y errores (media por partida)
            double[] aciertos = new double[grupos.Count];
            double[] errores = new double[grupos.Count];
            for (int i = 0; i < grupos.Count; i++)
            {
                List<Partida> partidas = PartidasFiltradas(grupos[i]);
                if (partidas.Count > 0)
                {
                    int sumaAciertos = 0;
                    foreach (Partida p in partidas)
                    {
                        sumaAciertos = sumaAciertos + p.Aciertos;
                    }
                    aciertos[i] = (double)sumaAciertos / partidas.Count;
                    errores[i] = Calculos.ErroresMedios(partidas);
                }
            }
            List<double[]> series = new List<double[]>();
            series.Add(aciertos);
            series.Add(errores);
            Ui.DibujarBarras(pnlBarras1, grupos.ToArray(), series, new Color[] { Ui.Naranja, Ui.Verde }, null);

            // 2. Gráfica de reacción por dificultad
            double[] reacciones = new double[grupos.Count];
            string[] textos = new string[grupos.Count];
            for (int i = 0; i < grupos.Count; i++)
            {
                int reaccion = Calculos.ReaccionMedia(PartidasFiltradas(grupos[i]));
                reacciones[i] = reaccion;
                if (reaccion > 0)
                {
                    textos[i] = reaccion + " ms";
                }
                else
                {
                    textos[i] = "sin datos";
                }
            }
            List<double[]> serie2 = new List<double[]>();
            serie2.Add(reacciones);
            Ui.DibujarBarras(pnlBarras2, grupos.ToArray(), serie2, new Color[] { Ui.VerdeClaro }, textos);

            // 3. Resumen por dificultad (siempre las tres y el total)
            List<FilaResumenDificultad> filas = new List<FilaResumenDificultad>();
            List<string> nombres = Calculos.NombresDificultades();
            nombres.Add("Total");
            foreach (string nombre in nombres)
            {
                List<Partida> partidas;
                if (nombre == "Total")
                {
                    partidas = PartidasFiltradas("");
                }
                else
                {
                    partidas = PartidasFiltradas(nombre);
                }
                FilaResumenDificultad f = new FilaResumenDificultad();
                f.Dificultad = nombre;
                f.Partidas = partidas.Count;
                f.Accuracy = "—";
                f.Errores = "—";
                f.Reaccion = "—";
                f.Abandonadas = "—";
                if (partidas.Count > 0)
                {
                    // "3 (12 %)": cuántas partidas dejó a medias y qué parte del total son
                    int abandonadas = Calculos.ContarAbandonadas(partidas);
                    f.Abandonadas = abandonadas + " (" + (abandonadas * 100 / partidas.Count) + " %)";
                    f.Accuracy = Calculos.PrecisionMedia(partidas) + " %";
                    f.Errores = Calculos.ErroresMedios(partidas).ToString("0.0");
                    int reaccion = Calculos.ReaccionMedia(partidas);
                    if (reaccion > 0)
                    {
                        f.Reaccion = reaccion + " ms";
                    }
                }
                filas.Add(f);
            }
            dgvResumen.DataSource = null;
            dgvResumen.DataSource = filas;
        }

        // ================= PESTAÑA SEGUIMIENTO =================

        private void cmbPeriodo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cargando)
            {
                return;
            }
            CargarSeguimiento();
        }

        private void CargarSeguimiento()
        {
            // 1. Niños a revisar
            int dias = 0;     // 0 = todas las partidas
            if (cmbPeriodo.SelectedIndex == 0)
            {
                dias = 7;
            }
            else if (cmbPeriodo.SelectedIndex == 1)
            {
                dias = 30;
            }

            List<FilaSeguimiento> filas = Calculos.NinosARevisar(dias);
            int totalNinos = Calculos.ContarJugadores();
            lblRevisarTitulo.Text = "Niños a revisar (" + filas.Count + " de " + totalNinos + ")";

            dgvRevisar.DataSource = null;
            dgvRevisar.DataSource = filas;

            // El avatar, la etiqueta naranja del aviso y los datos en rojo se ponen en dgvRevisar_CellFormatting

            // 2. CAMBIO 24/11: progreso del grupo en Infinito (siempre con todas las partidas):
            //    cuántos niños han jugado cada dificultad y la media de sus récords
            List<string> dificultades = Calculos.NombresDificultades();
            int[] ninos = new int[dificultades.Count];
            int[] recordMedio = new int[dificultades.Count];
            Calculos.ProgresoDificultades(ninos, recordMedio);

            double[] valores = new double[dificultades.Count];
            string[] textos = new string[dificultades.Count];
            Color[] colores = new Color[dificultades.Count];
            Color[] colorDe = { Ui.Verde, Ui.Naranja, Ui.Rojo };      // Fácil, Normal, Difícil
            for (int i = 0; i < dificultades.Count; i++)
            {
                valores[i] = ninos[i];
                textos[i] = "";
                if (ninos[i] > 0)
                {
                    textos[i] = ninos[i] + " niños\nrécord medio " + recordMedio[i];
                }
                colores[i] = colorDe[i % colorDe.Length];
            }
            List<double[]> serie = new List<double[]>();
            serie.Add(valores);
            Ui.DibujarBarras(pnlBarras3, dificultades.ToArray(), serie, new Color[] { Ui.Naranja }, textos, colores);
            lblProgresoSub.Text = "Niños que han jugado cada dificultad (de " + totalNinos + ") · récord medio";
        }

        // La tabla llama a este evento cada vez que va a pintar una celda.
        // Antes esto se hacía con un for después del DataSource, pero si la pestaña Seguimiento
        // estaba oculta, al enseñarla la tabla volvía a crear sus filas y se perdía (avatar en blanco).
        private void dgvRevisar_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            FilaSeguimiento f = (FilaSeguimiento)dgvRevisar.Rows[e.RowIndex].DataBoundItem;

            if (e.ColumnIndex == colRevAvatar.Index)
            {
                e.Value = Ui.Avatar(f.Jugador.AvatarId);     // CAMBIO 24/11: su avatar (antes, el de la temática)
            }
            else if (e.ColumnIndex == colRevAviso.Index)
            {
                e.CellStyle.BackColor = Ui.Naranja;
                e.CellStyle.SelectionBackColor = Ui.Naranja;
                e.CellStyle.ForeColor = Color.White;
                e.CellStyle.SelectionForeColor = Color.White;
                e.CellStyle.Font = fuenteAviso;
            }
            else if (e.ColumnIndex == colRevReaccion.Index)
            {
                PintarRojo(e.CellStyle, f.ReaccionEnRojo);
            }
            else if (e.ColumnIndex == colRevPrecision.Index)
            {
                PintarRojo(e.CellStyle, f.PrecisionEnRojo);
            }
            else if (e.ColumnIndex == colRevAbandonadas.Index)
            {
                PintarRojo(e.CellStyle, f.AbandonadasEnRojo);
            }
            else if (e.ColumnIndex == colRevDificultad.Index)
            {
                PintarRojo(e.CellStyle, f.DificultadEnRojo);
            }
        }

        private void PintarRojo(DataGridViewCellStyle estilo, bool enRojo)
        {
            if (enRojo)
            {
                estilo.ForeColor = Ui.Rojo;
                estilo.SelectionForeColor = Ui.Rojo;
            }
        }

        // ---------- Criterios de aviso ----------

        // Pone en las cajas los criterios que se están usando. Solo el admin puede cambiarlos.
        private void MostrarCriterios()
        {
            txtCriterio1.Text = Calculos.ReaccionLenta.ToString();
            txtCriterio2.Text = Calculos.PrecisionBaja.ToString();
            txtCriterio3.Text = Calculos.AbandonosAltos.ToString();
            txtCriterio5.Text = Calculos.DiasSinJugar.ToString();

            bool admin = Datos.EsAdmin();
            txtCriterio1.ReadOnly = !admin;
            txtCriterio2.ReadOnly = !admin;
            txtCriterio3.ReadOnly = !admin;
            txtCriterio5.ReadOnly = !admin;
            btnGuardarCriterios.Visible = admin;
            if (!admin)
            {
                lblCriteriosSub.Text = "Solo los puede cambiar un admin";
            }
        }

        // Lee un número de una caja con TryParse. Devuelve -1 si no es un número mayor que 0.
        private int LeerCriterio(TextBox caja)
        {
            int numero;
            if (int.TryParse(caja.Text.Trim(), out numero) && numero > 0)
            {
                return numero;
            }
            return -1;
        }

        private async void btnGuardarCriterios_Click(object sender, EventArgs e)
        {
            // 1. Comprobamos lo escrito
            CriteriosAviso c = new CriteriosAviso();
            c.ReaccionLenta = LeerCriterio(txtCriterio1);
            c.PrecisionBaja = LeerCriterio(txtCriterio2);
            c.AbandonosAltos = LeerCriterio(txtCriterio3);
            c.IntentosAtascado = Calculos.IntentosAtascado;     // CAMBIO 24/11: ya no se usa, se envía igual
            c.DiasSinJugar = LeerCriterio(txtCriterio5);
            if (c.ReaccionLenta == -1 || c.PrecisionBaja == -1 || c.AbandonosAltos == -1 || c.DiasSinJugar == -1)
            {
                MessageBox.Show("Todos los criterios tienen que ser números enteros mayores que 0.", "Datos incorrectos",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (c.PrecisionBaja > 100)
            {
                MessageBox.Show("La precisión es un porcentaje: tiene que estar entre 1 y 100.", "Datos incorrectos",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 2. Los guardamos en la API (en el modo demo, solo en memoria)
            if (!Datos.ModoDemo)
            {
                btnGuardarCriterios.Enabled = false;
                try
                {
                    bool guardado = await Api.GuardarCriterios(c);
                    if (!guardado)
                    {
                        MessageBox.Show(Api.UltimoError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("No se puede conectar con la API.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                finally
                {
                    btnGuardarCriterios.Enabled = true;
                }
            }

            // 3. Los usamos ya y recalculamos los niños a revisar
            Calculos.ReaccionLenta = c.ReaccionLenta;
            Calculos.PrecisionBaja = c.PrecisionBaja;
            Calculos.AbandonosAltos = c.AbandonosAltos;
            Calculos.DiasSinJugar = c.DiasSinJugar;
            CargarSeguimiento();
            MessageBox.Show("Criterios guardados.", "Guardado", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Botón "Ver" de un niño a revisar -> detalle del jugador
        private void dgvRevisar_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != colRevVer.Index)
            {
                return;
            }
            FilaSeguimiento f = (FilaSeguimiento)dgvRevisar.Rows[e.RowIndex].DataBoundItem;
            principal.MostrarDetalle(f.Jugador, "Seguimiento");
        }
    }
}
