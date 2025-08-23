using System.ComponentModel.DataAnnotations;

namespace Pc1_Linares.Models
{
    public class ProductoCredito
    {
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [Range(0.0001, 1)]
        public decimal Tea { get; set; }

        [Required]
        [Range(0.0001, 1)]
        public decimal ComisionPct { get; set; }

        public int MinMeses { get; set; }
        public int MaxMeses { get; set; }
    }
}
