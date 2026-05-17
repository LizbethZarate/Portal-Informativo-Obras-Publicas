using PortalInforObrasPublicas.Models;

namespace PortalInforObrasPublicas.Interfaces
{
    public interface IUsuarioRepository
    {
        Usuario? ObtenerPorEmail(string email);
        bool ExisteEmail(string email);
        void Agregar(Usuario usuario);
        Usuario? ObtenerPorTokenRecuperacion(string token);
        void Actualizar(Usuario usuario);
    }
}
