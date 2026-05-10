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
            if (reporte.IdObra <= 0)
                return "Debes seleccionar una obra.";

            if (string.IsNullOrWhiteSpace(reporte.Descripcion))
                return "La descripción no puede estar vacía.";

            if (reporte.Descripcion.Trim().Length < 20)
                return "La descripción debe tener mínimo 20 caracteres.";

            if (!_repo.ExisteObra(reporte.IdObra))
                return "La obra no existe.";

            reporte.Estado = "Pendiente";
            reporte.Fecha = DateTime.Now;

            _repo.Crear(reporte);

            return "";
        }

        public List<Reporte> ObtenerTodos()
        {
            return _repo.ObtenerTodos();
        }
        public List<Reporte> ObtenerPorUsuario(int idUsuario)
        {
            return _repo.ObtenerPorUsuario(idUsuario);
        }

        public Reporte? ObtenerPorId(int id)
        {
            return _repo.ObtenerPorId(id);
        }

        public string ActualizarGestion(int idReporte, string estado, string prioridad, string? observacionRespuesta)
        {
            var reporte = _repo.ObtenerPorId(idReporte);

            if (reporte == null)
                return "La denuncia no existe.";

            reporte.Estado = estado;
            reporte.Prioridad = prioridad;
            reporte.ObservacionRespuesta = observacionRespuesta;

            _repo.Actualizar(reporte);

            return "";
        }
    }
}