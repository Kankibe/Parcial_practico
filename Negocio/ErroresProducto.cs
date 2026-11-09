namespace Negocio
{
    // Resultado de validar el formulario de productos.
    // Cada propiedad trae el mensaje de error de su campo (vacío = campo correcto).
    public class ErroresProducto
    {
        public string Nombre { get; set; }
        public string Precio { get; set; }
        public string Cantidad { get; set; }

        public ErroresProducto()
        {
            Nombre = string.Empty;
            Precio = string.Empty;
            Cantidad = string.Empty;
        }

        public bool HayErrores
        {
            get
            {
                return Nombre != string.Empty || Precio != string.Empty || Cantidad != string.Empty;
            }
        }
    }
}
