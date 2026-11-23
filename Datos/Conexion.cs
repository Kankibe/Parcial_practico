using MySql.Data.MySqlClient;

namespace Datos
{
    // Se encarga de crear la conexión con MySQL.
    // Hay 2 cuentas de MySQL:
    //   Ibe   : privilegios mínimos (solo ejecuta algunos procedimientos). Es la cuenta por defecto.
    //   Kanki : superusuario (todos los privilegios). La usa la app cuando entra un admin.

    public class Conexion
    {
        private const string Servidor = "localhost";
        private const string Puerto = "3306";
        private const string BaseDatos = "lab_mvp";

        private const string UsuarioMinimo = "Ibe";
        private const string ClaveMinimo = "Ibe_2026*";
        private const string UsuarioSuper = "Kanki";
        private const string ClaveSuper = "Kanki_2026*";

        // static: todas las clases de Datos comparten la misma cuenta mientras dura la sesión
        private static string usuario = UsuarioMinimo;
        private static string clave = ClaveMinimo;

        // Nombre de la cuenta de MySQL que se está usando ahora (para mostrarlo en pantalla)
        public static string CuentaActual
        {
            get { return usuario; }
        }

        // Cuenta con todos los privilegios (cuando inicia sesión un admin)
        public static void UsarSuperusuario()
        {
            usuario = UsuarioSuper;
            clave = ClaveSuper;
        }

        // Cuenta con privilegios mínimos (login, registro y cualquier usuario que no sea admin)
        public static void UsarMinimo()
        {
            usuario = UsuarioMinimo;
            clave = ClaveMinimo;
        }

        public MySqlConnection ObtenerConexion()
        {
            string cadena = "Server=" + Servidor +
                            ";Port=" + Puerto +
                            ";Database=" + BaseDatos +
                            ";Uid=" + usuario +
                            ";Pwd=" + clave + ";";

            return new MySqlConnection(cadena);
        }
    }
}