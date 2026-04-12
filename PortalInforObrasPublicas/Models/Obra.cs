using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortalInforObrasPublicas.Models
{
    public class Obra
    {
        [Key]
        public int IdObra { get; set; }

        [Required]
        [StringLength(150)]
        public string Nombre { get; set; } = string.Empty;

        public string? Ubicacion { get; set; } = string.Empty;

        public string? Estado { get; set; }

        [Column(TypeName = "decimal(12,2)")]
        public decimal Presupuesto { get; set; }

        public DateTime FechaInicio { get; set; }

        public DateTime? FechaFin { get; set; }

        public List<Reporte>? Reportes { get; set; }
        public List<ObraImagen>? Imagenes { get; set; }
    }
}
