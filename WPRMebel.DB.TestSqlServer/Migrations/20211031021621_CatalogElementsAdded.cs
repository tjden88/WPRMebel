using Microsoft.EntityFrameworkCore.Migrations;

namespace WPRMebel.DB.TestSqlServer.Migrations
{
    public partial class CatalogElementsAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SectionId",
                table: "Categories",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Elements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(type: "int", nullable: true),
                    ExtraPrice = table.Column<double>(type: "float", nullable: false),
                    ElementId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Elements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Elements_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Elements_Elements_ElementId",
                        column: x => x.ElementId,
                        principalTable: "Elements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ElementProperties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ElementId = table.Column<int>(type: "int", nullable: true),
                    PriceChanging = table.Column<double>(type: "float", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ElementProperties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ElementProperties_Elements_ElementId",
                        column: x => x.ElementId,
                        principalTable: "Elements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ElementPropertyValues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ElementPropertyId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ElementPropertyValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ElementPropertyValues_ElementProperties_ElementPropertyId",
                        column: x => x.ElementPropertyId,
                        principalTable: "ElementProperties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_SectionId",
                table: "Categories",
                column: "SectionId");

            migrationBuilder.CreateIndex(
                name: "IX_ElementProperties_ElementId",
                table: "ElementProperties",
                column: "ElementId");

            migrationBuilder.CreateIndex(
                name: "IX_ElementPropertyValues_ElementPropertyId",
                table: "ElementPropertyValues",
                column: "ElementPropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_Elements_CategoryId",
                table: "Elements",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Elements_ElementId",
                table: "Elements",
                column: "ElementId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Sections_SectionId",
                table: "Categories",
                column: "SectionId",
                principalTable: "Sections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Sections_SectionId",
                table: "Categories");

            migrationBuilder.DropTable(
                name: "ElementPropertyValues");

            migrationBuilder.DropTable(
                name: "ElementProperties");

            migrationBuilder.DropTable(
                name: "Elements");

            migrationBuilder.DropIndex(
                name: "IX_Categories_SectionId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "SectionId",
                table: "Categories");
        }
    }
}
