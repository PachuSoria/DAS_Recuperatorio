using BE;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLL_Cuenta
    {
        ORM_Cuenta OrmCuenta;
        public BLL_Cuenta()
        {
            OrmCuenta = new ORM_Cuenta();
        }

        public void Agregar(BE_Cuenta cuenta)
        {
            OrmCuenta.Agregar(cuenta);
        }

        public void Modificar(BE_Cuenta cuenta)
        {
            OrmCuenta.Modificar(cuenta);
        }

        public void Eliminar(BE_Cuenta cuenta)
        {
            OrmCuenta.Eliminar(cuenta);
        }

        public void Actualizar()
        {
            OrmCuenta.Actualizar();
        }

        public void Cancelar()
        {
            OrmCuenta.Cancelar();
        }

        public List<BE_Cuenta> ObtenerCuentas(List<BE_Cliente> clientes)
        {
            return OrmCuenta.ObtenerCuentas(clientes);
        }

        public List<BE_Cuenta> ObtenerCuentasCliente(string dni)
        {
            return OrmCuenta.ObtenerCuentasCliente(dni);
        }

        public List<BE_Cuenta> DesdeHasta(decimal desde, decimal hasta, List<BE_Cuenta> lista)
        {
            return OrmCuenta.DesdeHasta(desde, hasta, lista);
        }
    }
}
