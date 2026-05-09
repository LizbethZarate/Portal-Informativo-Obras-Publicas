using PortalInforObrasPublicas.Models;

namespace PortalInforObrasPublicas.Interfaces
{
    public interface IObraRepository
    {
        List<Obra> ObtenerTodas();
        Obra ObtenerPorId(int id);
        void Crear(Obra obra);
        void Actualizar(Obra obra);
        void Eliminar(int id);
        bool ExisteObra(string nombre);
    }
}