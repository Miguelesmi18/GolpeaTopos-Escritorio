namespace TopohitEscritorio
{
    partial class FormLogin
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

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormLogin));
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblSubtitulo = new System.Windows.Forms.Label();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.txtUsuario = new System.Windows.Forms.TextBox();
            this.lblContrasena = new System.Windows.Forms.Label();
            this.txtContrasena = new System.Windows.Forms.TextBox();
            this.lblError = new System.Windows.Forms.Label();
            this.btnEntrar = new System.Windows.Forms.Button();
            this.lblEstadoApi = new System.Windows.Forms.Label();
            this.lblNota = new System.Windows.Forms.Label();
            this.lblArte = new System.Windows.Forms.Label();
            this.lblFondoLogin = new System.Windows.Forms.Label();
            this.lblBordeLogin = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.BackColor = System.Drawing.Color.White;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(35)))), ((int)(((byte)(20)))));
            this.lblTitulo.Location = new System.Drawing.Point(585, 160);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Panel del educador";
            // 
            // lblSubtitulo
            // 
            this.lblSubtitulo.AutoSize = true;
            this.lblSubtitulo.BackColor = System.Drawing.Color.White;
            this.lblSubtitulo.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.lblSubtitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(118)))), ((int)(((byte)(92)))));
            this.lblSubtitulo.Location = new System.Drawing.Point(589, 208);
            this.lblSubtitulo.Name = "lblSubtitulo";
            this.lblSubtitulo.TabIndex = 1;
            this.lblSubtitulo.Text = "Gestión de jugadores, modos de juego y estadísticas";
            // 
            // lblUsuario
            // 
            this.lblUsuario.AutoSize = true;
            this.lblUsuario.BackColor = System.Drawing.Color.White;
            this.lblUsuario.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblUsuario.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(35)))), ((int)(((byte)(20)))));
            this.lblUsuario.Location = new System.Drawing.Point(587, 255);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.TabIndex = 2;
            this.lblUsuario.Text = "Usuario";
            // 
            // txtUsuario
            // 
            this.txtUsuario.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUsuario.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtUsuario.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(35)))), ((int)(((byte)(20)))));
            this.txtUsuario.Location = new System.Drawing.Point(590, 278);
            this.txtUsuario.Name = "txtUsuario";
            this.txtUsuario.Size = new System.Drawing.Size(400, 27);
            this.txtUsuario.TabIndex = 3;
            // 
            // lblContrasena
            // 
            this.lblContrasena.AutoSize = true;
            this.lblContrasena.BackColor = System.Drawing.Color.White;
            this.lblContrasena.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblContrasena.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(35)))), ((int)(((byte)(20)))));
            this.lblContrasena.Location = new System.Drawing.Point(587, 320);
            this.lblContrasena.Name = "lblContrasena";
            this.lblContrasena.TabIndex = 4;
            this.lblContrasena.Text = "Contraseña";
            // 
            // txtContrasena
            // 
            this.txtContrasena.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtContrasena.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtContrasena.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(35)))), ((int)(((byte)(20)))));
            this.txtContrasena.Location = new System.Drawing.Point(590, 343);
            this.txtContrasena.Name = "txtContrasena";
            this.txtContrasena.Size = new System.Drawing.Size(400, 27);
            this.txtContrasena.TabIndex = 5;
            this.txtContrasena.UseSystemPasswordChar = true;
            // 
            // lblError
            // 
            this.lblError.AutoSize = true;
            this.lblError.BackColor = System.Drawing.Color.White;
            this.lblError.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblError.ForeColor = System.Drawing.Color.Firebrick;
            this.lblError.Location = new System.Drawing.Point(587, 376);
            this.lblError.Name = "lblError";
            this.lblError.TabIndex = 6;
            this.lblError.Text = "Usuario o contraseña incorrectos";
            this.lblError.Visible = false;
            // 
            // btnEntrar
            // 
            this.btnEntrar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(140)))), ((int)(((byte)(40)))));
            this.btnEntrar.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(26)))), ((int)(((byte)(16)))));
            this.btnEntrar.FlatAppearance.BorderSize = 2;
            this.btnEntrar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEntrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEntrar.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.btnEntrar.ForeColor = System.Drawing.Color.White;
            this.btnEntrar.Location = new System.Drawing.Point(590, 405);
            this.btnEntrar.Name = "btnEntrar";
            this.btnEntrar.Size = new System.Drawing.Size(400, 44);
            this.btnEntrar.TabIndex = 7;
            this.btnEntrar.Text = "Entrar";
            this.btnEntrar.UseVisualStyleBackColor = false;
            this.btnEntrar.Click += new System.EventHandler(this.btnEntrar_Click);
            // 
            // lblEstadoApi
            // 
            this.lblEstadoApi.AutoSize = true;
            this.lblEstadoApi.BackColor = System.Drawing.Color.White;
            this.lblEstadoApi.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblEstadoApi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(160)))), ((int)(((byte)(50)))));
            this.lblEstadoApi.Location = new System.Drawing.Point(587, 475);
            this.lblEstadoApi.Name = "lblEstadoApi";
            this.lblEstadoApi.TabIndex = 8;
            this.lblEstadoApi.Text = "Estado de la API: ● Conectado (SQL Server)";
            // 
            // lblNota
            // 
            this.lblNota.AutoSize = true;
            this.lblNota.BackColor = System.Drawing.Color.White;
            this.lblNota.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblNota.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(118)))), ((int)(((byte)(92)))));
            this.lblNota.Location = new System.Drawing.Point(587, 512);
            this.lblNota.Name = "lblNota";
            this.lblNota.TabIndex = 9;
            this.lblNota.Text = "Solo pueden entrar cuentas con rol «educador» o «admin».";
            // 
            // lblArte
            // 
            this.lblArte.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(244)))), ((int)(((byte)(220)))));
            this.lblArte.Image = ((System.Drawing.Image)(resources.GetObject("lblArte.Image")));
            this.lblArte.Location = new System.Drawing.Point(70, 140);
            this.lblArte.Name = "lblArte";
            this.lblArte.Size = new System.Drawing.Size(400, 400);
            this.lblArte.TabIndex = 10;
            // 
            // lblFondoLogin
            // 
            this.lblFondoLogin.BackColor = System.Drawing.Color.White;
            this.lblFondoLogin.Location = new System.Drawing.Point(557, 122);
            this.lblFondoLogin.Name = "lblFondoLogin";
            this.lblFondoLogin.Size = new System.Drawing.Size(466, 446);
            this.lblFondoLogin.TabIndex = 11;
            // 
            // lblBordeLogin
            // 
            this.lblBordeLogin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(26)))), ((int)(((byte)(16)))));
            this.lblBordeLogin.Location = new System.Drawing.Point(555, 120);
            this.lblBordeLogin.Name = "lblBordeLogin";
            this.lblBordeLogin.Size = new System.Drawing.Size(470, 454);
            this.lblBordeLogin.TabIndex = 12;
            // 
            // FormLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(244)))), ((int)(((byte)(220)))));
            this.ClientSize = new System.Drawing.Size(1100, 690);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;   // tamaño fijo
            this.MaximizeBox = false;                                                  // sin botón de maximizar
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.lblSubtitulo);
            this.Controls.Add(this.lblUsuario);
            this.Controls.Add(this.txtUsuario);
            this.Controls.Add(this.lblContrasena);
            this.Controls.Add(this.txtContrasena);
            this.Controls.Add(this.lblError);
            this.Controls.Add(this.btnEntrar);
            this.Controls.Add(this.lblEstadoApi);
            this.Controls.Add(this.lblNota);
            this.Controls.Add(this.lblArte);
            this.Controls.Add(this.lblFondoLogin);
            this.Controls.Add(this.lblBordeLogin);
            this.Name = "FormLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Topohit · Panel del educador";
            this.Load += new System.EventHandler(this.FormLogin_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblSubtitulo;
        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.TextBox txtUsuario;
        private System.Windows.Forms.Label lblContrasena;
        private System.Windows.Forms.TextBox txtContrasena;
        private System.Windows.Forms.Label lblError;
        private System.Windows.Forms.Button btnEntrar;
        private System.Windows.Forms.Label lblEstadoApi;
        private System.Windows.Forms.Label lblNota;
        private System.Windows.Forms.Label lblArte;
        private System.Windows.Forms.Label lblFondoLogin;
        private System.Windows.Forms.Label lblBordeLogin;
    }
}
