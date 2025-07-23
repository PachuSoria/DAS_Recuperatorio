using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class BE_InsRegular : BE_Inscripcion
    {
        public BE_InsRegular(string codigo, int legajo, string materia, DateTime fechaIns, decimal monto, string tipo) : base(codigo, legajo, materia, fechaIns, monto, "Regular")
        {

        }

        public BE_InsRegular(object[] datos) : base(datos) 
        {

        }
    }
}
