namespace Negocio.Validaciones
{
    // Contrato que cumplen todos los validadores de campos.
    // Cada validador sabe revisar un tipo de dato y explicar qué salió mal.
    public interface IValidadorCampo
    {
        // true si el texto es válido para este tipo de campo
        bool EsValido(string valor);

        // Mensaje para el usuario (vacío si el último valor fue válido)
        string MensajeError { get; }
    }
}
