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

            usuario.Rol = "Ciudadano";

            // Hash del password antes de guardar
            usuario.PasswordHash = _hasher.HashPassword(usuario, usuario.Password);

            _repo.Agregar(usuario);
        }

        public Usuario? ObtenerPorEmail(string email) =>
            _repo.ObtenerPorEmail(email);

        public void RegistrarAdministrador(Usuario usuario)
        {
            if (_repo.ExisteEmail(usuario.Email))
                throw new InvalidOperationException("El correo ya está registrado.");

            usuario.Rol = "Administrador";

            usuario.PasswordHash =
                _hasher.HashPassword(usuario, usuario.PasswordHash);

            _repo.Agregar(usuario);
        }
        public int? ObtenerIdPorEmail(string email)
        {
            var usuario = _repo.ObtenerPorEmail(email);
            return usuario?.IdUsuario;
        }

        public string GenerarTokenRecuperacion(string email)
        {
            var usuario = _repo.ObtenerPorEmail(email);

            if (usuario == null)
                return "";

            var token = Guid.NewGuid().ToString();

            usuario.ResetPasswordToken = token;
            usuario.ResetPasswordTokenExpira = DateTime.Now.AddMinutes(30);

            _repo.Actualizar(usuario);

            return token;
        }

        public string RestablecerPassword(string token, string nuevaPassword, string confirmarPassword)
        {
            if (string.IsNullOrWhiteSpace(token))
                return "Token inválido.";

            if (string.IsNullOrWhiteSpace(nuevaPassword))
                return "La nueva contraseña es obligatoria.";

            if (nuevaPassword.Length < 6)
                return "La contraseña debe tener mínimo 6 caracteres.";

            if (nuevaPassword != confirmarPassword)
                return "Las contraseñas no coinciden.";

            var usuario = _repo.ObtenerPorTokenRecuperacion(token);

            if (usuario == null)
                return "Token inválido.";

            if (usuario.ResetPasswordTokenExpira < DateTime.Now)
                return "El enlace de recuperación expiró.";

            usuario.PasswordHash = _hasher.HashPassword(usuario, nuevaPassword);
            usuario.ResetPasswordToken = null;
            usuario.ResetPasswordTokenExpira = null;

            _repo.Actualizar(usuario);

            return "";
        }
    }
}