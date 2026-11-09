namespace Negocio.Interfaces
{
    // Contrato 2: su ÚNICA responsabilidad es verificar una contraseña.
    // Dice si el texto plano coincide con el hash que está guardado.
    public interface IVerificarHashService
    {
        // true si la contraseña es correcta, false si no
        bool Verificar(string textoPlano, string salt, string hashAlmacenado);
    }
}