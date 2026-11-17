namespace TopohitEscritorio.Pantallas
{
    partial class FormModos
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
            this.lblSubtitulo = new System.Windows.Forms.Label();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.dgvHistoria = new System.Windows.Forms.DataGridView();
            this.colNumero = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colZona = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAgujeros = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colObjetivo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTiempo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNovedad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colActivo = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.pnlTablaHistoria = new System.Windows.Forms.Panel();
            this.pnlTablaHistoriaBorde = new System.Windows.Forms.Panel();
            this.lblNota = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistoria)).BeginInit();
            this.pnlTablaHistoria.SuspendLayout();
            this.pnlTablaHistoriaBorde.SuspendLayout();
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
            this.lblTitulo.Text = "Modos y niveles";
            // 
            // lblSubtitulo
            // 
            this.lblSubtitulo.AutoSize = true;
            this.lblSubtitulo.BackColor = System.Drawing.Color.Transparent;
            this.lblSubtitulo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSubtitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(118)))), ((int)(((byte)(92)))));
            this.lblSubtitulo.Location = new System.Drawing.Point(42, 70);
            this.lblSubtitulo.Name = "lblSubtitulo";
            this.lblSubtitulo.Text = "Niveles del modo Historia · marca o desmarca «Activo» para quitar o volver a poner un nivel · la app Android lo descarga de la API";
            // 
            // btnGuardar
            // 
            this.btnGuardar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(140)))), ((int)(((byte)(40)))));
            this.btnGuardar.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(35)))), ((int)(((byte)(20)))));
            this.btnGuardar.FlatAppearance.BorderSize = 2;
            this.btnGuardar.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.btnGuardar.ForeColor = System.Drawing.Color.White;
            this.btnGuardar.Location = new System.Drawing.Point(990, 28);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(170, 46);
            this.btnGuardar.Text = "Guardar cambios";
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // dgvHistoria
            // 
            this.dgvHistoria.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colNumero,
            this.colZona,
            this.colAgujeros,
            this.colObjetivo,
            this.colTiempo,
            this.colNovedad,
            this.colActivo});
            this.dgvHistoria.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvHistoria.Name = "dgvHistoria";
            this.dgvHistoria.ReadOnly = false;
            // 
            // colNumero
            // 
            this.colNumero.DataPropertyName = "Numero";
            this.colNumero.HeaderText = "NIVEL";
            this.colNumero.Name = "colNumero";
            this.colNumero.ReadOnly = true;
            this.colNumero.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colNumero.Width = 80;
            // 
            // colZona
            // 
            this.colZona.DataPropertyName = "Zona";
            this.colZona.HeaderText = "ZONA";
            this.colZona.Name = "colZona";
            this.colZona.ReadOnly = true;
            this.colZona.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colZona.Width = 150;
            // 
            // colAgujeros
            // 
            this.colAgujeros.DataPropertyName = "Agujeros";
            this.colAgujeros.HeaderText = "AGUJEROS";
            this.colAgujeros.Name = "colAgujeros";
            this.colAgujeros.ReadOnly = true;
            this.colAgujeros.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colAgujeros.Width = 120;
            // 
            // colObjetivo
            // 
            this.colObjetivo.DataPropertyName = "Objetivo";
            this.colObjetivo.HeaderText = "OBJETIVO";
            this.colObjetivo.Name = "colObjetivo";
            this.colObjetivo.ReadOnly = true;
            this.colObjetivo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colObjetivo.Width = 120;
            // 
            // colTiempo
            // 
            this.colTiempo.DataPropertyName = "Tiempo";
            this.colTiempo.HeaderText = "TIEMPO (s)";
            this.colTiempo.Name = "colTiempo";
            this.colTiempo.ReadOnly = true;
            this.colTiempo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colTiempo.Width = 120;
            // 
            // colNovedad
            // 
            this.colNovedad.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colNovedad.DataPropertyName = "Novedad";
            this.colNovedad.HeaderText = "NOVEDAD";
            this.colNovedad.Name = "colNovedad";
            this.colNovedad.ReadOnly = true;
            this.colNovedad.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colActivo
            // 
            this.colActivo.DataPropertyName = "Activo";
            this.colActivo.HeaderText = "ACTIVO";
            this.colActivo.Name = "colActivo";
            this.colActivo.ReadOnly = false;
            this.colActivo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colActivo.Width = 100;
            // 
            // pnlTablaHistoria
            // 
            this.pnlTablaHistoria.Controls.Add(this.dgvHistoria);
            this.pnlTablaHistoria.BackColor = System.Drawing.Color.White;
            this.pnlTablaHistoria.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTablaHistoria.Name = "pnlTablaHistoria";
            this.pnlTablaHistoria.Padding = new System.Windows.Forms.Padding(14);
            // 
            // pnlTablaHistoriaBorde
            // 
            this.pnlTablaHistoriaBorde.Controls.Add(this.pnlTablaHistoria);
            this.pnlTablaHistoriaBorde.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(35)))), ((int)(((byte)(20)))));
            this.pnlTablaHistoriaBorde.Location = new System.Drawing.Point(40, 110);
            this.pnlTablaHistoriaBorde.Name = "pnlTablaHistoriaBorde";
            this.pnlTablaHistoriaBorde.Padding = new System.Windows.Forms.Padding(2, 2, 2, 6);
            this.pnlTablaHistoriaBorde.Size = new System.Drawing.Size(1120, 644);
            // 
            // lblNota
            // 
            this.lblNota.AutoSize = true;
            this.lblNota.BackColor = System.Drawing.Color.Transparent;
            this.lblNota.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblNota.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(118)))), ((int)(((byte)(92)))));
            this.lblNota.Location = new System.Drawing.Point(56, 766);
            this.lblNota.Name = "lblNota";
            this.lblNota.Text = "Un nivel desactivado no aparece en la tablet. Objetivo 0 = nivel jefe.";
            // 
            // FormModos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(244)))), ((int)(((byte)(220)))));
            this.Name = "FormModos";
            this.ClientSize = new System.Drawing.Size(1200, 900);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Load += new System.EventHandler(this.FormModos_Load);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.lblSubtitulo);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.pnlTablaHistoriaBorde);
            this.Controls.Add(this.lblNota);
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistoria)).EndInit();
            this.pnlTablaHistoria.ResumeLayout(false);
            this.pnlTablaHistoria.PerformLayout();
            this.pnlTablaHistoriaBorde.ResumeLayout(false);
            this.pnlTablaHistoriaBorde.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblSubtitulo;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.DataGridView dgvHistoria;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNumero;
        private System.Windows.Forms.DataGridViewTextBoxColumn colZona;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAgujeros;
        private System.Windows.Forms.DataGridViewTextBoxColumn colObjetivo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTiempo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNovedad;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colActivo;
        private System.Windows.Forms.Panel pnlTablaHistoria;
        private System.Windows.Forms.Panel pnlTablaHistoriaBorde;
        private System.Windows.Forms.Label lblNota;
    }
}
