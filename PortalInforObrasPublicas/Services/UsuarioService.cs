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

            var result = _hasher.VerifyHashedPassword(usuario, usuario.PasswordHash, password);
            if (result == PasswordVerificationResult.Failed)
                return null;

            return usuario;
        }

        public void RegistrarUsuario(Usuario usuario)
        {
            if (_repo.ExisteEmail(usuario.Email))
                throw new InvalidOperationException("El correo ya está registrado.");

            if (string.IsNullOrWhiteSpace(usuario.Rol))
                usuario.Rol = "Ciudadano";
            else
                usuario.Rol = usuario.Rol.Trim();

            // Hash del password antes de guardar
            usuario.PasswordHash = _hasher.HashPassword(usuario, usuario.PasswordHash);

            _repo.Agregar(usuario);
        }

        public Usuario? ObtenerPorEmail(string email) =>
            _repo.ObtenerPorEmail(email);
    }
}