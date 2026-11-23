using Datos;
using Entidades;

namespace Negocio
{
    // Guarda quién inició sesión mientras la aplicación está abierta.
    public static class Sesion
    {
        public static E_Usuario UsuarioActual { get; set; }

        public static bool EsAdmin
        {
            get { return UsuarioActual != null && UsuarioActual.EsAdmin; }
        }

        public static bool PuedeCrear
        {
            get { return UsuarioActual != null && UsuarioActual.PuedeCrear; }
        }

        public static bool PuedeModificar
        {
            get { return UsuarioActual != null && UsuarioActual.PuedeModificar; }
        }

        public static bool PuedeBorrar
        {
            get { return UsuarioActual != null && UsuarioActual.PuedeBorrar; }
        }

        public static string CuentaBD
        {
            get { return Conexion.CuentaActual; }
        }

        // Borra al usuario (cerrar sesión) y vuelve a la cuenta de privilegios mínimos
        public static void Cerrar()
        {
            UsuarioActual = null;
            Conexion.UsarMinimo();
        }
    }
}