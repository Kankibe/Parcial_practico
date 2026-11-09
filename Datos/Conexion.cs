using MySql.Data.MySqlClient;

namespace Datos
{
    // Se encarga de crear la conexión con MySQL.

    public class Conexion
    {
        private const string Servidor = "localhost";
        private const string Puerto = "3306";
        private const string BaseDatos = "lab_mvp";
        private const string Usuario = "app_lab";
        private const string Clave = "Lab_2026*";


        public MySqlConnection ObtenerConexion()
        {
            string cadena = "Server=" + Servidor +
                            ";Port=" + Puerto +
                            ";Database=" + BaseDatos +
                            ";Uid=" + Usuario +
                            ";Pwd=" + Clave + ";";

            return new MySqlConnection(cadena);
        }
    }
}