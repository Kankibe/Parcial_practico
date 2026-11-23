using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Entidades;
using Presentacion.Interfaces;
using Presentacion.Presentadores;

namespace Presentacion.Vistas
{
    // Vista de gestión de usuarios (ventana hija del MDI, solo para el admin).
    // Solo muestra controles y le avisa al Presentador: no valida ni usa SQL.
    public partial class FrmUsuarios : Form, IUsuariosView
    {
        private UsuariosPresenter presentador;

        // Evita que el grid avise "se seleccionó una fila" mientras se está llenando
        private bool cargando = false;

        // Eventos que escucha el Presentador
        public event EventHandler VistaCargada;
        public event EventHandler GuardarRolClick;
        public event EventHandler CambiarEstadoClick;
        public event EventHandler CrearClick;
        public event EventHandler UsuarioSeleccionado;

        public FrmUsuarios()
        {
            InitializeComponent();

            // Configuración del grid: solo lectura, se elige la fila completa
            dgvUsuarios.ReadOnly = true;
            dgvUsuarios.AllowUserToAddRows = false;
            dgvUsuarios.AllowUserToDeleteRows = false;
            dgvUsuarios.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvUsuarios.MultiSelect = false;

            // Los dos ComboBox se llenan con los roles de la clase Roles (en el mismo orden)
            foreach (string rol in Roles.Todos)
            {
                cmbRol.Items.Add(Roles.Descripcion(rol));
                cmbRolNuevo.Items.Add(Roles.Descripcion(rol));
            }
            cmbRolNuevo.SelectedIndex = 0;   // los usuarios nuevos empiezan como lector
            btnEstado.Enabled = false;

            // Conectamos esta vista con su presentador
            presentador = new UsuariosPresenter(this);

            Load += FrmUsuarios_Load;
        }

        // ----- Datos que lee el Presentador -----
        public int IdSeleccionado
        {
            get
            {
                if (dgvUsuarios.SelectedRows.Count == 0)
                {
                    return 0;
                }

                object valor = dgvUsuarios.SelectedRows[0].Cells["colId"].Value;
                return valor == null ? 0 : Convert.ToInt32(valor);
            }
        }

        // Los ComboBox y Roles.Todos tienen el mismo orden, así que el índice dice el rol
        public string RolElegido
        {
            get
            {
                int indice = cmbRol.SelectedIndex;
                return indice < 0 ? "" : Roles.Todos[indice];
            }
        }

        public string NombreNuevo
        {
            get { return txtNombre.Text; }
        }

        public string CorreoNuevo
        {
            get { return txtCorreo.Text; }
        }

        public string ClaveNueva
        {
            get { return txtClave.Text; }
        }

        public string RolNuevo
        {
            get
            {
                int indice = cmbRolNuevo.SelectedIndex;
                return indice < 0 ? "" : Roles.Todos[indice];
            }
        }

        // ----- Órdenes que da el Presentador -----
        public void MostrarUsuarios(IList<E_Usuario> usuarios)
        {
            cargando = true;
            try
            {
                dgvUsuarios.Rows.Clear();

                foreach (E_Usuario u in usuarios)
                {
                    int fila = dgvUsuarios.Rows.Add(u.Id, u.Nombre, u.Correo, Roles.Nombre(u.Rol),
                                                    u.Activo ? "Activa" : "Desactivada");

                    // Las cuentas desactivadas se ven en gris
                    if (!u.Activo)
                    {
                        dgvUsuarios.Rows[fila].DefaultCellStyle.ForeColor = Color.Gray;
                    }
                }

                dgvUsuarios.ClearSelection();
                cmbRol.SelectedIndex = -1;
                btnEstado.Text = "Desactivar cuenta";
                btnEstado.Enabled = false;
            }
            finally
            {
                cargando = false;
            }
        }

        public void MostrarSeleccion(string rol, bool activo)
        {
            cmbRol.SelectedIndex = Array.IndexOf(Roles.Todos, rol);
            btnEstado.Enabled = true;
            btnEstado.Text = activo ? "Desactivar cuenta" : "Activar cuenta";
        }

        public void LimpiarFormularioNuevo()
        {
            txtNombre.Clear();
            txtCorreo.Clear();
            txtClave.Clear();
            cmbRolNuevo.SelectedIndex = 0;
        }

        public void MostrarMensaje(string mensaje, bool esError)
        {
            MessageBox.Show(mensaje, "Usuarios", MessageBoxButtons.OK,
                            esError ? MessageBoxIcon.Warning : MessageBoxIcon.Information);
        }

        public bool Confirmar(string pregunta)
        {
            return MessageBox.Show(pregunta, "Confirmar", MessageBoxButtons.YesNo,
                                   MessageBoxIcon.Question) == DialogResult.Yes;
        }

        // ----- Eventos de los controles: solo avisan al Presentador -----
        private void FrmUsuarios_Load(object sender, EventArgs e)
        {
            if (VistaCargada != null)
            {
                VistaCargada(this, EventArgs.Empty);
            }
        }

        private void dgvUsuarios_SelectionChanged(object sender, EventArgs e)
        {
            if (cargando)
            {
                return;
            }

            if (UsuarioSeleccionado != null)
            {
                UsuarioSeleccionado(this, EventArgs.Empty);
            }
        }

        private void btnGuardarRol_Click(object sender, EventArgs e)
        {
            if (GuardarRolClick != null)
            {
                GuardarRolClick(this, EventArgs.Empty);
            }
        }

        private void btnEstado_Click(object sender, EventArgs e)
        {
            if (CambiarEstadoClick != null)
            {
                CambiarEstadoClick(this, EventArgs.Empty);
            }
        }

        private void btnCrear_Click(object sender, EventArgs e)
        {
            if (CrearClick != null)
            {
                CrearClick(this, EventArgs.Empty);
            }
        }
    }
}
