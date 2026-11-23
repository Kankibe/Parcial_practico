using System;
using System.Collections.Generic;
using Entidades;

namespace Presentacion.Interfaces
{
    // Contrato de la VISTA del CRUD de productos.
    public interface IProductosView
    {
        // Avisos que la vista le manda al Presentador
        event EventHandler VistaCargada;         
        event EventHandler GuardarClick;
        event EventHandler ModificarClick;
        event EventHandler BorrarClick;
        event EventHandler LimpiarClick;
        event EventHandler BuscarCambio;          
        event EventHandler ProductoSeleccionado;  

        // Datos que escribió el usuario (texto tal cual, sin validar)
        string Nombre { get; }
        string Precio { get; }
        string Cantidad { get; }
        string Filtro { get; }

        // Foto elegida (null si el usuario no puso ninguna)
        byte[] Imagen { get; }

        // Id de la fila seleccionada en el grid (0 si no hay ninguna)
        int IdSeleccionado { get; }

        // Órdenes que el Presentador le da a la vista
        void MostrarProductos(IList<E_Producto> productos);
        void MostrarProducto(E_Producto producto);
        void MostrarErrores(string errorNombre, string errorPrecio, string errorCantidad);
        void MostrarMensaje(string mensaje, bool esError);
        bool Confirmar(string pregunta);
        void LimpiarCampos();

        // Activa o bloquea los botones según lo que permite el rol del usuario
        void AplicarPermisos(bool puedeCrear, bool puedeModificar, bool puedeBorrar);
    }
}
