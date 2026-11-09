namespace Negocio.Interfaces
{
    
    public interface IHashService
    {
        // Devuelve el hash (en hexadecimal) del texto mezclado con el salt
        string GenerarHash(string textoPlano, string salt);
    }
}