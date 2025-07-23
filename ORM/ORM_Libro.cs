using BE;
using DAO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM
{
    public class ORM_Libro
    {
        DAO_Nro5 _dao;
        DataSet ds;
        public ORM_Libro() 
        {
            _dao = new DAO_Nro5();
            ds = _dao.RetornarDS();
        }

        public void Agregar(BE_Libro libro)
        {
            DataRow dr = ds.Tables["Libro"].NewRow();
            dr.ItemArray = new object[]
            {
                libro.Codigo,
                libro.Titulo,
                libro.Genero,
                libro.FechaPublicacion,
                libro.EnPrestamo,
                libro.Precio
            };
            ds.Tables["Libro"].Rows.Add(dr);
        }

        public void Modificar(BE_Libro libro)
        {
            DataRow dr = ds.Tables["Libro"].Rows.Find(libro.Codigo);
            dr.ItemArray = new object[]
            {
                libro.Codigo,
                libro.Titulo,
                libro.Genero,
                libro.FechaPublicacion,
                libro.EnPrestamo,
                libro.Precio
            };
        }

        public void Eliminar(string codigo)
        {
            ds.Tables["Libro"].Rows.Find(codigo).Delete();
        }

        public void Actualizar()
        {
            _dao.ActualizarBD();
        }

        public void Cancelar()
        {
            ds.RejectChanges();
        }

        public List<BE_Libro> ObtenerLibros()
        {
            List<BE_Libro> lista = new List<BE_Libro>();
            foreach (DataRow dr in ds.Tables["Libro"].Rows)
            {
                if (dr.RowState != DataRowState.Deleted)
                {
                    lista.Add(new BE_Libro(dr.ItemArray));
                }
            }
            return lista;
        }

        public List<BE_Libro> DesdeHasta(decimal desde, decimal hasta) 
        {
            var linq = from x in ObtenerLibros() where x.Precio >= desde && x.Precio <= hasta orderby x.Precio descending select x;
            return linq.ToList();
        }

        public List<BE_Libro> Incremental(string s)
        {
            var linq = from x in ObtenerLibros() where x.Codigo.StartsWith(s) select x;
            return linq.ToList();
        }

        public DataView dv(string s)
        {
            DataView dv;
            if (s == "Agregados") dv = new DataView(ds.Tables["Libro"], "", "", DataViewRowState.Added);
            else if (s == "Eliminados") dv = new DataView(ds.Tables["Libro"], "", "", DataViewRowState.Deleted);
            else if (s == "ModOri") dv = new DataView(ds.Tables["Libro"], "", "", DataViewRowState.ModifiedOriginal);
            else dv = new DataView(ds.Tables["Libro"], "", "", DataViewRowState.ModifiedCurrent);
            return dv;
        }
    }
}
