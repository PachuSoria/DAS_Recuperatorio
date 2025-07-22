using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class BE_CajaAhorro : BE_Cuenta
    {
        public BE_CajaAhorro(string codigo, decimal saldo, BE_Cliente cliente, string tipo) : base(codigo, saldo, cliente, tipo)
        {
        }

        public override void Depositar(decimal monto)
        {
            Saldo += Saldo;
        }

        public override void Extraer(decimal monto)
        {
            if ((Saldo + 100) > Saldo) throw new Exception($"No puede extraer mas de ${Saldo}");
            else Saldo -= (monto + 100);
        }

        public override void Transferir(decimal saldo, BE_Cuenta c)
        {
            if (saldo > Saldo) throw new Exception($"No puede transferir mas de ${Saldo}");
            else
            {
                Saldo -= saldo;
                c.Saldo += saldo;
            }
        }
    }
}
