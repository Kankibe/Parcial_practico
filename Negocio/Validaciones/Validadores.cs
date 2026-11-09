using System.Globalization;

namespace Negocio.Validaciones
{
    // Validador para texto (ej. nombre del producto): no vacío y no más largo que la columna.
    public class ValidadorTexto : IValidadorCampo
    {
        private readonly int maxLongitud;

        public string MensajeError { get; private set; }

        public ValidadorTexto(int maxLongitud)
        {
            this.maxLongitud = maxLongitud;
            MensajeError = string.Empty;
        }

        public bool EsValido(string valor)
        {
            if (string.IsNullOrWhiteSpace(valor))
            {
                MensajeError = "El campo de texto no puede estar vacío.";
                return false;
            }

            if (valor.Trim().Length > maxLongitud)
            {
                MensajeError = "No puede tener más de " + maxLongitud + " caracteres.";
                return false;
            }

            MensajeError = string.Empty;
            return true;
        }
    }

    // Validador para decimales (ej. precio): número con punto decimal, mayor o igual a 0.
    public class ValidadorDecimal : IValidadorCampo
    {
        // Lo máximo que cabe en la columna DECIMAL(10,2)
        private const decimal Maximo = 99999999.99m;

        public string MensajeError { get; private set; }

        public ValidadorDecimal()
        {
            MensajeError = string.Empty;
        }

        public bool EsValido(string valor)
        {
            decimal resultado;

            if (!TryParsear(valor, out resultado))
            {
                MensajeError = "Debe ingresar un número decimal válido.";
                return false;
            }

            if (resultado > Maximo)
            {
                MensajeError = "El valor es demasiado grande.";
                return false;
            }

            MensajeError = string.Empty;
            return true;
        }

        // Convierte el texto a decimal sin depender del idioma de Windows.
        // Acepta "15.70" y también "15,70". No acepta negativos.
        public static bool TryParsear(string valor, out decimal resultado)
        {
            resultado = 0;

            if (string.IsNullOrWhiteSpace(valor))
            {
                return false;
            }

            string texto = valor.Trim().Replace(',', '.');

            return decimal.TryParse(texto, NumberStyles.AllowDecimalPoint,
                                    CultureInfo.InvariantCulture, out resultado);
        }
    }

    // Validador para enteros (ej. cantidad): solo dígitos, mayor o igual a 0.
    public class ValidadorEntero : IValidadorCampo
    {
        public string MensajeError { get; private set; }

        public ValidadorEntero()
        {
            MensajeError = string.Empty;
        }

        public bool EsValido(string valor)
        {
            int resultado;

            if (!TryParsear(valor, out resultado))
            {
                MensajeError = "Debe ingresar un número entero válido.";
                return false;
            }

            MensajeError = string.Empty;
            return true;
        }

        public static bool TryParsear(string valor, out int resultado)
        {
            resultado = 0;

            if (string.IsNullOrWhiteSpace(valor))
            {
                return false;
            }

            // NumberStyles.None = solo dígitos (sin signo ni espacios)
            return int.TryParse(valor.Trim(), NumberStyles.None,
                                CultureInfo.InvariantCulture, out resultado);
        }
    }
}
