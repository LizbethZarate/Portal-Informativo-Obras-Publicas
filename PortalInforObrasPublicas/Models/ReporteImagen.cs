using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortalInforObrasPublicas.Models
{
    public class ReporteImagen
    {
        [Key]
        public int IdReporteImagen { get; set; }

        [Required]
        public int IdReporte { get; set; }

        [ForeignKey("IdReporte")]
        public Reporte Reporte { get; set; }

        public string RutaImagen { get; set; } = string.Empty;
    }
}
