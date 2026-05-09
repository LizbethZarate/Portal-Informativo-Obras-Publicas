using Microsoft.AspNetCore.Identity;
using PortalInforObrasPublicas.Interfaces;
using PortalInforObrasPublicas.Models;

namespace PortalInforObrasPublicas.Services
{
    public class UsuarioService
    {
        private readonly IUsuarioRepository _repo;
        private readonly PasswordHasher<Usuario> _hasher = new();

        public UsuarioService(IUsuarioRepository repo)
        {
            _repo = repo;
        }

        public Usuario? ValidarUsuario(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
                return null;

            var usuario = _repo.ObtenerPorEmail(email);
            if (usuario is null)
                return null;

<<<<<<< HEAD
            if (usuario is null || usuario.PasswordHash != password)
                
=======
            var result = _hasher.VerifyHashedPassword(usuario, usuario.PasswordHash, password);
            if (result == PasswordVerificationResult.Failed)
>>>>>>> 7b1daec6bb3082888fd86b4fca5b639a6b80a4d6
                return null;

            return usuario;
        }

        public void RegistrarUsuario(Usuario usuario)
        {
            if (_repo.ExisteEmail(usuario.Email))
                throw new InvalidOperationException("El correo ya está registrado.");

            usuario.Rol = "Ciudadano";

            // Hash del password antes de guardar
            usuario.PasswordHash = _hasher.HashPassword(usuario, usuario.PasswordHash);

            _repo.Agregar(usuario);
        }

        public Usuario? ObtenerPorEmail(string email) =>
            _repo.ObtenerPorEmail(email);
    }
}