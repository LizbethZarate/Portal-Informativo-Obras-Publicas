using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PortalInforObrasPublicas.Models;

namespace PortalInforObrasPublicas.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Obra> Obras { get; set; }
        public DbSet<Reporte> Reportes { get; set; }
        public DbSet<ObraImagen> ObraImagenes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 1. Configuramos el Hasher
            var hasher = new PasswordHasher<Usuario>();

            // 2. Creamos la instancia del usuario inicial
            var usuarioAdmin = new Usuario
            {
                IdUsuario = 1, // Importante poner el ID ya que es el primero
                Nombre = "admin",
                Email = "admin@gmail.com",
                Rol = "Administrador"
            };

            // 3. Generamos el Hash de la clave "123456" usando el motor de Identity
            usuarioAdmin.PasswordHash = hasher.HashPassword(usuarioAdmin, "123456");

            // 4. Le decimos a EF que este registro debe existir en la base de datos
            modelBuilder.Entity<Usuario>().HasData(usuarioAdmin);
        }
    }
}