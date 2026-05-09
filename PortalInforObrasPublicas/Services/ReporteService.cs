using PortalInforObrasPublicas.Interfaces;
using PortalInforObrasPublicas.Models;

namespace PortalInforObrasPublicas.Services
{
    public class ReporteService
    {
        private readonly IReporteRepository _repo;

        public ReporteService(IReporteRepository repo)
        {
            _repo = repo;
        }

        public string CrearReporte(Reporte reporte)
        {
            if (string.IsNullOrWhiteSpace(reporte.Descripcion))
                return "El reporte no puede estar vacío.";

            if (!_repo.ExisteObra(reporte.IdObra))
                return "La obra no existe.";

            _repo.Crear(reporte);

            return "";
        }

        public List<Reporte> ObtenerTodos()
        {
            return _repo.ObtenerTodos();
        }
    }
}