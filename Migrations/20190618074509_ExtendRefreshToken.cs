using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dately.Migrations
{
    public partial class ExtendRefreshToken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ExpiryDate",
                table: "AspNetRefreshTokens",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "AspNetRefreshTokens",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Flag",
                table: "AspNetRefreshTokens",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "AspNetRefreshTokens",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TotalRefresh",
                table: "AspNetRefreshTokens",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "AspNetRefreshTokens");

            migrationBuilder.DropColumn(
                name: "Flag",
                table: "AspNetRefreshTokens");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "AspNetRefreshTokens");

            migrationBuilder.DropColumn(
                name: "TotalRefresh",
                table: "AspNetRefreshTokens");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ExpiryDate",
                table: "AspNetRefreshTokens",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);
        }
    }
}
