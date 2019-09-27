using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AbpDemo.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FileInfo",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FullName = table.Column<string>(maxLength: 300, nullable: true),
                    Name = table.Column<string>(maxLength: 100, nullable: true),
                    Extension = table.Column<string>(maxLength: 100, nullable: true),
                    Length = table.Column<long>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    LastWriteTime = table.Column<DateTime>(nullable: false),
                    LastAccessTime = table.Column<DateTime>(nullable: false),
                    SHA1 = table.Column<string>(maxLength: 36, nullable: true),
                    TimeSpan = table.Column<TimeSpan>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileInfo", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FileInfo");
        }
    }
}
