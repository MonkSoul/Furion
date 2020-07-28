using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Fur.EntityFramework.Core.Migrations
{
    public partial class v004 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TenantId",
                table: "Test",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Tenant",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Host = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenant", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Tenant",
                columns: new[] { "Id", "CreatedTime", "Host", "IsDeleted", "Name", "UpdatedTime" },
                values: new object[] { 1, new DateTime(2020, 7, 29, 0, 20, 51, 966, DateTimeKind.Local).AddTicks(5016), "localhost:44307", false, "默认租户", new DateTime(2020, 7, 29, 0, 20, 51, 967, DateTimeKind.Local).AddTicks(4925) });

            migrationBuilder.InsertData(
                table: "Tenant",
                columns: new[] { "Id", "CreatedTime", "Host", "IsDeleted", "Name", "UpdatedTime" },
                values: new object[] { 2, new DateTime(2020, 7, 29, 0, 20, 51, 967, DateTimeKind.Local).AddTicks(6734), "localhost:41529", false, "默认租户", new DateTime(2020, 7, 29, 0, 20, 51, 967, DateTimeKind.Local).AddTicks(6741) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tenant");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "Test");
        }
    }
}
