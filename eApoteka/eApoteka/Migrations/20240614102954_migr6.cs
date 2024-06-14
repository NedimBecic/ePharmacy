using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eApoteka.Migrations
{
    /// <inheritdoc />
    public partial class migr6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "brojTelefona",
                table: "DetaljDostave",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "brojTelefona",
                table: "DetaljDostave");
        }
    }
}
