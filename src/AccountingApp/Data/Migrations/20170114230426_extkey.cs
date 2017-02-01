using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AccountingApp.Data.Migrations
{
    public partial class extkey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Time_Customer_CustomerID",
                table: "Time");

            migrationBuilder.DropForeignKey(
                name: "FK_Time_Project_ProjectID",
                table: "Time");

            migrationBuilder.DropIndex(
                name: "IX_Time_CustomerID",
                table: "Time");

            migrationBuilder.DropIndex(
                name: "IX_Time_ProjectID",
                table: "Time");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.AlterColumn<int>(
                name: "ProjectID",
                table: "Time",
                nullable: false);

            migrationBuilder.AlterColumn<int>(
                name: "CustomerID",
                table: "Time",
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.ID);
                });

            migrationBuilder.AlterColumn<int>(
                name: "ProjectID",
                table: "Time",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CustomerID",
                table: "Time",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Time_CustomerID",
                table: "Time",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_Time_ProjectID",
                table: "Time",
                column: "ProjectID");

            migrationBuilder.AddForeignKey(
                name: "FK_Time_Customer_CustomerID",
                table: "Time",
                column: "CustomerID",
                principalTable: "Customer",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Time_Project_ProjectID",
                table: "Time",
                column: "ProjectID",
                principalTable: "Project",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
