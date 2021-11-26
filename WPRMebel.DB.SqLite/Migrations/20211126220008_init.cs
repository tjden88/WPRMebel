using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WPRMebel.DB.SqLite.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sections", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vendors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    VendorId = table.Column<int>(type: "INTEGER", nullable: true),
                    SectionId = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_Sections_SectionId",
                        column: x => x.SectionId,
                        principalTable: "Sections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Categories_Vendors_VendorId",
                        column: x => x.VendorId,
                        principalTable: "Vendors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "CatalogElement",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Price = table.Column<decimal>(type: "TEXT", nullable: false),
                    Extra = table.Column<double>(type: "REAL", nullable: false),
                    CategoryId = table.Column<int>(type: "INTEGER", nullable: false),
                    Discriminator = table.Column<string>(type: "TEXT", nullable: false),
                    Width = table.Column<int>(type: "INTEGER", nullable: true),
                    DetaliMaxWidth = table.Column<int>(type: "INTEGER", nullable: true),
                    DetaliMaxHeight = table.Column<int>(type: "INTEGER", nullable: true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatalogElement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CatalogElement_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChildCatalogElements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    OwnerCatalogElementId = table.Column<int>(type: "INTEGER", nullable: false),
                    CatalogElementId = table.Column<int>(type: "INTEGER", nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChildCatalogElements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChildCatalogElements_CatalogElement_CatalogElementId",
                        column: x => x.CatalogElementId,
                        principalTable: "CatalogElement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChildCatalogElements_CatalogElement_OwnerCatalogElementId",
                        column: x => x.OwnerCatalogElementId,
                        principalTable: "CatalogElement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ElementProperties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CatalogElementId = table.Column<int>(type: "INTEGER", nullable: false),
                    PriceChanging = table.Column<double>(type: "REAL", nullable: false),
                    IsRequired = table.Column<bool>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ElementProperties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ElementProperties_CatalogElement_CatalogElementId",
                        column: x => x.CatalogElementId,
                        principalTable: "CatalogElement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ElementPropertyValues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ElementPropertyId = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ElementPropertyValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ElementPropertyValues_ElementProperties_ElementPropertyId",
                        column: x => x.ElementPropertyId,
                        principalTable: "ElementProperties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CatalogElement_CategoryId",
                table: "CatalogElement",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_SectionId",
                table: "Categories",
                column: "SectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_VendorId",
                table: "Categories",
                column: "VendorId");

            migrationBuilder.CreateIndex(
                name: "IX_ChildCatalogElements_CatalogElementId",
                table: "ChildCatalogElements",
                column: "CatalogElementId");

            migrationBuilder.CreateIndex(
                name: "IX_ChildCatalogElements_OwnerCatalogElementId",
                table: "ChildCatalogElements",
                column: "OwnerCatalogElementId");

            migrationBuilder.CreateIndex(
                name: "IX_ElementProperties_CatalogElementId",
                table: "ElementProperties",
                column: "CatalogElementId");

            migrationBuilder.CreateIndex(
                name: "IX_ElementPropertyValues_ElementPropertyId",
                table: "ElementPropertyValues",
                column: "ElementPropertyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChildCatalogElements");

            migrationBuilder.DropTable(
                name: "ElementPropertyValues");

            migrationBuilder.DropTable(
                name: "ElementProperties");

            migrationBuilder.DropTable(
                name: "CatalogElement");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Sections");

            migrationBuilder.DropTable(
                name: "Vendors");
        }
    }
}
