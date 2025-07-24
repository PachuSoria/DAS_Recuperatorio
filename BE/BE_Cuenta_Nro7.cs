using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public abstract class BE_Cuenta_Nro7
    {
        public string Codigo { get; set; }
        public decimal Saldo { get; set; }
        public string Tipo { get; set; }
        public BE_Cuenta_Nro7(string codigo, decimal saldo, string tipo)
        {
            Codigo = codigo;
            Saldo = saldo;
            Tipo = tipo;
        }

        public BE_Cuenta_Nro7(object[] datos) : this(datos[0].ToString(), Convert.ToDecimal(datos[1]), datos[2].ToString())
        {

        }

        public abstract void Depositar(decimal monto);
        public abstract void Extraer(decimal monto);
    }
}
