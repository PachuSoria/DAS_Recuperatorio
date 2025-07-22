using BE;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLL_Cliente
    {
        ORM_Cliente OrmCliente;
        public BLL_Cliente()
        {
            OrmCliente = new ORM_Cliente();
        }

        public void Agregar(BE.BE_Cliente cliente)
        {
            OrmCliente.Agregar(cliente);
        }

        public void Modificar(BE.BE_Cliente cliente)
        {
            OrmCliente.Modificar(cliente);
        }

        public void Eliminar(string dni)
        {
            OrmCliente.Eliminar(dni);
        }

        public void Actualizar()
        {
            OrmCliente.Actualizar();
        }

        public void Cancelar()
        {
            OrmCliente.Cancelar();
        }

        public List<BE.BE_Cliente> ObtenerClientes()
        {
            return OrmCliente.ObtenerClientes();
        }

        public List<BE_Cliente> Incremental(BE_Cliente cliente)
        {
            return OrmCliente.Incremental(cliente);
        }
    }
}
