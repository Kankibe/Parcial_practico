using System.Security.Cryptography;
using System.Text;
using Negocio.Interfaces;

namespace Negocio.Servicios
{
    // Implementación de IHashService usando SHA-256.

    public class HashService : IHashService
    {
        public string GenerarHash(string textoPlano, string salt)
        {
            using (SHA256 sha = SHA256.Create())
            {
                // 1.  salt + contraseña y paso a bytes
                byte[] bytes = Encoding.UTF8.GetBytes(salt + textoPlano);

                // 2. calculo del hash
                byte[] hash = sha.ComputeHash(bytes);

                // 3.  cada byte a 2 caracteres hexadecimales
                StringBuilder resultado = new StringBuilder();
                foreach (byte b in hash)
                {
                    resultado.Append(b.ToString("x2"));
                }

                return resultado.ToString();
            }
        }
    }
}