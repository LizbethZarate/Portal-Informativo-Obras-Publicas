using PortalInforObrasPublicas.Models;

namespace PortalInforObrasPublicas.Interfaces
{
    public interface IReporteRepository
    {
        List<Reporte> ObtenerTodos();
        void Crear(Reporte reporte);
        bool ExisteObra(int idObra);
        List<Reporte> ObtenerPorUsuario(int idUsuario);
    }
}