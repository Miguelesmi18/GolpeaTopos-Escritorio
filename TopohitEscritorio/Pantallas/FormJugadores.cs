using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using TopohitEscritorio.Modelos;

namespace TopohitEscritorio.Pantallas
{
    // Pantalla "Jugadores": lista de usuarios con buscador y dos pestañas.
    //  - admin: puede crear (+ Nuevo jugador), editar y ver
    //  - educador: solo "Ver" (reto "Protecció dels usuaris")
    public partial class FormJugadores : Form
    {
        private const string TEXTO_BUSCAR = "Buscar por usuario...";

        private FormPrincipal principal;     // para poder abrir el detalle del jugador
        private bool verEducadores = false;  // false = pestaña Jugadores, true = Educadores y admins
        private Font fuenteBaja = new Font("Segoe UI", 10F, FontStyle.Italic | FontStyle.Strikeout);

        public FormJugadores(FormPrincipal principal)
        {
            InitializeComponent();
            this.principal = principal;
        }

        private void FormJugadores_Load(object sender, EventArgs e)
        {
            Ui.EstiloTabla(dgvJugadores);
            txtBuscar.Text = TEXTO_BUSCAR;
            txtBuscar.ForeColor = Ui.Gris;

            // Lo que ve cada rol
            if (Datos.EsAdmin())
            {
                lblVista.Text = "Vista del rol admin";
            }
            else
            {
                lblVista.Text = "Vista del rol educador";
                btnNuevo.Visible = false;
                colEditar.Visible = false;
                colVer.HeaderText = "ACCIONES";
            }

            MarcarPestana();
            RefrescarTabla();
        }

        // Rellena la tabla según la pestaña y el buscador
        private void RefrescarTabla()
        {
            string buscado = txtBuscar.Text.Trim().ToLower();
            if (txtBuscar.Text == TEXTO_BUSCAR)
            {
                buscado = "";
            }

            int numJugadores = 0;
            int numEducadores = 0;
            List<Usuario> resultado = new List<Usuario>();
            foreach (Usuario u in Datos.Usuarios)
            {
                bool esJugador = u.Rol == "jugador";
                if (esJugador)
                {
                    numJugadores++;
                }
                else
                {
                    numEducadores++;
                }

                if (esJugador == !verEducadores && u.NombreUsuario.ToLower().Contains(buscado))
                {
                    resultado.Add(u);
                }
            }

            btnTabJugadores.Text = "Jugadores (" + numJugadores + ")";
            btnTabEducadores.Text = "Educadores y admins (" + numEducadores + ")";

            dgvJugadores.DataSource = null;
            dgvJugadores.DataSource = resultado;
            colRol.Visible = verEducadores;

            // El avatar y el gris de los dados de baja se ponen en dgvJugadores_CellFormatting

            if (verEducadores)
            {
                lblTotal.Text = resultado.Count + " educadores y admins";
            }
            else
            {
                lblTotal.Text = resultado.Count + " jugadores";
            }
        }

        // La tabla llama a este evento cada vez que va a pintar una celda.
        // Aquí ponemos el avatar (el que tiene guardado, CAMBIO 19/11) y los dados de baja en gris y tachados.
        // Antes se ponía con un for justo después del DataSource, pero la primera vez la tabla
        // aún no se ve: al mostrarse vuelve a crear sus filas y la imagen se perdía (salía en blanco).
        private void dgvJugadores_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            Usuario u = (Usuario)dgvJugadores.Rows[e.RowIndex].DataBoundItem;
            if (e.ColumnIndex == colAvatar.Index)
            {
                e.Value = Ui.Avatar(u.AvatarId);          // CAMBIO 19/11: su avatar, no el de la temática
            }
            if (!u.Activo)
            {
                // Solo cambia cómo se ve, no el texto del objeto Usuario
                e.CellStyle.ForeColor = Ui.Gris;
                e.CellStyle.Font = fuenteBaja;
            }
        }

        // La pestaña activa en negrita con la raya naranja debajo
        private void MarcarPestana()
        {
            Button activa = btnTabJugadores;
            Button inactiva = btnTabEducadores;
            if (verEducadores)
            {
                activa = btnTabEducadores;
                inactiva = btnTabJugadores;
            }
            activa.ForeColor = Ui.Marron;
            inactiva.ForeColor = Ui.Gris;
            pnlTabLinea.Left = activa.Left;
            pnlTabLinea.Width = activa.Width - 20;
        }

        private void btnTabJugadores_Click(object sender, EventArgs e)
        {
            verEducadores = false;
            MarcarPestana();
            RefrescarTabla();
        }

        private void btnTabEducadores_Click(object sender, EventArgs e)
        {
            verEducadores = true;
            MarcarPestana();
            RefrescarTabla();
        }

        // ---------- Buscador con texto de ayuda ----------
        private void txtBuscar_Enter(object sender, EventArgs e)
        {
            if (txtBuscar.Text == TEXTO_BUSCAR)
            {
                txtBuscar.Text = "";
                txtBuscar.ForeColor = Ui.Marron;
            }
        }

        private void txtBuscar_Leave(object sender, EventArgs e)
        {
            if (txtBuscar.Text == "")
            {
                txtBuscar.Text = TEXTO_BUSCAR;
                txtBuscar.ForeColor = Ui.Gris;
            }
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            RefrescarTabla();
        }

        // ---------- Botones ----------
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            FormEditarJugador f = new FormEditarJugador(null);   // null = nuevo
            if (f.ShowDialog() == DialogResult.OK)
            {
                RefrescarTabla();
            }
        }

        // Clic en "Editar" o "Ver" de una fila
        private void dgvJugadores_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;    // han pulsado en la cabecera
            }
            Usuario u = (Usuario)dgvJugadores.Rows[e.RowIndex].DataBoundItem;

            if (e.ColumnIndex == colEditar.Index)
            {
                FormEditarJugador f = new FormEditarJugador(u);
                if (f.ShowDialog() == DialogResult.OK)
                {
                    RefrescarTabla();
                }
            }
            else if (e.ColumnIndex == colVer.Index)
            {
                principal.MostrarDetalle(u, "Jugadores");
            }
        }
    }
}
