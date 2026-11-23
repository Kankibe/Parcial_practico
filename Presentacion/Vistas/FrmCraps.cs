using System;
using System.Drawing;
using System.Windows.Forms;
using Presentacion.Interfaces;
using Presentacion.Presentadores;

namespace Presentacion.Vistas
{

    public partial class FrmCraps : Form, ICrapsView
    {
        private CrapsPresenter presentador;

        // Eventos que escucha el Presentador
        public event EventHandler TirarClick;
        public event EventHandler NuevoClick;

        public FrmCraps()
        {
            InitializeComponent();

            // Conectamos esta vista con su presentador
            presentador = new CrapsPresenter(this);
        }
        public void MostrarDados(int dado1, int dado2)
        {
            lblDado1.Text = dado1 == 0 ? "-" : dado1.ToString();
            lblDado2.Text = dado2 == 0 ? "-" : dado2.ToString();
        }

        public void MostrarSuma(int suma)
        {
            lblSuma.Text = suma == 0 ? "Suma: -" : "Suma: " + suma;
        }

        public void MostrarPunto(int punto)
        {
            lblPunto.Text = punto == 0 ? "Punto: -" : "Punto: " + punto;
        }

        public void MostrarMensaje(string mensaje, bool terminado, bool gano)
        {
            lblMensaje.Text = mensaje;

            if (!terminado)
            {
                lblMensaje.ForeColor = Color.SteelBlue;
            }
            else if (gano)
            {
                lblMensaje.ForeColor = Color.SeaGreen;
            }
            else
            {
                lblMensaje.ForeColor = Color.Firebrick;
            }
        }

        public void MostrarMarcador(int ganadas, int perdidas)
        {
            lblMarcador.Text = "Ganadas: " + ganadas + "     Perdidas: " + perdidas;
        }

        private void btnTirar_Click(object sender, EventArgs e)
        {
            if (TirarClick != null)
            {
                TirarClick(this, EventArgs.Empty);
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            if (NuevoClick != null)
            {
                NuevoClick(this, EventArgs.Empty);
            }
        }
    }
}