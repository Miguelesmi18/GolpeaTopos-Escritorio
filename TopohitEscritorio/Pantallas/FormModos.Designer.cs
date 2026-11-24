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
            this.dgvInfinito = new System.Windows.Forms.DataGridView();
            this.colNombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAgujeros = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colVidas = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colIncremento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPuntos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRequisito = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlTablaInfinito = new System.Windows.Forms.Panel();
            this.pnlTablaInfinitoBorde = new System.Windows.Forms.Panel();
            this.lblNota = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInfinito)).BeginInit();
            this.pnlTablaInfinito.SuspendLayout();
            this.pnlTablaInfinitoBorde.SuspendLayout();
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
            this.lblTitulo.Text = "Dificultades";
            // 
            // lblSubtitulo
            // 
            this.lblSubtitulo.AutoSize = true;
            this.lblSubtitulo.BackColor = System.Drawing.Color.Transparent;
            this.lblSubtitulo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSubtitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(118)))), ((int)(((byte)(92)))));
            this.lblSubtitulo.Location = new System.Drawing.Point(42, 70);
            this.lblSubtitulo.Name = "lblSubtitulo";
            this.lblSubtitulo.Text = "Modo Infinito · cambia las vidas, la aceleración y los puntos que hay que conseguir en la dificultad anterior para abrir cada una";
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
            // dgvInfinito
            // 
            this.dgvInfinito.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colNombre,
            this.colAgujeros,
            this.colVidas,
            this.colIncremento,
            this.colPuntos,
            this.colRequisito});
            this.dgvInfinito.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvInfinito.Name = "dgvInfinito";
            this.dgvInfinito.ReadOnly = false;
            this.dgvInfinito.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dgvInfinito_CellBeginEdit);
            this.dgvInfinito.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvInfinito_CellEndEdit);
            this.dgvInfinito.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgvInfinito_DataError);
            // 
            // colNombre
            // 
            this.colNombre.DataPropertyName = "Nombre";
            this.colNombre.HeaderText = "DIFICULTAD";
            this.colNombre.Name = "colNombre";
            this.colNombre.ReadOnly = true;
            this.colNombre.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colNombre.Width = 140;
            // 
            // colAgujeros
            // 
            this.colAgujeros.DataPropertyName = "Agujeros";
            this.colAgujeros.HeaderText = "AGUJEROS";
            this.colAgujeros.Name = "colAgujeros";
            this.colAgujeros.ReadOnly = true;
            this.colAgujeros.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colAgujeros.Width = 110;
            // 
            // colVidas
            // 
            this.colVidas.DataPropertyName = "Vidas";
            this.colVidas.HeaderText = "VIDAS";
            this.colVidas.Name = "colVidas";
            this.colVidas.ReadOnly = false;
            this.colVidas.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colVidas.Width = 100;
            // 
            // colIncremento
            // 
            this.colIncremento.DataPropertyName = "Incremento";
            this.colIncremento.HeaderText = "ACELERA (% por segundo)";
            this.colIncremento.Name = "colIncremento";
            this.colIncremento.ReadOnly = false;
            this.colIncremento.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colIncremento.Width = 190;
            // 
            // colPuntos
            // 
            this.colPuntos.DataPropertyName = "PuntosDesbloqueo";
            this.colPuntos.HeaderText = "PUNTOS PARA ABRIR";
            this.colPuntos.Name = "colPuntos";
            this.colPuntos.ReadOnly = false;
            this.colPuntos.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colPuntos.Width = 170;
            // 
            // colRequisito
            // 
            this.colRequisito.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colRequisito.DataPropertyName = "Requisito";
            this.colRequisito.HeaderText = "SE ABRE CON";
            this.colRequisito.Name = "colRequisito";
            this.colRequisito.ReadOnly = true;
            this.colRequisito.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // pnlTablaInfinito
            // 
            this.pnlTablaInfinito.Controls.Add(this.dgvInfinito);
            this.pnlTablaInfinito.BackColor = System.Drawing.Color.White;
            this.pnlTablaInfinito.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTablaInfinito.Name = "pnlTablaInfinito";
            this.pnlTablaInfinito.Padding = new System.Windows.Forms.Padding(14);
            // 
            // pnlTablaInfinitoBorde
            // 
            this.pnlTablaInfinitoBorde.Controls.Add(this.pnlTablaInfinito);
            this.pnlTablaInfinitoBorde.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(35)))), ((int)(((byte)(20)))));
            this.pnlTablaInfinitoBorde.Location = new System.Drawing.Point(40, 110);
            this.pnlTablaInfinitoBorde.Name = "pnlTablaInfinitoBorde";
            this.pnlTablaInfinitoBorde.Padding = new System.Windows.Forms.Padding(2, 2, 2, 6);
            this.pnlTablaInfinitoBorde.Size = new System.Drawing.Size(1120, 260);
            // 
            // lblNota
            // 
            this.lblNota.AutoSize = true;
            this.lblNota.BackColor = System.Drawing.Color.Transparent;
            this.lblNota.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblNota.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(118)))), ((int)(((byte)(92)))));
            this.lblNota.Location = new System.Drawing.Point(56, 384);
            this.lblNota.Name = "lblNota";
            this.lblNota.Text = "Las celdas blancas se pueden editar. Fácil siempre está abierta. Para bloquear una dificultad a un niño concreto, abre su ficha y pulsa «Bloquear dificultades».";
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
            this.Controls.Add(this.pnlTablaInfinitoBorde);
            this.Controls.Add(this.lblNota);
            ((System.ComponentModel.ISupportInitialize)(this.dgvInfinito)).EndInit();
            this.pnlTablaInfinito.ResumeLayout(false);
            this.pnlTablaInfinito.PerformLayout();
            this.pnlTablaInfinitoBorde.ResumeLayout(false);
            this.pnlTablaInfinitoBorde.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblSubtitulo;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.DataGridView dgvInfinito;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAgujeros;
        private System.Windows.Forms.DataGridViewTextBoxColumn colVidas;
        private System.Windows.Forms.DataGridViewTextBoxColumn colIncremento;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPuntos;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRequisito;
        private System.Windows.Forms.Panel pnlTablaInfinito;
        private System.Windows.Forms.Panel pnlTablaInfinitoBorde;
        private System.Windows.Forms.Label lblNota;
    }
}
