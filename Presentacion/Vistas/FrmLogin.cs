using System;
using System.Drawing;
using System.Windows.Forms;
using Negocio;
using Presentacion.Interfaces;
using Presentacion.Presentadores;

namespace Presentacion.Vistas
{
    // Vista del login. Solo muestra controles y le avisa al Presentador.
    // No sabe nada de hashes ni de la base de datos.
    public partial class FrmLogin : Form, ILoginView
    {
        private LoginPresenter presentador;

        // Eventos que escucha el Presentador
        public event EventHandler IngresarClick;
        public event EventHandler RegistrarClick;

        public FrmLogin()
        {
            InitializeComponent();

            // Conectamos esta vista con su presentador
            presentador = new LoginPresenter(this);
        }

        // ----- Datos que lee el Presentador -----
        public string Nombre
        {
            get { return txtNombre.Text; }
        }

        public string Correo
        {
            get { return txtCorreo.Text; }
        }

        public string Clave
        {
            get { return txtClave.Text; }
        }

        // ----- Órdenes que da el Presentador -----
        public void MostrarMensaje(string mensaje, bool esError)
        {
            lblMensaje.Text = mensaje;
            lblMensaje.ForeColor = esError ? Color.Firebrick : Color.SeaGreen;
        }

        public void LimpiarClave()
        {
            txtClave.Clear();
            txtClave.Focus();
        }

        public void AbrirPrincipal()
        {
            FrmPrincipal principal = new FrmPrincipal();

            // Cuando se cierra la ventana principal hay dos casos:
            principal.FormClosed += (s, e) =>
            {
                if (Sesion.UsuarioActual == null)
                {
                    // Cerró sesión: volvemos a mostrar el login limpio
                    txtCorreo.Clear();
                    txtClave.Clear();
                    txtNombre.Clear();
                    lblMensaje.Text = "";
                    Show();
                }
                else
                {
                    // Cerró la ventana: termina la aplicación
                    Close();
                }
            };

            Hide();
            principal.Show();
        }

        // ----- Clics de los botones: solo avisan al Presentador -----
        private void btnIngresar_Click(object sender, EventArgs e)
        {
            if (IngresarClick != null)
            {
                IngresarClick(this, EventArgs.Empty);
            }
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            if (RegistrarClick != null)
            {
                RegistrarClick(this, EventArgs.Empty);
            }
        }
    }
}