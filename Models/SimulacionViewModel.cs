using System.ComponentModel.DataAnnotations;

namespace Pc1_Linares.Models
{
    public class SimulacionViewModel
    {
        public int ProductId { get; set; }
        public string ProductoNombre { get; set; } = string.Empty;
        public decimal Tea { get; set; }
        public decimal ComisionPct { get; set; }
        public int MinMeses { get; set; }
        public int MaxMeses { get; set; }

        [Required(ErrorMessage = "El campo Monto es obligatorio.")]
        [Range(0.01, 1_000_000, ErrorMessage = "El monto debe estar entre 0.01 y 1,000,000.")]
        public decimal Monto { get; set; }

        [Required(ErrorMessage = "El campo Meses es obligatorio.")]
        [Range(1, 600, ErrorMessage = "El campo Meses debe estar entre 1 y 600.")]
        public int Meses { get; set; }

        public decimal? TEM { get; set; }
        public decimal? Cuota { get; set; }
        public decimal? Comision { get; set; }
        public decimal? CostoTotal { get; set; }
    }
}
