namespace TopohitEscritorio.Pantallas
{
    partial class FormMetricas
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
            this.btnCalcular = new System.Windows.Forms.Button();
            this.lblEstado = new System.Windows.Forms.Label();
            this.flpTarjetas = new System.Windows.Forms.FlowLayoutPanel();
            this.flpGraficas = new System.Windows.Forms.FlowLayoutPanel();
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
            this.lblTitulo.Text = "Métricas";
            //
            // lblSubtitulo
            //
            this.lblSubtitulo.AutoSize = true;
            this.lblSubtitulo.BackColor = System.Drawing.Color.Transparent;
            this.lblSubtitulo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSubtitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(118)))), ((int)(((byte)(92)))));
            this.lblSubtitulo.Location = new System.Drawing.Point(42, 70);
            this.lblSubtitulo.Name = "lblSubtitulo";
            this.lblSubtitulo.Text = "Todos los jugadores · modo Infinito · modelo que predice si un niño volverá a jugar (haz clic en una gráfica para verla grande)";
            //
            // btnCalcular
            //
            this.btnCalcular.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCalcular.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCalcular.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(140)))), ((int)(((byte)(40)))));
            this.btnCalcular.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(35)))), ((int)(((byte)(20)))));
            this.btnCalcular.FlatAppearance.BorderSize = 2;
            this.btnCalcular.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.btnCalcular.ForeColor = System.Drawing.Color.White;
            this.btnCalcular.Location = new System.Drawing.Point(990, 28);
            this.btnCalcular.Name = "btnCalcular";
            this.btnCalcular.Size = new System.Drawing.Size(170, 46);
            this.btnCalcular.Text = "Calcular otra vez";
            this.btnCalcular.UseVisualStyleBackColor = false;
            this.btnCalcular.Click += new System.EventHandler(this.btnCalcular_Click);
            //
            // lblEstado
            //
            this.lblEstado.AutoSize = true;
            this.lblEstado.BackColor = System.Drawing.Color.Transparent;
            this.lblEstado.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblEstado.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(35)))), ((int)(((byte)(20)))));
            this.lblEstado.Location = new System.Drawing.Point(42, 110);
            this.lblEstado.MaximumSize = new System.Drawing.Size(1100, 0);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Text = "Calculando las métricas…";
            //
            // flpTarjetas
            //
            this.flpTarjetas.BackColor = System.Drawing.Color.Transparent;
            this.flpTarjetas.Location = new System.Drawing.Point(36, 104);
            this.flpTarjetas.Name = "flpTarjetas";
            this.flpTarjetas.Size = new System.Drawing.Size(1130, 200);
            //
            // flpGraficas
            //
            this.flpGraficas.AutoScroll = true;
            this.flpGraficas.BackColor = System.Drawing.Color.Transparent;
            this.flpGraficas.Location = new System.Drawing.Point(36, 312);
            this.flpGraficas.Name = "flpGraficas";
            this.flpGraficas.Size = new System.Drawing.Size(1140, 570);
            //
            // FormMetricas
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(244)))), ((int)(((byte)(220)))));
            this.ClientSize = new System.Drawing.Size(1200, 900);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.lblSubtitulo);
            this.Controls.Add(this.btnCalcular);
            this.Controls.Add(this.lblEstado);
            this.Controls.Add(this.flpTarjetas);
            this.Controls.Add(this.flpGraficas);
            this.Name = "FormMetricas";
            this.Text = "Métricas";
            this.Load += new System.EventHandler(this.FormMetricas_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblSubtitulo;
        private System.Windows.Forms.Button btnCalcular;
        private System.Windows.Forms.Label lblEstado;
        private System.Windows.Forms.FlowLayoutPanel flpTarjetas;
        private System.Windows.Forms.FlowLayoutPanel flpGraficas;
    }
}
