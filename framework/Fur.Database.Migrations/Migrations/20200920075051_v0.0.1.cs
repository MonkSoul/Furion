using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Fur.Database.Migrations.Migrations
{
    public partial class v001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "City",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    ParentId = table.Column<int>(type: "INTEGER", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_City", x => x.Id);
                    table.ForeignKey(
                        name: "FK_City_City_ParentId",
                        column: x => x.ParentId,
                        principalTable: "City",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Person",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 32, nullable: true),
                    Age = table.Column<int>(type: "INTEGER", nullable: false),
                    Address = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Post",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Post", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Children",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Gender = table.Column<int>(type: "INTEGER", nullable: false),
                    PersonId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Children", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Children_Person_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: true),
                    QQ = table.Column<string>(type: "TEXT", nullable: true),
                    PersonId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonDetail_Person_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonPost",
                columns: table => new
                {
                    PersonsId = table.Column<int>(type: "INTEGER", nullable: false),
                    PostsId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonPost", x => new { x.PersonsId, x.PostsId });
                    table.ForeignKey(
                        name: "FK_PersonPost_Person_PersonsId",
                        column: x => x.PersonsId,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonPost_Post_PostsId",
                        column: x => x.PostsId,
                        principalTable: "Post",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "City",
                columns: new[] { "Id", "CreatedTime", "IsDeleted", "Name", "ParentId", "UpdatedTime" },
                values: new object[] { 1, new DateTime(2020, 8, 20, 15, 30, 20, 0, DateTimeKind.Unspecified), false, "中国", null, null });

            migrationBuilder.InsertData(
                table: "City",
                columns: new[] { "Id", "CreatedTime", "IsDeleted", "Name", "ParentId", "UpdatedTime" },
                values: new object[] { 2, new DateTime(2020, 8, 20, 15, 30, 20, 0, DateTimeKind.Unspecified), false, "广东省", 1, null });

            migrationBuilder.InsertData(
                table: "City",
                columns: new[] { "Id", "CreatedTime", "IsDeleted", "Name", "ParentId", "UpdatedTime" },
                values: new object[] { 5, new DateTime(2020, 8, 20, 15, 30, 20, 0, DateTimeKind.Unspecified), false, "浙江省", 1, null });

            migrationBuilder.InsertData(
                table: "City",
                columns: new[] { "Id", "CreatedTime", "IsDeleted", "Name", "ParentId", "UpdatedTime" },
                values: new object[] { 3, new DateTime(2020, 8, 20, 15, 30, 20, 0, DateTimeKind.Unspecified), false, "中山市", 2, null });

            migrationBuilder.InsertData(
                table: "City",
                columns: new[] { "Id", "CreatedTime", "IsDeleted", "Name", "ParentId", "UpdatedTime" },
                values: new object[] { 4, new DateTime(2020, 8, 20, 15, 30, 20, 0, DateTimeKind.Unspecified), false, "珠海市", 2, null });

            migrationBuilder.CreateIndex(
                name: "IX_Children_PersonId",
                table: "Children",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_City_ParentId",
                table: "City",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonDetail_PersonId",
                table: "PersonDetail",
                column: "PersonId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PersonPost_PostsId",
                table: "PersonPost",
                column: "PostsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Children");

            migrationBuilder.DropTable(
                name: "City");

            migrationBuilder.DropTable(
                name: "PersonDetail");

            migrationBuilder.DropTable(
                name: "PersonPost");

            migrationBuilder.DropTable(
                name: "Person");

            migrationBuilder.DropTable(
                name: "Post");
        }
    }
}
