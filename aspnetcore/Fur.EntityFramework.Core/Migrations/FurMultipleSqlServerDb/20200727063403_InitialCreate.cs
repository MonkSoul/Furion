using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Fur.EntityFramework.Core.Migrations.FurMultipleSqlServerDb
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tenants",
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
                    table.PrimaryKey("PK_Tenants", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Tenants",
                columns: new[] { "Id", "CreatedTime", "Host", "IsDeleted", "Name", "UpdatedTime" },
                values: new object[] { 1, new DateTime(2020, 7, 27, 14, 34, 2, 947, DateTimeKind.Local).AddTicks(8901), "localhost:44307", false, "默认租户", new DateTime(2020, 7, 27, 14, 34, 2, 948, DateTimeKind.Local).AddTicks(8459) });

            migrationBuilder.InsertData(
                table: "Tenants",
                columns: new[] { "Id", "CreatedTime", "Host", "IsDeleted", "Name", "UpdatedTime" },
                values: new object[] { 2, new DateTime(2020, 7, 27, 14, 34, 2, 949, DateTimeKind.Local).AddTicks(255), "localhost:41529", false, "默认租户", new DateTime(2020, 7, 27, 14, 34, 2, 949, DateTimeKind.Local).AddTicks(261) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tenants");
        }
    }
}
