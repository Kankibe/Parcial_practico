using System;
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

        // Constructor para pruebas
        public N_Usuario(IHashService hashService, IVerificarHashService verificarService)
        {
            this.hashService = hashService;
            this.verificarService = verificarService;
        }


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

  
                if (usuario == null ||
                    !verificarService.Verificar(clave, usuario.Salt, usuario.PasswordHash))
                {
                    mensaje = "Correo o contraseña incorrectos.";
                    return false;
                }


                Sesion.UsuarioActual = usuario;
                mensaje = "Bienvenido, " + usuario.Nombre + ".";
                return true;
            }
            catch (Exception)
            {
                mensaje = "No se pudo conectar con la base de datos.";
                return false;
            }
        }

        // Registra un usuario nuevo: genera el salt, genera el hash y lo guarda.
        public bool Registrar(string nombre, string correo, string clave, out string mensaje)
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

                datos.Insertar(usuario);

                mensaje = "Usuario registrado correctamente.";
                return true;
            }
            catch (Exception)
            {
                mensaje = "No se pudo conectar con la base de datos.";
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