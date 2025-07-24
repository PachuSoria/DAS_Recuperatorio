using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class BE_CajaAhorro_Nro7 : BE_Cuenta_Nro7
    {
        public BE_CajaAhorro_Nro7(string codigo, decimal saldo, string tipo) : base(codigo, saldo, "Caja ahorro")
        {
        }

        public BE_CajaAhorro_Nro7(object[] datos) : base(datos)
        {

        }

        public override void Depositar(decimal monto)
        {
            Saldo += monto;
        }

        public override void Extraer(decimal monto)
        {
            if ((monto + 100) > Saldo) throw new Exception($"No puede extraer mas de ${Saldo}");
            else Saldo -= (monto + 100);
        }
    }
}
