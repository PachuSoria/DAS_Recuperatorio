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
    public class BLL_Inscripcion
    {
        public EventHandler<Evento_Nro6> MasDe50K;
        ORM_Inscripcion _orm;
        public BLL_Inscripcion()
        {
            _orm = new ORM_Inscripcion();
        }

        public void Agregar(BE_Inscripcion ins)
        {
            CalcularMonto(ins);
            _orm.Agregar(ins);
        }

        public void Modificar(BE_Inscripcion ins)
        {
            CalcularMonto(ins);
            _orm.Modificar(ins);
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

        public List<BE_Inscripcion> ObtenerInscripciones()
        {
            return _orm.ObtenerInscripciones();
        }

        public List<BE_Inscripcion> DesdeHasta(decimal desde, decimal hasta) 
        {
            return _orm.DesdeHasta(desde, hasta);
        }

        public List<BE_Inscripcion> Incremental(string cod)
        {
            return _orm.Incremental(cod);
        }

        public decimal CalcularMonto(BE_Inscripcion ins)
        {
            decimal extra = 0;
            if (ins is BE_InsRegular)
            {
                if (ins.FechaIns < new DateTime(2025, 1, 1)) ins.Monto = ins.Monto * 0.9m;
            }
            else if (ins is BE_InsEspecial)
            {
                extra = ins.Monto * 0.2m;
                ins.Monto += extra;
            }
            return extra;
        }

        public DataView dv(string s)
        {
            return _orm.dv(s);
        }

        public void DesencadenarEvento(BE_Inscripcion ins)
        {
            MasDe50K?.Invoke(this, new Evento_Nro6(ins.Codigo, ins.Legajo, ins.Monto));
        }
    }
}
