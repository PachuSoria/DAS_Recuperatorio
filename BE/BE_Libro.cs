using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class BE_Libro
    {
        public string Codigo { get; set; }
        public string Titulo { get; set; } 
        public string Genero { get; set; }
        public DateTime FechaPublicacion { get; set; }
        public bool EnPrestamo { get; set; }
        public decimal Precio { get; set; }
        public BE_Libro (string codigo, string titulo, string genero, DateTime fechaPublicacion, bool enPrestamo, decimal precio)
        {
            Codigo = codigo;
            Titulo = titulo;
            Genero = genero;
            FechaPublicacion = fechaPublicacion;
            EnPrestamo = enPrestamo;
            Precio = precio;
        }

        public BE_Libro(object[] datos) : this(datos[0].ToString(), datos[1].ToString(), datos[2].ToString(), Convert.ToDateTime(datos[3]), Convert.ToBoolean(datos[4]), Convert.ToDecimal(datos[5]))
        {

        }
    }
}
