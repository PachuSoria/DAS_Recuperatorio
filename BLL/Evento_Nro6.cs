using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Evento_Nro6 : EventArgs
    {
        public string CodIns { get; set; }
        public int LegEst { get; set; }
        public decimal MontoFin { get; set; }
        public Evento_Nro6(string codIns, int legEst, decimal montoFin)
        {
            CodIns = codIns;
            LegEst = legEst;
            MontoFin = montoFin;
        }
    }
}
