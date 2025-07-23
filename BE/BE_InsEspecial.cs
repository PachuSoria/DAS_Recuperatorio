using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class BE_InsEspecial : BE_Inscripcion
    {
        public decimal Extra {  get; set; }
        public BE_InsEspecial(string codigo, int legajo, string materia, DateTime fechaIns, decimal monto, string tipo, decimal extra) : base(codigo, legajo, materia, fechaIns, monto, "Especial")
        {
            Extra = extra;
        }

        public BE_InsEspecial(object[] datos) : base(datos) 
        {
            Extra = Convert.ToDecimal(datos[6]);
        }
    }
}
