using Microsoft.EntityFrameworkCore.Migrations;

namespace WPRMebel.DB.TestSqlServer.Migrations
{
    public partial class EditInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Elements_Elements_ElementId",
                table: "Elements");

            migrationBuilder.DropIndex(
                name: "IX_Elements_ElementId",
                table: "Elements");

            migrationBuilder.DropColumn(
                name: "ElementId",
                table: "Elements");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ElementId",
                table: "Elements",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Elements_ElementId",
                table: "Elements",
                column: "ElementId");

            migrationBuilder.AddForeignKey(
                name: "FK_Elements_Elements_ElementId",
                table: "Elements",
                column: "ElementId",
                principalTable: "Elements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
