using BE;
using DAO;
using System.Data;

namespace ORM
{
    public class ORM_Auto
    {
        public DAO_AccesoDatos DaoAcceso;
        DataSet ds;
        public ORM_Auto() 
        {
            DaoAcceso = new DAO_AccesoDatos();
            ds = DaoAcceso.RetornarDS();
        }

        public void Agregar(BE_Auto auto)
        {
            DataRow dr = ds.Tables[0].NewRow();
            dr.ItemArray = new object[] 
            {
                auto.AuPatente, 
                auto.AuFechaIngreso, 
                auto.AuFechaBaja,
                auto.AuAño,
                auto.AuEnUso,
                auto.AuValor
            };
            ds.Tables[0].Rows.Add(dr);
        }

        public void Modificar(BE_Auto auto)
        {
            DataRow dr = ds.Tables[0].Rows.Find(auto.AuPatente);
            dr.ItemArray = new object[]
            {
                auto.AuPatente,
                auto.AuFechaIngreso,
                auto.AuFechaBaja,
                auto.AuAño,
                auto.AuEnUso,
                auto.AuValor
            };
        }

        public void Eliminar(BE_Auto auto)
        {
            ds.Tables[0].Rows.Find(auto.AuPatente).Delete();
        }

        public void Actualizar()
        {
            DaoAcceso.ActualizarBD();
        }

        public void Cancelar()
        {
            ds.RejectChanges();
        }

        public List<BE_Auto> Autos()
        {
            List<BE_Auto> aux = new List<BE_Auto>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                if (dr.RowState != DataRowState.Deleted)
                {
                    aux.Add(new BE_Auto(dr.ItemArray));
                }
            }
            return aux;
        }

        public DataView dv(string s)
        {
            DataView dv;
            if (s == "Agregados") dv = new DataView(ds.Tables[0], "", "", DataViewRowState.Added);
            else if (s == "Eliminados") dv = new DataView(ds.Tables[0], "", "", DataViewRowState.Deleted);
            else if (s == "ModOrig") dv = new DataView(ds.Tables[0], "", "", DataViewRowState.ModifiedOriginal);
            else dv = new DataView(ds.Tables[0], "", "", DataViewRowState.ModifiedCurrent);
            return dv;
        }

        public List<BE_Auto> ConsultaDesdeHasta(BE_Auto auto1, BE_Auto auto2, List<BE_Auto> lista)
        {
            var linq = from x in lista where x.AuValorResidual >= auto1.AuValorResidual && x.AuValorResidual <= auto2.AuValorResidual orderby x.AuValorResidual descending select x;
            return linq.ToList();
        }

        public List<BE_Auto> ConsultaIncremental(BE_Auto auto)
        {
            var linq = from x in Autos() where x.AuPatente.StartsWith(auto.AuPatente) select x;
            return linq.ToList();
        }
    }
}
