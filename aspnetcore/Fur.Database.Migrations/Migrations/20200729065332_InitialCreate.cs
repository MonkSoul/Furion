using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Fur.Database.Migrations.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tenant",
                columns: table => new
                {
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Host = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Schema = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConnectionString = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenant", x => x.TenantId);
                });

            migrationBuilder.CreateTable(
                name: "Test",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Age = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Test", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Tenant",
                columns: new[] { "TenantId", "ConnectionString", "CreatedTime", "Host", "IsDeleted", "Name", "Schema", "UpdatedTime" },
                values: new object[] { new Guid("f1e60229-222d-4c93-933f-2a50b3c6d26c"), "Server=localhost;Database=Fur;User=sa;Password=000000;MultipleActiveResultSets=True;", new DateTime(2020, 7, 29, 14, 53, 31, 541, DateTimeKind.Local).AddTicks(9823), "localhost:44307", false, "默认租户", "fur", new DateTime(2020, 7, 29, 14, 53, 31, 543, DateTimeKind.Local).AddTicks(2096) });

            migrationBuilder.InsertData(
                table: "Tenant",
                columns: new[] { "TenantId", "ConnectionString", "CreatedTime", "Host", "IsDeleted", "Name", "Schema", "UpdatedTime" },
                values: new object[] { new Guid("5dbfaa37-420c-461d-9ba1-dd27052201f4"), "Server=localhost;Database=Other;User=sa;Password=000000;MultipleActiveResultSets=True;", new DateTime(2020, 7, 29, 14, 53, 31, 543, DateTimeKind.Local).AddTicks(4744), "localhost:41529", false, "其他租户", "other", new DateTime(2020, 7, 29, 14, 53, 31, 543, DateTimeKind.Local).AddTicks(4750) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tenant");

            migrationBuilder.DropTable(
                name: "Test");
        }
    }
}
