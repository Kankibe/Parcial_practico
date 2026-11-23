namespace Presentacion.Vistas
{
    partial class FrmCraps
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
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblReglas = new System.Windows.Forms.Label();
            this.lblDado1 = new System.Windows.Forms.Label();
            this.lblDado2 = new System.Windows.Forms.Label();
            this.lblSuma = new System.Windows.Forms.Label();
            this.lblPunto = new System.Windows.Forms.Label();
            this.btnTirar = new System.Windows.Forms.Button();
            this.btnNuevo = new System.Windows.Forms.Button();
            this.lblMensaje = new System.Windows.Forms.Label();
            this.lblMarcador = new System.Windows.Forms.Label();
            this.SuspendLayout();
            //
            // lblTitulo
            //
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.Location = new System.Drawing.Point(18, 14);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(165, 30);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Juego de Craps";
            //
            // lblReglas
            //
            this.lblReglas.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblReglas.ForeColor = System.Drawing.Color.DimGray;
            this.lblReglas.Location = new System.Drawing.Point(20, 50);
            this.lblReglas.Name = "lblReglas";
            this.lblReglas.Size = new System.Drawing.Size(420, 34);
            this.lblReglas.TabIndex = 1;
            this.lblReglas.Text = "Primer tiro: 7 u 11 ganas; 2, 3 o 12 pierdes; otro número es tu punto. Luego, si sacas tu punto ganas y si sale 7 pierdes.";
            //
            // lblDado1
            //
            this.lblDado1.BackColor = System.Drawing.Color.White;
            this.lblDado1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblDado1.Font = new System.Drawing.Font("Segoe UI", 44F, System.Drawing.FontStyle.Bold);
            this.lblDado1.Location = new System.Drawing.Point(80, 100);
            this.lblDado1.Name = "lblDado1";
            this.lblDado1.Size = new System.Drawing.Size(110, 110);
            this.lblDado1.TabIndex = 2;
            this.lblDado1.Text = "-";
            this.lblDado1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // lblDado2
            //
            this.lblDado2.BackColor = System.Drawing.Color.White;
            this.lblDado2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblDado2.Font = new System.Drawing.Font("Segoe UI", 44F, System.Drawing.FontStyle.Bold);
            this.lblDado2.Location = new System.Drawing.Point(270, 100);
            this.lblDado2.Name = "lblDado2";
            this.lblDado2.Size = new System.Drawing.Size(110, 110);
            this.lblDado2.TabIndex = 3;
            this.lblDado2.Text = "-";
            this.lblDado2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // lblSuma
            //
            this.lblSuma.Font = new System.Drawing.Font("Segoe UI", 13F);
            this.lblSuma.Location = new System.Drawing.Point(20, 228);
            this.lblSuma.Name = "lblSuma";
            this.lblSuma.Size = new System.Drawing.Size(420, 28);
            this.lblSuma.TabIndex = 4;
            this.lblSuma.Text = "Suma: -";
            this.lblSuma.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // lblPunto
            //
            this.lblPunto.Font = new System.Drawing.Font("Segoe UI", 13F);
            this.lblPunto.Location = new System.Drawing.Point(20, 258);
            this.lblPunto.Name = "lblPunto";
            this.lblPunto.Size = new System.Drawing.Size(420, 28);
            this.lblPunto.TabIndex = 5;
            this.lblPunto.Text = "Punto: -";
            this.lblPunto.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // btnTirar
            //
            this.btnTirar.BackColor = System.Drawing.Color.FromArgb(0, 120, 215);
            this.btnTirar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTirar.FlatAppearance.BorderSize = 0;
            this.btnTirar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTirar.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.btnTirar.ForeColor = System.Drawing.Color.White;
            this.btnTirar.Location = new System.Drawing.Point(60, 304);
            this.btnTirar.Name = "btnTirar";
            this.btnTirar.Size = new System.Drawing.Size(160, 42);
            this.btnTirar.TabIndex = 6;
            this.btnTirar.Text = "Tirar dados";
            this.btnTirar.UseVisualStyleBackColor = false;
            this.btnTirar.Click += new System.EventHandler(this.btnTirar_Click);
            //
            // btnNuevo
            //
            this.btnNuevo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNuevo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNuevo.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.btnNuevo.Location = new System.Drawing.Point(240, 304);
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(160, 42);
            this.btnNuevo.TabIndex = 7;
            this.btnNuevo.Text = "Nuevo juego";
            this.btnNuevo.UseVisualStyleBackColor = true;
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            //
            // lblMensaje
            //
            this.lblMensaje.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblMensaje.Location = new System.Drawing.Point(20, 360);
            this.lblMensaje.Name = "lblMensaje";
            this.lblMensaje.Size = new System.Drawing.Size(420, 40);
            this.lblMensaje.TabIndex = 8;
            this.lblMensaje.Text = "mensaje";
            this.lblMensaje.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // lblMarcador
            //
            this.lblMarcador.ForeColor = System.Drawing.Color.DimGray;
            this.lblMarcador.Location = new System.Drawing.Point(20, 404);
            this.lblMarcador.Name = "lblMarcador";
            this.lblMarcador.Size = new System.Drawing.Size(420, 22);
            this.lblMarcador.TabIndex = 9;
            this.lblMarcador.Text = "Ganadas: 0     Perdidas: 0";
            this.lblMarcador.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // FrmCraps
            //
            this.AcceptButton = this.btnTirar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(460, 440);
            this.Controls.Add(this.lblMarcador);
            this.Controls.Add(this.lblMensaje);
            this.Controls.Add(this.btnNuevo);
            this.Controls.Add(this.btnTirar);
            this.Controls.Add(this.lblPunto);
            this.Controls.Add(this.lblSuma);
            this.Controls.Add(this.lblDado2);
            this.Controls.Add(this.lblDado1);
            this.Controls.Add(this.lblReglas);
            this.Controls.Add(this.lblTitulo);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FrmCraps";
            this.Text = "Craps";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblReglas;
        private System.Windows.Forms.Label lblDado1;
        private System.Windows.Forms.Label lblDado2;
        private System.Windows.Forms.Label lblSuma;
        private System.Windows.Forms.Label lblPunto;
        private System.Windows.Forms.Button btnTirar;
        private System.Windows.Forms.Button btnNuevo;
        private System.Windows.Forms.Label lblMensaje;
        private System.Windows.Forms.Label lblMarcador;
    }
}