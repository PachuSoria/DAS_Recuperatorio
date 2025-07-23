using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public abstract class BE_Inscripcion
    {
        public string Codigo { get; set; }
        public int Legajo { get; set; }
        public string Materia { get; set; }
        public DateTime FechaIns {  get; set; }
        public decimal Monto { get; set; }
        public string Tipo { get; set; }
        public BE_Inscripcion(string codigo, int legajo, string materia, DateTime fechaIns, decimal monto, string tipo) 
        {
            Codigo = codigo;
            Legajo = legajo;
            Materia = materia;
            FechaIns = fechaIns;
            Monto = monto;
            Tipo = tipo;
        }

        public BE_Inscripcion(object[] datos) : this(datos[0].ToString(), Convert.ToInt32(datos[1]), datos[2].ToString(), Convert.ToDateTime(datos[3]), Convert.ToDecimal(datos[4]), datos[5].ToString()) 
        {

        }
    }
}
