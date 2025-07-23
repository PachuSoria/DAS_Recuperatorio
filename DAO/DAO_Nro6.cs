using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class DAO_Nro6
    {
        DataSet ds;
        public DAO_Nro6() 
        {
            ds = new DataSet();
            if (File.Exists("Datos.xml"))
            {
                ds.ReadXml("Datos.xml");
                ds.AcceptChanges();
            }
            else
            {
                DataTable dt = new DataTable("Inscripcion");
                dt.Columns.Add("Codigo", typeof(string));
                dt.Columns.Add("Legajo", typeof(int));
                dt.Columns.Add("Materia", typeof(string));
                dt.Columns.Add("FechaIns", typeof(DateTime));
                dt.Columns.Add("Monto", typeof(decimal));
                dt.Columns.Add("Tipo", typeof(string));
                dt.Columns.Add("Extra", typeof(decimal));
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
