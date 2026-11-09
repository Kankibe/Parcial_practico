using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using Entidades;

namespace Datos
{
    // Acceso a datos de los productos.
    public class D_Producto
    {
        private Conexion conexion = new Conexion();

        // Trae los productos con sp_listar_productos.
        // Si el filtro está vacío trae todos; si no, busca en id, nombre, precio y cantidad.
        public List<E_Producto> Listar(string filtro)
        {
            List<E_Producto> lista = new List<E_Producto>();

            using (MySqlConnection con = conexion.ObtenerConexion())
            {
                con.Open();

                using (MySqlCommand cmd = new MySqlCommand("sp_listar_productos", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_filtro", filtro ?? "");

                    using (MySqlDataReader lector = cmd.ExecuteReader())
                    {
                        while (lector.Read())
                        {
                            E_Producto producto = new E_Producto();
                            producto.Id = lector.GetInt32("id");
                            producto.Nombre = lector.GetString("nombre");
                            producto.Precio = lector.GetDecimal("precio");
                            producto.Cantidad = lector.GetInt32("cantidad");

                            // La imagen puede ser NULL en la base de datos
                            int posImagen = lector.GetOrdinal("imagen");
                            producto.Imagen = lector.IsDBNull(posImagen) ? null : (byte[])lector.GetValue(posImagen);

                            lista.Add(producto);
                        }
                    }
                }
            }

            return lista;
        }

        // Guarda un producto nuevo 
        public void Insertar(E_Producto producto)
        {
            using (MySqlConnection con = conexion.ObtenerConexion())
            {
                con.Open();

                using (MySqlCommand cmd = new MySqlCommand("sp_insertar_producto", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_nombre", producto.Nombre);
                    cmd.Parameters.AddWithValue("p_precio", producto.Precio);
                    cmd.Parameters.AddWithValue("p_cantidad", producto.Cantidad);
                    AgregarImagen(cmd, producto.Imagen);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Actualiza un producto existente 
        public void Actualizar(E_Producto producto)
        {
            using (MySqlConnection con = conexion.ObtenerConexion())
            {
                con.Open();

                using (MySqlCommand cmd = new MySqlCommand("sp_actualizar_producto", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_id", producto.Id);
                    cmd.Parameters.AddWithValue("p_nombre", producto.Nombre);
                    cmd.Parameters.AddWithValue("p_precio", producto.Precio);
                    cmd.Parameters.AddWithValue("p_cantidad", producto.Cantidad);
                    AgregarImagen(cmd, producto.Imagen);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Borra un producto
        public bool Eliminar(int id)
        {
            using (MySqlConnection con = conexion.ObtenerConexion())
            {
                con.Open();

                using (MySqlCommand cmd = new MySqlCommand("sp_eliminar_producto", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_id", id);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        // La imagen va como LONGBLOB; si no hay foto se manda NULL
        private void AgregarImagen(MySqlCommand cmd, byte[] imagen)
        {
            MySqlParameter parametro = cmd.Parameters.Add("p_imagen", MySqlDbType.LongBlob);

            if (imagen == null || imagen.Length == 0)
            {
                parametro.Value = System.DBNull.Value;
            }
            else
            {
                parametro.Value = imagen;
            }
        }
    }
}
