using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class BE_CuentaCorr_Nro7 : BE_Cuenta_Nro7
    {
        public decimal Descubierto { get; set; }
        public BE_CuentaCorr_Nro7(string codigo, decimal saldo, string tipo, decimal descubierto) : base(codigo, saldo, "Cuenta corriente")
        {
            Descubierto = descubierto;
        }

        public BE_CuentaCorr_Nro7(object[] datos) : base(datos)
        {
            Descubierto = Convert.ToDecimal(datos[3]);
        }

        public override void Depositar(decimal monto)
        {
            Saldo += monto;
        }

        public override void Extraer(decimal monto)
        {
            if ((monto + 250) > (Saldo + Descubierto)) throw new Exception($"No puede extraer mas de ${Saldo + Descubierto}");
            else Saldo -= (monto + 250);
        }
    }
}
