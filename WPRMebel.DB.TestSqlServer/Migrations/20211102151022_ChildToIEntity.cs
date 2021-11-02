using Microsoft.EntityFrameworkCore.Migrations;

namespace WPRMebel.DB.TestSqlServer.Migrations
{
    public partial class ChildToIEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "ChildCatalogElements");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "ChildCatalogElements",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
