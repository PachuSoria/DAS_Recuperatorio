using BE;
using BLL;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace Nro3
{
    public partial class Form1 : Form
    {
        BLL_Producto _bllProducto;
        public Form1()
        {
            InitializeComponent();
            _bllProducto = new BLL_Producto();
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect; dataGridView1.MultiSelect = false; dataGridView1.ReadOnly = true;
            dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect; dataGridView2.MultiSelect = false; dataGridView2.ReadOnly = true;
            dataGridView3.SelectionMode = DataGridViewSelectionMode.FullRowSelect; dataGridView3.MultiSelect = false; dataGridView3.ReadOnly = true;
            dataGridView4.SelectionMode = DataGridViewSelectionMode.FullRowSelect; dataGridView4.MultiSelect = false; dataGridView4.ReadOnly = true;
            dataGridView5.SelectionMode = DataGridViewSelectionMode.FullRowSelect; dataGridView5.MultiSelect = false; dataGridView5.ReadOnly = true;
            dataGridView6.SelectionMode = DataGridViewSelectionMode.FullRowSelect; dataGridView6.MultiSelect = false; dataGridView6.ReadOnly = true;
            MostrarGrilla();
            ActualizarGrillas();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text.Length == 0) throw new Exception("Debe ingresar el código del producto");
                string codigo = textBox1.Text;
                if (!Regex.IsMatch(codigo, @"^[A-Z]{3}[0-9]{3}$")) throw new Exception("El código no es valido");
                if (_bllProducto.ObtenerProductos().Any(x => x.Codigo == codigo)) throw new Exception($"Ya existe un producto con el código {codigo}");
                if (textBox2.Text.Length == 0) throw new Exception("Debe ingresar el nombre del producto");
                string nombre = textBox2.Text;
                if (textBox3.Text.Length == 0) throw new Exception("Debe ingresar la categoria del producto");
                string categoria = textBox3.Text;
                if (textBox4.Text.Length == 0) throw new Exception("Debe ingresar el precio del producto");
                decimal precio = Convert.ToDecimal(textBox4.Text);
                if (precio <= 0) throw new Exception("El precio no es valido");
                if (textBox5.Text.Length == 0) throw new Exception("Debe ingresar el stock del producto");
                int stock = Convert.ToInt32(textBox5.Text);
                if (stock <= 0) throw new Exception("El stock no es valido");
                DateTime fechaVto = dateTimePicker1.Value;
                if (fechaVto <= DateTime.Now) throw new Exception("La fecha de vencimiento no es valida");
                _bllProducto.Agregar(new BE.BE_Producto(codigo, nombre, categoria, precio, stock, fechaVto));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.Rows.Count == 0) throw new Exception("Debe seleccionar un producto");
                var cod = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                var producto = _bllProducto.ObtenerProductos().Find(x => x.Codigo == cod);
                producto.Nombre = textBox2.Text;
                producto.Categoria = textBox3.Text;
                producto.Precio = Convert.ToDecimal(textBox4.Text);
                if (producto.Precio <= 0) throw new Exception("El precio no es valido");
                producto.Stock = Convert.ToInt32(textBox5.Text);
                if (producto.Stock <= 0) throw new Exception("El stock no es valido");
                producto.FechaVto = dateTimePicker1.Value;
                if (producto.FechaVto <= DateTime.Now) throw new Exception("La fecha de vencimiento no es valida");
                _bllProducto.Modificar(producto);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.Rows.Count == 0) throw new Exception("Debe seleccionar un producto");
                var cod = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                var producto = _bllProducto.ObtenerProductos().Find(x => x.Codigo == cod);
                _bllProducto.Eliminar(producto.Codigo);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                _bllProducto.Actualizar();
                MostrarGrilla();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                _bllProducto.Cancelar();
                ActualizarGrillas();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox7.Text.Length == 0) throw new Exception("Debe ingresar el valor 'Desde'");
                decimal desde = Convert.ToDecimal(textBox7.Text);
                if (textBox8.Text.Length == 0) throw new Exception("Debe ingresar el valor 'Hasta'");
                decimal hasta = Convert.ToDecimal(textBox8.Text);
                if (desde < 0 || desde > hasta) throw new Exception("Los valores no son validos");
                List<BE_Producto> resultado = _bllProducto.DesdeHasta(desde, hasta);
                dataGridView2.DataSource = null; dataGridView2.DataSource = resultado;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void MostrarGrilla()
        {
            dataGridView1.DataSource = null; dataGridView1.DataSource = _bllProducto.ObtenerProductos();
        }

        private void ActualizarGrillas()
        {
            dataGridView3.DataSource = _bllProducto.dv("Agregados");
            dataGridView4.DataSource = _bllProducto.dv("Eliminados");
            dataGridView5.DataSource = _bllProducto.dv("ModOri");
            dataGridView6.DataSource = _bllProducto.dv("ModAct");
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            string iniCodigo = textBox6.Text;
            List<BE_Producto> resultado = _bllProducto.Incremental(iniCodigo);
            dataGridView2.DataSource = null; dataGridView2.DataSource = resultado;
        }
    }
}
