using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class DAO_Nro4
    {
        DataSet ds;
        public DAO_Nro4()
        {
            if (File.Exists("Datos.xml"))
            {
                ds.ReadXml("Datos.xml");
                ds.AcceptChanges();
            }
            else
            {
                DataTable dt = new DataTable("Cliente");
                dt.Columns.Add("DNI", typeof(string));
                dt.Columns.Add("Nombre", typeof(string));
                dt.Columns.Add("Apellido", typeof(string));
                dt.PrimaryKey = new DataColumn[] { dt.Columns[0] };

                DataTable dt2 = new DataTable("Cuenta");
                dt2.Columns.Add("Codigo", typeof(string));
                dt2.Columns.Add("Saldo", typeof(decimal));
                dt2.Columns.Add("DNICliente", typeof(string));
                dt2.Columns.Add("Tipo", typeof(string));
                dt2.Columns.Add("Descubierto", typeof(decimal));
                dt2.PrimaryKey = new DataColumn[] { dt2.Columns[0] };

                ds.Tables.Add(dt);
                ds.Tables.Add(dt2);

                ds.Relations.Add("Cliente_Cuenta",
                    ds.Tables["Cliente"].Columns["DNI"],
                    ds.Tables["Cuenta"].Columns["DNICliente"]);
            }
        }

        public DataSet RetornarDS()
        {
            return ds;
        }

        public void ActualizarBD()
        {
            ds.AcceptChanges();
            ds.WriteXml("Datos.xml", XmlWriteMode.WriteSchema);
        }
    }
}
