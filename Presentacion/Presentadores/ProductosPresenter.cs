using System;
using System.Collections.Generic;
using Entidades;
using Negocio;
using Presentacion.Interfaces;

namespace Presentacion.Presentadores
{
    // Presentador del CRUD de productos: puente entre FrmProductos y N_Producto.
    // Recibe las acciones de la vista, le pide el trabajo al Negocio y le dice a la vista qué mostrar.
    public class ProductosPresenter
    {
        private readonly IProductosView vista;
        private readonly N_Producto negocio;

        // Productos que se están mostrando en el grid
        private List<E_Producto> productos = new List<E_Producto>();

        // Producto elegido en el grid 
        private int idActual = 0;

        // Constructor normal: usa el N_Producto real
        public ProductosPresenter(IProductosView vista) : this(vista, new N_Producto())
        {
        }

        public ProductosPresenter(IProductosView vista, N_Producto negocio)
        {
            this.vista = vista;
            this.negocio = negocio;

            this.vista.VistaCargada += OnVistaCargada;
            this.vista.GuardarClick += OnGuardar;
            this.vista.ModificarClick += OnModificar;
            this.vista.BorrarClick += OnBorrar;
            this.vista.LimpiarClick += OnLimpiar;
            this.vista.BuscarCambio += OnBuscar;
            this.vista.ProductoSeleccionado += OnProductoSeleccionado;
        }

        private void OnVistaCargada(object sender, EventArgs e)
        {
            // Solo quedan activos los botones que el rol del usuario permite
            vista.AplicarPermisos(Sesion.PuedeCrear, Sesion.PuedeModificar, Sesion.PuedeBorrar);

            CargarLista();
        }

        private void OnBuscar(object sender, EventArgs e)
        {
            CargarLista();
        }

        // Al elegir una fila, se llenan los campos con ese producto
        private void OnProductoSeleccionado(object sender, EventArgs e)
        {
            int id = vista.IdSeleccionado;
            if (id == 0)
            {
                return;
            }

            E_Producto producto = productos.Find(p => p.Id == id);
            if (producto != null)
            {
                idActual = id;
                vista.MostrarProducto(producto);
            }
        }

        private void OnGuardar(object sender, EventArgs e)
        {
            if (!CamposCorrectos())
            {
                return;
            }

            string mensaje;
            bool correcto = negocio.Guardar(vista.Nombre, vista.Precio, vista.Cantidad, vista.Imagen, out mensaje);

            vista.MostrarMensaje(mensaje, !correcto);

            if (correcto)
            {
                Limpiar();
                CargarLista();
            }
        }

        private void OnModificar(object sender, EventArgs e)
        {
            if (idActual == 0)
            {
                vista.MostrarMensaje("Selecciona un producto del grid antes de modificar.", true);
                return;
            }

            if (!CamposCorrectos())
            {
                return;
            }

            string mensaje;
            bool correcto = negocio.Modificar(idActual, vista.Nombre, vista.Precio, vista.Cantidad, vista.Imagen, out mensaje);

            vista.MostrarMensaje(mensaje, !correcto);

            if (correcto)
            {
                Limpiar();
                CargarLista();
            }
        }

        private void OnBorrar(object sender, EventArgs e)
        {
            if (idActual == 0)
            {
                vista.MostrarMensaje("Selecciona un producto del grid antes de borrar.", true);
                return;
            }

            if (!vista.Confirmar("¿Seguro que deseas borrar este producto?"))
            {
                return;
            }

            string mensaje;
            bool correcto = negocio.Borrar(idActual, out mensaje);

            vista.MostrarMensaje(mensaje, !correcto);

            if (correcto)
            {
                Limpiar();
                CargarLista();
            }
        }

        private void OnLimpiar(object sender, EventArgs e)
        {
            Limpiar();
        }

        // Pide validar al Negocio y marca en la vista los campos con error
        private bool CamposCorrectos()
        {
            ErroresProducto errores = negocio.Validar(vista.Nombre, vista.Precio, vista.Cantidad);
            vista.MostrarErrores(errores.Nombre, errores.Precio, errores.Cantidad);
            return !errores.HayErrores;
        }

        private void Limpiar()
        {
            idActual = 0;
            vista.LimpiarCampos();
        }

        // Trae los productos (según el filtro de búsqueda) y los manda al grid
        private void CargarLista()
        {
            string mensaje;
            productos = negocio.Listar(vista.Filtro, out mensaje);
            vista.MostrarProductos(productos);

            if (mensaje != "")
            {
                vista.MostrarMensaje(mensaje, true);
            }
        }
    }
}
