using BE;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DAL_Producto
    {
        private SqlConnection conexion;
        private SqlDataAdapter adapter;
        private DataSet ds;
        public DAL_Producto()
        {
            conexion = new SqlConnection("Data Source=.;Initial Catalog=\"Empresa Productos\";Integrated Security=True;Trust Server Certificate=True");
            adapter = new SqlDataAdapter("SELECT * FROM Producto", conexion);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            ds = new DataSet();
            adapter.Fill(ds, "Producto");
            ds.Tables["Producto"].PrimaryKey = new DataColumn[] { ds.Tables["Producto"].Columns["Codigo"] };
        }

        public void Agregar(BE_Producto producto)
        {
            DataRow fila = ds.Tables["Producto"].NewRow();
            fila["Codigo"] = producto.Codigo;
            fila["Nombre"] = producto.Nombre;
            fila["Categoria"] = producto.Categoria;
            fila["Precio"] = producto.Precio;
            fila["Stock"] = producto.Stock;
            fila["FechaVto"] = producto.FechaVto;
            ds.Tables["Producto"].Rows.Add(fila);
        }

        public void Modificar(BE_Producto producto)
        {
            DataRow fila = ds.Tables["Producto"].Rows.Find(producto.Codigo);
            if (fila != null)
            {
                fila["Nombre"] = producto.Nombre;
                fila["Categoria"] = producto.Categoria;
                fila["Precio"] = producto.Precio;
                fila["Stock"] = producto.Stock;
                fila["FechaVto"] = producto.FechaVto;
            }
        }

        public void Eliminar(string codigo)
        {
            DataRow fila = ds.Tables["Producto"].Rows.Find(codigo);
            if (fila != null) fila.Delete();
        }

        public List<BE_Producto> ObtenerProductos()
        {
            List<BE_Producto> lista = new List<BE_Producto>();
            foreach (DataRow fila in ds.Tables["Producto"].Rows)
            {
                if (fila.RowState != DataRowState.Deleted)
                {
                    lista.Add(new BE_Producto(
                        fila["Codigo"].ToString(),
                        fila["Nombre"].ToString(),
                        fila["Categoria"].ToString(),
                        Convert.ToDecimal(fila["Precio"]),
                        Convert.ToInt32(fila["Stock"]),
                        Convert.ToDateTime(fila["FechaVto"])
                        ));
                }
            }
            return lista;
        }

        public void Actualizar()
        {
            adapter.Update(ds, "Producto");
        }

        public List<BE_Producto> DesdeHasta(decimal desde, decimal hasta)
        {
            List<BE_Producto> lista = new List<BE_Producto>();
            foreach (DataRow fila in ds.Tables["Producto"].Rows)
            {
                if (fila.RowState != DataRowState.Deleted)
                {
                    decimal precio = Convert.ToDecimal(fila["Precio"]);
                    if (precio >= desde && precio <= hasta)
                    {
                        lista.Add(new BE_Producto(
                        fila["Codigo"].ToString(),
                        fila["Nombre"].ToString(),
                        fila["Categoria"].ToString(),
                        precio,
                        Convert.ToInt32(fila["Stock"]),
                        Convert.ToDateTime(fila["FechaVto"])
                        ));
                    }
                }
            }
            return lista;
        }

        public List<BE_Producto> Incremental(string iniCodigo)
        {
            List<BE_Producto> lista = new List<BE_Producto>();
            foreach (DataRow fila in ds.Tables["Producto"].Rows)
            {
                if (fila.RowState != DataRowState.Deleted)
                {
                    string codigo = fila["Codigo"].ToString();
                    if (codigo.StartsWith(iniCodigo))
                    {
                        lista.Add(new BE_Producto(
                        codigo,
                        fila["Nombre"].ToString(),
                        fila["Categoria"].ToString(),
                        Convert.ToDecimal(fila["Precio"]),
                        Convert.ToInt32(fila["Stock"]),
                        Convert.ToDateTime(fila["FechaVto"])
                        ));
                    }
                }
            }
            return lista;
        }

        public DataView dv(string s)
        {
            DataView dv;
            if (s == "Agregados") dv = new DataView(ds.Tables["Producto"], "", "", DataViewRowState.Added);
            else if (s == "Eliminados") dv = new DataView(ds.Tables["Producto"], "", "", DataViewRowState.Deleted);
            else if (s == "ModOri") dv = new DataView(ds.Tables["Producto"], "", "", DataViewRowState.ModifiedOriginal);
            else dv = new DataView(ds.Tables["Producto"], "", "", DataViewRowState.ModifiedCurrent);
            return dv;
        }

        public void Cancelar()
        {
            ds.RejectChanges();
        }

        //public DataRow ObtenerFila(string codigo)
        //{
        //    return ds.Tables["Producto"].Rows.Find(codigo);
        //}
    }
}
