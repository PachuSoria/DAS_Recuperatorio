using BE;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLL_Empleado
    {
        DAL_Empleado _dalEmpleado;
        public BLL_Empleado()
        {
            _dalEmpleado = new DAL_Empleado();
        }

        public void Agregar(BE_Empleado empleado)
        {
            _dalEmpleado.GuardarEmpleado(empleado);
        }

        public void Modificar(BE_Empleado empleado)
        {
            _dalEmpleado.ModificarEmpleado(empleado);
        }

        public void Eliminar(string id)
        {
            _dalEmpleado.EliminarEmpleado(id);
        }

        public List<BE_Empleado> ObtenerEmpleados()
        {
            return _dalEmpleado.ObtenerEmpleados();
        }

        public List<BE_Empleado> ConsultaDesdeHasta(decimal salarioMIN, decimal salarioMAX)
        {
            return _dalEmpleado.DesdeHasta(salarioMIN, salarioMAX);
        }

        public List<BE_Empleado> ConsultaIncremental(string apellido)
        {
            return _dalEmpleado.Incremental(apellido);
        }
    }
}
