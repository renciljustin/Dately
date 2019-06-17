using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dately.Migrations
{
    public partial class ModifyUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Interest",
                table: "AspNetUsers",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "Gender",
                table: "AspNetUsers",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<DateTime>(
                name: "BirthDate",
                table: "AspNetUsers",
                type: "DATE",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "DATE");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Interest",
                table: "AspNetUsers",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Gender",
                table: "AspNetUsers",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "BirthDate",
                table: "AspNetUsers",
                type: "DATE",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "DATE",
                oldNullable: true);
        }
    }
}
