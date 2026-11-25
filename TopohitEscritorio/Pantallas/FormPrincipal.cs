using System;
using System.Drawing;
using System.Windows.Forms;
using TopohitEscritorio.Modelos;
using TopohitEscritorio.Pantallas;

namespace TopohitEscritorio
{
    // Ventana principal: barra lateral a la izquierda y, a la derecha, la pantalla activa.
    // Cada pantalla es un formulario normal (FormJugadores, FormModos, FormEstadisticas,
    // FormMetricas, FormDetalleJugador) que se mete dentro de pnlContenido, como si fuera un control más.
    public partial class FormPrincipal : Form, IMessageFilter
    {
        // CAMBIO 25/11: medidas con las que están diseñadas las pantallas
        private const int ANCHO_BARRA = 240;
        private const int ANCHO_PANTALLA = 1200;
        private const int ALTO_PANTALLA = 900;

        // true si la ventana no cabe en la pantalla del ordenador y el contenido va con barra de desplazamiento
        private bool conDesplazamiento = false;

        public FormPrincipal()
        {
            InitializeComponent();
        }

        private void FormPrincipal_Load(object sender, EventArgs e)
        {
            lblUsuarioActual.Text = Datos.UsuarioActual.NombreUsuario;
            lblRolActual.Text = "Rol: " + Datos.UsuarioActual.Rol;
            AjustarAPantalla();
            MostrarJugadores();      // después del login se entra a Jugadores
        }

        // CAMBIO 25/11: en un portátil con escalado (por ejemplo 1920 × 1080 al 125 % = 1536 × 864)
        // la ventana de 1440 × 900 no cabe en altura. Si no cabe, la ventana se hace más baja
        // y la pantalla activa se mueve con una barra de desplazamiento (las pantallas no cambian).
        // Esta parte es de información externa que no sale en los vídeos
        // (Screen.FromControl(this).WorkingArea es el espacio libre del monitor sin la barra de tareas
        // y SystemInformation.VerticalScrollBarWidth el ancho de la barra de desplazamiento)
        private void AjustarAPantalla()
        {
            Rectangle libre = Screen.FromControl(this).WorkingArea;
            int bordesAncho = this.Width - this.ClientSize.Width;     // bordes de la ventana
            int bordesAlto = this.Height - this.ClientSize.Height;    // barra de título y bordes
            int anchoMaximo = libre.Width - bordesAncho;
            int altoMaximo = libre.Height - bordesAlto;

            if (altoMaximo >= ALTO_PANTALLA && anchoMaximo >= ANCHO_BARRA + ANCHO_PANTALLA)
            {
                return;     // cabe: se queda como está diseñada
            }

            conDesplazamiento = true;
            int alto = Math.Min(ALTO_PANTALLA, altoMaximo);
            int ancho = Math.Min(ANCHO_BARRA + ANCHO_PANTALLA + SystemInformation.VerticalScrollBarWidth, anchoMaximo);
            this.ClientSize = new Size(ancho, alto);

            pnlBarra.Height = alto;
            pnlUsuario.Top = alto - pnlUsuario.Height - 16;           // el bloque del usuario sigue abajo del todo
            pnlContenido.Size = new Size(ancho - ANCHO_BARRA, alto);
            pnlContenido.AutoScroll = true;
            Application.AddMessageFilter(this);       // para que la rueda del ratón mueva la pantalla
            this.FormClosed += FormPrincipal_FormClosed;

            // Centrada en horizontal y pegada arriba
            this.Location = new Point(libre.Left + (libre.Width - this.Width) / 2, libre.Top);
        }

        private void FormPrincipal_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.RemoveMessageFilter(this);
        }

        // CAMBIO 25/11: la rueda del ratón mueve la pantalla cuando va con barra de desplazamiento.
        // Esta parte es de información externa que no sale en los vídeos
        // (IMessageFilter.PreFilterMessage ve los mensajes de Windows antes que los controles;
        // 0x020A es WM_MOUSEWHEEL y los 16 bits altos de WParam son el giro: +120 o -120 por paso.
        // Si devuelve true, el mensaje ya no llega al control)
        public bool PreFilterMessage(ref Message m)
        {
            const int WM_MOUSEWHEEL = 0x020A;
            if (m.Msg != WM_MOUSEWHEEL || Form.ActiveForm != this)
            {
                return false;
            }
            Point raton = pnlContenido.PointToClient(Cursor.Position);
            if (!pnlContenido.ClientRectangle.Contains(raton))
            {
                return false;
            }

            // Si el ratón está sobre una tabla o un panel que tienen su propia barra, la rueda es para ellos
            Control debajo = pnlContenido;
            Control hijo = debajo.GetChildAtPoint(raton);
            while (hijo != null)
            {
                if (TieneBarraPropia(hijo))
                {
                    return false;
                }
                raton = hijo.PointToClient(Cursor.Position);
                debajo = hijo;
                hijo = debajo.GetChildAtPoint(raton);
            }

            int giro = (short)((long)m.WParam >> 16);
            int y = -pnlContenido.AutoScrollPosition.Y - giro;   // AutoScrollPosition se lee en negativo
            pnlContenido.AutoScrollPosition = new Point(0, y);
            return true;
        }

        private bool TieneBarraPropia(Control c)
        {
            DataGridView tabla = c as DataGridView;
            if (tabla != null)
            {
                return tabla.DisplayedRowCount(false) < tabla.RowCount;   // hay filas que no se ven
            }
            ScrollableControl panel = c as ScrollableControl;
            if (panel != null && panel != pnlContenido && panel.AutoScroll)
            {
                return panel.VerticalScroll.Visible;
            }
            return false;
        }

        // Quita la pantalla que había y pone la nueva
        private void MostrarPantalla(Form pantalla, Button botonActivo)
        {
            while (pnlContenido.Controls.Count > 0)
            {
                pnlContenido.Controls[0].Dispose();     // cierra la pantalla anterior y la quita del panel
            }

            // Un formulario normalmente es una ventana. Con estas 3 propiedades se mete dentro del panel:
            pantalla.TopLevel = false;                            // no es una ventana suelta (va ANTES del Add)
            pantalla.FormBorderStyle = FormBorderStyle.None;      // sin barra de título ni bordes
            if (conDesplazamiento)
            {
                // CAMBIO 25/11: con su tamaño de diseño, y el panel pone la barra de desplazamiento
                pnlContenido.AutoScrollPosition = new Point(0, 0);
                pantalla.Location = new Point(0, 0);
                pantalla.Size = new Size(ANCHO_PANTALLA, ALTO_PANTALLA);
            }
            else
            {
                pantalla.Dock = DockStyle.Fill;                   // ocupa todo el panel
            }
            pnlContenido.Controls.Add(pantalla);
            pantalla.Show();                                      // igual que un formulario no modal: lanza su Load
            MarcarBoton(botonActivo);
        }

        // El botón de la sección activa en naranja y su cuadradito en blanco
        private void MarcarBoton(Button activo)
        {
            Button[] botones = { btnJugadores, btnModos, btnEstadisticas, btnMetricas };
            Panel[] iconos = { pnlIconoJugadores, pnlIconoModos, pnlIconoEstadisticas, pnlIconoMetricas };
            for (int i = 0; i < botones.Length; i++)
            {
                if (botones[i] == activo)
                {
                    botones[i].BackColor = Ui.Naranja;
                    iconos[i].BackColor = Color.White;
                }
                else
                {
                    botones[i].BackColor = Ui.Marron;
                    iconos[i].BackColor = Ui.Naranja;
                }
            }
        }

        // public: las pantallas las llaman para moverse de una a otra
        public void MostrarJugadores()
        {
            MostrarPantalla(new FormJugadores(this), btnJugadores);
        }

        public void MostrarModos()
        {
            MostrarPantalla(new FormModos(), btnModos);
        }

        public void MostrarEstadisticas(bool abrirSeguimiento)
        {
            MostrarPantalla(new FormEstadisticas(this, abrirSeguimiento), btnEstadisticas);
        }

        // CAMBIO 24/11: métricas de todos los jugadores y modelo de abandono (Python)
        public void MostrarMetricas()
        {
            MostrarPantalla(new FormMetricas(), btnMetricas);
        }

        // vieneDe: "Jugadores" o "Seguimiento", para que el enlace "←" vuelva al sitio correcto
        public void MostrarDetalle(Usuario jugador, string vieneDe)
        {
            Button activo = btnJugadores;
            if (vieneDe == "Seguimiento")
            {
                activo = btnEstadisticas;
            }
            MostrarPantalla(new FormDetalleJugador(this, jugador, vieneDe), activo);
        }

        private void btnJugadores_Click(object sender, EventArgs e)
        {
            MostrarJugadores();
        }

        private void btnModos_Click(object sender, EventArgs e)
        {
            MostrarModos();
        }

        private void btnEstadisticas_Click(object sender, EventArgs e)
        {
            MostrarEstadisticas(false);
        }

        private void btnMetricas_Click(object sender, EventArgs e)
        {
            MostrarMetricas();
        }

        // Abre la ventana para cambiar la contraseña del usuario que ha entrado (modal)
        private void btnCambiarContrasena_Click(object sender, EventArgs e)
        {
            FormCambiarContrasena f = new FormCambiarContrasena();
            f.ShowDialog();
        }

        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            this.Close();   // al cerrarse, vuelves al login
        }
    }
}
