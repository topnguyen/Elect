using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Elect.Sample.Data.EF.Migrations
{
    public partial class User : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(nullable: false),
                    DeletedBy = table.Column<Guid>(nullable: true),
                    DeletedTime = table.Column<DateTimeOffset>(nullable: true),
                    LastUpdatedBy = table.Column<Guid>(nullable: true),
                    LastUpdatedTime = table.Column<DateTimeOffset>(nullable: false),
                    UserName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_User_DeletedTime",
                table: "User",
                column: "DeletedTime");

            migrationBuilder.CreateIndex(
                name: "IX_User_Id",
                table: "User",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_User_UserName",
                table: "User",
                column: "UserName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User");
        }
    }
}