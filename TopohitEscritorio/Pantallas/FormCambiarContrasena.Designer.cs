namespace TopohitEscritorio
{
    partial class FormCambiarContrasena
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
            this.btnCerrar = new System.Windows.Forms.Button();
            this.lblActual = new System.Windows.Forms.Label();
            this.txtActual = new System.Windows.Forms.TextBox();
            this.lblNueva = new System.Windows.Forms.Label();
            this.txtNueva = new System.Windows.Forms.TextBox();
            this.lblRepetir = new System.Windows.Forms.Label();
            this.txtRepetir = new System.Windows.Forms.TextBox();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.pnlFondo = new System.Windows.Forms.Panel();
            this.pnlFondo.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.BackColor = System.Drawing.Color.Transparent;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(35)))), ((int)(((byte)(20)))));
            this.lblTitulo.Location = new System.Drawing.Point(30, 24);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Text = "Cambiar contraseña";
            // 
            // btnCerrar
            // 
            this.btnCerrar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCerrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCerrar.BackColor = System.Drawing.Color.White;
            this.btnCerrar.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(35)))), ((int)(((byte)(20)))));
            this.btnCerrar.FlatAppearance.BorderSize = 2;
            this.btnCerrar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnCerrar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(35)))), ((int)(((byte)(20)))));
            this.btnCerrar.Location = new System.Drawing.Point(384, 22);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(44, 40);
            this.btnCerrar.Text = "✕";
            this.btnCerrar.UseVisualStyleBackColor = false;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // lblActual
            // 
            this.lblActual.AutoSize = true;
            this.lblActual.BackColor = System.Drawing.Color.Transparent;
            this.lblActual.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblActual.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(35)))), ((int)(((byte)(20)))));
            this.lblActual.Location = new System.Drawing.Point(34, 92);
            this.lblActual.Name = "lblActual";
            this.lblActual.Text = "Contraseña actual";
            // 
            // txtActual
            // 
            this.txtActual.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtActual.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtActual.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(35)))), ((int)(((byte)(20)))));
            this.txtActual.Location = new System.Drawing.Point(34, 116);
            this.txtActual.Name = "txtActual";
            this.txtActual.Size = new System.Drawing.Size(380, 27);
            this.txtActual.UseSystemPasswordChar = true;
            // 
            // lblNueva
            // 
            this.lblNueva.AutoSize = true;
            this.lblNueva.BackColor = System.Drawing.Color.Transparent;
            this.lblNueva.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblNueva.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(35)))), ((int)(((byte)(20)))));
            this.lblNueva.Location = new System.Drawing.Point(34, 160);
            this.lblNueva.Name = "lblNueva";
            this.lblNueva.Text = "Contraseña nueva (mínimo 4 caracteres)";
            // 
            // txtNueva
            // 
            this.txtNueva.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNueva.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtNueva.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(35)))), ((int)(((byte)(20)))));
            this.txtNueva.Location = new System.Drawing.Point(34, 184);
            this.txtNueva.Name = "txtNueva";
            this.txtNueva.Size = new System.Drawing.Size(380, 27);
            this.txtNueva.UseSystemPasswordChar = true;
            // 
            // lblRepetir
            // 
            this.lblRepetir.AutoSize = true;
            this.lblRepetir.BackColor = System.Drawing.Color.Transparent;
            this.lblRepetir.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblRepetir.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(35)))), ((int)(((byte)(20)))));
            this.lblRepetir.Location = new System.Drawing.Point(34, 228);
            this.lblRepetir.Name = "lblRepetir";
            this.lblRepetir.Text = "Repite la contraseña nueva";
            // 
            // txtRepetir
            // 
            this.txtRepetir.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRepetir.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtRepetir.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(35)))), ((int)(((byte)(20)))));
            this.txtRepetir.Location = new System.Drawing.Point(34, 252);
            this.txtRepetir.Name = "txtRepetir";
            this.txtRepetir.Size = new System.Drawing.Size(380, 27);
            this.txtRepetir.UseSystemPasswordChar = true;
            // 
            // btnCancelar
            // 
            this.btnCancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.BackColor = System.Drawing.Color.White;
            this.btnCancelar.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(35)))), ((int)(((byte)(20)))));
            this.btnCancelar.FlatAppearance.BorderSize = 2;
            this.btnCancelar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnCancelar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(35)))), ((int)(((byte)(20)))));
            this.btnCancelar.Location = new System.Drawing.Point(166, 302);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(114, 44);
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
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
            this.btnGuardar.Location = new System.Drawing.Point(294, 302);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(134, 44);
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // pnlFondo
            // 
            this.pnlFondo.Controls.Add(this.lblTitulo);
            this.pnlFondo.Controls.Add(this.btnCerrar);
            this.pnlFondo.Controls.Add(this.lblActual);
            this.pnlFondo.Controls.Add(this.txtActual);
            this.pnlFondo.Controls.Add(this.lblNueva);
            this.pnlFondo.Controls.Add(this.txtNueva);
            this.pnlFondo.Controls.Add(this.lblRepetir);
            this.pnlFondo.Controls.Add(this.txtRepetir);
            this.pnlFondo.Controls.Add(this.btnCancelar);
            this.pnlFondo.Controls.Add(this.btnGuardar);
            this.pnlFondo.BackColor = System.Drawing.Color.White;
            this.pnlFondo.Location = new System.Drawing.Point(3, 3);
            this.pnlFondo.Name = "pnlFondo";
            this.pnlFondo.Size = new System.Drawing.Size(454, 364);
            // 
            // FormCambiarContrasena
            // 
            this.AcceptButton = this.btnGuardar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(35)))), ((int)(((byte)(20)))));
            this.ClientSize = new System.Drawing.Size(460, 370);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormCambiarContrasena";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Cambiar contraseña";
            this.Controls.Add(this.pnlFondo);
            this.pnlFondo.ResumeLayout(false);
            this.pnlFondo.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.Label lblActual;
        private System.Windows.Forms.TextBox txtActual;
        private System.Windows.Forms.Label lblNueva;
        private System.Windows.Forms.TextBox txtNueva;
        private System.Windows.Forms.Label lblRepetir;
        private System.Windows.Forms.TextBox txtRepetir;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Panel pnlFondo;
    }
}
