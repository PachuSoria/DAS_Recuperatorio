using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class DAO_Nro7
    {
        DataSet ds;
        public DAO_Nro7()
        {
            ds = new DataSet();
            if (File.Exists("Datos.xml"))
            {
                ds.ReadXml("Datos.xml");
                ds.AcceptChanges();
            }
            else
            {
                DataTable dt = new DataTable("Cuenta");
                dt.Columns.Add("Codigo", typeof(string));
                dt.Columns.Add("Saldo", typeof(decimal));
                dt.Columns.Add("Tipo", typeof(string));
                dt.Columns.Add("Descubierto", typeof(decimal));
                dt.PrimaryKey = new DataColumn[] { dt.Columns["Codigo"] };
                ds.Tables.Add(dt);
                ActualizarBD();
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
