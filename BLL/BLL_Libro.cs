using BE;
using ORM;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLL_Libro
    {
        ORM_Libro _orm;
        public BLL_Libro()
        {
            _orm = new ORM_Libro();
        }

        public void Agregar(BE_Libro libro)
        {
            CalcularPrecio(libro);
            _orm.Agregar(libro);
        }

        public void Modificar(BE_Libro libro)
        {
            _orm.Modificar(libro);
        }

        public void Eliminar(string codigo)
        {
            _orm.Eliminar(codigo);
        }

        public void Actualizar()
        {
            _orm.Actualizar();
        }

        public void Cancelar()
        {
            _orm.Cancelar();
        }

        public List<BE_Libro> ObtenerLibros()
        {
            foreach (var libro in _orm.ObtenerLibros())
            {
                CalcularPrecio(libro);
            }
            return _orm.ObtenerLibros();
        }

        public List<BE_Libro> DesdeHasta(decimal desde, decimal hasta)
        {
            return _orm.DesdeHasta(desde, hasta);
        }

        public List<BE_Libro> Incremental(string s)
        {
            return _orm.Incremental(s);
        }

        public void CalcularPrecio(BE_Libro libro)
        {
            int dias = (DateTime.Now - libro.FechaPublicacion).Days;
            if (dias > 30) libro.Precio = libro.Precio * 0.8m;
        }

        public DataView dv(string s)
        {
            return _orm.dv(s);
        }
    }
}
