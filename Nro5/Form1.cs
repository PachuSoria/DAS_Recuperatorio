using BLL;
using Microsoft.IdentityModel.Tokens;
using System.Text.RegularExpressions;
using BE;

namespace Nro5
{
    public partial class Form1 : Form
    {
        BLL_Libro _bll;
        public Form1()
        {
            InitializeComponent();
            _bll = new BLL_Libro();
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect; dataGridView1.MultiSelect = false; dataGridView1.ReadOnly = true;
            dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect; dataGridView2.MultiSelect = false; dataGridView2.ReadOnly = true;
            dataGridView3.SelectionMode = DataGridViewSelectionMode.FullRowSelect; dataGridView3.MultiSelect = false; dataGridView3.ReadOnly = true;
            dataGridView4.SelectionMode = DataGridViewSelectionMode.FullRowSelect; dataGridView4.MultiSelect = false; dataGridView4.ReadOnly = true;
            dataGridView5.SelectionMode = DataGridViewSelectionMode.FullRowSelect; dataGridView5.MultiSelect = false; dataGridView5.ReadOnly = true;
            dataGridView6.SelectionMode = DataGridViewSelectionMode.FullRowSelect; dataGridView6.MultiSelect = false; dataGridView6.ReadOnly = true;
            MostrarGrillaLibros();
            ActualizarGrillas();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text.Length == 0) throw new Exception("Debe ingresar el codigo");
                string codigo = textBox1.Text;
                if (!Regex.IsMatch(codigo, @"^[A-Z]{3}[0-9]{3}$")) throw new Exception("El codigo no es valido");
                if (_bll.ObtenerLibros().Any(x => x.Codigo == codigo)) throw new Exception($"Ya existe un libro con codigo {codigo}");
                if (textBox2.Text.Length == 0) throw new Exception("Debe ingresar el titulo");
                string titulo = textBox2.Text;
                if (textBox3.Text.Length == 0) throw new Exception("Debe ingresar el genero");
                string genero = textBox3.Text;
                if (textBox4.Text.Length == 0) throw new Exception("Debe ingresar el precio");
                decimal precio = Convert.ToDecimal(textBox4.Text);
                if (precio <= 0) throw new Exception("El precio no es valido");
                bool enPrestamo = true;
                if (comboBox1.Text == "Si") enPrestamo = true;
                else if (comboBox1.Text == "No") enPrestamo = false;
                if (string.IsNullOrWhiteSpace(comboBox1.Text)) throw new Exception("Debe seleccionar si esta en prestamo o no");
                DateTime fechaPublicacion = dateTimePicker1.Value;
                if (fechaPublicacion > DateTime.Now) throw new Exception("La fecha no es valida");
                _bll.Agregar(new BE_Libro(codigo, titulo, genero, fechaPublicacion, enPrestamo, precio));
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
                if (dataGridView1.Rows.Count == 0) throw new Exception("Debe seleccionar un libro");
                var cod = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                var libro = _bll.ObtenerLibros().Find(x => x.Codigo == cod);
                libro.Titulo = textBox2.Text;
                libro.Genero = textBox3.Text;
                libro.FechaPublicacion = dateTimePicker1.Value;
                libro.EnPrestamo = comboBox1.Text == "Si";
                libro.Precio = Convert.ToDecimal(textBox4.Text);
                _bll.Modificar(libro);
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
                if (dataGridView1.Rows.Count == 0) throw new Exception("Debe seleccionar un libro");
                var cod = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                _bll.Eliminar(cod);
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
                _bll.Actualizar();
                MostrarGrillaLibros();
                ActualizarGrillas();
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
                _bll.Cancelar();
                MostrarGrillaLibros();
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
                if (textBox6.Text.Length == 0) throw new Exception("Debe ingresar el valor 'Desde'");
                decimal desde = Convert.ToDecimal(textBox6.Text);
                if (textBox7.Text.Length == 0) throw new Exception("Debe ingresar el valor 'Hasta'");
                decimal hasta = Convert.ToDecimal(textBox7.Text);
                if (desde < 0 || desde > hasta) throw new Exception("Los valores no son validos");
                var resultado = _bll.DesdeHasta(desde, hasta);
                dataGridView2.DataSource = null; dataGridView2.DataSource = resultado;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void MostrarGrillaLibros()
        {
            dataGridView1.DataSource = null; dataGridView1.DataSource = _bll.ObtenerLibros();
        }

        public void ActualizarGrillas()
        {
            dataGridView3.DataSource = _bll.dv("Agregados");
            dataGridView4.DataSource = _bll.dv("Eliminados");
            dataGridView5.DataSource = _bll.dv("ModOri");
            dataGridView6.DataSource = _bll.dv("ModAct");
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            string s = textBox5.Text;
            dataGridView2.DataSource = null; dataGridView2.DataSource = _bll.Incremental(s);
        }
    }
}
