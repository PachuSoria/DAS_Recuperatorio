using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class EventoDeposito_Nro7 : EventArgs
    {
        public string CodCuenta { get; set; }
        public decimal Monto { get; set; }
        public EventoDeposito_Nro7(string codCuenta, decimal monto)
        {
            CodCuenta = codCuenta;
            Monto = monto;
        }
    }
}
