using System;
using System.Collections.Generic;
using Datos;
using Entidades;
using Negocio.Validaciones;

namespace Negocio
{
    // Lógica de negocio de los productos: validaciones y CRUD.
    // La vista manda texto tal cual lo escribió el usuario; aquí se valida y se convierte.
    public class N_Producto
    {
        private readonly D_Producto datos = new D_Producto();

        private readonly IValidadorCampo validadorNombre = new ValidadorTexto(100);
        private readonly IValidadorCampo validadorPrecio = new ValidadorDecimal();
        private readonly IValidadorCampo validadorCantidad = new ValidadorEntero();

        // Revisa los tres campos y devuelve el error de cada uno (vacío si está bien).
        public ErroresProducto Validar(string nombre, string precio, string cantidad)
        {
            ErroresProducto errores = new ErroresProducto();

            if (!validadorNombre.EsValido(nombre))
            {
                errores.Nombre = validadorNombre.MensajeError;
            }
            if (!validadorPrecio.EsValido(precio))
            {
                errores.Precio = validadorPrecio.MensajeError;
            }
            if (!validadorCantidad.EsValido(cantidad))
            {
                errores.Cantidad = validadorCantidad.MensajeError;
            }

            return errores;
        }

        // Lista los productos
        // Si falla la base de datos devuelve una lista vacía y deja el motivo en "mensaje".
        public List<E_Producto> Listar(string filtro, out string mensaje)
        {
            try
            {
                mensaje = "";
                return datos.Listar(filtro == null ? "" : filtro.Trim());
            }
            catch (Exception ex)
            {
                mensaje = "No se pudieron cargar los productos: " + ex.Message;
                return new List<E_Producto>();
            }
        }

        // Valida y guarda un producto nuevo.
        public bool Guardar(string nombre, string precio, string cantidad, byte[] imagen, out string mensaje)
        {
            // Según el rol: lector no escribe, editor crea y modifica, admin hace todo
            if (!Sesion.PuedeCrear)
            {
                mensaje = "Tu rol no permite crear productos.";
                return false;
            }

            E_Producto producto = Armar(0, nombre, precio, cantidad, imagen, out mensaje);
            if (producto == null)
            {
                return false;
            }

            try
            {
                datos.Insertar(producto);
                mensaje = "Se ha guardado satisfactoriamente el registro.";
                return true;
            }
            catch (Exception ex)
            {
                mensaje = "No se pudo guardar el producto: " + ex.Message;
                return false;
            }
        }

        // Valida y actualiza un producto que ya existe.
        public bool Modificar(int id, string nombre, string precio, string cantidad, byte[] imagen, out string mensaje)
        {
            if (!Sesion.PuedeModificar)
            {
                mensaje = "Tu rol no permite modificar productos.";
                return false;
            }

            if (id <= 0)
            {
                mensaje = "Selecciona un producto del grid antes de modificar.";
                return false;
            }

            E_Producto producto = Armar(id, nombre, precio, cantidad, imagen, out mensaje);
            if (producto == null)
            {
                return false;
            }

            try
            {
                datos.Actualizar(producto);
                mensaje = "Producto actualizado correctamente.";
                return true;
            }
            catch (Exception ex)
            {
                mensaje = "No se pudo actualizar el producto: " + ex.Message;
                return false;
            }
        }

        // Borra un producto por su id.
        public bool Borrar(int id, out string mensaje)
        {
            if (!Sesion.PuedeBorrar)
            {
                mensaje = "Solo el administrador puede borrar productos.";
                return false;
            }

            if (id <= 0)
            {
                mensaje = "Selecciona un producto del grid antes de borrar.";
                return false;
            }

            try
            {
                if (!datos.Eliminar(id))
                {
                    mensaje = "El producto ya no existe.";
                    return false;
                }

                mensaje = "Producto borrado correctamente.";
                return true;
            }
            catch (Exception ex)
            {
                mensaje = "No se pudo borrar el producto: " + ex.Message;
                return false;
            }
        }

        // Valida los textos y los convierte en un E_Producto.
        // Devuelve null (y el motivo en "mensaje") si algún campo no es válido.
        private E_Producto Armar(int id, string nombre, string precio, string cantidad, byte[] imagen, out string mensaje)
        {
            if (Validar(nombre, precio, cantidad).HayErrores)
            {
                mensaje = "Revisa los campos marcados.";
                return null;
            }

            decimal precioNumero;
            int cantidadNumero;
            ValidadorDecimal.TryParsear(precio, out precioNumero);
            ValidadorEntero.TryParsear(cantidad, out cantidadNumero);

            E_Producto producto = new E_Producto();
            producto.Id = id;
            producto.Nombre = nombre.Trim();
            producto.Precio = Math.Round(precioNumero, 2);
            producto.Cantidad = cantidadNumero;
            producto.Imagen = imagen;

            mensaje = "";
            return producto;
        }
    }
}
