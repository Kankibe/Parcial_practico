namespace Entidades
{
    // Representa a un producto de la tabla "productos" de la base de datos.
    public class E_Producto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }

        // La foto se guarda en la base de datos como bytes (PNG). Puede ser null.
        public byte[] Imagen { get; set; }
    }
}
