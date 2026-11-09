using Entidades;

namespace Negocio
{
    // Guarda quién inició sesión mientras la aplicación está abierta.
    public static class Sesion
    {
        public static E_Usuario UsuarioActual { get; set; }

        // Borra al usuario (cerrar sesión)
        public static void Cerrar()
        {
            UsuarioActual = null;
        }
    }
}