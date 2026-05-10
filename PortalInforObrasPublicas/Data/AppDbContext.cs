using Microsoft.EntityFrameworkCore;
using PortalInforObrasPublicas.Models;

namespace PortalInforObrasPublicas.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Obra> Obras { get; set; }
        public DbSet<Reporte> Reportes { get; set; }
        public DbSet<ObraImagen> ObraImagenes { get; set; }
        public DbSet<ReporteImagen> ReporteImagenes { get; set; }
    }
}