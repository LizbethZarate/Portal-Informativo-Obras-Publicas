using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortalInforObrasPublicas.Models
{
    public class Reporte
    {
        [Key]
        public int IdReporte { get; set; }

        [Required]
        public int IdObra { get; set; }

        [ForeignKey("IdObra")]
        public Obra Obra { get; set; }

        [Required]
        public string Descripcion { get; set; } = string.Empty;

        public DateTime Fecha { get; set; } = DateTime.Now;

        public string Estado { get; set; } = "Pendiente";

        // Opcional: para luego relacionar con usuario
        public int? IdUsuario { get; set; }

        [ForeignKey("IdUsuario")]
        public Usuario? Usuario { get; set; }
    }
}
