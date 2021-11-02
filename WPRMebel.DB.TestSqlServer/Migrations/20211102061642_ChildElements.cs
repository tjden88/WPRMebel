using Microsoft.EntityFrameworkCore.Migrations;

namespace WPRMebel.DB.TestSqlServer.Migrations
{
    public partial class ChildElements : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsRequired",
                table: "ElementProperties",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "ChildCatalogElements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OwnerCatalogElementId = table.Column<int>(type: "int", nullable: false),
                    CatalogElementId = table.Column<int>(type: "int", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChildCatalogElements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChildCatalogElements_CatalogElement_CatalogElementId",
                        column: x => x.CatalogElementId,
                        principalTable: "CatalogElement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChildCatalogElements_CatalogElement_OwnerCatalogElementId",
                        column: x => x.OwnerCatalogElementId,
                        principalTable: "CatalogElement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChildCatalogElements_CatalogElementId",
                table: "ChildCatalogElements",
                column: "CatalogElementId");

            migrationBuilder.CreateIndex(
                name: "IX_ChildCatalogElements_OwnerCatalogElementId",
                table: "ChildCatalogElements",
                column: "OwnerCatalogElementId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChildCatalogElements");

            migrationBuilder.DropColumn(
                name: "IsRequired",
                table: "ElementProperties");
        }
    }
}
