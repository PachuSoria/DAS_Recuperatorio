namespace BE
{
    public class BE_Auto
    {
        public string AuPatente { get; set; }
        public DateTime AuFechaIngreso { get; set; }
        public DateTime AuFechaBaja { get; set; }
        public int AuAño { get; set; }
        public bool AuEnUso { get; set; }
        public decimal AuValor { get; set; }
        public decimal AuValorResidual { get; set; }
        public int AuDias { get; set; }
        public BE_Auto(string auPatente, DateTime auFechaIngreso, DateTime auFechaBaja, int auAño, bool auEnUso, decimal auValor) 
        {
            AuPatente = auPatente;
            AuFechaIngreso = auFechaIngreso;
            AuFechaBaja = auFechaBaja;
            AuAño = auAño;
            AuEnUso = auEnUso;
            AuValor = auValor;
        }

        public BE_Auto(decimal valorResidual) 
        {
            AuValorResidual = valorResidual;
        }

        public BE_Auto(string patente)
        {
            AuPatente = patente;
        }

        public BE_Auto(object[] datos) : this(datos[0].ToString(), Convert.ToDateTime(datos[1]), Convert.ToDateTime(datos[2]), Convert.ToInt32(datos[3]), Convert.ToBoolean(datos[4]), Convert.ToDecimal(datos[5]))
        {

        }
    }
}
