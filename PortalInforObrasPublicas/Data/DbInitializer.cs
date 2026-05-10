using Microsoft.AspNetCore.Identity;
using PortalInforObrasPublicas.Models;

namespace PortalInforObrasPublicas.Data
{
    public static class DbInitializer
    {
        public static void Inicializar(AppDbContext context)
        {
            if (context.Usuarios.Any(u => u.Email == "admin@gmail.com"))
                return;

            var hasher = new PasswordHasher<Usuario>();

            var admin = new Usuario
            {
                Nombre = "Administrador",
                Email = "admin@gmail.com",
                Rol = "Administrador"
            };

            admin.PasswordHash =
                hasher.HashPassword(admin, "Admin123*");

            context.Usuarios.Add(admin);

            context.SaveChanges();
        }
    }
}
