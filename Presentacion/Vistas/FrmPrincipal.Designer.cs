namespace Presentacion.Vistas
{
    partial class FrmPrincipal
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pnlMenu = new System.Windows.Forms.Panel();
            this.pnlUsuario = new System.Windows.Forms.Panel();
            this.lblCorreo = new System.Windows.Forms.Label();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.lblSesionTitulo = new System.Windows.Forms.Label();
            this.btnCerrarSesion = new System.Windows.Forms.Button();
            this.btnOrganizar = new System.Windows.Forms.Button();
            this.btnCraps = new System.Windows.Forms.Button();
            this.btnProductos = new System.Windows.Forms.Button();
            this.btnUsuarios = new System.Windows.Forms.Button();
            this.lblFecha = new System.Windows.Forms.Label();
            this.lblHora = new System.Windows.Forms.Label();
            this.lblApp = new System.Windows.Forms.Label();
            this.tmrReloj = new System.Windows.Forms.Timer(this.components);
            this.pnlMenu.SuspendLayout();
            this.pnlUsuario.SuspendLayout();
            this.SuspendLayout();
            //
            // pnlMenu
            //
            this.pnlMenu.BackColor = System.Drawing.Color.FromArgb(24, 37, 61);
            this.pnlMenu.Controls.Add(this.pnlUsuario);
            this.pnlMenu.Controls.Add(this.btnCerrarSesion);
            this.pnlMenu.Controls.Add(this.btnOrganizar);
            this.pnlMenu.Controls.Add(this.btnCraps);
            this.pnlMenu.Controls.Add(this.btnProductos);
            this.pnlMenu.Controls.Add(this.btnUsuarios);
            this.pnlMenu.Controls.Add(this.lblFecha);
            this.pnlMenu.Controls.Add(this.lblHora);
            this.pnlMenu.Controls.Add(this.lblApp);
            this.pnlMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlMenu.Location = new System.Drawing.Point(0, 0);
            this.pnlMenu.Name = "pnlMenu";
            this.pnlMenu.Size = new System.Drawing.Size(240, 650);
            this.pnlMenu.TabIndex = 0;
            //
            // pnlUsuario
            //
            this.pnlUsuario.BackColor = System.Drawing.Color.FromArgb(14, 22, 38);
            this.pnlUsuario.Controls.Add(this.lblCorreo);
            this.pnlUsuario.Controls.Add(this.lblUsuario);
            this.pnlUsuario.Controls.Add(this.lblSesionTitulo);
            this.pnlUsuario.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlUsuario.Location = new System.Drawing.Point(0, 560);
            this.pnlUsuario.Name = "pnlUsuario";
            this.pnlUsuario.Size = new System.Drawing.Size(240, 90);
            this.pnlUsuario.TabIndex = 6;
            //
            // lblCorreo
            //
            this.lblCorreo.AutoEllipsis = true;
            this.lblCorreo.ForeColor = System.Drawing.Color.Silver;
            this.lblCorreo.Location = new System.Drawing.Point(16, 58);
            this.lblCorreo.Name = "lblCorreo";
            this.lblCorreo.Size = new System.Drawing.Size(210, 20);
            this.lblCorreo.TabIndex = 2;
            this.lblCorreo.Text = "correo";
            //
            // lblUsuario
            //
            this.lblUsuario.AutoEllipsis = true;
            this.lblUsuario.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblUsuario.ForeColor = System.Drawing.Color.White;
            this.lblUsuario.Location = new System.Drawing.Point(16, 32);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Size = new System.Drawing.Size(210, 24);
            this.lblUsuario.TabIndex = 1;
            this.lblUsuario.Text = "usuario";
            //
            // lblSesionTitulo
            //
            this.lblSesionTitulo.AutoSize = true;
            this.lblSesionTitulo.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblSesionTitulo.ForeColor = System.Drawing.Color.LightSkyBlue;
            this.lblSesionTitulo.Location = new System.Drawing.Point(16, 12);
            this.lblSesionTitulo.Name = "lblSesionTitulo";
            this.lblSesionTitulo.Size = new System.Drawing.Size(115, 15);
            this.lblSesionTitulo.TabIndex = 0;
            this.lblSesionTitulo.Text = "Sesión iniciada como:";
            //
            // btnCerrarSesion
            //
            this.btnCerrarSesion.BackColor = System.Drawing.Color.FromArgb(24, 37, 61);
            this.btnCerrarSesion.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCerrarSesion.FlatAppearance.BorderSize = 0;
            this.btnCerrarSesion.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(45, 64, 102);
            this.btnCerrarSesion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCerrarSesion.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.btnCerrarSesion.ForeColor = System.Drawing.Color.White;
            this.btnCerrarSesion.Location = new System.Drawing.Point(0, 344);
            this.btnCerrarSesion.Name = "btnCerrarSesion";
            this.btnCerrarSesion.Padding = new System.Windows.Forms.Padding(24, 0, 0, 0);
            this.btnCerrarSesion.Size = new System.Drawing.Size(240, 46);
            this.btnCerrarSesion.TabIndex = 5;
            this.btnCerrarSesion.Text = "Cerrar sesión";
            this.btnCerrarSesion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCerrarSesion.UseVisualStyleBackColor = false;
            this.btnCerrarSesion.Click += new System.EventHandler(this.btnCerrarSesion_Click);
            //
            // btnOrganizar
            //
            this.btnOrganizar.BackColor = System.Drawing.Color.FromArgb(24, 37, 61);
            this.btnOrganizar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOrganizar.FlatAppearance.BorderSize = 0;
            this.btnOrganizar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(45, 64, 102);
            this.btnOrganizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOrganizar.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.btnOrganizar.ForeColor = System.Drawing.Color.White;
            this.btnOrganizar.Location = new System.Drawing.Point(0, 252);
            this.btnOrganizar.Name = "btnOrganizar";
            this.btnOrganizar.Padding = new System.Windows.Forms.Padding(24, 0, 0, 0);
            this.btnOrganizar.Size = new System.Drawing.Size(240, 46);
            this.btnOrganizar.TabIndex = 4;
            this.btnOrganizar.Text = "Organizar ventanas";
            this.btnOrganizar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOrganizar.UseVisualStyleBackColor = false;
            this.btnOrganizar.Click += new System.EventHandler(this.btnOrganizar_Click);
            //
            // btnCraps
            //
            this.btnCraps.BackColor = System.Drawing.Color.FromArgb(24, 37, 61);
            this.btnCraps.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCraps.FlatAppearance.BorderSize = 0;
            this.btnCraps.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(45, 64, 102);
            this.btnCraps.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCraps.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.btnCraps.ForeColor = System.Drawing.Color.White;
            this.btnCraps.Location = new System.Drawing.Point(0, 160);
            this.btnCraps.Name = "btnCraps";
            this.btnCraps.Padding = new System.Windows.Forms.Padding(24, 0, 0, 0);
            this.btnCraps.Size = new System.Drawing.Size(240, 46);
            this.btnCraps.TabIndex = 3;
            this.btnCraps.Text = "Juego de Craps";
            this.btnCraps.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCraps.UseVisualStyleBackColor = false;
            this.btnCraps.Click += new System.EventHandler(this.btnCraps_Click);
            //
            // btnProductos
            //
            this.btnProductos.BackColor = System.Drawing.Color.FromArgb(24, 37, 61);
            this.btnProductos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnProductos.FlatAppearance.BorderSize = 0;
            this.btnProductos.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(45, 64, 102);
            this.btnProductos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnProductos.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.btnProductos.ForeColor = System.Drawing.Color.White;
            this.btnProductos.Location = new System.Drawing.Point(0, 206);
            this.btnProductos.Name = "btnProductos";
            this.btnProductos.Padding = new System.Windows.Forms.Padding(24, 0, 0, 0);
            this.btnProductos.Size = new System.Drawing.Size(240, 46);
            this.btnProductos.TabIndex = 7;
            this.btnProductos.Text = "Productos";
            this.btnProductos.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnProductos.UseVisualStyleBackColor = false;
            this.btnProductos.Click += new System.EventHandler(this.btnProductos_Click);
            //
            // btnUsuarios
            //
            this.btnUsuarios.BackColor = System.Drawing.Color.FromArgb(24, 37, 61);
            this.btnUsuarios.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUsuarios.FlatAppearance.BorderSize = 0;
            this.btnUsuarios.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(45, 64, 102);
            this.btnUsuarios.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUsuarios.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.btnUsuarios.ForeColor = System.Drawing.Color.White;
            this.btnUsuarios.Location = new System.Drawing.Point(0, 298);
            this.btnUsuarios.Name = "btnUsuarios";
            this.btnUsuarios.Padding = new System.Windows.Forms.Padding(24, 0, 0, 0);
            this.btnUsuarios.Size = new System.Drawing.Size(240, 46);
            this.btnUsuarios.TabIndex = 8;
            this.btnUsuarios.Text = "Usuarios y roles";
            this.btnUsuarios.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnUsuarios.UseVisualStyleBackColor = false;
            this.btnUsuarios.Visible = false;
            this.btnUsuarios.Click += new System.EventHandler(this.btnUsuarios_Click);
            //
            // lblFecha
            //
            this.lblFecha.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblFecha.ForeColor = System.Drawing.Color.LightSkyBlue;
            this.lblFecha.Location = new System.Drawing.Point(20, 104);
            this.lblFecha.Name = "lblFecha";
            this.lblFecha.Size = new System.Drawing.Size(205, 32);
            this.lblFecha.TabIndex = 2;
            this.lblFecha.Text = "fecha";
            //
            // lblHora
            //
            this.lblHora.AutoSize = true;
            this.lblHora.Font = new System.Drawing.Font("Segoe UI", 22F, System.Drawing.FontStyle.Bold);
            this.lblHora.ForeColor = System.Drawing.Color.White;
            this.lblHora.Location = new System.Drawing.Point(16, 60);
            this.lblHora.Name = "lblHora";
            this.lblHora.Size = new System.Drawing.Size(150, 41);
            this.lblHora.TabIndex = 1;
            this.lblHora.Text = "00:00:00";
            //
            // lblApp
            //
            this.lblApp.AutoSize = true;
            this.lblApp.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblApp.ForeColor = System.Drawing.Color.White;
            this.lblApp.Location = new System.Drawing.Point(18, 16);
            this.lblApp.Name = "lblApp";
            this.lblApp.Size = new System.Drawing.Size(90, 30);
            this.lblApp.TabIndex = 0;
            this.lblApp.Text = "Lab MVP";
            //
            // tmrReloj
            //
            this.tmrReloj.Enabled = true;
            this.tmrReloj.Interval = 1000;
            this.tmrReloj.Tick += new System.EventHandler(this.tmrReloj_Tick);
            //
            // FrmPrincipal
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 650);
            this.Controls.Add(this.pnlMenu);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.IsMdiContainer = true;
            this.Name = "FrmPrincipal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lab MVP - Principal";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.pnlMenu.ResumeLayout(false);
            this.pnlMenu.PerformLayout();
            this.pnlUsuario.ResumeLayout(false);
            this.pnlUsuario.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlMenu;
        private System.Windows.Forms.Label lblApp;
        private System.Windows.Forms.Label lblHora;
        private System.Windows.Forms.Label lblFecha;
        private System.Windows.Forms.Button btnCraps;
        private System.Windows.Forms.Button btnProductos;
        private System.Windows.Forms.Button btnUsuarios;
        private System.Windows.Forms.Button btnOrganizar;
        private System.Windows.Forms.Button btnCerrarSesion;
        private System.Windows.Forms.Panel pnlUsuario;
        private System.Windows.Forms.Label lblSesionTitulo;
        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.Label lblCorreo;
        private System.Windows.Forms.Timer tmrReloj;
    }
}