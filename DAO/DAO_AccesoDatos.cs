using System.Data;

namespace DAO
{
    public class DAO_AccesoDatos
    {
        DataSet ds;
        public DAO_AccesoDatos() 
        {
            ds = new DataSet();
            if (File.Exists("Datos.xml"))
            {
                ds.ReadXml("Datos.xml");
                ds.AcceptChanges();
            }
            else
            {
                DataTable dt = new DataTable("Auto");
                dt.Columns.Add("Patente", typeof(string));
                dt.Columns.Add("FechaIngreso", typeof(DateTime));
                dt.Columns.Add("FechaBaja", typeof (DateTime));
                dt.Columns.Add("Año", typeof(int));
                dt.Columns.Add("EnUso", typeof(bool));
                dt.Columns.Add("Valor", typeof(decimal));
                dt.PrimaryKey = new DataColumn[] {dt.Columns[0]};
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
