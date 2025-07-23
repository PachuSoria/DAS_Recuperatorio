using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class DAO_Nro5
    {
        private SqlConnection cx;
        private SqlDataAdapter adapter;
        private SqlCommandBuilder cb;
        DataSet ds;
        public DAO_Nro5()
        {
            cx = new SqlConnection("Data Source=.;Initial Catalog=Libreria;Integrated Security=True;Trust Server Certificate=True");
            adapter = new SqlDataAdapter("SELECT * FROM Libro", cx);
            cb = new SqlCommandBuilder(adapter);
            adapter.InsertCommand = cb.GetInsertCommand();
            adapter.DeleteCommand = cb.GetDeleteCommand();
            adapter.UpdateCommand = cb.GetUpdateCommand();
            ds = new DataSet();
            adapter.Fill(ds, "Libro");
            ds.Tables["Libro"].PrimaryKey = new DataColumn[] { ds.Tables["Libro"].Columns["Codigo"] };
        }

        public DataSet RetornarDS()
        {
            return ds;
        }

        public void ActualizarBD()
        {
            adapter.Update(ds, "Libro");
        }
    }
}
