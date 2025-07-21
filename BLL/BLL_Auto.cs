using BE;
using ORM;
using System.Data;

namespace BLL
{
    public class BLL_Auto
    {
        ORM_Auto OrmAuto;
        public BLL_Auto()
        {
            OrmAuto = new ORM_Auto();
        }

        public void Agregar(BE_Auto auto)
        {
            CalcularDatos(auto);
            OrmAuto.Agregar(auto);
        }

        public void Modificar(BE_Auto auto)
        {
            OrmAuto.Modificar(auto);
        }

        public void Eliminar(BE_Auto auto)
        {
            OrmAuto.Eliminar(auto);
        }

        public void Actualizar()
        {
            OrmAuto.Actualizar();
        }

        public void Cancelar()
        {
            OrmAuto.Cancelar();
        }

        public void CalcularDatos(BE_Auto auto)
        {
            decimal cuenta1 = DateTime.Now.Year - auto.AuAño;
            decimal cuenta2 = auto.AuValor * 0.1m;
            if (auto.AuValor - cuenta2 * cuenta1 < 0) auto.AuValorResidual = 0;
            else auto.AuValorResidual = auto.AuValor - cuenta2 * cuenta1;

            if (auto.AuFechaBaja != Convert.ToDateTime("01/01/2999"))
            {
                TimeSpan aux = auto.AuFechaBaja - auto.AuFechaIngreso;
                auto.AuDias = aux.Days;
            }
            else
            {
                TimeSpan aux = DateTime.Now - auto.AuFechaIngreso;
                auto.AuDias = aux.Days;
            }
        }

        public List<BE_Auto> Autos()
        {
            var lista = OrmAuto.Autos();
            foreach (var item in lista)
            {
                CalcularDatos(item);
            }
            return lista;
        }

        public DataView dv(string s)
        {
            return OrmAuto.dv(s);
        }

        public List<BE_Auto> ConsultaDesdeHasta(BE_Auto auto1, BE_Auto auto2, List<BE_Auto> lista)
        {
            return OrmAuto.ConsultaDesdeHasta(auto1, auto2, lista);
        }

        public List<BE_Auto> ConsultaIncremental(BE_Auto auto)
        {
            return OrmAuto.ConsultaIncremental(auto);
        }
    }
}
