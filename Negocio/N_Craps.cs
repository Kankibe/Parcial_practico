using System;

namespace Negocio
{

    public class N_Craps
    {
        private readonly Random azar = new Random();

        public int Dado1 { get; private set; }
        public int Dado2 { get; private set; }
        public int Suma { get; private set; }

        public int Punto { get; private set; }

        public bool JuegoTerminado { get; private set; }
        public bool Gano { get; private set; }
        public string Mensaje { get; private set; }

        // Marcador de la sesión
        public int Ganadas { get; private set; }
        public int Perdidas { get; private set; }

        // Tira los dos dados y aplica las reglas
        public void Tirar()
        {

            if (JuegoTerminado)
            {
                NuevoJuego();
            }

            Dado1 = azar.Next(1, 7);
            Dado2 = azar.Next(1, 7);
            Suma = Dado1 + Dado2;

            if (Punto == 0)
            {
                // Primer tiro
                if (Suma == 7 || Suma == 11)
                {
                    Terminar(true, "¡Ganaste! Sacaste " + Suma + " en el primer tiro.");
                }
                else if (Suma == 2 || Suma == 3 || Suma == 12)
                {
                    Terminar(false, "Perdiste (craps). Sacaste " + Suma + " en el primer tiro.");
                }
                else
                {
                    Punto = Suma;
                    Mensaje = "Tu punto es " + Punto + ". Sigue tirando hasta sacarlo (¡cuidado con el 7!).";
                }
            }
            else
            {
                // Tiros siguientes
                if (Suma == Punto)
                {
                    Terminar(true, "¡Ganaste! Sacaste tu punto (" + Punto + ").");
                }
                else if (Suma == 7)
                {
                    Terminar(false, "Perdiste. Salió 7 antes de tu punto (" + Punto + ").");
                }
                else
                {
                    Mensaje = "Sacaste " + Suma + ". Sigue tirando, tu punto es " + Punto + ".";
                }
            }
        }

        // Reinicia la partida 
        public void NuevoJuego()
        {
            Dado1 = 0;
            Dado2 = 0;
            Suma = 0;
            Punto = 0;
            JuegoTerminado = false;
            Gano = false;
            Mensaje = "";
        }

        // Marca el fin del juego y actualiza el marcador
        private void Terminar(bool gano, string mensaje)
        {
            JuegoTerminado = true;
            Gano = gano;
            Mensaje = mensaje;

            if (gano)
            {
                Ganadas++;
            }
            else
            {
                Perdidas++;
            }
        }
    }
}