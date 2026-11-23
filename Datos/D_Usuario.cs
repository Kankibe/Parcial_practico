using System.Collections.Generic;
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
                            usuario.Rol = lector.GetString("rol");   // "admin", "editor" o "lector"
                            usuario.Activo = System.Convert.ToInt32(lector["activo"]) != 0;   // 0 = desactivada
                        }
                    }
                }
            }

            return usuario;
        }

        // Trae todos los usuarios con sp_listar_usuarios (sin salt ni contraseña).
        public List<E_Usuario> Listar()
        {
            List<E_Usuario> lista = new List<E_Usuario>();

            using (MySqlConnection con = conexion.ObtenerConexion())
            {
                con.Open();

                using (MySqlCommand cmd = new MySqlCommand("sp_listar_usuarios", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (MySqlDataReader lector = cmd.ExecuteReader())
                    {
                        while (lector.Read())
                        {
                            E_Usuario u = new E_Usuario();
                            u.Id = lector.GetInt32("id");
                            u.Nombre = lector.GetString("nombre");
                            u.Correo = lector.GetString("correo");
                            u.Rol = lector.GetString("rol");
                            u.Activo = System.Convert.ToInt32(lector["activo"]) != 0;
                            lista.Add(u);
                        }
                    }
                }
            }

            return lista;
        }

        // Cambia el rol de un usuario con sp_cambiar_rol_usuario.
        // El procedimiento lanza un error si se intenta dejar la base sin ningún admin.
        public void CambiarRol(int id, string rol)
        {
            using (MySqlConnection con = conexion.ObtenerConexion())
            {
                con.Open();

                using (MySqlCommand cmd = new MySqlCommand("sp_cambiar_rol_usuario", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_id", id);
                    cmd.Parameters.AddWithValue("p_rol", rol);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Guarda un usuario nuevo con el rol indicado (sp_insertar_usuario_con_rol).
        public void InsertarConRol(E_Usuario usuario)
        {
            using (MySqlConnection con = conexion.ObtenerConexion())
            {
                con.Open();

                using (MySqlCommand cmd = new MySqlCommand("sp_insertar_usuario_con_rol", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_nombre", usuario.Nombre);
                    cmd.Parameters.AddWithValue("p_correo", usuario.Correo);
                    cmd.Parameters.AddWithValue("p_salt", usuario.Salt);
                    cmd.Parameters.AddWithValue("p_password_hash", usuario.PasswordHash);
                    cmd.Parameters.AddWithValue("p_rol", usuario.Rol);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Activa o desactiva una cuenta (sp_cambiar_estado_usuario).
        // El procedimiento lanza un error si se intenta desactivar al último admin activo.
        public void CambiarEstado(int id, bool activo)
        {
            using (MySqlConnection con = conexion.ObtenerConexion())
            {
                con.Open();

                using (MySqlCommand cmd = new MySqlCommand("sp_cambiar_estado_usuario", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_id", id);
                    cmd.Parameters.AddWithValue("p_activo", activo ? 1 : 0);

                    cmd.ExecuteNonQuery();
                }
            }
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