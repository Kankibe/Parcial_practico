using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using Datos;
using Entidades;
using Negocio.Interfaces;
using Negocio.Servicios;

namespace Negocio
{
    // Lógica de negocio de los usuarios: validaciones, login y registro.
    public class N_Usuario
    {
        private readonly D_Usuario datos = new D_Usuario();
        private readonly IHashService hashService;
        private readonly IVerificarHashService verificarService;

        // Constructor 
        public N_Usuario()
        {
            hashService = new HashService();
            verificarService = new VerificarHashService(hashService);
        }

     
        public N_Usuario(IHashService hashService, IVerificarHashService verificarService)
        {
            this.hashService = hashService;
            this.verificarService = verificarService;
        }

        // Intenta iniciar sesión. Devuelve true si todo está bien.
        // En "mensaje" deja el texto que se le mostrará al usuario.
        public bool IniciarSesion(string correo, string clave, out string mensaje)
        {
            // Validaciones
            if (string.IsNullOrWhiteSpace(correo))
            {
                mensaje = "Escribe tu correo.";
                return false;
            }
            if (!CorreoValido(correo))
            {
                mensaje = "El correo no tiene un formato válido.";
                return false;
            }
            if (string.IsNullOrEmpty(clave))
            {
                mensaje = "Escribe tu contraseña.";
                return false;
            }

            try
            {
                // usuario en la base de datos
                E_Usuario usuario = datos.ObtenerPorCorreo(correo.Trim());

                // Mismo mensaje si no existe o si la clave está mal
                if (usuario == null ||
                    !verificarService.Verificar(clave, usuario.Salt, usuario.PasswordHash))
                {
                    mensaje = "Correo o contraseña incorrectos.";
                    return false;
                }

                // La contraseña es correcta, pero el admin pudo haber desactivado la cuenta
                if (!usuario.Activo)
                {
                    mensaje = "Tu cuenta está desactivada. Contacta al administrador.";
                    return false;
                }

                // Todo bien: guardamos quién inició sesión
                Sesion.UsuarioActual = usuario;

                // Elegimos la cuenta de MySQL según el rol: admin -> Kanki, los demás -> Ibe
                if (usuario.EsAdmin)
                {
                    Conexion.UsarSuperusuario();
                }
                else
                {
                    Conexion.UsarMinimo();
                }
                mensaje = "Bienvenido, " + usuario.Nombre + ".";
                return true;
            }
            catch (Exception)
            {
                mensaje = "No se pudo conectar con la base de datos.";
                return false;
            }
        }

        // Registro desde la pantalla de login: la cuenta siempre queda como "lector".
        public bool Registrar(string nombre, string correo, string clave, out string mensaje)
        {
            return Crear(nombre, correo, clave, Roles.Lector, out mensaje);
        }

        // Crear un usuario desde la ventana de gestión: solo el admin, y él elige el rol.
        public bool CrearUsuario(string nombre, string correo, string clave, string rol, out string mensaje)
        {
            if (!Sesion.EsAdmin)
            {
                mensaje = "Solo el administrador puede crear usuarios.";
                return false;
            }
            if (!Roles.EsValido(rol))
            {
                mensaje = "Elige un rol de la lista.";
                return false;
            }

            return Crear(nombre, correo, clave, rol, out mensaje);
        }

        // Valida, genera el salt y el hash y guarda el usuario con el rol indicado.
        private bool Crear(string nombre, string correo, string clave, string rol, out string mensaje)
        {
            // Validaciones
            if (string.IsNullOrWhiteSpace(nombre))
            {
                mensaje = "Escribe tu nombre.";
                return false;
            }
            if (string.IsNullOrWhiteSpace(correo) || !CorreoValido(correo))
            {
                mensaje = "Escribe un correo válido.";
                return false;
            }
            if (string.IsNullOrEmpty(clave) || clave.Length < 6)
            {
                mensaje = "La contraseña debe tener al menos 6 caracteres.";
                return false;
            }

            try
            {
                // No permitimos dos usuarios con el mismo correo
                if (datos.ObtenerPorCorreo(correo.Trim()) != null)
                {
                    mensaje = "Ese correo ya está registrado.";
                    return false;
                }

                E_Usuario usuario = new E_Usuario();
                usuario.Nombre = nombre.Trim();
                usuario.Correo = correo.Trim();
                usuario.Salt = GenerarSalt();
                usuario.PasswordHash = hashService.GenerarHash(clave, usuario.Salt);

                usuario.Rol = rol;

                if (rol == Roles.Lector)
                {
                    datos.Insertar(usuario);
                }
                else
                {
                    datos.InsertarConRol(usuario);
                }

                mensaje = "Usuario registrado correctamente.";
                return true;
            }
            catch (Exception)
            {
                mensaje = "No se pudo conectar con la base de datos.";
                return false;
            }
        }

        // Lista los usuarios para la ventana de gestión. Solo el admin puede verlos.
        // Si algo falla devuelve una lista vacía y deja el motivo en "mensaje".
        public List<E_Usuario> Listar(out string mensaje)
        {
            if (!Sesion.EsAdmin)
            {
                mensaje = "Solo el administrador puede gestionar usuarios.";
                return new List<E_Usuario>();
            }

            try
            {
                mensaje = "";
                return datos.Listar();
            }
            catch (Exception ex)
            {
                mensaje = "No se pudieron cargar los usuarios: " + ex.Message;
                return new List<E_Usuario>();
            }
        }

        // Le cambia el rol a un usuario. Solo el admin puede hacerlo.
        public bool CambiarRol(int id, string rol, out string mensaje)
        {
            if (!Sesion.EsAdmin)
            {
                mensaje = "Solo el administrador puede cambiar roles.";
                return false;
            }
            if (id <= 0)
            {
                mensaje = "Selecciona un usuario de la lista.";
                return false;
            }
            if (!Roles.EsValido(rol))
            {
                mensaje = "Elige un rol de la lista.";
                return false;
            }

            // El admin no puede cambiarse el rol a sí mismo: así nunca se queda sin acceso
            if (Sesion.UsuarioActual != null && Sesion.UsuarioActual.Id == id)
            {
                mensaje = "No puedes cambiar tu propio rol.";
                return false;
            }

            try
            {
                datos.CambiarRol(id, rol);
                mensaje = "Rol actualizado. Se aplicará la próxima vez que ese usuario inicie sesión.";
                return true;
            }
            catch (Exception ex)
            {
                mensaje = "No se pudo cambiar el rol: " + ex.Message;
                return false;
            }
        }

        // Activa o desactiva una cuenta. Solo el admin puede hacerlo.
        // Una cuenta desactivada se conserva en la base, pero ya no puede iniciar sesión.
        public bool CambiarEstado(int id, bool activo, out string mensaje)
        {
            if (!Sesion.EsAdmin)
            {
                mensaje = "Solo el administrador puede activar o desactivar cuentas.";
                return false;
            }
            if (id <= 0)
            {
                mensaje = "Selecciona un usuario de la lista.";
                return false;
            }

            // El admin no puede desactivarse a sí mismo: así nunca se queda fuera
            if (Sesion.UsuarioActual != null && Sesion.UsuarioActual.Id == id)
            {
                mensaje = "No puedes desactivar tu propia cuenta.";
                return false;
            }

            try
            {
                datos.CambiarEstado(id, activo);
                mensaje = activo ? "Cuenta activada." : "Cuenta desactivada. Ya no podrá iniciar sesión.";
                return true;
            }
            catch (Exception ex)
            {
                mensaje = "No se pudo cambiar el estado: " + ex.Message;
                return false;
            }
        }

        // Revisa que el correo tenga forma de correo (algo@algo.algo)
        private bool CorreoValido(string correo)
        {
            return Regex.IsMatch(correo.Trim(), @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }

        // Crea un salt aleatorio de 32 caracteres hexadecimales
        private string GenerarSalt()
        {
            byte[] bytes = new byte[16];
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(bytes);
            }
            return BitConverter.ToString(bytes).Replace("-", "").ToLower();
        }
    }
}