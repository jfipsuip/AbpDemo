using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AbpDemo.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sys_Area",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: true),
                    AreaCode = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_Area", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys_Dept",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: true),
                    Code = table.Column<string>(maxLength: 100, nullable: true),
                    AreaId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_Dept", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sys_Dept_Sys_Area_AreaId",
                        column: x => x.AreaId,
                        principalTable: "Sys_Area",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sys_Dept_AreaId",
                table: "Sys_Dept",
                column: "AreaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sys_Dept");

            migrationBuilder.DropTable(
                name: "Sys_Area");
        }
    }
}
