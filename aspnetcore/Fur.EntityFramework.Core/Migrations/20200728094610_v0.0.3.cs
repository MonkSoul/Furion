using Microsoft.EntityFrameworkCore.Migrations;

namespace Fur.EntityFramework.Core.Migrations
{
    public partial class v003 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "Test");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TenantId",
                table: "Test",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
