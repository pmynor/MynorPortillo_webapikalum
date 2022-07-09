namespace WebApiKalum.Entities
{
    public class CuentasXCobrar
    {
        public string Cargo { get; set; }
        public string Anio { get; set; }
        public string Carne { get; set; }
        public string CargoId { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaCargo { get; set; }
        public DateTime FechaAplica { get; set; }
        public double Monto { get; set; }
        public double Mora { get; set; }
        public double Descuento { get; set; }
        public virtual Cargo Cargos { get; set; }
        public virtual Alumno Alumnos { get; set; }

    }
}