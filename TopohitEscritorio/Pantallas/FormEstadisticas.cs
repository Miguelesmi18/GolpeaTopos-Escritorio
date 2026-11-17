using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using TopohitEscritorio.Modelos;
using TopohitEscritorio.Servicios;

namespace TopohitEscritorio.Pantallas
{
    // Pantalla "Estadísticas" con dos pestañas:
    //  - General: aciertos/errores y reacción por nivel o dificultad, y resumen por modo
    //  - Seguimiento: niños a revisar (avisos), progreso del grupo en Historia y criterios
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

            cmbModo.Items.Add("Modo: Historia");
            cmbModo.Items.Add("Modo: Contrarreloj");
            cmbModo.Items.Add("Modo: Infinito");
            cmbModo.SelectedIndex = 0;

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

        // "historia", "contrarreloj" o "infinito" según el combo
        private string ModoElegido()
        {
            if (cmbModo.SelectedIndex == 1)
            {
                return "contrarreloj";
            }
            if (cmbModo.SelectedIndex == 2)
            {
                return "infinito";
            }
            return "historia";
        }

        // El combo de dificultad depende del modo (Historia no tiene dificultades)
        private void RellenarDificultades()
        {
            cargando = true;
            cmbDificultad.Items.Clear();
            string modo = ModoElegido();
            if (modo == "historia")
            {
                cmbDificultad.Items.Add("Dificultad: —");
                cmbDificultad.Enabled = false;
            }
            else
            {
                cmbDificultad.Items.Add("Dificultad: todas");
                foreach (string nombre in NombresDificultades(modo))
                {
                    cmbDificultad.Items.Add("Dificultad: " + nombre);
                }
                cmbDificultad.Enabled = true;
            }
            cmbDificultad.SelectedIndex = 0;
            cargando = false;
        }

        private List<string> NombresDificultades(string modo)
        {
            List<string> nombres = new List<string>();
            if (modo == "contrarreloj")
            {
                foreach (DificultadContrarreloj d in Datos.Contrarreloj)
                {
                    nombres.Add(d.Nombre);
                }
            }
            else
            {
                foreach (DificultadInfinito d in Datos.Infinito)
                {
                    nombres.Add(d.Nombre);
                }
            }
            return nombres;
        }

        private void cmbModo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cargando)
            {
                return;
            }
            RellenarDificultades();
            CargarGeneral();
        }

        private void cmbDificultad_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cargando)
            {
                return;
            }
            CargarGeneral();
        }

        // Partidas de un modo y, si la hay, de una dificultad concreta ("" = todas).
        // Cuentan todas: también las de educadoras y admins que juegan.
        private List<Partida> PartidasFiltradas(string modo, string dificultad)
        {
            List<Partida> lista = new List<Partida>();
            foreach (Partida p in Datos.Partidas)
            {
                if (p.Modo == modo && (dificultad == "" || p.Nivel.ToLower() == dificultad.ToLower()))
                {
                    lista.Add(p);
                }
            }
            return lista;
        }

        private void CargarGeneral()
        {
            string modo = ModoElegido();

            // Grupos de la gráfica: niveles 1-10 en Historia, dificultades en los otros modos
            List<string> grupos = new List<string>();
            if (modo == "historia")
            {
                for (int n = 1; n <= 10; n++)
                {
                    grupos.Add(n.ToString());
                }
                lblGraf1Titulo.Text = "Aciertos y errores por nivel (Historia)";
                lblGraf2Titulo.Text = "Tiempo medio de reacción por zona";
            }
            else
            {
                if (cmbDificultad.SelectedIndex > 0)
                {
                    grupos.Add(NombresDificultades(modo)[cmbDificultad.SelectedIndex - 1]);
                }
                else
                {
                    grupos = NombresDificultades(modo);
                }
                string nombreModo = cmbModo.Text.Replace("Modo: ", "");
                lblGraf1Titulo.Text = "Aciertos y errores por dificultad (" + nombreModo + ")";
                lblGraf2Titulo.Text = "Tiempo medio de reacción";
            }

            // 1. Gráfica de aciertos y errores (media por partida)
            double[] aciertos = new double[grupos.Count];
            double[] errores = new double[grupos.Count];
            for (int i = 0; i < grupos.Count; i++)
            {
                List<Partida> partidas;
                if (modo == "historia")
                {
                    partidas = new List<Partida>();
                    foreach (Partida p in PartidasFiltradas("historia", ""))
                    {
                        if (p.NivelNumero == i + 1)
                        {
                            partidas.Add(p);
                        }
                    }
                }
                else
                {
                    partidas = PartidasFiltradas(modo, grupos[i]);
                }
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

            // 2. Gráfica de reacción: por zona en Historia, por dificultad en los otros
            List<string> grupos2 = new List<string>();
            List<double> reacciones = new List<double>();
            if (modo == "historia")
            {
                string[] zonas = { "Campo", "Huerto", "Mina" };
                foreach (string zona in zonas)
                {
                    List<Partida> deZona = new List<Partida>();
                    foreach (Partida p in PartidasFiltradas("historia", ""))
                    {
                        NivelHistoria nivel = Calculos.BuscarNivel(p.NivelNumero);
                        if (nivel != null && nivel.Zona == zona)
                        {
                            deZona.Add(p);
                        }
                    }
                    grupos2.Add(zona);
                    reacciones.Add(Calculos.ReaccionMedia(deZona));
                }
            }
            else
            {
                foreach (string dificultad in grupos)
                {
                    grupos2.Add(dificultad);
                    reacciones.Add(Calculos.ReaccionMedia(PartidasFiltradas(modo, dificultad)));
                }
            }
            string[] textos = new string[reacciones.Count];
            for (int i = 0; i < reacciones.Count; i++)
            {
                if (reacciones[i] > 0)
                {
                    textos[i] = reacciones[i] + " ms";
                }
                else
                {
                    textos[i] = "sin datos";
                }
            }
            List<double[]> serie2 = new List<double[]>();
            serie2.Add(reacciones.ToArray());
            Ui.DibujarBarras(pnlBarras2, grupos2.ToArray(), serie2, new Color[] { Ui.VerdeClaro }, textos);

            // 3. Resumen por modo (siempre los tres modos)
            List<FilaResumenModo> filas = new List<FilaResumenModo>();
            string[] modos = { "historia", "contrarreloj", "infinito" };
            string[] nombres = { "Historia", "Contrarreloj", "Infinito" };
            for (int i = 0; i < modos.Length; i++)
            {
                List<Partida> partidas = PartidasFiltradas(modos[i], "");
                FilaResumenModo f = new FilaResumenModo();
                f.Modo = nombres[i];
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

            // 2. Progreso del grupo en Historia (siempre con todas las partidas)
            int[] superados = new int[10];
            double[] estrellas = new double[10];
            Calculos.ProgresoHistoria(superados, estrellas);

            string[] niveles = new string[10];
            string[] textos = new string[10];
            double[] valores = new double[10];
            Color[] colores = new Color[10];
            for (int i = 0; i < 10; i++)
            {
                niveles[i] = (i + 1).ToString();
                valores[i] = superados[i];
                textos[i] = "";
                if (superados[i] > 0)
                {
                    textos[i] = superados[i] + "\n" + estrellas[i].ToString("0.0") + "★";
                }
                NivelHistoria nivel = Calculos.BuscarNivel(i + 1);
                colores[i] = Ui.Naranja;
                if (nivel != null && nivel.Zona == "Huerto")
                {
                    colores[i] = Ui.Verde;
                }
                else if (nivel != null && nivel.Zona == "Mina")
                {
                    colores[i] = Ui.Morado;
                }
            }
            List<double[]> serie = new List<double[]>();
            serie.Add(valores);
            Ui.DibujarBarras(pnlBarras3, niveles, serie, new Color[] { Ui.Naranja }, textos, colores);
            lblProgresoSub.Text = "Niños que han superado cada nivel (de " + totalNinos + ") · ★ media obtenida";
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
                e.Value = Ui.Avatar(f.Jugador.Tematica);
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
            else if (e.ColumnIndex == colRevHistoria.Index)
            {
                PintarRojo(e.CellStyle, f.HistoriaEnRojo);
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
            txtCriterio4.Text = Calculos.IntentosAtascado.ToString();
            txtCriterio5.Text = Calculos.DiasSinJugar.ToString();

            bool admin = Datos.EsAdmin();
            txtCriterio1.ReadOnly = !admin;
            txtCriterio2.ReadOnly = !admin;
            txtCriterio3.ReadOnly = !admin;
            txtCriterio4.ReadOnly = !admin;
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
            c.IntentosAtascado = LeerCriterio(txtCriterio4);
            c.DiasSinJugar = LeerCriterio(txtCriterio5);
            if (c.ReaccionLenta == -1 || c.PrecisionBaja == -1 || c.AbandonosAltos == -1 ||
                c.IntentosAtascado == -1 || c.DiasSinJugar == -1)
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
            Calculos.IntentosAtascado = c.IntentosAtascado;
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
