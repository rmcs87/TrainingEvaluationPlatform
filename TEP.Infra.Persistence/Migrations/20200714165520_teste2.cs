using Microsoft.EntityFrameworkCore.Migrations;

namespace TEP.Infra.Persistence.Migrations
{
    public partial class teste2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FilePath",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "ImgPath",
                table: "Assets");

            migrationBuilder.AddColumn<string>(
                name: "FileURI",
                table: "Assets",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IconPath",
                table: "Assets",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileURI",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "IconPath",
                table: "Assets");

            migrationBuilder.AddColumn<string>(
                name: "FilePath",
                table: "Assets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImgPath",
                table: "Assets",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
