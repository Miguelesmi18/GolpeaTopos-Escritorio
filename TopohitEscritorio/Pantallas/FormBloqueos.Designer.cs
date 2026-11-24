namespace TopohitEscritorio.Pantallas
{
    partial class FormBloqueos
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
            this.lblExplicacion = new System.Windows.Forms.Label();
            this.lblCargando = new System.Windows.Forms.Label();
            this.chkBloqueo1 = new System.Windows.Forms.CheckBox();
            this.txtMotivo1 = new System.Windows.Forms.TextBox();
            this.lblEstado1 = new System.Windows.Forms.Label();
            this.chkBloqueo2 = new System.Windows.Forms.CheckBox();
            this.txtMotivo2 = new System.Windows.Forms.TextBox();
            this.lblEstado2 = new System.Windows.Forms.Label();
            this.chkBloqueo3 = new System.Windows.Forms.CheckBox();
            this.txtMotivo3 = new System.Windows.Forms.TextBox();
            this.lblEstado3 = new System.Windows.Forms.Label();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.BackColor = System.Drawing.Color.Transparent;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.FromArgb(242, 140, 40);
            this.lblTitulo.Location = new System.Drawing.Point(24, 16);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Text = "Bloquear dificultades";
            // 
            // lblExplicacion
            // 
            this.lblExplicacion.BackColor = System.Drawing.Color.Transparent;
            this.lblExplicacion.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblExplicacion.ForeColor = System.Drawing.Color.FromArgb(150, 118, 92);
            this.lblExplicacion.Location = new System.Drawing.Point(28, 58);
            this.lblExplicacion.Size = new System.Drawing.Size(600, 40);
            this.lblExplicacion.Name = "lblExplicacion";
            this.lblExplicacion.Text = "Marca las dificultades que este niño no puede jugar. En la tablet saldrán con un candado y el motivo (si no escribes ninguno, saldrá «Tu tutor ha bloqueado esta dificultad»).";
            // 
            // lblCargando
            // 
            this.lblCargando.AutoSize = true;
            this.lblCargando.BackColor = System.Drawing.Color.Transparent;
            this.lblCargando.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblCargando.ForeColor = System.Drawing.Color.FromArgb(150, 118, 92);
            this.lblCargando.Location = new System.Drawing.Point(28, 110);
            this.lblCargando.Name = "lblCargando";
            this.lblCargando.Text = "Cargando…";
            // 
            // chkBloqueo1
            // 
            this.chkBloqueo1.AutoSize = true;
            this.chkBloqueo1.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.chkBloqueo1.ForeColor = System.Drawing.Color.FromArgb(59, 35, 20);
            this.chkBloqueo1.Location = new System.Drawing.Point(28, 150);
            this.chkBloqueo1.Name = "chkBloqueo1";
            this.chkBloqueo1.Text = "Bloquear";
            this.chkBloqueo1.CheckedChanged += new System.EventHandler(this.chkBloqueo_CheckedChanged);
            // 
            // txtMotivo1
            // 
            this.txtMotivo1.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtMotivo1.Location = new System.Drawing.Point(200, 150);
            this.txtMotivo1.MaxLength = 200;
            this.txtMotivo1.Name = "txtMotivo1";
            this.txtMotivo1.Size = new System.Drawing.Size(428, 25);
            // 
            // lblEstado1
            // 
            this.lblEstado1.AutoSize = true;
            this.lblEstado1.BackColor = System.Drawing.Color.Transparent;
            this.lblEstado1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblEstado1.ForeColor = System.Drawing.Color.FromArgb(150, 118, 92);
            this.lblEstado1.Location = new System.Drawing.Point(202, 180);
            this.lblEstado1.Name = "lblEstado1";
            this.lblEstado1.Text = "";
            // 
            // chkBloqueo2
            // 
            this.chkBloqueo2.AutoSize = true;
            this.chkBloqueo2.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.chkBloqueo2.ForeColor = System.Drawing.Color.FromArgb(59, 35, 20);
            this.chkBloqueo2.Location = new System.Drawing.Point(28, 222);
            this.chkBloqueo2.Name = "chkBloqueo2";
            this.chkBloqueo2.Text = "Bloquear";
            this.chkBloqueo2.CheckedChanged += new System.EventHandler(this.chkBloqueo_CheckedChanged);
            // 
            // txtMotivo2
            // 
            this.txtMotivo2.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtMotivo2.Location = new System.Drawing.Point(200, 222);
            this.txtMotivo2.MaxLength = 200;
            this.txtMotivo2.Name = "txtMotivo2";
            this.txtMotivo2.Size = new System.Drawing.Size(428, 25);
            // 
            // lblEstado2
            // 
            this.lblEstado2.AutoSize = true;
            this.lblEstado2.BackColor = System.Drawing.Color.Transparent;
            this.lblEstado2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblEstado2.ForeColor = System.Drawing.Color.FromArgb(150, 118, 92);
            this.lblEstado2.Location = new System.Drawing.Point(202, 252);
            this.lblEstado2.Name = "lblEstado2";
            this.lblEstado2.Text = "";
            // 
            // chkBloqueo3
            // 
            this.chkBloqueo3.AutoSize = true;
            this.chkBloqueo3.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.chkBloqueo3.ForeColor = System.Drawing.Color.FromArgb(59, 35, 20);
            this.chkBloqueo3.Location = new System.Drawing.Point(28, 294);
            this.chkBloqueo3.Name = "chkBloqueo3";
            this.chkBloqueo3.Text = "Bloquear";
            this.chkBloqueo3.CheckedChanged += new System.EventHandler(this.chkBloqueo_CheckedChanged);
            // 
            // txtMotivo3
            // 
            this.txtMotivo3.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtMotivo3.Location = new System.Drawing.Point(200, 294);
            this.txtMotivo3.MaxLength = 200;
            this.txtMotivo3.Name = "txtMotivo3";
            this.txtMotivo3.Size = new System.Drawing.Size(428, 25);
            // 
            // lblEstado3
            // 
            this.lblEstado3.AutoSize = true;
            this.lblEstado3.BackColor = System.Drawing.Color.Transparent;
            this.lblEstado3.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblEstado3.ForeColor = System.Drawing.Color.FromArgb(150, 118, 92);
            this.lblEstado3.Location = new System.Drawing.Point(202, 324);
            this.lblEstado3.Name = "lblEstado3";
            this.lblEstado3.Text = "";
            // 
            // btnCancelar
            // 
            this.btnCancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelar.Location = new System.Drawing.Point(318, 376);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(150, 42);
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGuardar.Location = new System.Drawing.Point(478, 376);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(150, 42);
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // FormBloqueos
            // 
            this.AcceptButton = this.btnGuardar;
            this.CancelButton = this.btnCancelar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(255, 244, 220);
            this.ClientSize = new System.Drawing.Size(660, 438);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.lblExplicacion);
            this.Controls.Add(this.lblCargando);
            this.Controls.Add(this.chkBloqueo1);
            this.Controls.Add(this.txtMotivo1);
            this.Controls.Add(this.lblEstado1);
            this.Controls.Add(this.chkBloqueo2);
            this.Controls.Add(this.txtMotivo2);
            this.Controls.Add(this.lblEstado2);
            this.Controls.Add(this.chkBloqueo3);
            this.Controls.Add(this.txtMotivo3);
            this.Controls.Add(this.lblEstado3);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.btnCancelar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormBloqueos";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Bloquear dificultades";
            this.Load += new System.EventHandler(this.FormBloqueos_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblExplicacion;
        private System.Windows.Forms.Label lblCargando;
        private System.Windows.Forms.CheckBox chkBloqueo1;
        private System.Windows.Forms.TextBox txtMotivo1;
        private System.Windows.Forms.Label lblEstado1;
        private System.Windows.Forms.CheckBox chkBloqueo2;
        private System.Windows.Forms.TextBox txtMotivo2;
        private System.Windows.Forms.Label lblEstado2;
        private System.Windows.Forms.CheckBox chkBloqueo3;
        private System.Windows.Forms.TextBox txtMotivo3;
        private System.Windows.Forms.Label lblEstado3;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnCancelar;
    }
}
