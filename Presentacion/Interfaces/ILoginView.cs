using System;

namespace Presentacion.Interfaces
{
    // Contrato de la VISTA del login.

    public interface ILoginView
    {
        // Datos que escribió el usuario
        string Nombre { get; }
        string Correo { get; }
        string Clave { get; }

        // Avisos que la vista le manda al Presentador cuando se pulsa un botón
        event EventHandler IngresarClick;
        event EventHandler RegistrarClick;

        // Órdenes que el Presentador le da a la vista
        void MostrarMensaje(string mensaje, bool esError);
        void LimpiarClave();
        void AbrirPrincipal();
    }
}