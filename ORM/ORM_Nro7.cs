using BE;
using DAO;
using Microsoft.IdentityModel.Tokens.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM
{
    public class ORM_Nro7
    {
        DataSet ds;
        DAO_Nro7 _dao;
        public ORM_Nro7()
        {
            _dao = new DAO_Nro7();
            ds = _dao.RetornarDS();
        }

        public void Agregar(BE_Cuenta_Nro7 cuenta)
        {
            DataRow dr = ds.Tables["Cuenta"].NewRow();
            dr["Codigo"] = cuenta.Codigo;
            dr["Saldo"] = cuenta.Saldo;
            dr["Tipo"] = cuenta.Tipo;

            if (cuenta is BE_CuentaCorr_Nro7 ctaCorr) dr["Descubierto"] = ctaCorr.Descubierto;
            else dr["Descubierto"] = DBNull.Value;
            ds.Tables["Cuenta"].Rows.Add(dr);
        }

        public void Modificar(BE_Cuenta_Nro7 cuenta)
        {
            DataRow dr = ds.Tables["Cuenta"].Rows.Find(cuenta.Codigo);
            dr["Codigo"] = cuenta.Codigo;
            dr["Saldo"] = cuenta.Saldo;
            dr["Tipo"] = cuenta.Tipo;

            if (cuenta is BE_CuentaCorr_Nro7 ctaCorr) dr["Descubierto"] = ctaCorr.Descubierto;
            else dr["Descubierto"] = DBNull.Value;
        }

        public void Eliminar(string cod)
        {
            ds.Tables["Cuenta"].Rows.Find(cod).Delete();
        }

        public void Actualizar()
        {
            _dao.ActualizarBD();
        }

        public void Cancelar()
        {
            ds.RejectChanges();
        }

        public List<BE_Cuenta_Nro7> ObtenerCuentas()
        {
            List<BE_Cuenta_Nro7> lista = new List<BE_Cuenta_Nro7>();
            foreach (DataRow dr in ds.Tables["Cuenta"].Rows)
            {
                if (dr.RowState != DataRowState.Deleted)
                {
                    string tipo = dr["Tipo"].ToString();
                    BE_Cuenta_Nro7 cuenta;
                    if (tipo == "Cuenta corriente") cuenta = new BE_CuentaCorr_Nro7(dr.ItemArray);
                    else cuenta = new BE_CajaAhorro_Nro7(dr.ItemArray);
                }
            }
            return lista;
        }

        public List<BE_Cuenta_Nro7> DesdeHasta(decimal desde, decimal hasta)
        {
            var linq = from x in ObtenerCuentas() where x.Saldo >= desde && x.Saldo <= hasta orderby x.Saldo descending select x;
            return linq.ToList();
        }

        public List<BE_Cuenta_Nro7> Incremental(string s)
        {
            var linq = from x in ObtenerCuentas() where x.Codigo.StartsWith(s) select x;
            return linq.ToList();
        }

        public DataView dv(string s)
        {
            DataView dv;
            if (s == "Agregados") dv = new DataView(ds.Tables["Cuenta"], "", "", DataViewRowState.Added);
            else if (s == "Eliminados") dv = new DataView(ds.Tables["Cuenta"], "", "", DataViewRowState.Deleted);
            else if (s == "ModOri") dv = new DataView(ds.Tables["Cuenta"], "", "", DataViewRowState.ModifiedOriginal);
            else dv = new DataView(ds.Tables["Cuenta"], "", "", DataViewRowState.ModifiedCurrent);
            return dv;
        }
    }
}
