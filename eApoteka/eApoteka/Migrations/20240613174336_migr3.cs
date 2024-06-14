using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eApoteka.Migrations
{
    /// <inheritdoc />
    public partial class migr3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SearchViewModelId",
                table: "Proizvod",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AdminPanelViewModelId",
                table: "Korisnik",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AdminPanelViewModelId",
                table: "Dostavljac",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AdminPanelViewModelId",
                table: "Apotekar",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AdminPanelViewModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminPanelViewModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LoginViewModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoginViewModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlaceOrderViewModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentMethod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeliveryOptions = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlaceOrderViewModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RegisterViewModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Adress = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegisterViewModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SearchViewModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Search = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SearchViewModel", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Proizvod_SearchViewModelId",
                table: "Proizvod",
                column: "SearchViewModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Korisnik_AdminPanelViewModelId",
                table: "Korisnik",
                column: "AdminPanelViewModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Dostavljac_AdminPanelViewModelId",
                table: "Dostavljac",
                column: "AdminPanelViewModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Apotekar_AdminPanelViewModelId",
                table: "Apotekar",
                column: "AdminPanelViewModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Apotekar_AdminPanelViewModel_AdminPanelViewModelId",
                table: "Apotekar",
                column: "AdminPanelViewModelId",
                principalTable: "AdminPanelViewModel",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Dostavljac_AdminPanelViewModel_AdminPanelViewModelId",
                table: "Dostavljac",
                column: "AdminPanelViewModelId",
                principalTable: "AdminPanelViewModel",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Korisnik_AdminPanelViewModel_AdminPanelViewModelId",
                table: "Korisnik",
                column: "AdminPanelViewModelId",
                principalTable: "AdminPanelViewModel",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Proizvod_SearchViewModel_SearchViewModelId",
                table: "Proizvod",
                column: "SearchViewModelId",
                principalTable: "SearchViewModel",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Apotekar_AdminPanelViewModel_AdminPanelViewModelId",
                table: "Apotekar");

            migrationBuilder.DropForeignKey(
                name: "FK_Dostavljac_AdminPanelViewModel_AdminPanelViewModelId",
                table: "Dostavljac");

            migrationBuilder.DropForeignKey(
                name: "FK_Korisnik_AdminPanelViewModel_AdminPanelViewModelId",
                table: "Korisnik");

            migrationBuilder.DropForeignKey(
                name: "FK_Proizvod_SearchViewModel_SearchViewModelId",
                table: "Proizvod");

            migrationBuilder.DropTable(
                name: "AdminPanelViewModel");

            migrationBuilder.DropTable(
                name: "LoginViewModel");

            migrationBuilder.DropTable(
                name: "PlaceOrderViewModel");

            migrationBuilder.DropTable(
                name: "RegisterViewModel");

            migrationBuilder.DropTable(
                name: "SearchViewModel");

            migrationBuilder.DropIndex(
                name: "IX_Proizvod_SearchViewModelId",
                table: "Proizvod");

            migrationBuilder.DropIndex(
                name: "IX_Korisnik_AdminPanelViewModelId",
                table: "Korisnik");

            migrationBuilder.DropIndex(
                name: "IX_Dostavljac_AdminPanelViewModelId",
                table: "Dostavljac");

            migrationBuilder.DropIndex(
                name: "IX_Apotekar_AdminPanelViewModelId",
                table: "Apotekar");

            migrationBuilder.DropColumn(
                name: "SearchViewModelId",
                table: "Proizvod");

            migrationBuilder.DropColumn(
                name: "AdminPanelViewModelId",
                table: "Korisnik");

            migrationBuilder.DropColumn(
                name: "AdminPanelViewModelId",
                table: "Dostavljac");

            migrationBuilder.DropColumn(
                name: "AdminPanelViewModelId",
                table: "Apotekar");
        }
    }
}
