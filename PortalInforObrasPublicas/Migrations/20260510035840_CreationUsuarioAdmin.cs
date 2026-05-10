using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortalInforObrasPublicas.Migrations
{
    /// <inheritdoc />
    public partial class CreationUsuarioAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "IdUsuario", "Email", "Nombre", "PasswordHash", "Rol" },
                values: new object[] { 1, "admin@gmail.com", "admin", "AQAAAAIAAYagAAAAENEBAUbj6boFRStMPcawDNt1jvR5htSdxk5RjOmTkqlMwJgJK1IkDgpIlxf+6ri37w==", "Administrador" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "IdUsuario",
                keyValue: 1);
        }
    }
}
