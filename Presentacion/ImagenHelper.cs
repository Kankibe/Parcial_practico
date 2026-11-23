using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Presentacion
{

    public static class ImagenHelper
    {
        // Convierte una imagen en un arreglo de bytes (PNG) para guardarla en la base de datos
        public static byte[] ImageToByteArray(Image imagen)
        {
            if (imagen == null)
            {
                return null;
            }

            using (MemoryStream ms = new MemoryStream())
            {
                imagen.Save(ms, ImageFormat.Png);
                return ms.ToArray();
            }
        }

        public static Image ByteArrayToImage(byte[] bytes, int ancho = 0, int alto = 0)
        {
            if (bytes == null || bytes.Length == 0)
            {
                return null;
            }

            try
            {
                using (MemoryStream ms = new MemoryStream(bytes))
                using (Bitmap original = new Bitmap(ms))
                {
                    if (ancho > 0 && alto > 0)
                    {
                        return new Bitmap(original, new Size(ancho, alto));
                    }

                    // Se clona para poder cerrar el MemoryStream
                    return new Bitmap(original);
                }
            }
            catch (ArgumentException)
            {
                return null;
            }
        }
    }
}
