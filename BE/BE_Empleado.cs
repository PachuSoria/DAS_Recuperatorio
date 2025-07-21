using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class BE_Empleado
    {
        public string ID { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime FechaIngreso { get; set; }
        public decimal Salario { get; set; }
        public bool Activo { get; set; }
        public BE_Empleado(string id, string nombre, string apellido, DateTime fechaIngreso, decimal salario, bool activo) 
        {
            ID = id;
            Nombre = nombre;
            Apellido = apellido;
            FechaIngreso = fechaIngreso;
            Salario = salario;
            Activo = activo;
        }
    }
}
