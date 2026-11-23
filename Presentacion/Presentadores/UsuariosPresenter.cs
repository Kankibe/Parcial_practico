using System;
using System.Collections.Generic;
using Entidades;
using Negocio;
using Presentacion.Interfaces;

namespace Presentacion.Presentadores
{
    // Presentador de la gestión de usuarios (solo el admin).
    // Crea usuarios, cambia roles y activa o desactiva cuentas.
    public class UsuariosPresenter
    {
        private readonly IUsuariosView vista;
        private readonly N_Usuario negocio;

        // Usuarios que se están mostrando en el grid
        private List<E_Usuario> usuarios = new List<E_Usuario>();

        // Constructor
        public UsuariosPresenter(IUsuariosView vista) : this(vista, new N_Usuario())
        {
        }

        public UsuariosPresenter(IUsuariosView vista, N_Usuario negocio)
        {
            this.vista = vista;
            this.negocio = negocio;

            this.vista.VistaCargada += OnVistaCargada;
            this.vista.GuardarRolClick += OnGuardarRol;
            this.vista.CambiarEstadoClick += OnCambiarEstado;
            this.vista.CrearClick += OnCrear;
            this.vista.UsuarioSeleccionado += OnUsuarioSeleccionado;
        }

        private void OnVistaCargada(object sender, EventArgs e)
        {
            CargarLista();
        }

        // Al elegir una fila, el ComboBox muestra su rol y el botón dice "Activar" o "Desactivar"
        private void OnUsuarioSeleccionado(object sender, EventArgs e)
        {
            E_Usuario usuario = BuscarSeleccionado();
            if (usuario != null)
            {
                vista.MostrarSeleccion(usuario.Rol, usuario.Activo);
            }
        }

        private void OnGuardarRol(object sender, EventArgs e)
        {
            string mensaje;
            bool correcto = negocio.CambiarRol(vista.IdSeleccionado, vista.RolElegido, out mensaje);

            vista.MostrarMensaje(mensaje, !correcto);

            if (correcto)
            {
                CargarLista();
            }
        }

        // Si la cuenta está activa la desactiva, y al revés
        private void OnCambiarEstado(object sender, EventArgs e)
        {
            E_Usuario usuario = BuscarSeleccionado();
            if (usuario == null)
            {
                vista.MostrarMensaje("Selecciona un usuario de la lista.", true);
                return;
            }

            bool nuevoEstado = !usuario.Activo;

            if (!nuevoEstado && !vista.Confirmar("¿Desactivar la cuenta de " + usuario.Nombre + "? Ya no podrá iniciar sesión."))
            {
                return;
            }

            string mensaje;
            bool correcto = negocio.CambiarEstado(usuario.Id, nuevoEstado, out mensaje);

            vista.MostrarMensaje(mensaje, !correcto);

            if (correcto)
            {
                CargarLista();
            }
        }

        private void OnCrear(object sender, EventArgs e)
        {
            string mensaje;
            bool correcto = negocio.CrearUsuario(vista.NombreNuevo, vista.CorreoNuevo, vista.ClaveNueva, vista.RolNuevo, out mensaje);

            vista.MostrarMensaje(mensaje, !correcto);

            if (correcto)
            {
                vista.LimpiarFormularioNuevo();
                CargarLista();
            }
        }

        // Devuelve el usuario elegido en el grid (null si no hay ninguno)
        private E_Usuario BuscarSeleccionado()
        {
            int id = vista.IdSeleccionado;
            if (id == 0)
            {
                return null;
            }
            return usuarios.Find(u => u.Id == id);
        }

        private void CargarLista()
        {
            string mensaje;
            usuarios = negocio.Listar(out mensaje);
            vista.MostrarUsuarios(usuarios);

            if (mensaje != "")
            {
                vista.MostrarMensaje(mensaje, true);
            }
        }
    }
}
