using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class BE_Cliente
    {
        public string DNI { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public BE_Cliente(string dni, string nombre, string apellido)
        {
            DNI = dni;
            Nombre = nombre;
            Apellido = apellido;
        }

        public BE_Cliente(object[]datos) : this(datos[0].ToString(), datos[1].ToString(), datos[2].ToString())
        {

        }

    }
}
