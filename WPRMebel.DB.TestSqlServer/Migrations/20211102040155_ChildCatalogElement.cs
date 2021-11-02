using Microsoft.EntityFrameworkCore.Migrations;

namespace WPRMebel.DB.TestSqlServer.Migrations
{
    public partial class ChildCatalogElement : Migration
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
                name: "ChildCatalogElement",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OwnerElementId = table.Column<int>(type: "int", nullable: false),
                    CatalogElementId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChildCatalogElement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChildCatalogElement_CatalogElement_CatalogElementId",
                        column: x => x.CatalogElementId,
                        principalTable: "CatalogElement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChildCatalogElement_CatalogElementId",
                table: "ChildCatalogElement",
                column: "CatalogElementId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChildCatalogElement");

            migrationBuilder.DropColumn(
                name: "IsRequired",
                table: "ElementProperties");
        }
    }
}
