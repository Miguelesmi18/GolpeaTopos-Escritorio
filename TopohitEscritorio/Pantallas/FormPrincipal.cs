using System;
using System.Drawing;
using System.Windows.Forms;
using TopohitEscritorio.Modelos;
using TopohitEscritorio.Pantallas;

namespace TopohitEscritorio
{
    // Ventana principal: barra lateral a la izquierda y, a la derecha, la pantalla activa.
    // Cada pantalla es un formulario normal (FormJugadores, FormModos, FormEstadisticas,
    // FormDetalleJugador) que se mete dentro de pnlContenido, como si fuera un control más.
    public partial class FormPrincipal : Form
    {
        public FormPrincipal()
        {
            InitializeComponent();
        }

        private void FormPrincipal_Load(object sender, EventArgs e)
        {
            lblUsuarioActual.Text = Datos.UsuarioActual.NombreUsuario;
            lblRolActual.Text = "Rol: " + Datos.UsuarioActual.Rol;
            MostrarJugadores();      // después del login se entra a Jugadores
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
            pantalla.Dock = DockStyle.Fill;                       // ocupa todo el panel
            pnlContenido.Controls.Add(pantalla);
            pantalla.Show();                                      // igual que un formulario no modal: lanza su Load
            MarcarBoton(botonActivo);
        }

        // El botón de la sección activa en naranja y su cuadradito en blanco
        private void MarcarBoton(Button activo)
        {
            Button[] botones = { btnJugadores, btnModos, btnEstadisticas };
            Panel[] iconos = { pnlIconoJugadores, pnlIconoModos, pnlIconoEstadisticas };
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
