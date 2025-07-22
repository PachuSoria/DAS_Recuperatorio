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
    public class ORM_Cliente
    {
        public DAO_Nro4 DaoAcceso;
        DataSet ds;
        public ORM_Cliente()
        {
            DaoAcceso = new DAO_Nro4();
            ds = DaoAcceso.RetornarDS();
        }

        public void Agregar(BE_Cliente cliente)
        {
            DataRow dr = ds.Tables["Cliente"].NewRow();
            dr.ItemArray = new object[]
            {
                cliente.DNI,
                cliente.Nombre,
                cliente.Apellido
            };
            ds.Tables["Cliente"].Rows.Add(dr);
        }

        public void Modificar(BE_Cliente cliente)
        {
            DataRow dr = ds.Tables["Cliente"].Rows.Find(cliente.DNI);
            dr.ItemArray = new object[]
            {
                cliente.DNI,
                cliente.Nombre,
                cliente.Apellido
            };
        }

        public void Eliminar(string dni)
        {
            ds.Tables["Cliente"].Rows.Find(dni).Delete();
        }

        public void Actualizar()
        {
            DaoAcceso.ActualizarBD();
        }

        public void Cancelar()
        {
            ds.RejectChanges();
        }

        public List<BE_Cliente> ObtenerClientes()
        {
            List<BE_Cliente> lista = new List<BE_Cliente>();
            foreach (DataRow dr in ds.Tables["Cliente"].Rows)
            {
                if (dr.RowState != DataRowState.Deleted)
                {
                    lista.Add(new BE_Cliente(dr.ItemArray));
                }
            }
            return lista;
        }

        public List<BE_Cliente> Incremental(BE_Cliente cliente)
        {
            var linq = from x in ObtenerClientes() where x.DNI.StartsWith(cliente.DNI) select x;
            return linq.ToList();
        }
    }
}
