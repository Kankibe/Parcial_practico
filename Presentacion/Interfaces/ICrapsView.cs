using System;

namespace Presentacion.Interfaces
{
    // Contrato de la VISTA del juego de Craps.
    public interface ICrapsView
    {
        // Avisos que la vista le manda al Presentador
        event EventHandler TirarClick;
        event EventHandler NuevoClick;

        void MostrarDados(int dado1, int dado2);
        void MostrarSuma(int suma);
        void MostrarPunto(int punto);
        void MostrarMensaje(string mensaje, bool terminado, bool gano);
        void MostrarMarcador(int ganadas, int perdidas);
    }
}