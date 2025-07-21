using BE;
using Microsoft.Data.SqlClient;

namespace DAL
{
    public class DAL_Empleado
    {
        private string cx;
        public DAL_Empleado()
        {
            cx = "Data Source=.;Initial Catalog=\"Empresa Empleados\";Integrated Security=True;Trust Server Certificate=True";
        }

        public void GuardarEmpleado(BE_Empleado empleado)
        {
            string consulta = "INSERT INTO EMPLEADO (ID, Nombre, Apellido, FechaIngreso, Salario, Activo)" +
                "VALUES (@ID, @Nombre, @Apellido, @FechaIngreso, @Salario, @Activo)";
            using (SqlConnection conexion = new SqlConnection(cx))
            {
                conexion.Open();
                using (SqlCommand comando = new SqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddWithValue("@ID", empleado.ID);
                    comando.Parameters.AddWithValue("@Nombre", empleado.Nombre);
                    comando.Parameters.AddWithValue("@Apellido", empleado.Apellido);
                    comando.Parameters.AddWithValue("@FechaIngreso", empleado.FechaIngreso);
                    comando.Parameters.AddWithValue("@Salario", empleado.Salario);
                    comando.Parameters.AddWithValue("@Activo", empleado.Activo);

                    comando.ExecuteNonQuery();
                }
            }
        }

        public void ModificarEmpleado(BE_Empleado empleado)
        {
            string consulta = "UPDATE EMPLEADO SET Nombre = @Nombre, Apellido = @Apellido, FechaIngreso = @FechaIngreso, Salario = @Salario, Activo = @Activo" +
                "WHERE ID = @ID";
            using (SqlConnection conexion = new SqlConnection(cx))
            {
                conexion.Open();
                using (SqlCommand comando = new SqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddWithValue("@ID", empleado.ID);
                    comando.Parameters.AddWithValue("@Nombre", empleado.Nombre);
                    comando.Parameters.AddWithValue("@Apellido", empleado.Apellido);
                    comando.Parameters.AddWithValue("@FechaIngreso", empleado.FechaIngreso);
                    comando.Parameters.AddWithValue("@Salario", empleado.Salario);
                    comando.Parameters.AddWithValue("@Activo", empleado.Activo);

                    comando.ExecuteNonQuery();
                }
            }
        }

        public void EliminarEmpleado(string id)
        {
            using (SqlConnection conexion = new SqlConnection(cx))
            {
                conexion.Open();
                string consulta = "DELETE FROM EMPLEADO WHERE ID = @ID";
                SqlCommand comando = new SqlCommand(consulta, conexion);
                comando.Parameters.AddWithValue("@ID", id);
                comando.ExecuteNonQuery();
            }
        }

        public List<BE_Empleado> ObtenerEmpleados()
        {
            List<BE_Empleado> lista = new List<BE_Empleado>();
            using (SqlConnection conexion = new SqlConnection(cx))
            {
                conexion.Open();
                string consulta = "SELECT ID, Nombre, Apellido, FechaIngreso, Salario, Activo, FROM EMPLEADO";
                SqlCommand comando = new SqlCommand(consulta, conexion);
                using (SqlDataReader reader = comando.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new BE_Empleado(
                            reader["ID"].ToString(),
                            reader["Nombre"].ToString(),
                            reader["Apellido"].ToString(),
                            Convert.ToDateTime(reader["FechaIngreso"]),
                            Convert.ToDecimal(reader["Salario"]),
                            Convert.ToBoolean(reader["Activo"])
                            ));
                    }
                }
            }
            return lista;
        }

        public List<BE_Empleado> DesdeHasta(decimal salarioMIN, decimal salarioMAX)
        {
            List<BE_Empleado> lista = new List<BE_Empleado>();
            using (SqlConnection conexion = new SqlConnection(cx))
            {
                conexion.Open();
                string consulta = @"SELECT ID, Nombre, Apellido, FechaIngreso, Salario, Activo FROM EMPLEADO WHERE Salario BETWEEN @Min AND @Max ORDER BY Salario DESC";
                SqlCommand comando = new SqlCommand( consulta, conexion);
                comando.Parameters.AddWithValue("@Min", salarioMIN);
                comando.Parameters.AddWithValue("@Max", salarioMAX);
                using (SqlDataReader reader = comando.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new BE_Empleado(
                            reader["ID"].ToString(),
                            reader["Nombre"].ToString(),
                            reader["Apellido"].ToString(),
                            Convert.ToDateTime(reader["FechaIngreso"]),
                            Convert.ToDecimal(reader["Salario"]),
                            Convert.ToBoolean(reader["Activo"])
                            ));
                    }
                }
            }
            return lista;
        }

        public List<BE_Empleado> Incremental(string apellido)
        {
            List<BE_Empleado> lista = new List<BE_Empleado>();
            using (SqlConnection conexion = new SqlConnection(cx))
            {
                conexion.Open();
                string consulta = @"SELECT ID, Nombre, Apellido, FechaIngreso, Salario, Activo FROM EMPLEADO WHERE Apellido LIKE @Prefijo + '%'";
                SqlCommand comando = new SqlCommand(consulta, conexion);
                comando.Parameters.AddWithValue("@Prefijo", apellido);
                using (SqlDataReader reader = comando.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new BE_Empleado(
                            reader["ID"].ToString(),
                            reader["Nombre"].ToString(),
                            reader["Apellido"].ToString(),
                            Convert.ToDateTime(reader["FechaIngreso"]),
                            Convert.ToDecimal(reader["Salario"]),
                            Convert.ToBoolean(reader["Activo"])
                            ));
                    }
                }
            }
            return lista;
        }
    }
}
