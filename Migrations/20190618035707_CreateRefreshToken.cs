using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dately.Migrations
{
    public partial class CreateRefreshToken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRefreshTokens",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Token = table.Column<string>(nullable: false),
                    ExpiryDate = table.Column<DateTime>(nullable: false),
                    Revoked = table.Column<bool>(nullable: false),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRefreshTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRefreshTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRefreshTokens_UserId",
                table: "AspNetRefreshTokens",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRefreshTokens");
        }
    }
}
