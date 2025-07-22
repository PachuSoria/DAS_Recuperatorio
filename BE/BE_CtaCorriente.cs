using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class BE_CtaCorriente : BE_Cuenta
    {
        public decimal Descubierto { get; set; }
        public BE_CtaCorriente(string codigo, decimal saldo, BE_Cliente cliente, string tipo, decimal descubierto) : base(codigo, saldo, cliente, tipo)
        {
            Descubierto = descubierto;
        }

        public BE_CtaCorriente(object[] datos) : this(datos[0].ToString(), Convert.ToDecimal(datos[1]), new BE_Cliente(datos[2].ToString(), datos[3].ToString(), datos[4].ToString()), datos[5].ToString(), Convert.ToDecimal(datos[6]))
        {
        }

        public override void Depositar(decimal monto)
        {
            Saldo += monto;
        }

        public override void Extraer(decimal monto)
        {
            if ((Saldo + 250) > (Saldo + Descubierto)) throw new Exception($"No puede extraer mas de ${Saldo + Descubierto}");
            else Saldo -= (monto + 250);
        }

        public override void Transferir(decimal saldo, BE_Cuenta c)
        {
            if (saldo > (Saldo + Descubierto)) throw new Exception($"No puede transferir mas de ${Saldo + Descubierto}");
            else
            {
                Saldo -= saldo;
                c.Saldo += saldo;
            }
        }
    }
}
