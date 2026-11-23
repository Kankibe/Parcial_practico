using System;
using System.Globalization;
using Entidades;
using Negocio;
using Presentacion.Interfaces;

namespace Presentacion.Presentadores
{
    // Presentador de la ventana principal.
    // Se encarga de: mostrar la hora, mostrar quién inició sesión,
    // abrir el Craps y los Productos dentro del MDI y cerrar la sesión.
    public class PrincipalPresenter
    {
        private readonly IPrincipalView vista;
        private readonly CultureInfo cultura = new CultureInfo("es-PA");

        public PrincipalPresenter(IPrincipalView vista)
        {
            this.vista = vista;

            this.vista.CrapsClick += OnCraps;
            this.vista.ProductosClick += OnProductos;
            this.vista.UsuariosClick += OnUsuarios;
            this.vista.OrganizarClick += OnOrganizar;
            this.vista.CerrarSesionClick += OnCerrarSesion;
            this.vista.RelojTick += OnRelojTick;

            // El botón de gestión de usuarios solo lo ve el admin
            this.vista.MostrarBotonUsuarios(Sesion.EsAdmin);

            if (Sesion.UsuarioActual != null)
            {
                // Se le agrega el rol al nombre: (Administrador), (Editor) o (Lector)
                string rol = " (" + Roles.Nombre(Sesion.UsuarioActual.Rol) + ")";
                this.vista.MostrarUsuario(Sesion.UsuarioActual.Nombre + rol, "BD: " + Sesion.CuentaBD + " | " + Sesion.UsuarioActual.Correo);
            }
            else
            {
                this.vista.MostrarUsuario("Sin sesión", "");
            }


            ActualizarHora();
        }

        private void OnRelojTick(object sender, EventArgs e)
        {
            ActualizarHora();
        }

        private void ActualizarHora()
        {
            DateTime ahora = DateTime.Now;
            string hora = ahora.ToString("HH:mm:ss");
            string fecha = ahora.ToString("dddd, d 'de' MMMM 'de' yyyy", cultura);
            vista.MostrarHora(hora, fecha);
        }

        private void OnCraps(object sender, EventArgs e)
        {
            vista.AbrirCraps();
        }

        private void OnProductos(object sender, EventArgs e)
        {
            vista.AbrirProductos();
        }

        private void OnUsuarios(object sender, EventArgs e)
        {
            // Segunda barrera: aunque el botón se viera, solo el admin entra
            if (Sesion.EsAdmin)
            {
                vista.AbrirUsuarios();
            }
        }

        private void OnOrganizar(object sender, EventArgs e)
        {
            vista.OrganizarVentanas();
        }

        private void OnCerrarSesion(object sender, EventArgs e)
        {
            // Borramos la sesión y cerramos la ventana; el login vuelve a aparecer
            Sesion.Cerrar();
            vista.CerrarVentana();
        }
    }
}