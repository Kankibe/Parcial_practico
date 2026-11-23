namespace Entidades
{
    // Los tres roles que existen. El texto es el mismo que se guarda en la columna "rol" de MySQL.
    //   lector : solo ve y busca
    //   editor : ve, crea y modifica, pero NO borra 
    //   admin  : puede todo y además gestiona a los demás usuarios
    public static class Roles
    {
        public const string Lector = "lector";
        public const string Editor = "editor";
        public const string Admin = "admin";

        // Orden en que se muestran en el ComboBox
        public static readonly string[] Todos = { Lector, Editor, Admin };

        public static bool EsValido(string rol)
        {
            return System.Array.IndexOf(Todos, rol) >= 0;
        }

        public static string Nombre(string rol)
        {
            switch (rol)
            {
                case Admin: return "Administrador";
                case Editor: return "Editor";
                default: return "Lector";
            }
        }

        public static string Descripcion(string rol)
        {
            switch (rol)
            {
                case Admin: return "Administrador (todo + gestionar usuarios)";
                case Editor: return "Editor (ver, crear y modificar)";
                default: return "Lector (solo ver)";
            }
        }
    }
}
