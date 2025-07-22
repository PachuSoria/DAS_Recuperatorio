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
    public class ORM_Cuenta
    {
        DAO_Nro4 DaoAcceso;
        DataSet ds;
        public ORM_Cuenta()
        {
            DaoAcceso = new DAO_Nro4();
            ds = DaoAcceso.RetornarDS();
        }

        public void Agregar(BE_Cuenta cuenta)
        {
            DataRow dr = ds.Tables["Cuenta"].NewRow();
            dr["Codigo"] = cuenta.Codigo;
            dr["Saldo"] = cuenta.Saldo;
            dr["DNICliente"] = cuenta.Cliente.DNI;
            dr["Tipo"] = cuenta.Tipo;

            if (cuenta is BE_CtaCorriente cc) dr["Descubierto"] = cc.Descubierto;
            else dr["Descubierto"] = DBNull.Value;

            ds.Tables["Cuenta"].Rows.Add(dr);
        }

        public void Modificar(BE_Cuenta cuenta)
        {
            DataRow dr = ds.Tables["Cuenta"].Rows.Find(cuenta.Codigo);
            dr["Saldo"] = cuenta.Saldo;
            dr["DNICliente"] = cuenta.Cliente.DNI;
            dr["Tipo"] = cuenta.Tipo;

            if (cuenta is BE_CtaCorriente cc) dr["Descubierto"] = cc.Descubierto;
            else dr["Descubierto"] = DBNull.Value;
        }

        public void Eliminar(BE_Cuenta cuenta)
        {
            ds.Tables["Cuenta"].Rows.Find(cuenta.Codigo).Delete();
        }

        public void Actualizar()
        {
            DaoAcceso.ActualizarBD();
        }

        public void Cancelar()
        {
            ds.RejectChanges();
        }
        
        public List<BE_Cuenta> ObtenerCuentas(List<BE_Cliente> clientes)
        {
            List<BE_Cuenta> lista = new List<BE_Cuenta>();
            foreach (DataRow dr in ds.Tables["Cuenta"].Rows)
            {
                if (dr.RowState != DataRowState.Deleted)
                {
                    string codigo = dr["Codigo"].ToString();
                    decimal saldo = Convert.ToDecimal(dr["Saldo"]);
                    string dniCliente = dr["DNICliente"].ToString();
                    string tipo = dr["Tipo"].ToString();

                    DataRow drCliente = ds.Tables["Cliente"].Rows.Find(dniCliente);
                    BE_Cliente cliente = new BE_Cliente(drCliente.ItemArray);

                    if (tipo == "CuentaCorriente")
                    {
                        decimal descubierto = dr["Descubierto"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["Descubierto"]);
                        lista.Add(new BE_CtaCorriente(codigo, saldo, cliente, tipo, descubierto));
                    }
                    else
                    {
                        lista.Add(new BE_CajaAhorro(codigo, saldo, cliente, tipo));
                    }
                }
            }
            return lista;
        }

        public List<BE_Cuenta> ObtenerCuentasCliente(string dni)
        {
            List<BE_Cuenta> lista = new List<BE_Cuenta>();

            foreach (DataRow dr in ds.Tables["Cuenta"].Rows)
            {
                if (dr.RowState != DataRowState.Deleted)
                {
                    if (dr["DNICliente"].ToString() == dni)
                    {
                        string codigo = dr["Codigo"].ToString();
                        decimal saldo = Convert.ToDecimal(dr["Saldo"]);
                        string tipo = dr["Tipo"].ToString();

                        DataRow drCliente = ds.Tables["Cliente"].Rows.Find(dni);
                        BE_Cliente cliente = new BE_Cliente(drCliente.ItemArray);

                        if (tipo == "CuentaCorriente")
                        {
                            decimal descubierto = dr["Descubierto"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["Descubierto"]);
                            lista.Add(new BE_CtaCorriente(codigo, saldo, cliente, tipo, descubierto));
                        }
                        else
                        {
                            lista.Add(new BE_CajaAhorro(codigo, saldo, cliente, tipo));
                        }
                    }
                }
            }

            return lista;
        }

        public List<BE_Cuenta> DesdeHasta(decimal desde, decimal hasta, List<BE_Cuenta> lista)
        {
            var linq = from cuenta in lista where cuenta.Saldo >= desde && cuenta.Saldo <= hasta orderby cuenta.Saldo descending select cuenta;
            return linq.ToList();
        }
    }
}
