using PortalInforObrasPublicas.Data;
using PortalInforObrasPublicas.Interfaces;
using PortalInforObrasPublicas.Models;

namespace PortalInforObrasPublicas.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly AppDbContext _context;

        public UsuarioRepository(AppDbContext context)
        {
            _context = context;
        }

        public Usuario? ObtenerPorEmail(string email)
        {
            var emailNorm = email.ToLower().Trim();
            return _context.Usuarios
                .FirstOrDefault(u => u.Email == emailNorm);
        }

        public bool ExisteEmail(string email)
        {
            var emailNorm = email.ToLower().Trim();
            return _context.Usuarios
                .Any(u => u.Email == emailNorm);
        }

        public void Agregar(Usuario usuario)
        {
            usuario.Email = usuario.Email.ToLower().Trim();
            usuario.Nombre = usuario.Nombre.Trim();
            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
        }

        public Usuario? ObtenerPorTokenRecuperacion(string token)
        {
            return _context.Usuarios
                .FirstOrDefault(u => u.ResetPasswordToken == token);
        }

        public void Actualizar(Usuario usuario)
        {
            _context.Usuarios.Update(usuario);
            _context.SaveChanges();
        }
    }
}
