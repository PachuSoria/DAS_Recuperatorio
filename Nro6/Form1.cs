using BE;
using BLL;
using System.Text.RegularExpressions;

namespace Nro6
{
    public partial class Form1 : Form
    {
        BLL_Inscripcion _bll;
        public Form1()
        {
            InitializeComponent();
            _bll = new BLL_Inscripcion();
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
                decimal extra = 0;
                string codigo = textBox1.Text;
                if (!Regex.IsMatch(codigo, @"^[A-Z]{3}[0-9]{3}$")) throw new Exception("El codigo no es valido");
                if (_bll.ObtenerInscripciones().Any(x => x.Codigo == codigo)) throw new Exception($"Ya existe una inscripcion con codigo {codigo}");
                int legajo = Convert.ToInt32(textBox2.Text);
                if (legajo <= 0) throw new Exception("El legajo no es valido");
                string materia = textBox3.Text;
                decimal monto = Convert.ToDecimal(textBox4.Text);
                if (monto <= 0) throw new Exception("El monto no es valido");
                DateTime fechaIns = dateTimePicker1.Value;
                if (fechaIns > DateTime.Now) throw new Exception("La fecha no es valida");
                if (string.IsNullOrWhiteSpace(comboBox1.Text)) throw new Exception("Debe seleccionar el tipo");
                BE_Inscripcion inscripcion = null;
                if (comboBox1.Text == "Regular") inscripcion = new BE_InsRegular(codigo, legajo, materia, fechaIns, monto, "Regular");
                else if (comboBox1.Text == "Especial") inscripcion = new BE_InsEspecial(codigo, legajo, materia, fechaIns, monto, "Especial", extra);
                extra = _bll.CalcularMonto(inscripcion);
                if (inscripcion is BE_InsEspecial especial) especial.Extra = extra;
                _bll.Agregar(inscripcion);
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
                if (dataGridView1.Rows.Count == 0) throw new Exception("Debe seleccionar una inscripcion");
                var cod = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                var ins = _bll.ObtenerInscripciones().Find(x => x.Codigo == cod);
                ins.Legajo = Convert.ToInt32(textBox2.Text);
                ins.Materia = textBox3.Text;
                ins.FechaIns = Convert.ToDateTime(textBox4.Text);
                ins.Monto = Convert.ToDecimal(textBox5.Text);
                ins.Tipo = comboBox1.Text;
                _bll.Modificar(ins);
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
                if (dataGridView1.Rows.Count == 0) throw new Exception("Debe seleccionar una inscripcion");
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
                MostrarGrilla();
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
                MostrarGrilla();
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

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        public void MostrarGrilla()
        {
            dataGridView1.DataSource = null; dataGridView1.DataSource = _bll.ObtenerInscripciones();
        }

        public void ActualizarGrillas()
        {
            dataGridView3.DataSource = _bll.dv("Agregados");
            dataGridView4.DataSource = _bll.dv("Eliminados");
            dataGridView5.DataSource = _bll.dv("ModOri");
            dataGridView6.DataSource = _bll.dv("ModAct");
        }
    }
}
