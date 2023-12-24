using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API_CRUD_1B.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SinhVien",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HoTen = table.Column<string>(nullable: true),
                    GioiTinh = table.Column<bool>(nullable: false),
                    Tuoi = table.Column<int>(nullable: false),
                    NgaySinh = table.Column<DateTime>(nullable: false),
                    Luong = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SinhVien", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SinhVien");
        }
    }
}
