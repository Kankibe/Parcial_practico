using System;
using System.Windows.Forms;
using Presentacion.Interfaces;
using Presentacion.Presentadores;

namespace Presentacion.Vistas
{
    // Ventana principal: es el contenedor MDI.
    public partial class FrmPrincipal : Form, IPrincipalView
    {
        private PrincipalPresenter presentador;

        // Para alternar entre cascada y mosaico al organizar las ventanas
        private bool enCascada = true;

        // Eventos que escucha el Presentador
        public event EventHandler CrapsClick;
        public event EventHandler ProductosClick;
        public event EventHandler UsuariosClick;
        public event EventHandler OrganizarClick;
        public event EventHandler CerrarSesionClick;
        public event EventHandler RelojTick;

        public FrmPrincipal()
        {
            InitializeComponent();

            // Conectamos esta vista con su presentador
            presentador = new PrincipalPresenter(this);
        }

        public void MostrarHora(string hora, string fecha)
        {
            lblHora.Text = hora;
            lblFecha.Text = fecha;
        }

        public void MostrarUsuario(string nombre, string correo)
        {
            lblUsuario.Text = nombre;
            lblCorreo.Text = correo;
        }

        // Abre una ventana de Craps DENTRO de esta ventana (MDI)
        public void AbrirCraps()
        {
            FrmCraps craps = new FrmCraps();
            craps.MdiParent = this;
            craps.Show();
        }

        // Abre el CRUD de productos DENTRO de esta ventana (MDI).
        // Si ya está abierto no crea otro: solo lo trae al frente.
        public void AbrirProductos()
        {
            foreach (Form hijo in MdiChildren)
            {
                if (hijo is FrmProductos)
                {
                    hijo.Activate();
                    return;
                }
            }

            FrmProductos productos = new FrmProductos();
            productos.MdiParent = this;
            productos.Show();
        }

        // Abre la gestión de usuarios DENTRO de esta ventana (MDI), igual que Productos
        public void AbrirUsuarios()
        {
            foreach (Form hijo in MdiChildren)
            {
                if (hijo is FrmUsuarios)
                {
                    hijo.Activate();
                    return;
                }
            }

            FrmUsuarios usuarios = new FrmUsuarios();
            usuarios.MdiParent = this;
            usuarios.Show();
        }

        // Muestra u oculta el botón de usuarios; si se oculta, "Cerrar sesión" sube a su lugar
        public void MostrarBotonUsuarios(bool visible)
        {
            btnUsuarios.Visible = visible;
            btnCerrarSesion.Top = visible ? btnUsuarios.Bottom : btnUsuarios.Top;
        }

        // Acomoda las ventanas hijas: una vez en cascada, la siguiente en mosaico
        public void OrganizarVentanas()
        {
            if (enCascada)
            {
                LayoutMdi(MdiLayout.Cascade);
            }
            else
            {
                LayoutMdi(MdiLayout.TileVertical);
            }

            enCascada = !enCascada;
        }

        public void CerrarVentana()
        {
            Close();
        }

        private void btnCraps_Click(object sender, EventArgs e)
        {
            if (CrapsClick != null)
            {
                CrapsClick(this, EventArgs.Empty);
            }
        }

        private void btnProductos_Click(object sender, EventArgs e)
        {
            if (ProductosClick != null)
            {
                ProductosClick(this, EventArgs.Empty);
            }
        }

        private void btnUsuarios_Click(object sender, EventArgs e)
        {
            if (UsuariosClick != null)
            {
                UsuariosClick(this, EventArgs.Empty);
            }
        }

        private void btnOrganizar_Click(object sender, EventArgs e)
        {
            if (OrganizarClick != null)
            {
                OrganizarClick(this, EventArgs.Empty);
            }
        }

        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            if (CerrarSesionClick != null)
            {
                CerrarSesionClick(this, EventArgs.Empty);
            }
        }

        private void tmrReloj_Tick(object sender, EventArgs e)
        {
            if (RelojTick != null)
            {
                RelojTick(this, EventArgs.Empty);
            }
        }
    }
}