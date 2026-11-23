using System;
using System.Collections.Generic;
using Entidades;

namespace Presentacion.Interfaces
{
    // Desde aquí el admin crea usuarios, les cambia el rol y activa o desactiva cuentas.
    public interface IUsuariosView
    {
        // Avisos que la vista le manda al Presentador
        event EventHandler VistaCargada;        
        event EventHandler GuardarRolClick;
        event EventHandler CambiarEstadoClick;   
        event EventHandler CrearClick;           
        event EventHandler UsuarioSeleccionado;  

        int IdSeleccionado { get; }
        string RolElegido { get; }

        // Datos del formulario "Crear usuario nuevo" (texto tal cual, sin validar)
        string NombreNuevo { get; }
        string CorreoNuevo { get; }
        string ClaveNueva { get; }
        string RolNuevo { get; }

        // Órdenes que el Presentador le da a la vista
        void MostrarUsuarios(IList<E_Usuario> usuarios);
        void MostrarSeleccion(string rol, bool activo); 
        void LimpiarFormularioNuevo();
        void MostrarMensaje(string mensaje, bool esError);
        bool Confirmar(string pregunta);
    }
}
