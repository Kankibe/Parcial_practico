namespace Entidades
{
    // Representa a un usuario de la tabla "usuarios" de la base de datos.
    public class E_Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }

        // El salt es un texto extra que se mezcla con la contraseña antes de hacer el hash
        public string Salt { get; set; }

        public string PasswordHash { get; set; }

        // Rol del usuario en la base de datos: "lector", "editor" o "admin" (ver clase Roles)
        public string Rol { get; set; }

        // false = cuenta desactivada por el admin: no puede iniciar sesión
        public bool Activo { get; set; }

        // Permisos que da cada rol.
        public bool EsAdmin
        {
            get { return Rol == Roles.Admin; }
        }

        // Crear y modificar: editor y admin
        public bool PuedeCrear
        {
            get { return Rol == Roles.Editor || Rol == Roles.Admin; }
        }

        public bool PuedeModificar
        {
            get { return Rol == Roles.Editor || Rol == Roles.Admin; }
        }

        // Borrar: solo el admin
        public bool PuedeBorrar
        {
            get { return Rol == Roles.Admin; }
        }
    }
}