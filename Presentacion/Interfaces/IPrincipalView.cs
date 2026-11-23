using System;

namespace Presentacion.Interfaces
{
    // Contrato de la VISTA de la ventana principal
    public interface IPrincipalView
    {
        // Avisos que la vista le manda al Presentador
        event EventHandler CrapsClick;
        event EventHandler ProductosClick;
        event EventHandler UsuariosClick;
        event EventHandler OrganizarClick;
        event EventHandler CerrarSesionClick;
        event EventHandler RelojTick;      // el Timer avisa cada segundo

        // Órdenes que el Presentador le da a la vista
        void MostrarHora(string hora, string fecha);
        void MostrarUsuario(string nombre, string correo);
        void AbrirCraps();
        void AbrirProductos();
        void AbrirUsuarios();
        void MostrarBotonUsuarios(bool visible);   // solo el admin ve el botón
        void OrganizarVentanas();
        void CerrarVentana();
    }
}