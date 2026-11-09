using System.Data;
using MySql.Data.MySqlClient;
using Entidades;

namespace Datos
{
    // Acceso a datos de los usuarios
    public class D_Usuario
    {
        private Conexion conexion = new Conexion();

        // Busca un usuario por su correo con el procedimiento sp_obtener_usuario.
        // Devuelve null si el correo no existe.
        public E_Usuario ObtenerPorCorreo(string correo)
        {
            E_Usuario usuario = null;

            using (MySqlConnection con = conexion.ObtenerConexion())
            {
                con.Open();

                using (MySqlCommand cmd = new MySqlCommand("sp_obtener_usuario", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_correo", correo);

                    using (MySqlDataReader lector = cmd.ExecuteReader())
                    {
                        // Si encontró una fila, la convertimos en un E_Usuario
                        if (lector.Read())
                        {
                            usuario = new E_Usuario();
                            usuario.Id = lector.GetInt32("id");
                            usuario.Nombre = lector.GetString("nombre");
                            usuario.Correo = lector.GetString("correo");
                            usuario.Salt = lector.GetString("salt");
                            usuario.PasswordHash = lector.GetString("password_hash");
                        }
                    }
                }
            }

            return usuario;
        }

        // Guarda un usuario nuevo con el procedimiento sp_insertar_usuario.
        public void Insertar(E_Usuario usuario)
        {
            using (MySqlConnection con = conexion.ObtenerConexion())
            {
                con.Open();

                using (MySqlCommand cmd = new MySqlCommand("sp_insertar_usuario", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_nombre", usuario.Nombre);
                    cmd.Parameters.AddWithValue("p_correo", usuario.Correo);
                    cmd.Parameters.AddWithValue("p_salt", usuario.Salt);
                    cmd.Parameters.AddWithValue("p_password_hash", usuario.PasswordHash);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}