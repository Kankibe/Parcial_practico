using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using Entidades;
using Presentacion.Interfaces;
using Presentacion.Presentadores;

namespace Presentacion.Vistas
{
    // Vista del CRUD de productos (ventana hija del MDI).
    // Solo muestra controles y le avisa al Presentador: no valida, no usa SQL
    // ni sabe nada de la base de datos.
    public partial class FrmProductos : Form, IProductosView
    {
        private ProductosPresenter presentador;

        // Evita que el grid avise "se seleccionó una fila" mientras se está llenando
        private bool cargando = false;

        // Imagen de relleno que trae el diseñador, y si el usuario ya puso una foto real
        private Image imagenPorDefecto;
        private bool tieneImagen = false;

        // Lo que permite el rol del usuario (lo manda el Presentador con AplicarPermisos)
        private bool puedeCrear = false;
        private bool puedeModificar = false;
        private bool puedeBorrar = false;

        // Eventos que escucha el Presentador
        public event EventHandler VistaCargada;
        public event EventHandler GuardarClick;
        public event EventHandler ModificarClick;
        public event EventHandler BorrarClick;
        public event EventHandler LimpiarClick;
        public event EventHandler BuscarCambio;
        public event EventHandler ProductoSeleccionado;

        public FrmProductos()
        {
            InitializeComponent();

            // Configuración del grid: solo lectura, se elige la fila completa
            dgvProductos.ReadOnly = true;
            dgvProductos.AllowUserToAddRows = false;
            dgvProductos.AllowUserToDeleteRows = false;
            dgvProductos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProductos.MultiSelect = false;
            colImagen.DefaultCellStyle.NullValue = null;   // sin foto: celda vacía, no la X roja

            foreach (DataGridViewColumn col in dgvProductos.Columns)
            {
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            txtId.ReadOnly = true;
            btnModificar.Enabled = false;
            imagenPorDefecto = PictureBox.Image;

            // Conectamos esta vista con su presentador
            presentador = new ProductosPresenter(this);

            Load += FrmProductos_Load;
        }

        // ----- Datos que lee el Presentador -----
        public string Nombre
        {
            get { return txtNombre.Text; }
        }

        public string Precio
        {
            get { return txtPrecio.Text; }
        }

        public string Cantidad
        {
            get { return txtCantidad.Text; }
        }

        public string Filtro
        {
            get { return txtBusqueda.Text; }
        }

        // Solo devuelve bytes si el usuario eligió una foto (o el producto ya traía una)
        public byte[] Imagen
        {
            get
            {
                if (!tieneImagen || PictureBox.Image == null)
                {
                    return null;
                }
                return ImagenHelper.ImageToByteArray(PictureBox.Image);
            }
        }

        public int IdSeleccionado
        {
            get
            {
                if (dgvProductos.SelectedRows.Count == 0)
                {
                    return 0;
                }

                object valor = dgvProductos.SelectedRows[0].Cells["colId"].Value;
                return valor == null ? 0 : Convert.ToInt32(valor);
            }
        }

        // ----- Órdenes que da el Presentador -----

        // Llena el grid con la lista de productos
        public void MostrarProductos(IList<E_Producto> productos)
        {
            cargando = true;
            try
            {
                dgvProductos.Rows.Clear();

                foreach (E_Producto p in productos)
                {
                    Image miniatura = ImagenHelper.ByteArrayToImage(p.Imagen, 80, 60);
                    dgvProductos.Rows.Add(p.Id, p.Nombre, p.Precio, p.Cantidad, miniatura);
                }

                dgvProductos.ClearSelection();
            }
            finally
            {
                cargando = false;
            }
        }

        // Pasa un producto a los campos de arriba para poder modificarlo o borrarlo
        public void MostrarProducto(E_Producto producto)
        {
            txtId.Text = producto.Id.ToString();
            txtNombre.Text = producto.Nombre;
            txtPrecio.Text = producto.Precio.ToString("0.00", CultureInfo.InvariantCulture);
            txtCantidad.Text = producto.Cantidad.ToString();

            Image foto = ImagenHelper.ByteArrayToImage(producto.Imagen);
            if (foto != null)
            {
                PonerImagen(foto, true);
            }
            else
            {
                PonerImagen(imagenPorDefecto, false);
            }

            errorProvider1.Clear();
            btnGuardar.Enabled = false;
            btnModificar.Enabled = puedeModificar;   // según el rol
        }

        // Marca en rojo (ErrorProvider) los campos con error; texto vacío = sin error
        public void MostrarErrores(string errorNombre, string errorPrecio, string errorCantidad)
        {
            errorProvider1.SetError(txtNombre, errorNombre);
            errorProvider1.SetError(txtPrecio, errorPrecio);
            errorProvider1.SetError(txtCantidad, errorCantidad);
        }

        public void MostrarMensaje(string mensaje, bool esError)
        {
            MessageBox.Show(mensaje, "Productos", MessageBoxButtons.OK,
                            esError ? MessageBoxIcon.Warning : MessageBoxIcon.Information);
        }

        public bool Confirmar(string pregunta)
        {
            return MessageBox.Show(pregunta, "Confirmar", MessageBoxButtons.YesNo,
                                   MessageBoxIcon.Question) == DialogResult.Yes;
        }

        public void LimpiarCampos()
        {
            txtId.Clear();
            txtNombre.Clear();
            txtPrecio.Clear();
            txtCantidad.Clear();
            PonerImagen(imagenPorDefecto, false);

            btnGuardar.Enabled = puedeCrear;         // según el rol
            btnModificar.Enabled = false;
            dgvProductos.ClearSelection();
            errorProvider1.Clear();
        }

        // Activa solo lo que el rol permite:
        //   lector -> nada (solo ve y busca), editor -> guardar y modificar, admin -> todo
        public void AplicarPermisos(bool crear, bool modificar, bool borrar)
        {
            puedeCrear = crear;
            puedeModificar = modificar;
            puedeBorrar = borrar;

            bool puedeEscribir = crear || modificar;

            // Sin permiso de escritura los campos quedan solo para leer
            txtNombre.ReadOnly = !puedeEscribir;
            txtPrecio.ReadOnly = !puedeEscribir;
            txtCantidad.ReadOnly = !puedeEscribir;
            PictureBox.Enabled = puedeEscribir;   // no se puede cambiar la foto

            btnGuardar.Enabled = crear;
            btnModificar.Enabled = false;          // se activa al elegir un producto del grid
            btnBorrar.Enabled = borrar;

            if (!puedeEscribir)
            {
                Text = Text + " (solo lectura)";
            }
        }

        // Cambia la foto del cuadro y libera la anterior (menos la de relleno)
        private void PonerImagen(Image nueva, bool esFotoReal)
        {
            Image anterior = PictureBox.Image;
            PictureBox.Image = nueva;
            PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            tieneImagen = esFotoReal;

            if (anterior != null && anterior != imagenPorDefecto && anterior != nueva)
            {
                anterior.Dispose();
            }
        }

        // ----- Eventos de los controles: solo avisan al Presentador -----

        private void FrmProductos_Load(object sender, EventArgs e)
        {
            if (VistaCargada != null)
            {
                VistaCargada(this, EventArgs.Empty);
            }
        }

        private void dgvProductos_SelectionChanged(object sender, EventArgs e)
        {
            if (cargando)
            {
                return;
            }

            if (ProductoSeleccionado != null)
            {
                ProductoSeleccionado(this, EventArgs.Empty);
            }
        }

        private void txtBusqueda_TextChanged(object sender, EventArgs e)
        {
            if (BuscarCambio != null)
            {
                BuscarCambio(this, EventArgs.Empty);
            }
        }

        // Clic en la lupa: vuelve a buscar
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (BuscarCambio != null)
            {
                BuscarCambio(this, EventArgs.Empty);
            }
        }

        // Clic en la foto del producto: abre el selector de imagen.
        // Elegir un archivo es cosa de la interfaz, por eso se queda en la vista.
        private void PictureBox_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialogo = new OpenFileDialog())
            {
                dialogo.Title = "Seleccionar imagen del producto";
                dialogo.Filter = "Archivos de imagen|*.jpg;*.jpeg;*.png;*.bmp";

                if (dialogo.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                try
                {
                    // Se copia a un Bitmap para no dejar el archivo bloqueado
                    using (Image original = Image.FromFile(dialogo.FileName))
                    {
                        PonerImagen(new Bitmap(original), true);
                    }
                }
                catch (Exception)
                {
                    MostrarMensaje("El archivo seleccionado no es una imagen válida.", true);
                }
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (GuardarClick != null)
            {
                GuardarClick(this, EventArgs.Empty);
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (ModificarClick != null)
            {
                ModificarClick(this, EventArgs.Empty);
            }
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            if (BorrarClick != null)
            {
                BorrarClick(this, EventArgs.Empty);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            if (LimpiarClick != null)
            {
                LimpiarClick(this, EventArgs.Empty);
            }
        }
    }
}
