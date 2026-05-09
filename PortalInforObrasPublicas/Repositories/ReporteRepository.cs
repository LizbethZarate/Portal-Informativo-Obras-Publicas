using PortalInforObrasPublicas.Data;
using PortalInforObrasPublicas.Interfaces;
using PortalInforObrasPublicas.Models;

namespace PortalInforObrasPublicas.Repositories
{
    public class ReporteRepository : IReporteRepository
    {
        private readonly AppDbContext _context;

        public ReporteRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<Reporte> ObtenerTodos()
        {
            return _context.Reportes.ToList();
        }

        public void Crear(Reporte reporte)
        {
            _context.Reportes.Add(reporte);
            _context.SaveChanges();
        }

        public bool ExisteObra(int idObra)
        {
            return _context.Obras.Any(o => o.IdObra == idObra);
        }
    }
}