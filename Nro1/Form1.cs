using BE;
using BLL;
using System.Text.RegularExpressions;

namespace Nro1
{
    public partial class Form1 : Form
    {
        BLL_Auto BllAuto;
        public event EventHandler evento;
        public Form1()
        {
            InitializeComponent();
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect; dataGridView1.MultiSelect = false; dataGridView1.ReadOnly = true;
            dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect; dataGridView2.MultiSelect = false; dataGridView2.ReadOnly = true;
            dataGridView3.SelectionMode = DataGridViewSelectionMode.FullRowSelect; dataGridView3.MultiSelect = false; dataGridView3.ReadOnly = true;
            dataGridView4.SelectionMode = DataGridViewSelectionMode.FullRowSelect; dataGridView4.MultiSelect = false; dataGridView4.ReadOnly = true;
            dataGridView5.SelectionMode = DataGridViewSelectionMode.FullRowSelect; dataGridView5.MultiSelect = false; dataGridView5.ReadOnly = true;
            dataGridView6.SelectionMode = DataGridViewSelectionMode.FullRowSelect; dataGridView6.MultiSelect = false; dataGridView6.ReadOnly = true;
            BllAuto = new BLL_Auto();
            MostrarGrilla();
            ActualizarGrillas();
            evento += Desencadenar;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text.Length == 0) throw new Exception("Debe ingresar la patente");
                string patente = textBox1.Text;
                if (!Regex.IsMatch(patente, @"^[A-Z]{2}[0-9]{3}[A-Z]{3}$"))
                {
                    evento?.Invoke(this, new EventArgs());
                    return;
                }
                if (BllAuto.Autos().Any(x => x.AuPatente == patente)) throw new Exception($"Ya existe un auto con la patente {patente}");
                if (textBox2.Text.Length == 0) throw new Exception("Debe ingresar el año");
                int año = Convert.ToInt32(textBox2.Text);
                if (año > DateTime.Now.Year) throw new Exception("El año no es valido");
                if (textBox3.Text.Length == 0) throw new Exception("El valor no es valido");
                decimal valor = Convert.ToDecimal(textBox3.Text);
                if (valor <= 0) throw new Exception("El valor no es valido");
                DateTime fechaIngreso = dateTimePicker1.Value;
                if (fechaIngreso > DateTime.Now) throw new Exception("La fecha de ingreso no es valida");
                DateTime fechaBaja = dateTimePicker2.Value;
                bool uso = false;
                if (comboBox1.Text == "Uso")
                {
                    uso = true;
                    fechaBaja = Convert.ToDateTime("01/01/2999");
                }
                else if (comboBox1.Text == "Baja")
                {
                    uso = false;
                    fechaBaja = dateTimePicker2.Value;
                    if (fechaBaja < fechaIngreso) throw new Exception("La fecha de baja no es valida");
                }
                if (uso == null) throw new Exception("Debe ingresar el estado");
                BllAuto.Agregar(new BE_Auto(patente, fechaIngreso, fechaBaja, año, uso, valor));
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
                if (dataGridView1.Rows.Count == 0) throw new Exception("Debe seleccionar un auto");
                var pat = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                var auto = BllAuto.Autos().Find(x => x.AuPatente == pat);
                auto.AuAño = Convert.ToInt32(textBox2.Text);
                auto.AuValor = Convert.ToDecimal(textBox3.Text);
                auto.AuFechaIngreso = dateTimePicker1.Value;
                auto.AuEnUso = false;
                auto.AuFechaBaja = dateTimePicker2.Value;
                BllAuto.Modificar(auto);
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
                if (dataGridView1.Rows.Count == 0) throw new Exception("Debe seleccionar un auto");
                var pat = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                var auto = BllAuto.Autos().Find(x => x.AuPatente == pat);
                auto.AuEnUso = false;
                auto.AuFechaBaja = dateTimePicker2.Value;
                BllAuto.Eliminar(auto);
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
                if (dataGridView1.Rows.Count == 0) throw new Exception("Debe seleccionar un auto");
                var pat = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                var auto = BllAuto.Autos().Find(x => x.AuPatente == pat);
                BllAuto.Eliminar(auto);
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
                BllAuto.Actualizar();
                MostrarGrilla();
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
                if (textBox5.Text.Length == 0) throw new Exception("Debe ingresar el valor 'Desde'");
                decimal desde = Convert.ToDecimal(textBox5.Text);
                if (textBox6.Text.Length == 0) throw new Exception("Debe ingresar el valor 'Hasta'");
                decimal hasta = Convert.ToDecimal(textBox6.Text);
                if (desde < 0 || hasta < desde) throw new Exception("Los valores no son validos");
                BE_Auto auto1 = new BE_Auto(desde);
                BE_Auto auto2 = new BE_Auto(hasta);
                List<BE_Auto> lista = BllAuto.Autos();
                dataGridView2.DataSource = null; dataGridView2.DataSource = BllAuto.ConsultaDesdeHasta(auto1, auto2, lista);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void MostrarGrilla()
        {
            dataGridView1.DataSource = null; dataGridView1.DataSource = BllAuto.Autos();
        }

        private void ActualizarGrillas()
        {
            dataGridView3.DataSource = BllAuto.dv("Agregados");
            dataGridView4.DataSource = BllAuto.dv("Eliminados");
            dataGridView5.DataSource = BllAuto.dv("ModOrig");
            dataGridView6.DataSource = BllAuto.dv("ModAct");
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            string s = textBox4.Text;
            BE_Auto auto = new BE_Auto(s);
            dataGridView2.DataSource = null; dataGridView2.DataSource = BllAuto.ConsultaIncremental(auto);
        }

        private void Desencadenar(object sender, EventArgs e)
        {
            throw new Exception("La patente es incorrecta");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                BllAuto.Cancelar();
                MostrarGrilla();
                ActualizarGrillas();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
