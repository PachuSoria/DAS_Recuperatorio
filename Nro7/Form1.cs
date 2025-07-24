using BE;
using BLL;
using System.Text.RegularExpressions;

namespace Nro7
{
    public partial class Form1 : Form
    {
        BLL_Nro7 _bll;
        public Form1()
        {
            InitializeComponent();
            _bll = new BLL_Nro7();
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
                decimal descubierto = 0;
                if (textBox1.Text.Length == 0) throw new Exception("Debe ingresar el codigo");
                string codigo = textBox1.Text;
                if (!Regex.IsMatch(codigo, @"^[A-Z]{3}[0-9]{3}$")) throw new Exception("El codigo no es valido");
                if (_bll.ObtenerCuentas().Any(x => x.Codigo == codigo)) throw new Exception($"Ya existe una cuenta con el codigo {codigo}");
                if (textBox2.Text.Length == 0) throw new Exception("Debe ingresar el saldo");
                decimal saldo = Convert.ToDecimal(textBox2.Text);
                if (saldo < 0) throw new Exception("El saldo no es valido");
                if (string.IsNullOrWhiteSpace(comboBox1.Text)) throw new Exception("Debe seleccionar el tipo");
                BE_Cuenta_Nro7 cuenta = null;
                if (comboBox1.Text == "Caja ahorro")
                {
                    cuenta = new BE_CajaAhorro_Nro7(codigo, saldo, "Caja ahorro");
                }
                else if (comboBox1.Text == "Cuenta corriente")
                {
                    descubierto = Convert.ToDecimal(textBox6.Text);
                    cuenta = new BE_CuentaCorr_Nro7(codigo, saldo, "Cuenta corriente", descubierto);
                }
                _bll.Agregar(cuenta);
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
                if (dataGridView1.Rows.Count == 0) throw new Exception("Debe seleccionar una cuenta");
                var cod = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                var cuenta = _bll.ObtenerCuentas().Find(x => x.Codigo == cod);
                cuenta.Saldo = Convert.ToDecimal(textBox2.Text);
                cuenta.Tipo = comboBox1.Text;
                _bll.Modificar(cuenta);
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
                if (dataGridView1.Rows.Count == 0) throw new Exception("Debe seleccionar una cuenta");
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
                if (textBox4.Text.Length == 0) throw new Exception("Debe ingresar el valor 'Desde'");
                decimal desde = Convert.ToDecimal(textBox4.Text);
                if (textBox5.Text.Length == 0) throw new Exception("Debe ingresar el valor 'Hasta'");
                decimal hasta = Convert.ToDecimal(textBox5.Text);
                if (desde > hasta) throw new Exception("Los valores no son validos");
                _bll.DesdeHasta(desde, hasta);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox2.Text.Length == 0) throw new Exception("Debe ingresar el monto");
                decimal monto = Convert.ToDecimal(textBox2.Text);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button8_Click(object sender, EventArgs e)
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
            dataGridView1.DataSource = null; dataGridView1.DataSource = _bll.ObtenerCuentas();
        }

        public void ActualizarGrillas()
        {
            dataGridView3.DataSource = _bll.dv("Agregados");
            dataGridView4.DataSource = _bll.dv("Eliminados");
            dataGridView5.DataSource = _bll.dv("ModOri");
            dataGridView6.DataSource = _bll.dv("ModAct");
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            string s = textBox3.Text;
            if (dataGridView1.Rows.Count > 0) _bll.Incremental(s);
        }

        private void Desencadenar(object sender, EventArgs e)
        {
            
        }
    }
}
