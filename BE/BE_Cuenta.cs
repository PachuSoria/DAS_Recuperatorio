using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public abstract class BE_Cuenta
    {
        public string Codigo { get; set; }
        public decimal Saldo { get; set; }
        public BE_Cliente Cliente { get; set; }
        public string Tipo { get; set; }
        public BE_Cuenta(string codigo, decimal saldo, BE_Cliente cliente, string tipo)
        {
            Codigo = codigo;
            Saldo = saldo;
            Cliente = cliente;
            Tipo = tipo;
        }

        public BE_Cuenta(object[] datos) : this(datos[0].ToString(), Convert.ToDecimal(datos[1]), new BE_Cliente(datos[2].ToString(), datos[3].ToString(), datos[4].ToString()), datos[5].ToString())
        {

        }

        public class OrdenasAsc : IComparer<BE_Cuenta>
        {
            public int Compare(BE_Cuenta x, BE_Cuenta y)
            {
                return x.Saldo.CompareTo(y.Saldo);
            }
        }

        public class OrdenasDesc : IComparer<BE_Cuenta>
        {
            public int Compare(BE_Cuenta x, BE_Cuenta y)
            {
                return y.Saldo.CompareTo(x.Saldo);
            }
        }

        public abstract void Depositar(decimal monto);
        public abstract void Extraer(decimal monto);
        public abstract void Transferir(decimal saldo, BE_Cuenta c);
    }
}
