namespace Presentacion.Vistas
{
    partial class FrmUsuarios
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

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblAyuda = new System.Windows.Forms.Label();
            this.dgvUsuarios = new System.Windows.Forms.DataGridView();
            this.colId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCorreo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEstado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblRol = new System.Windows.Forms.Label();
            this.cmbRol = new System.Windows.Forms.ComboBox();
            this.btnGuardarRol = new System.Windows.Forms.Button();
            this.btnEstado = new System.Windows.Forms.Button();
            this.grpNuevo = new System.Windows.Forms.GroupBox();
            this.lblNombre = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.lblCorreo = new System.Windows.Forms.Label();
            this.txtCorreo = new System.Windows.Forms.TextBox();
            this.lblClave = new System.Windows.Forms.Label();
            this.txtClave = new System.Windows.Forms.TextBox();
            this.lblRolNuevo = new System.Windows.Forms.Label();
            this.cmbRolNuevo = new System.Windows.Forms.ComboBox();
            this.btnCrear = new System.Windows.Forms.Button();
            this.lblPermisos = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsuarios)).BeginInit();
            this.grpNuevo.SuspendLayout();
            this.SuspendLayout();
            //
            // lblTitulo
            //
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI Semibold", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.Location = new System.Drawing.Point(24, 18);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(220, 30);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Gestión de usuarios";
            //
            // lblAyuda
            //
            this.lblAyuda.Location = new System.Drawing.Point(26, 54);
            this.lblAyuda.Name = "lblAyuda";
            this.lblAyuda.Size = new System.Drawing.Size(710, 34);
            this.lblAyuda.TabIndex = 1;
            this.lblAyuda.Text = "Elige un usuario para cambiarle el rol o desactivar su cuenta. El cambio de rol se aplica la próxima vez que esa persona inicie sesión.";
            //
            // dgvUsuarios
            //
            this.dgvUsuarios.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.dgvUsuarios.BackgroundColor = System.Drawing.Color.White;
            this.dgvUsuarios.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUsuarios.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colId,
            this.colNombre,
            this.colCorreo,
            this.colRol,
            this.colEstado});
            this.dgvUsuarios.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvUsuarios.Location = new System.Drawing.Point(24, 92);
            this.dgvUsuarios.Name = "dgvUsuarios";
            this.dgvUsuarios.RowHeadersVisible = false;
            this.dgvUsuarios.Size = new System.Drawing.Size(712, 210);
            this.dgvUsuarios.TabIndex = 2;
            this.dgvUsuarios.SelectionChanged += new System.EventHandler(this.dgvUsuarios_SelectionChanged);
            //
            // colId
            //
            this.colId.FillWeight = 12F;
            this.colId.HeaderText = "Id";
            this.colId.Name = "colId";
            //
            // colNombre
            //
            this.colNombre.FillWeight = 80F;
            this.colNombre.HeaderText = "Nombre";
            this.colNombre.Name = "colNombre";
            //
            // colCorreo
            //
            this.colCorreo.FillWeight = 105F;
            this.colCorreo.HeaderText = "Correo";
            this.colCorreo.Name = "colCorreo";
            //
            // colRol
            //
            this.colRol.FillWeight = 55F;
            this.colRol.HeaderText = "Rol";
            this.colRol.Name = "colRol";
            //
            // colEstado
            //
            this.colEstado.FillWeight = 55F;
            this.colEstado.HeaderText = "Estado";
            this.colEstado.Name = "colEstado";
            //
            // lblRol
            //
            this.lblRol.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            this.lblRol.AutoSize = true;
            this.lblRol.Location = new System.Drawing.Point(26, 326);
            this.lblRol.Name = "lblRol";
            this.lblRol.Size = new System.Drawing.Size(27, 15);
            this.lblRol.TabIndex = 3;
            this.lblRol.Text = "Rol:";
            //
            // cmbRol
            //
            this.cmbRol.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            this.cmbRol.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRol.FormattingEnabled = true;
            this.cmbRol.Location = new System.Drawing.Point(64, 322);
            this.cmbRol.Name = "cmbRol";
            this.cmbRol.Size = new System.Drawing.Size(300, 23);
            this.cmbRol.TabIndex = 4;
            //
            // btnGuardarRol
            //
            this.btnGuardarRol.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            this.btnGuardarRol.Location = new System.Drawing.Point(384, 316);
            this.btnGuardarRol.Name = "btnGuardarRol";
            this.btnGuardarRol.Size = new System.Drawing.Size(120, 34);
            this.btnGuardarRol.TabIndex = 5;
            this.btnGuardarRol.Text = "Guardar rol";
            this.btnGuardarRol.UseVisualStyleBackColor = true;
            this.btnGuardarRol.Click += new System.EventHandler(this.btnGuardarRol_Click);
            //
            // btnEstado
            //
            this.btnEstado.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            this.btnEstado.Location = new System.Drawing.Point(516, 316);
            this.btnEstado.Name = "btnEstado";
            this.btnEstado.Size = new System.Drawing.Size(150, 34);
            this.btnEstado.TabIndex = 6;
            this.btnEstado.Text = "Desactivar cuenta";
            this.btnEstado.UseVisualStyleBackColor = true;
            this.btnEstado.Click += new System.EventHandler(this.btnEstado_Click);
            //
            // grpNuevo
            //
            this.grpNuevo.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            this.grpNuevo.Controls.Add(this.lblNombre);
            this.grpNuevo.Controls.Add(this.txtNombre);
            this.grpNuevo.Controls.Add(this.lblCorreo);
            this.grpNuevo.Controls.Add(this.txtCorreo);
            this.grpNuevo.Controls.Add(this.lblClave);
            this.grpNuevo.Controls.Add(this.txtClave);
            this.grpNuevo.Controls.Add(this.lblRolNuevo);
            this.grpNuevo.Controls.Add(this.cmbRolNuevo);
            this.grpNuevo.Controls.Add(this.btnCrear);
            this.grpNuevo.Location = new System.Drawing.Point(24, 368);
            this.grpNuevo.Name = "grpNuevo";
            this.grpNuevo.Size = new System.Drawing.Size(712, 150);
            this.grpNuevo.TabIndex = 7;
            this.grpNuevo.TabStop = false;
            this.grpNuevo.Text = "Crear usuario nuevo";
            //
            // lblNombre
            //
            this.lblNombre.AutoSize = true;
            this.lblNombre.Location = new System.Drawing.Point(16, 34);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(54, 15);
            this.lblNombre.TabIndex = 0;
            this.lblNombre.Text = "Nombre:";
            //
            // txtNombre
            //
            this.txtNombre.Location = new System.Drawing.Point(100, 30);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(260, 23);
            this.txtNombre.TabIndex = 1;
            //
            // lblCorreo
            //
            this.lblCorreo.AutoSize = true;
            this.lblCorreo.Location = new System.Drawing.Point(16, 68);
            this.lblCorreo.Name = "lblCorreo";
            this.lblCorreo.Size = new System.Drawing.Size(47, 15);
            this.lblCorreo.TabIndex = 2;
            this.lblCorreo.Text = "Correo:";
            //
            // txtCorreo
            //
            this.txtCorreo.Location = new System.Drawing.Point(100, 64);
            this.txtCorreo.Name = "txtCorreo";
            this.txtCorreo.Size = new System.Drawing.Size(260, 23);
            this.txtCorreo.TabIndex = 3;
            //
            // lblClave
            //
            this.lblClave.AutoSize = true;
            this.lblClave.Location = new System.Drawing.Point(16, 102);
            this.lblClave.Name = "lblClave";
            this.lblClave.Size = new System.Drawing.Size(73, 15);
            this.lblClave.TabIndex = 4;
            this.lblClave.Text = "Contraseña:";
            //
            // txtClave
            //
            this.txtClave.Location = new System.Drawing.Point(100, 98);
            this.txtClave.Name = "txtClave";
            this.txtClave.Size = new System.Drawing.Size(260, 23);
            this.txtClave.TabIndex = 5;
            this.txtClave.UseSystemPasswordChar = true;
            //
            // lblRolNuevo
            //
            this.lblRolNuevo.AutoSize = true;
            this.lblRolNuevo.Location = new System.Drawing.Point(390, 34);
            this.lblRolNuevo.Name = "lblRolNuevo";
            this.lblRolNuevo.Size = new System.Drawing.Size(27, 15);
            this.lblRolNuevo.TabIndex = 6;
            this.lblRolNuevo.Text = "Rol:";
            //
            // cmbRolNuevo
            //
            this.cmbRolNuevo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRolNuevo.FormattingEnabled = true;
            this.cmbRolNuevo.Location = new System.Drawing.Point(430, 30);
            this.cmbRolNuevo.Name = "cmbRolNuevo";
            this.cmbRolNuevo.Size = new System.Drawing.Size(260, 23);
            this.cmbRolNuevo.TabIndex = 7;
            //
            // btnCrear
            //
            this.btnCrear.Location = new System.Drawing.Point(430, 92);
            this.btnCrear.Name = "btnCrear";
            this.btnCrear.Size = new System.Drawing.Size(150, 34);
            this.btnCrear.TabIndex = 8;
            this.btnCrear.Text = "Crear usuario";
            this.btnCrear.UseVisualStyleBackColor = true;
            this.btnCrear.Click += new System.EventHandler(this.btnCrear_Click);
            //
            // lblPermisos
            //
            this.lblPermisos.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            this.lblPermisos.Location = new System.Drawing.Point(26, 530);
            this.lblPermisos.Name = "lblPermisos";
            this.lblPermisos.Size = new System.Drawing.Size(710, 80);
            this.lblPermisos.TabIndex = 8;
            this.lblPermisos.Text = "Lector: solo puede ver y buscar productos.\r\nEditor: puede ver, crear y modificar productos, pero no borrar.\r\nAdministrador: puede hacer todo y además gestionar los usuarios.\r\nUna cuenta desactivada no puede iniciar sesión, pero se conserva en la base de datos.";
            //
            // FrmUsuarios
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(760, 620);
            this.Controls.Add(this.lblPermisos);
            this.Controls.Add(this.grpNuevo);
            this.Controls.Add(this.btnEstado);
            this.Controls.Add(this.btnGuardarRol);
            this.Controls.Add(this.cmbRol);
            this.Controls.Add(this.lblRol);
            this.Controls.Add(this.dgvUsuarios);
            this.Controls.Add(this.lblAyuda);
            this.Controls.Add(this.lblTitulo);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "FrmUsuarios";
            this.Text = "Gestión de usuarios";
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsuarios)).EndInit();
            this.grpNuevo.ResumeLayout(false);
            this.grpNuevo.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblAyuda;
        private System.Windows.Forms.DataGridView dgvUsuarios;
        private System.Windows.Forms.DataGridViewTextBoxColumn colId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCorreo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRol;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEstado;
        private System.Windows.Forms.Label lblRol;
        private System.Windows.Forms.ComboBox cmbRol;
        private System.Windows.Forms.Button btnGuardarRol;
        private System.Windows.Forms.Button btnEstado;
        private System.Windows.Forms.GroupBox grpNuevo;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label lblCorreo;
        private System.Windows.Forms.TextBox txtCorreo;
        private System.Windows.Forms.Label lblClave;
        private System.Windows.Forms.TextBox txtClave;
        private System.Windows.Forms.Label lblRolNuevo;
        private System.Windows.Forms.ComboBox cmbRolNuevo;
        private System.Windows.Forms.Button btnCrear;
        private System.Windows.Forms.Label lblPermisos;
    }
}
