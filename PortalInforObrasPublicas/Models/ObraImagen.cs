using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortalInforObrasPublicas.Models
{
    public class ObraImagen
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int IdObra { get; set; }

        [ForeignKey("IdObra")]
        public Obra Obra { get; set; }

        public string RutaImagen { get; set; } = string.Empty;
    }
}
