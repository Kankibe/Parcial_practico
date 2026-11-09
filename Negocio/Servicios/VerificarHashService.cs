using Negocio.Interfaces;

namespace Negocio.Servicios
{
    // Implementación de IVerificarHashService.
    // Para verificar, vuelve a generar el hash con el mismo salt y lo compara
    // con el hash que está guardado en la base de datos.
    public class VerificarHashService : IVerificarHashService
    {
        private readonly IHashService hashService;

        // Recibe el generador de hash por el constructor (usa el contrato 1)
        public VerificarHashService(IHashService hashService)
        {
            this.hashService = hashService;
        }

        public bool Verificar(string textoPlano, string salt, string hashAlmacenado)
        {
            string hashNuevo = hashService.GenerarHash(textoPlano, salt);
            return hashNuevo == hashAlmacenado;
        }
    }
}