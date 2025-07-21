using BLL;
using System.Net.Sockets;
using System.Text.RegularExpressions;

namespace Nro2
{
    public partial class Form1 : Form
    {
        BLL_Empleado _bllEmpleado;
        public Form1()
        {
            InitializeComponent();
            _bllEmpleado = new BLL_Empleado();
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect; dataGridView1.MultiSelect = false; dataGridView1.ReadOnly = true;
            dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect; dataGridView2.MultiSelect = false; dataGridView2.ReadOnly = true;
            MostrarGrilla();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text.Length == 0) throw new Exception("Debe ingresar el ID");
                string id = textBox1.Text;
                if (_bllEmpleado.ObtenerEmpleados().Any(x => x.ID == id)) throw new Exception($"Ya hay un empleado con ID {id}");
                if (!Regex.IsMatch(id, @"^[A-Z]{2}[0-9]{3}[A-Z]{3}$")) throw new Exception("El ID no es valido");
                if (textBox2.Text.Length == 0) throw new Exception("Debe ingresar el nombre");
                string nombre = textBox2.Text;
                if (textBox3.Text.Length == 0) throw new Exception("Debe ingresar el apellido");
                string apellido = textBox3.Text;
                DateTime fechaIngreso = dateTimePicker1.Value;
                if (fechaIngreso > DateTime.Now) throw new Exception("La fecha ingreso no es valida");
                if (textBox4.Text.Length == 0) throw new Exception("Debe ingresar el salario");
                decimal salario = Convert.ToDecimal(textBox4.Text);
                if (salario <= 0) throw new Exception("El salario no es valido");
                bool activo = false;
                if (comboBox1.Text == "Activo") activo = true;
                else if (comboBox1.Text == "Inactivo") activo = false;
                if (activo == null) throw new Exception("Debe seleccionar el estado");
                _bllEmpleado.Agregar(new BE.BE_Empleado(id, nombre, apellido, fechaIngreso, salario, activo));
                MostrarGrilla();
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

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void MostrarGrilla()
        {
            dataGridView1.DataSource = null; dataGridView1.DataSource = _bllEmpleado.ObtenerEmpleados();
        }
    }
}
