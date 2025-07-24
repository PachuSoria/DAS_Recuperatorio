using BE;
using ORM;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLL_Nro7
    {
        ORM_Nro7 _orm;
        public EventHandler<EventoDeposito_Nro7> MasDe10K;
        public BLL_Nro7()
        {
            _orm = new ORM_Nro7();
        }

        public void Agregar(BE_Cuenta_Nro7 c)
        {
            _orm.Agregar(c);
        }

        public void Modificar(BE_Cuenta_Nro7 c)
        {
            _orm.Modificar(c);
        }

        public void Eliminar(string cod)
        {
            _orm.Eliminar(cod);
        }

        public void Actualizar()
        {
            _orm.Actualizar();
        }

        public void Cancelar()
        {
            _orm.Cancelar();
        }

        public List<BE_Cuenta_Nro7> ObtenerCuentas()
        {
            return _orm.ObtenerCuentas();
        }

        public List<BE_Cuenta_Nro7> DesdeHasta(decimal d, decimal h)
        {
            return _orm.DesdeHasta(d, h);
        }

        public List<BE_Cuenta_Nro7> Incremental(string s)
        {
            return _orm.Incremental(s);
        }

        public DataView dv(string s)
        {
            return _orm.dv(s);
        }

        public void DesencadenarEvento(BE_Cuenta_Nro7 c)
        {
            MasDe10K?.Invoke(this, new EventoDeposito_Nro7(c.Codigo, c.Saldo));
        }
    }
}
