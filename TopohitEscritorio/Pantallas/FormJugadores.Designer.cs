namespace TopohitEscritorio.Pantallas
{
    partial class FormJugadores
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador

        private void InitializeComponent()
        {
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblVista = new System.Windows.Forms.Label();
            this.lblSubtitulo = new System.Windows.Forms.Label();
            this.btnNuevo = new System.Windows.Forms.Button();
            this.btnTabJugadores = new System.Windows.Forms.Button();
            this.btnTabEducadores = new System.Windows.Forms.Button();
            this.pnlTabLinea = new System.Windows.Forms.Panel();
            this.pnlTabFondo = new System.Windows.Forms.Panel();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.dgvJugadores = new System.Windows.Forms.DataGridView();
            this.colAvatar = new System.Windows.Forms.DataGridViewImageColumn();
            this.colUsuario = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPartidas = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUltima = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEditar = new System.Windows.Forms.DataGridViewButtonColumn();
            this.colVer = new System.Windows.Forms.DataGridViewButtonColumn();
            this.pnlTabla = new System.Windows.Forms.Panel();
            this.pnlTablaBorde = new System.Windows.Forms.Panel();
            this.lblTotal = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvJugadores)).BeginInit();
            this.pnlTabLinea.SuspendLayout();
            this.pnlTabFondo.SuspendLayout();
            this.pnlTabla.SuspendLayout();
            this.pnlTablaBorde.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.BackColor = System.Drawing.Color.Transparent;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(140)))), ((int)(((byte)(40)))));
            this.lblTitulo.Location = new System.Drawing.Point(36, 18);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Text = "Jugadores";
            // 
            // lblVista
            // 
            this.lblVista.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(140)))), ((int)(((byte)(40)))));
            this.lblVista.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblVista.ForeColor = System.Drawing.Color.White;
            this.lblVista.Location = new System.Drawing.Point(210, 34);
            this.lblVista.Name = "lblVista";
            this.lblVista.Size = new System.Drawing.Size(160, 24);
            this.lblVista.Text = "Vista del rol admin";
            this.lblVista.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSubtitulo
            // 
            this.lblSubtitulo.AutoSize = true;
            this.lblSubtitulo.BackColor = System.Drawing.Color.Transparent;
            this.lblSubtitulo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSubtitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(118)))), ((int)(((byte)(92)))));
            this.lblSubtitulo.Location = new System.Drawing.Point(42, 70);
            this.lblSubtitulo.Name = "lblSubtitulo";
            this.lblSubtitulo.Text = "Alta, edición y baja de los usuarios de Topohit";
            // 
            // btnNuevo
            // 
            this.btnNuevo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNuevo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNuevo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(140)))), ((int)(((byte)(40)))));
            this.btnNuevo.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(35)))), ((int)(((byte)(20)))));
            this.btnNuevo.FlatAppearance.BorderSize = 2;
            this.btnNuevo.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.btnNuevo.ForeColor = System.Drawing.Color.White;
            this.btnNuevo.Location = new System.Drawing.Point(990, 28);
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(170, 46);
            this.btnNuevo.Text = "+ Nuevo jugador";
            this.btnNuevo.UseVisualStyleBackColor = false;
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // btnTabJugadores
            // 
            this.btnTabJugadores.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTabJugadores.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTabJugadores.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(244)))), ((int)(((byte)(220)))));
            this.btnTabJugadores.FlatAppearance.BorderSize = 0;
            this.btnTabJugadores.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnTabJugadores.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(35)))), ((int)(((byte)(20)))));
            this.btnTabJugadores.Location = new System.Drawing.Point(40, 106);
            this.btnTabJugadores.Name = "btnTabJugadores";
            this.btnTabJugadores.Size = new System.Drawing.Size(160, 34);
            this.btnTabJugadores.Text = "Jugadores (0)";
            this.btnTabJugadores.UseVisualStyleBackColor = false;
            this.btnTabJugadores.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTabJugadores.Click += new System.EventHandler(this.btnTabJugadores_Click);
            // 
            // btnTabEducadores
            // 
            this.btnTabEducadores.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTabEducadores.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTabEducadores.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(244)))), ((int)(((byte)(220)))));
            this.btnTabEducadores.FlatAppearance.BorderSize = 0;
            this.btnTabEducadores.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnTabEducadores.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(35)))), ((int)(((byte)(20)))));
            this.btnTabEducadores.Location = new System.Drawing.Point(210, 106);
            this.btnTabEducadores.Name = "btnTabEducadores";
            this.btnTabEducadores.Size = new System.Drawing.Size(240, 34);
            this.btnTabEducadores.Text = "Educadores y admins (0)";
            this.btnTabEducadores.UseVisualStyleBackColor = false;
            this.btnTabEducadores.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTabEducadores.Click += new System.EventHandler(this.btnTabEducadores_Click);
            // 
            // pnlTabLinea
            // 
            this.pnlTabLinea.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(140)))), ((int)(((byte)(40)))));
            this.pnlTabLinea.Location = new System.Drawing.Point(40, 141);
            this.pnlTabLinea.Name = "pnlTabLinea";
            this.pnlTabLinea.Size = new System.Drawing.Size(130, 3);
            // 
            // pnlTabFondo
            // 
            this.pnlTabFondo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(228)))), ((int)(((byte)(200)))));
            this.pnlTabFondo.Location = new System.Drawing.Point(40, 144);
            this.pnlTabFondo.Name = "pnlTabFondo";
            this.pnlTabFondo.Size = new System.Drawing.Size(1120, 1);
            // 
            // txtBuscar
            // 
            this.txtBuscar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBuscar.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtBuscar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(35)))), ((int)(((byte)(20)))));
            this.txtBuscar.Location = new System.Drawing.Point(40, 162);
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(340, 27);
            this.txtBuscar.TextChanged += new System.EventHandler(this.txtBuscar_TextChanged);
            this.txtBuscar.Enter += new System.EventHandler(this.txtBuscar_Enter);
            this.txtBuscar.Leave += new System.EventHandler(this.txtBuscar_Leave);
            // 
            // dgvJugadores
            // 
            this.dgvJugadores.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colAvatar,
            this.colUsuario,
            this.colRol,
            this.colPartidas,
            this.colUltima,
            this.colEditar,
            this.colVer});
            this.dgvJugadores.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvJugadores.Name = "dgvJugadores";
            this.dgvJugadores.ReadOnly = true;
            this.dgvJugadores.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvJugadores_CellContentClick);
            this.dgvJugadores.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvJugadores_CellFormatting);
            // 
            // colAvatar
            // 
            this.colAvatar.HeaderText = "";
            this.colAvatar.Name = "colAvatar";
            this.colAvatar.ReadOnly = true;
            this.colAvatar.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colAvatar.Width = 64;
            // 
            // colUsuario
            // 
            this.colUsuario.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colUsuario.DataPropertyName = "NombreUsuario";
            this.colUsuario.HeaderText = "USUARIO";
            this.colUsuario.Name = "colUsuario";
            this.colUsuario.ReadOnly = true;
            this.colUsuario.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colRol
            // 
            this.colRol.DataPropertyName = "Rol";
            this.colRol.HeaderText = "ROL";
            this.colRol.Name = "colRol";
            this.colRol.ReadOnly = true;
            this.colRol.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colRol.Width = 130;
            // 
            // colPartidas
            // 
            this.colPartidas.DataPropertyName = "NumPartidas";
            this.colPartidas.HeaderText = "PARTIDAS";
            this.colPartidas.Name = "colPartidas";
            this.colPartidas.ReadOnly = true;
            this.colPartidas.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colPartidas.Width = 130;
            // 
            // colUltima
            // 
            this.colUltima.DataPropertyName = "UltimaPartidaTexto";
            this.colUltima.HeaderText = "ÚLTIMA PARTIDA";
            this.colUltima.Name = "colUltima";
            this.colUltima.ReadOnly = true;
            this.colUltima.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colUltima.Width = 200;
            // 
            // colEditar
            // 
            this.colEditar.HeaderText = "ACCIONES";
            this.colEditar.Name = "colEditar";
            this.colEditar.ReadOnly = true;
            this.colEditar.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colEditar.Text = "Editar";
            this.colEditar.UseColumnTextForButtonValue = true;
            this.colEditar.Width = 100;
            // 
            // colVer
            // 
            this.colVer.HeaderText = "";
            this.colVer.Name = "colVer";
            this.colVer.ReadOnly = true;
            this.colVer.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colVer.Text = "Ver";
            this.colVer.UseColumnTextForButtonValue = true;
            this.colVer.Width = 90;
            // 
            // pnlTabla
            // 
            this.pnlTabla.Controls.Add(this.dgvJugadores);
            this.pnlTabla.BackColor = System.Drawing.Color.White;
            this.pnlTabla.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTabla.Name = "pnlTabla";
            this.pnlTabla.Padding = new System.Windows.Forms.Padding(14);
            // 
            // pnlTablaBorde
            // 
            this.pnlTablaBorde.Controls.Add(this.pnlTabla);
            this.pnlTablaBorde.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(35)))), ((int)(((byte)(20)))));
            this.pnlTablaBorde.Location = new System.Drawing.Point(40, 210);
            this.pnlTablaBorde.Name = "pnlTablaBorde";
            this.pnlTablaBorde.Padding = new System.Windows.Forms.Padding(2, 2, 2, 6);
            this.pnlTablaBorde.Size = new System.Drawing.Size(1120, 610);
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.BackColor = System.Drawing.Color.Transparent;
            this.lblTotal.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblTotal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(118)))), ((int)(((byte)(92)))));
            this.lblTotal.Location = new System.Drawing.Point(56, 826);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Text = "0 jugadores";
            // 
            // FormJugadores
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(244)))), ((int)(((byte)(220)))));
            this.Name = "FormJugadores";
            this.ClientSize = new System.Drawing.Size(1200, 900);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Load += new System.EventHandler(this.FormJugadores_Load);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.lblVista);
            this.Controls.Add(this.lblSubtitulo);
            this.Controls.Add(this.btnNuevo);
            this.Controls.Add(this.btnTabJugadores);
            this.Controls.Add(this.btnTabEducadores);
            this.Controls.Add(this.pnlTabLinea);
            this.Controls.Add(this.pnlTabFondo);
            this.Controls.Add(this.txtBuscar);
            this.Controls.Add(this.pnlTablaBorde);
            this.Controls.Add(this.lblTotal);
            ((System.ComponentModel.ISupportInitialize)(this.dgvJugadores)).EndInit();
            this.pnlTabLinea.ResumeLayout(false);
            this.pnlTabLinea.PerformLayout();
            this.pnlTabFondo.ResumeLayout(false);
            this.pnlTabFondo.PerformLayout();
            this.pnlTabla.ResumeLayout(false);
            this.pnlTabla.PerformLayout();
            this.pnlTablaBorde.ResumeLayout(false);
            this.pnlTablaBorde.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblVista;
        private System.Windows.Forms.Label lblSubtitulo;
        private System.Windows.Forms.Button btnNuevo;
        private System.Windows.Forms.Button btnTabJugadores;
        private System.Windows.Forms.Button btnTabEducadores;
        private System.Windows.Forms.Panel pnlTabLinea;
        private System.Windows.Forms.Panel pnlTabFondo;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.DataGridView dgvJugadores;
        private System.Windows.Forms.DataGridViewImageColumn colAvatar;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUsuario;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRol;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPartidas;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUltima;
        private System.Windows.Forms.DataGridViewButtonColumn colEditar;
        private System.Windows.Forms.DataGridViewButtonColumn colVer;
        private System.Windows.Forms.Panel pnlTabla;
        private System.Windows.Forms.Panel pnlTablaBorde;
        private System.Windows.Forms.Label lblTotal;
    }
}
