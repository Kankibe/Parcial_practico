using System;
using Negocio;
using Presentacion.Interfaces;

namespace Presentacion.Presentadores
{
    // Presentador del login: es el puente entre la Vista (FrmLogin) y el Negocio.

    public class LoginPresenter
    {
        private readonly ILoginView vista;
        private readonly N_Usuario negocio;

        // Constructor normal: usa el N_Usuario real
        public LoginPresenter(ILoginView vista) : this(vista, new N_Usuario())
        {
        }

        public LoginPresenter(ILoginView vista, N_Usuario negocio)
        {
            this.vista = vista;
            this.negocio = negocio;

            this.vista.IngresarClick += OnIngresar;
            this.vista.RegistrarClick += OnRegistrar;
        }

        private void OnIngresar(object sender, EventArgs e)
        {
            string mensaje;
            bool correcto = negocio.IniciarSesion(vista.Correo, vista.Clave, out mensaje);

            if (correcto)
            {
                vista.AbrirPrincipal();
            }
            else
            {
                vista.MostrarMensaje(mensaje, true);
                vista.LimpiarClave();
            }
        }

        private void OnRegistrar(object sender, EventArgs e)
        {
            string mensaje;
            bool correcto = negocio.Registrar(vista.Nombre, vista.Correo, vista.Clave, out mensaje);

            vista.MostrarMensaje(mensaje, !correcto);

            if (correcto)
            {
                vista.LimpiarClave();
            }
        }
    }
}