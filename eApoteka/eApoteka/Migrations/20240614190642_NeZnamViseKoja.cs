using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eApoteka.Migrations
{
    /// <inheritdoc />
    public partial class NeZnamViseKoja : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Email",
                table: "RegisterViewModel",
                newName: "Username");

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "Korisnik",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "Korisnik");

            migrationBuilder.RenameColumn(
                name: "Username",
                table: "RegisterViewModel",
                newName: "Email");
        }
    }
}
