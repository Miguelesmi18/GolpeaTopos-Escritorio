using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using TopohitEscritorio.Modelos;
using TopohitEscritorio.Servicios;

namespace TopohitEscritorio.Pantallas
{
    // Detalle de un jugador: tarjetas resumen, gráfica de sus últimas partidas e historial.
    // Se abre con "Ver" desde Jugadores o desde Estadísticas > Seguimiento.
    public partial class FormDetalleJugador : Form
    {
        private FormPrincipal principal;
        private Usuario jugador;
        private string vieneDe;      // "Jugadores" o "Seguimiento"
        private Font fuenteEstado = new Font("Segoe UI", 8.5F, FontStyle.Bold);

        public FormDetalleJugador(FormPrincipal principal, Usuario jugador, string vieneDe)
        {
            InitializeComponent();
            this.principal = principal;
            this.jugador = jugador;
            this.vieneDe = vieneDe;
        }

        private void FormDetalleJugador_Load(object sender, EventArgs e)
        {
            Ui.EstiloTabla(dgvHistorial);
            lblVolver.Text = "← " + vieneDe;
            picAvatar.Image = Ui.Avatar(jugador.AvatarId);     // CAMBIO 19/11
            lblNombre.Text = jugador.NombreUsuario;
            lblRegistrado.Text = "Registrado el " + jugador.FechaRegistro.ToString("dd/MM/yyyy");
            if (!jugador.Activo)
            {
                lblRegistrado.Text = lblRegistrado.Text + "  ·  dado de baja";
            }
            lblSeQuedo.Text = TextoSeQuedo();

            cmbModo.Items.Add("Todos los modos");
            cmbModo.Items.Add("Historia");
            cmbModo.Items.Add("Contrarreloj");
            cmbModo.Items.Add("Infinito");
            cmbModo.SelectedIndex = 0;      // esto llama a cmbModo_SelectedIndexChanged -> CargarDatos()
        }

        // Línea "Se quedó en": el último juego que jugó y hasta dónde ha llegado en Historia.
        // Ejemplo: "Se quedó en: Historia · 4 (abandonada) · Hoy 10:31   ·   Historia: va por el nivel 4 · 6★"
        private string TextoSeQuedo()
        {
            List<Partida> todas = Calculos.PartidasDe(jugador.NombreUsuario, null);
            if (todas.Count == 0)
            {
                return "Todavía no ha jugado ninguna partida";
            }

            // 1. Su última partida: la de fecha más reciente
            Partida ultima = todas[0];
            foreach (Partida p in todas)
            {
                if (p.Fecha > ultima.Fecha)
                {
                    ultima = p;
                }
            }
            string texto = "Se quedó en: " + ultima.ModoNivelTexto;
            if (ultima.Abandonada)
            {
                texto = texto + " (abandonada)";
            }
            texto = texto + " · " + Calculos.TextoFecha(ultima.Fecha);

            // 2. Modo Historia: el primer nivel que todavía no ha superado (con 1★ o más)
            List<Partida> historia = Calculos.PartidasDe(jugador.NombreUsuario, "historia");
            if (historia.Count == 0)
            {
                return texto + "   ·   Historia: sin empezar";
            }
            // CAMBIO 18/11-B: misma regla que la API y la pestaña Seguimiento (se saltan los niveles desactivados)
            int estrellasTotal;
            int nivelActual = Calculos.NivelActualHistoria(historia, out estrellasTotal);
            if (nivelActual == 0)
            {
                return texto + "   ·   Historia completada · " + estrellasTotal + "★";
            }
            return texto + "   ·   Historia: va por el nivel " + nivelActual + " · " + estrellasTotal + "★";
        }

        private void cmbModo_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarDatos();
        }

        private void CargarDatos()
        {
            string modo = null;     // null = todos los modos
            if (cmbModo.SelectedIndex > 0)
            {
                modo = cmbModo.Text.ToLower();
            }
            List<Partida> partidas = Calculos.PartidasDe(jugador.NombreUsuario, modo);

            // 1. Tarjetas
            lblCardValor1.Text = partidas.Count.ToString();
            if (partidas.Count == 0)
            {
                lblCardValor2.Text = "—";
                lblCardValor3.Text = "—";
                lblCardValor4.Text = "—";
            }
            else
            {
                lblCardValor2.Text = Calculos.PrecisionMedia(partidas) + " %";
                lblCardValor3.Text = Calculos.ErroresMedios(partidas).ToString("0.0");
                int reaccion = Calculos.ReaccionMedia(partidas);
                if (reaccion > 0)
                {
                    lblCardValor4.Text = reaccion + " ms";
                }
                else
                {
                    lblCardValor4.Text = "—";
                }
            }

            // 2. Gráfica: las 12 últimas partidas (vienen ordenadas de la más antigua a la más reciente)
            int desde = partidas.Count - 12;
            if (desde < 0)
            {
                desde = 0;
            }
            int cuantas = partidas.Count - desde;
            string[] fechas = new string[cuantas];
            double[] aciertos = new double[cuantas];
            double[] errores = new double[cuantas];
            for (int i = 0; i < cuantas; i++)
            {
                Partida p = partidas[desde + i];
                fechas[i] = p.Fecha.ToString("dd/MM");
                aciertos[i] = p.Aciertos;
                errores[i] = p.Errores;
            }
            List<double[]> series = new List<double[]>();
            series.Add(aciertos);
            series.Add(errores);
            Ui.DibujarBarras(pnlBarrasPartidas, fechas, series, new Color[] { Ui.Naranja, Ui.Verde }, null);

            // 3. Historial: de la más reciente a la más antigua
            List<Partida> historial = new List<Partida>(partidas);
            historial.Reverse();
            dgvHistorial.DataSource = null;
            dgvHistorial.DataSource = historial;

            // La etiqueta del estado (naranja o beige) se pinta en dgvHistorial_CellFormatting
        }

        // La tabla llama a este evento cada vez que va a pintar una celda.
        // Etiqueta del estado: naranja si la abandonó, beige si la completó.
        // (Con un for después del DataSource, la primera vez se perdía al mostrarse la tabla.)
        private void dgvHistorial_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != colEstado.Index)
            {
                return;
            }
            Partida p = (Partida)dgvHistorial.Rows[e.RowIndex].DataBoundItem;
            e.CellStyle.Font = fuenteEstado;
            if (p.Abandonada)
            {
                e.CellStyle.BackColor = Ui.Naranja;
                e.CellStyle.ForeColor = Color.White;
            }
            else
            {
                e.CellStyle.BackColor = Ui.Beige;
                e.CellStyle.ForeColor = Ui.Marron;
            }
            e.CellStyle.SelectionBackColor = e.CellStyle.BackColor;
            e.CellStyle.SelectionForeColor = e.CellStyle.ForeColor;
        }

        private void lblVolver_Click(object sender, EventArgs e)
        {
            if (vieneDe == "Seguimiento")
            {
                principal.MostrarEstadisticas(true);
            }
            else
            {
                principal.MostrarJugadores();
            }
        }
    }
}
