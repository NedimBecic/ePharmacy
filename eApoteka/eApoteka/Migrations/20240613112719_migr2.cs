using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eApoteka.Migrations
{
    /// <inheritdoc />
    public partial class migr2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StatusNarudzbeId",
                table: "Narudzba",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Narudzba_StatusNarudzbeId",
                table: "Narudzba",
                column: "StatusNarudzbeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Narudzba_StatusNarudzbe_StatusNarudzbeId",
                table: "Narudzba",
                column: "StatusNarudzbeId",
                principalTable: "StatusNarudzbe",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Narudzba_StatusNarudzbe_StatusNarudzbeId",
                table: "Narudzba");

            migrationBuilder.DropIndex(
                name: "IX_Narudzba_StatusNarudzbeId",
                table: "Narudzba");

            migrationBuilder.DropColumn(
                name: "StatusNarudzbeId",
                table: "Narudzba");
        }
    }
}
