using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AccountingApp.Data.Migrations
{
    public partial class int_type : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Memo",
                table: "Time",
                maxLength: 160,
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Duration",
                table: "Time",
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Project",
                maxLength: 120,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Customer",
                maxLength: 120,
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Amount",
                table: "Bill",
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Memo",
                table: "Time",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Duration",
                table: "Time",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Project",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Customer",
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "Bill",
                nullable: false);
        }
    }
}
