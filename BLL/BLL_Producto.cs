using BE;
using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLL_Producto
    {
        DAL_Producto _dalProducto;
        public BLL_Producto() 
        {
            _dalProducto = new DAL_Producto();
        }

        public void Agregar(BE_Producto producto)
        {
            ValidarProducto(producto);
            _dalProducto.Agregar(producto);
        }

        public void Modificar(BE_Producto producto)
        {
            ValidarProducto(producto);
            _dalProducto.Modificar(producto);
        }

        public void Eliminar(string codigo)
        {
            _dalProducto.Eliminar(codigo);
        }

        public void Actualizar()
        {
            _dalProducto.Actualizar();
        }

        public void ValidarProducto(BE_Producto producto)
        {
            TimeSpan dias = producto.FechaVto - DateTime.Now;
            if (dias.Days <= 15)
            {
                producto.Precio = producto.Precio * 0.8m;
                //DataRow fila = _dalProducto.ObtenerFila(producto.Codigo);
                //if (fila != null) fila["Precio"] = producto.Precio;
            }
        }

        public List<BE_Producto> ObtenerProductos()
        {
            var lista = _dalProducto.ObtenerProductos();
            foreach (var p in lista)
            {
                ValidarProducto(p);
            }
            return lista;
        }

        public List<BE_Producto> DesdeHasta(decimal desde, decimal hasta)
        {
            return _dalProducto.DesdeHasta(desde, hasta);
        }

        public List<BE_Producto> Incremental(string codigo)
        {
            return _dalProducto.Incremental(codigo);
        }

        public DataView dv(string s)
        {
            return _dalProducto.dv(s);
        }

        public void Cancelar()
        {
            _dalProducto.Cancelar();
        }
    }
}
