using System;
using Negocio;
using Presentacion.Interfaces;

namespace Presentacion.Presentadores
{
    // Presentador del juego de Craps.

    public class CrapsPresenter
    {
        private readonly ICrapsView vista;
        private readonly N_Craps juego;

        public CrapsPresenter(ICrapsView vista) : this(vista, new N_Craps())
        {
        }

        public CrapsPresenter(ICrapsView vista, N_Craps juego)
        {
            this.vista = vista;
            this.juego = juego;

            this.vista.TirarClick += OnTirar;
            this.vista.NuevoClick += OnNuevo;

            MostrarEstadoInicial();
        }

        private void OnTirar(object sender, EventArgs e)
        {
            juego.Tirar();

            vista.MostrarDados(juego.Dado1, juego.Dado2);
            vista.MostrarSuma(juego.Suma);
            vista.MostrarPunto(juego.Punto);
            vista.MostrarMensaje(juego.Mensaje, juego.JuegoTerminado, juego.Gano);
            vista.MostrarMarcador(juego.Ganadas, juego.Perdidas);
        }

        private void OnNuevo(object sender, EventArgs e)
        {
            juego.NuevoJuego();
            MostrarEstadoInicial();
        }

        // Pantalla de arranque
        private void MostrarEstadoInicial()
        {
            vista.MostrarDados(0, 0);
            vista.MostrarSuma(0);
            vista.MostrarPunto(0);
            vista.MostrarMensaje("Presiona Tirar dados para empezar.", false, false);
            vista.MostrarMarcador(juego.Ganadas, juego.Perdidas);
        }
    }
}