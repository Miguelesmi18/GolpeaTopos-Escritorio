namespace TopohitEscritorio.Pantallas
{
    partial class FormDetalleJugador
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
            this.lblVolver = new System.Windows.Forms.Label();
            this.picAvatar = new System.Windows.Forms.PictureBox();
            this.lblNombre = new System.Windows.Forms.Label();
            this.lblRegistrado = new System.Windows.Forms.Label();
            this.lblSeQuedo = new System.Windows.Forms.Label();
            this.cmbDificultad = new System.Windows.Forms.ComboBox();
            this.btnBloqueos = new System.Windows.Forms.Button();
            this.lblCardTitulo1 = new System.Windows.Forms.Label();
            this.lblCardValor1 = new System.Windows.Forms.Label();
            this.pnlCard1 = new System.Windows.Forms.Panel();
            this.pnlCard1Borde = new System.Windows.Forms.Panel();
            this.lblCardTitulo2 = new System.Windows.Forms.Label();
            this.lblCardValor2 = new System.Windows.Forms.Label();
            this.pnlCard2 = new System.Windows.Forms.Panel();
            this.pnlCard2Borde = new System.Windows.Forms.Panel();
            this.lblCardTitulo3 = new System.Windows.Forms.Label();
            this.lblCardValor3 = new System.Windows.Forms.Label();
            this.pnlCard3 = new System.Windows.Forms.Panel();
            this.pnlCard3Borde = new System.Windows.Forms.Panel();
            this.lblCardTitulo4 = new System.Windows.Forms.Label();
            this.lblCardValor4 = new System.Windows.Forms.Label();
            this.pnlCard4 = new System.Windows.Forms.Panel();
            this.pnlCard4Borde = new System.Windows.Forms.Panel();
            this.lblGraficaTitulo = new System.Windows.Forms.Label();
            this.lblGraficaSub = new System.Windows.Forms.Label();
            this.lblLeyHits = new System.Windows.Forms.Label();
            this.lblLeyErrores = new System.Windows.Forms.Label();
            this.pnlBarrasPartidas = new System.Windows.Forms.Panel();
            this.pnlGrafica = new System.Windows.Forms.Panel();
            this.pnlGraficaBorde = new System.Windows.Forms.Panel();
            this.lblHistorialTitulo = new System.Windows.Forms.Label();
            this.dgvHistorial = new System.Windows.Forms.DataGridView();
            this.colFecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDificultad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPuntos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colHits = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colErrores = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEscapados = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colReaccion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDuracion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEstado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlHistorial = new System.Windows.Forms.Panel();
            this.pnlHistorialBorde = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistorial)).BeginInit();
            this.pnlCard1.SuspendLayout();
            this.pnlCard1Borde.SuspendLayout();
            this.pnlCard2.SuspendLayout();
            this.pnlCard2Borde.SuspendLayout();
            this.pnlCard3.SuspendLayout();
            this.pnlCard3Borde.SuspendLayout();
            this.pnlCard4.SuspendLayout();
            this.pnlCard4Borde.SuspendLayout();
            this.pnlBarrasPartidas.SuspendLayout();
            this.pnlGrafica.SuspendLayout();
            this.pnlGraficaBorde.SuspendLayout();
            this.pnlHistorial.SuspendLayout();
            this.pnlHistorialBorde.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnBloqueos
            // 
            this.btnBloqueos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBloqueos.Location = new System.Drawing.Point(740, 54);
            this.btnBloqueos.Name = "btnBloqueos";
            this.btnBloqueos.Size = new System.Drawing.Size(200, 34);
            this.btnBloqueos.Text = "Bloquear dificultades";
            this.btnBloqueos.Click += new System.EventHandler(this.btnBloqueos_Click);
            // 
            // lblVolver
            // 
            this.lblVolver.AutoSize = true;
            this.lblVolver.BackColor = System.Drawing.Color.Transparent;
            this.lblVolver.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblVolver.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(118)))), ((int)(((byte)(92)))));
            this.lblVolver.Location = new System.Drawing.Point(40, 18);
            this.lblVolver.Name = "lblVolver";
            this.lblVolver.Text = "← Jugadores";
            this.lblVolver.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblVolver.Click += new System.EventHandler(this.lblVolver_Click);
            // 
            // picAvatar
            // 
            this.picAvatar.Location = new System.Drawing.Point(40, 44);
            this.picAvatar.Name = "picAvatar";
            this.picAvatar.Size = new System.Drawing.Size(62, 62);
            this.picAvatar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            // 
            // lblNombre
            // 
            this.lblNombre.AutoSize = true;
            this.lblNombre.BackColor = System.Drawing.Color.Transparent;
            this.lblNombre.Font = new System.Drawing.Font("Segoe UI", 22F, System.Drawing.FontStyle.Bold);
            this.lblNombre.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(35)))), ((int)(((byte)(20)))));
            this.lblNombre.Location = new System.Drawing.Point(112, 40);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Text = "jugador";
            // 
            // lblRegistrado
            // 
            this.lblRegistrado.AutoSize = true;
            this.lblRegistrado.BackColor = System.Drawing.Color.Transparent;
            this.lblRegistrado.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblRegistrado.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(118)))), ((int)(((byte)(92)))));
            this.lblRegistrado.Location = new System.Drawing.Point(118, 84);
            this.lblRegistrado.Name = "lblRegistrado";
            this.lblRegistrado.Text = "Registrado el";
            // 
            // lblSeQuedo: su última partida y hasta qué dificultad ha llegado (CAMBIO 24/11)
            // 
            this.lblSeQuedo.AutoSize = true;
            this.lblSeQuedo.BackColor = System.Drawing.Color.Transparent;
            this.lblSeQuedo.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblSeQuedo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(35)))), ((int)(((byte)(20)))));
            this.lblSeQuedo.Location = new System.Drawing.Point(118, 104);
            this.lblSeQuedo.Name = "lblSeQuedo";
            this.lblSeQuedo.Text = "Se quedó en:";
            // 
            // cmbDificultad
            // 
            this.cmbDificultad.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDificultad.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbDificultad.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.cmbDificultad.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(35)))), ((int)(((byte)(20)))));
            this.cmbDificultad.Location = new System.Drawing.Point(960, 58);
            this.cmbDificultad.Name = "cmbDificultad";
            this.cmbDificultad.Size = new System.Drawing.Size(200, 27);
            this.cmbDificultad.SelectedIndexChanged += new System.EventHandler(this.cmbDificultad_SelectedIndexChanged);
            // 
            // lblCardTitulo1
            // 
            this.lblCardTitulo1.AutoSize = true;
            this.lblCardTitulo1.BackColor = System.Drawing.Color.Transparent;
            this.lblCardTitulo1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblCardTitulo1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(118)))), ((int)(((byte)(92)))));
            this.lblCardTitulo1.Location = new System.Drawing.Point(16, 12);
            this.lblCardTitulo1.Name = "lblCardTitulo1";
            this.lblCardTitulo1.Text = "Partidas";
            // 
            // lblCardValor1
            // 
            this.lblCardValor1.AutoSize = true;
            this.lblCardValor1.BackColor = System.Drawing.Color.Transparent;
            this.lblCardValor1.Font = new System.Drawing.Font("Segoe UI", 22F, System.Drawing.FontStyle.Bold);
            this.lblCardValor1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(35)))), ((int)(((byte)(20)))));
            this.lblCardValor1.Location = new System.Drawing.Point(12, 32);
            this.lblCardValor1.Name = "lblCardValor1";
            this.lblCardValor1.Text = "0";
            // 
            // pnlCard1
            // 
            this.pnlCard1.Controls.Add(this.lblCardTitulo1);
            this.pnlCard1.Controls.Add(this.lblCardValor1);
            this.pnlCard1.BackColor = System.Drawing.Color.White;
            this.pnlCard1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCard1.Name = "pnlCard1";
            // 
            // pnlCard1Borde
            // 
            this.pnlCard1Borde.Controls.Add(this.pnlCard1);
            this.pnlCard1Borde.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(35)))), ((int)(((byte)(20)))));
            this.pnlCard1Borde.Location = new System.Drawing.Point(40, 128);
            this.pnlCard1Borde.Name = "pnlCard1Borde";
            this.pnlCard1Borde.Padding = new System.Windows.Forms.Padding(2, 2, 2, 6);
            this.pnlCard1Borde.Size = new System.Drawing.Size(268, 100);
            // 
            // lblCardTitulo2
            // 
            this.lblCardTitulo2.AutoSize = true;
            this.lblCardTitulo2.BackColor = System.Drawing.Color.Transparent;
            this.lblCardTitulo2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblCardTitulo2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(118)))), ((int)(((byte)(92)))));
            this.lblCardTitulo2.Location = new System.Drawing.Point(16, 12);
            this.lblCardTitulo2.Name = "lblCardTitulo2";
            this.lblCardTitulo2.Text = "Accuracy";
            // 
            // lblCardValor2
            // 
            this.lblCardValor2.AutoSize = true;
            this.lblCardValor2.BackColor = System.Drawing.Color.Transparent;
            this.lblCardValor2.Font = new System.Drawing.Font("Segoe UI", 22F, System.Drawing.FontStyle.Bold);
            this.lblCardValor2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(35)))), ((int)(((byte)(20)))));
            this.lblCardValor2.Location = new System.Drawing.Point(12, 32);
            this.lblCardValor2.Name = "lblCardValor2";
            this.lblCardValor2.Text = "0";
            // 
            // pnlCard2
            // 
            this.pnlCard2.Controls.Add(this.lblCardTitulo2);
            this.pnlCard2.Controls.Add(this.lblCardValor2);
            this.pnlCard2.BackColor = System.Drawing.Color.White;
            this.pnlCard2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCard2.Name = "pnlCard2";
            // 
            // pnlCard2Borde
            // 
            this.pnlCard2Borde.Controls.Add(this.pnlCard2);
            this.pnlCard2Borde.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(35)))), ((int)(((byte)(20)))));
            this.pnlCard2Borde.Location = new System.Drawing.Point(324, 128);
            this.pnlCard2Borde.Name = "pnlCard2Borde";
            this.pnlCard2Borde.Padding = new System.Windows.Forms.Padding(2, 2, 2, 6);
            this.pnlCard2Borde.Size = new System.Drawing.Size(268, 100);
            // 
            // lblCardTitulo3
            // 
            this.lblCardTitulo3.AutoSize = true;
            this.lblCardTitulo3.BackColor = System.Drawing.Color.Transparent;
            this.lblCardTitulo3.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblCardTitulo3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(118)))), ((int)(((byte)(92)))));
            this.lblCardTitulo3.Location = new System.Drawing.Point(16, 12);
            this.lblCardTitulo3.Name = "lblCardTitulo3";
            this.lblCardTitulo3.Text = "Errores / partida";
            // 
            // lblCardValor3
            // 
            this.lblCardValor3.AutoSize = true;
            this.lblCardValor3.BackColor = System.Drawing.Color.Transparent;
            this.lblCardValor3.Font = new System.Drawing.Font("Segoe UI", 22F, System.Drawing.FontStyle.Bold);
            this.lblCardValor3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(35)))), ((int)(((byte)(20)))));
            this.lblCardValor3.Location = new System.Drawing.Point(12, 32);
            this.lblCardValor3.Name = "lblCardValor3";
            this.lblCardValor3.Text = "0";
            // 
            // pnlCard3
            // 
            this.pnlCard3.Controls.Add(this.lblCardTitulo3);
            this.pnlCard3.Controls.Add(this.lblCardValor3);
            this.pnlCard3.BackColor = System.Drawing.Color.White;
            this.pnlCard3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCard3.Name = "pnlCard3";
            // 
            // pnlCard3Borde
            // 
            this.pnlCard3Borde.Controls.Add(this.pnlCard3);
            this.pnlCard3Borde.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(35)))), ((int)(((byte)(20)))));
            this.pnlCard3Borde.Location = new System.Drawing.Point(608, 128);
            this.pnlCard3Borde.Name = "pnlCard3Borde";
            this.pnlCard3Borde.Padding = new System.Windows.Forms.Padding(2, 2, 2, 6);
            this.pnlCard3Borde.Size = new System.Drawing.Size(268, 100);
            // 
            // lblCardTitulo4
            // 
            this.lblCardTitulo4.AutoSize = true;
            this.lblCardTitulo4.BackColor = System.Drawing.Color.Transparent;
            this.lblCardTitulo4.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblCardTitulo4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(118)))), ((int)(((byte)(92)))));
            this.lblCardTitulo4.Location = new System.Drawing.Point(16, 12);
            this.lblCardTitulo4.Name = "lblCardTitulo4";
            this.lblCardTitulo4.Text = "Reacción media";
            // 
            // lblCardValor4
            // 
            this.lblCardValor4.AutoSize = true;
            this.lblCardValor4.BackColor = System.Drawing.Color.Transparent;
            this.lblCardValor4.Font = new System.Drawing.Font("Segoe UI", 22F, System.Drawing.FontStyle.Bold);
            this.lblCardValor4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(35)))), ((int)(((byte)(20)))));
            this.lblCardValor4.Location = new System.Drawing.Point(12, 32);
            this.lblCardValor4.Name = "lblCardValor4";
            this.lblCardValor4.Text = "0";
            // 
            // pnlCard4
            // 
            this.pnlCard4.Controls.Add(this.lblCardTitulo4);
            this.pnlCard4.Controls.Add(this.lblCardValor4);
            this.pnlCard4.BackColor = System.Drawing.Color.White;
            this.pnlCard4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCard4.Name = "pnlCard4";
            // 
            // pnlCard4Borde
            // 
            this.pnlCard4Borde.Controls.Add(this.pnlCard4);
            this.pnlCard4Borde.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(35)))), ((int)(((byte)(20)))));
            this.pnlCard4Borde.Location = new System.Drawing.Point(892, 128);
            this.pnlCard4Borde.Name = "pnlCard4Borde";
            this.pnlCard4Borde.Padding = new System.Windows.Forms.Padding(2, 2, 2, 6);
            this.pnlCard4Borde.Size = new System.Drawing.Size(268, 100);
            // 
            // lblGraficaTitulo
            // 
            this.lblGraficaTitulo.AutoSize = true;
            this.lblGraficaTitulo.BackColor = System.Drawing.Color.Transparent;
            this.lblGraficaTitulo.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblGraficaTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(35)))), ((int)(((byte)(20)))));
            this.lblGraficaTitulo.Location = new System.Drawing.Point(16, 12);
            this.lblGraficaTitulo.Name = "lblGraficaTitulo";
            this.lblGraficaTitulo.Text = "Aciertos y errores por partida";
            // 
            // lblGraficaSub
            // 
            this.lblGraficaSub.AutoSize = true;
            this.lblGraficaSub.BackColor = System.Drawing.Color.Transparent;
            this.lblGraficaSub.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblGraficaSub.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(118)))), ((int)(((byte)(92)))));
            this.lblGraficaSub.Location = new System.Drawing.Point(18, 38);
            this.lblGraficaSub.Name = "lblGraficaSub";
            this.lblGraficaSub.Text = "Últimas 12 partidas, de la más antigua a la más reciente";
            // 
            // lblLeyHits
            // 
            this.lblLeyHits.AutoSize = true;
            this.lblLeyHits.BackColor = System.Drawing.Color.Transparent;
            this.lblLeyHits.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblLeyHits.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(140)))), ((int)(((byte)(40)))));
            this.lblLeyHits.Location = new System.Drawing.Point(930, 18);
            this.lblLeyHits.Name = "lblLeyHits";
            this.lblLeyHits.Text = "■ hits";
            // 
            // lblLeyErrores
            // 
            this.lblLeyErrores.AutoSize = true;
            this.lblLeyErrores.BackColor = System.Drawing.Color.Transparent;
            this.lblLeyErrores.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblLeyErrores.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(170)))), ((int)(((byte)(58)))));
            this.lblLeyErrores.Location = new System.Drawing.Point(990, 18);
            this.lblLeyErrores.Name = "lblLeyErrores";
            this.lblLeyErrores.Text = "■ errors_made";
            // 
            // pnlBarrasPartidas
            // 
            this.pnlBarrasPartidas.BackColor = System.Drawing.Color.White;
            this.pnlBarrasPartidas.Location = new System.Drawing.Point(24, 60);
            this.pnlBarrasPartidas.Name = "pnlBarrasPartidas";
            this.pnlBarrasPartidas.Size = new System.Drawing.Size(1066, 214);
            // 
            // pnlGrafica
            // 
            this.pnlGrafica.Controls.Add(this.lblGraficaTitulo);
            this.pnlGrafica.Controls.Add(this.lblGraficaSub);
            this.pnlGrafica.Controls.Add(this.lblLeyHits);
            this.pnlGrafica.Controls.Add(this.lblLeyErrores);
            this.pnlGrafica.Controls.Add(this.pnlBarrasPartidas);
            this.pnlGrafica.BackColor = System.Drawing.Color.White;
            this.pnlGrafica.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGrafica.Name = "pnlGrafica";
            // 
            // pnlGraficaBorde
            // 
            this.pnlGraficaBorde.Controls.Add(this.pnlGrafica);
            this.pnlGraficaBorde.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(35)))), ((int)(((byte)(20)))));
            this.pnlGraficaBorde.Location = new System.Drawing.Point(40, 246);
            this.pnlGraficaBorde.Name = "pnlGraficaBorde";
            this.pnlGraficaBorde.Padding = new System.Windows.Forms.Padding(2, 2, 2, 6);
            this.pnlGraficaBorde.Size = new System.Drawing.Size(1120, 304);
            // 
            // lblHistorialTitulo
            // 
            this.lblHistorialTitulo.AutoSize = true;
            this.lblHistorialTitulo.BackColor = System.Drawing.Color.Transparent;
            this.lblHistorialTitulo.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblHistorialTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(35)))), ((int)(((byte)(20)))));
            this.lblHistorialTitulo.Location = new System.Drawing.Point(16, 12);
            this.lblHistorialTitulo.Name = "lblHistorialTitulo";
            this.lblHistorialTitulo.Text = "Historial de partidas";
            // 
            // dgvHistorial
            // 
            this.dgvHistorial.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colFecha,
            this.colDificultad,
            this.colPuntos,
            this.colHits,
            this.colErrores,
            this.colEscapados,
            this.colReaccion,
            this.colDuracion,
            this.colEstado});
            this.dgvHistorial.Location = new System.Drawing.Point(14, 44);
            this.dgvHistorial.Size = new System.Drawing.Size(1088, 244);
            this.dgvHistorial.Name = "dgvHistorial";
            this.dgvHistorial.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvHistorial_CellFormatting);
            this.dgvHistorial.ReadOnly = true;
            // 
            // colFecha
            // 
            this.colFecha.DataPropertyName = "FechaTexto";
            this.colFecha.HeaderText = "FECHA / HORA";
            this.colFecha.Name = "colFecha";
            this.colFecha.ReadOnly = true;
            this.colFecha.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colFecha.Width = 130;
            // 
            // colDificultad
            // 
            this.colDificultad.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colDificultad.DataPropertyName = "Nivel";
            this.colDificultad.HeaderText = "DIFICULTAD";
            this.colDificultad.Name = "colDificultad";
            this.colDificultad.ReadOnly = true;
            this.colDificultad.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colPuntos
            // 
            this.colPuntos.DataPropertyName = "Puntos";
            this.colPuntos.HeaderText = "PUNTOS";
            this.colPuntos.Name = "colPuntos";
            this.colPuntos.ReadOnly = true;
            this.colPuntos.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colPuntos.Width = 90;
            // 
            // colHits
            // 
            this.colHits.DataPropertyName = "Aciertos";
            this.colHits.HeaderText = "HITS";
            this.colHits.Name = "colHits";
            this.colHits.ReadOnly = true;
            this.colHits.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colHits.Width = 80;
            // 
            // colErrores
            // 
            this.colErrores.DataPropertyName = "Errores";
            this.colErrores.HeaderText = "ERRORES";
            this.colErrores.Name = "colErrores";
            this.colErrores.ReadOnly = true;
            this.colErrores.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colErrores.Width = 90;
            // 
            // colEscapados
            // 
            this.colEscapados.DataPropertyName = "Escapados";
            this.colEscapados.HeaderText = "ESCAPADOS";
            this.colEscapados.Name = "colEscapados";
            this.colEscapados.ReadOnly = true;
            this.colEscapados.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colEscapados.Width = 105;
            // 
            // colReaccion
            // 
            this.colReaccion.DataPropertyName = "ReaccionTexto";
            this.colReaccion.HeaderText = "REACCIÓN";
            this.colReaccion.Name = "colReaccion";
            this.colReaccion.ReadOnly = true;
            this.colReaccion.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colReaccion.Width = 110;
            // 
            // colDuracion
            // 
            this.colDuracion.DataPropertyName = "DuracionTexto";
            this.colDuracion.HeaderText = "DURACIÓN";
            this.colDuracion.Name = "colDuracion";
            this.colDuracion.ReadOnly = true;
            this.colDuracion.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colDuracion.Width = 100;
            // 
            // colEstado
            // 
            this.colEstado.DataPropertyName = "EstadoTexto";
            this.colEstado.HeaderText = "ESTADO";
            this.colEstado.Name = "colEstado";
            this.colEstado.ReadOnly = true;
            this.colEstado.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colEstado.Width = 120;
            // 
            // pnlHistorial
            // 
            this.pnlHistorial.Controls.Add(this.lblHistorialTitulo);
            this.pnlHistorial.Controls.Add(this.dgvHistorial);
            this.pnlHistorial.BackColor = System.Drawing.Color.White;
            this.pnlHistorial.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlHistorial.Name = "pnlHistorial";
            // 
            // pnlHistorialBorde
            // 
            this.pnlHistorialBorde.Controls.Add(this.pnlHistorial);
            this.pnlHistorialBorde.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(35)))), ((int)(((byte)(20)))));
            this.pnlHistorialBorde.Location = new System.Drawing.Point(40, 566);
            this.pnlHistorialBorde.Name = "pnlHistorialBorde";
            this.pnlHistorialBorde.Padding = new System.Windows.Forms.Padding(2, 2, 2, 6);
            this.pnlHistorialBorde.Size = new System.Drawing.Size(1120, 304);
            // 
            // FormDetalleJugador
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(244)))), ((int)(((byte)(220)))));
            this.Name = "FormDetalleJugador";
            this.ClientSize = new System.Drawing.Size(1200, 900);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Load += new System.EventHandler(this.FormDetalleJugador_Load);
            this.Controls.Add(this.lblVolver);
            this.Controls.Add(this.picAvatar);
            this.Controls.Add(this.lblNombre);
            this.Controls.Add(this.lblRegistrado);
            this.Controls.Add(this.lblSeQuedo);
            this.Controls.Add(this.cmbDificultad);
            this.Controls.Add(this.btnBloqueos);
            this.Controls.Add(this.pnlCard1Borde);
            this.Controls.Add(this.pnlCard2Borde);
            this.Controls.Add(this.pnlCard3Borde);
            this.Controls.Add(this.pnlCard4Borde);
            this.Controls.Add(this.pnlGraficaBorde);
            this.Controls.Add(this.pnlHistorialBorde);
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistorial)).EndInit();
            this.pnlCard1.ResumeLayout(false);
            this.pnlCard1.PerformLayout();
            this.pnlCard1Borde.ResumeLayout(false);
            this.pnlCard1Borde.PerformLayout();
            this.pnlCard2.ResumeLayout(false);
            this.pnlCard2.PerformLayout();
            this.pnlCard2Borde.ResumeLayout(false);
            this.pnlCard2Borde.PerformLayout();
            this.pnlCard3.ResumeLayout(false);
            this.pnlCard3.PerformLayout();
            this.pnlCard3Borde.ResumeLayout(false);
            this.pnlCard3Borde.PerformLayout();
            this.pnlCard4.ResumeLayout(false);
            this.pnlCard4.PerformLayout();
            this.pnlCard4Borde.ResumeLayout(false);
            this.pnlCard4Borde.PerformLayout();
            this.pnlBarrasPartidas.ResumeLayout(false);
            this.pnlBarrasPartidas.PerformLayout();
            this.pnlGrafica.ResumeLayout(false);
            this.pnlGrafica.PerformLayout();
            this.pnlGraficaBorde.ResumeLayout(false);
            this.pnlGraficaBorde.PerformLayout();
            this.pnlHistorial.ResumeLayout(false);
            this.pnlHistorial.PerformLayout();
            this.pnlHistorialBorde.ResumeLayout(false);
            this.pnlHistorialBorde.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblVolver;
        private System.Windows.Forms.PictureBox picAvatar;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.Label lblRegistrado;
        private System.Windows.Forms.Label lblSeQuedo;
        private System.Windows.Forms.ComboBox cmbDificultad;
        private System.Windows.Forms.Button btnBloqueos;
        private System.Windows.Forms.Label lblCardTitulo1;
        private System.Windows.Forms.Label lblCardValor1;
        private System.Windows.Forms.Panel pnlCard1;
        private System.Windows.Forms.Panel pnlCard1Borde;
        private System.Windows.Forms.Label lblCardTitulo2;
        private System.Windows.Forms.Label lblCardValor2;
        private System.Windows.Forms.Panel pnlCard2;
        private System.Windows.Forms.Panel pnlCard2Borde;
        private System.Windows.Forms.Label lblCardTitulo3;
        private System.Windows.Forms.Label lblCardValor3;
        private System.Windows.Forms.Panel pnlCard3;
        private System.Windows.Forms.Panel pnlCard3Borde;
        private System.Windows.Forms.Label lblCardTitulo4;
        private System.Windows.Forms.Label lblCardValor4;
        private System.Windows.Forms.Panel pnlCard4;
        private System.Windows.Forms.Panel pnlCard4Borde;
        private System.Windows.Forms.Label lblGraficaTitulo;
        private System.Windows.Forms.Label lblGraficaSub;
        private System.Windows.Forms.Label lblLeyHits;
        private System.Windows.Forms.Label lblLeyErrores;
        private System.Windows.Forms.Panel pnlBarrasPartidas;
        private System.Windows.Forms.Panel pnlGrafica;
        private System.Windows.Forms.Panel pnlGraficaBorde;
        private System.Windows.Forms.Label lblHistorialTitulo;
        private System.Windows.Forms.DataGridView dgvHistorial;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFecha;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDificultad;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPuntos;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHits;
        private System.Windows.Forms.DataGridViewTextBoxColumn colErrores;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEscapados;
        private System.Windows.Forms.DataGridViewTextBoxColumn colReaccion;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDuracion;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEstado;
        private System.Windows.Forms.Panel pnlHistorial;
        private System.Windows.Forms.Panel pnlHistorialBorde;
    }
}
