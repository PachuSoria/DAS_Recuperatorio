using BE;
using DAO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM
{
    public class ORM_Inscripcion
    {
        DAO_Nro6 _dao;
        DataSet ds;
        public ORM_Inscripcion()
        {
            _dao = new DAO_Nro6();
            ds = _dao.RetornarDS();
        }

        public void Agregar(BE_Inscripcion inscripcion)
        {
            DataRow dr = ds.Tables["Inscripcion"].NewRow();
            dr["Codigo"] = inscripcion.Codigo;
            dr["Legajo"] = inscripcion.Legajo;
            dr["Materia"] = inscripcion.Materia;
            dr["FechaIns"] = inscripcion.FechaIns;
            dr["Monto"] = inscripcion.Monto;
            dr["Tipo"] = inscripcion.Tipo;
            
            if (inscripcion is BE_InsEspecial especial) dr["Extra"] = especial.Extra;
            else dr["Extra"] = DBNull.Value;
            ds.Tables["Inscripcion"].Rows.Add(dr);
        }

        public void Modificar(BE_Inscripcion inscripcion)
        {
            DataRow dr = ds.Tables["Inscripcion"].Rows.Find(inscripcion.Codigo);
            dr["Codigo"] = inscripcion.Codigo;
            dr["Legajo"] = inscripcion.Legajo;
            dr["Materia"] = inscripcion.Materia;
            dr["FechaIns"] = inscripcion.FechaIns;
            dr["Monto"] = inscripcion.Monto;
            dr["Tipo"] = inscripcion.Tipo;

            if (inscripcion is BE_InsEspecial especial) dr["Extra"] = especial.Extra;
            else dr["Extra"] = DBNull.Value;
        }

        public void Eliminar(string codigo)
        {
            ds.Tables["Inscripcion"].Rows.Find(codigo).Delete();
        }

        public void Actualizar()
        {
            _dao.ActualizarBD();
        }

        public void Cancelar()
        {
            ds.RejectChanges();
        }

        public List<BE_Inscripcion> ObtenerInscripciones()
        {
            List<BE_Inscripcion> lista = new List<BE_Inscripcion>();
            foreach (DataRow dr in ds.Tables["Inscripcion"].Rows)
            {
                if (dr.RowState != DataRowState.Deleted)
                {
                    string tipo = dr["Tipo"].ToString();
                    BE_Inscripcion ins;
                    if (tipo == "Especial") ins = new BE_InsEspecial(dr.ItemArray);
                    else ins = new BE_InsRegular(dr.ItemArray);
                    lista.Add(ins);
                }
            }
            return lista;
        }

        public List<BE_Inscripcion> DesdeHasta(decimal desde, decimal hasta)
        {
            var linq = from x in ObtenerInscripciones() where x.Monto >= desde && x.Monto <= hasta orderby x.Monto descending select x;
            return linq.ToList();
        }

        public List<BE_Inscripcion> Incremental(string s)
        {
            var linq = from x in ObtenerInscripciones() where x.Codigo.StartsWith(s) select x;
            return linq.ToList();
        }

        public DataView dv(string s)
        {
            DataView dv = new DataView();
            if (s == "Agregados") dv = new DataView(ds.Tables["Inscripcion"], "", "", DataViewRowState.Added);
            else if (s == "Eliminados") dv = new DataView(ds.Tables["Inscripcion"], "", "", DataViewRowState.Deleted);
            else if (s == "ModOri") dv = new DataView(ds.Tables["Inscripcion"], "", "", DataViewRowState.ModifiedOriginal);
            else dv = new DataView(ds.Tables["Inscripcion"], "", "", DataViewRowState.ModifiedCurrent);
            return dv;
        }
    }
}
