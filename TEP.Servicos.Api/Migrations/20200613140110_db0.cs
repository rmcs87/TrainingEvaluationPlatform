using Microsoft.EntityFrameworkCore.Migrations;

namespace TEP.Servicos.Api.Migrations
{
    public partial class db0 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_simple_asset",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Path = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_simple_asset", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_interaction",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    act = table.Column<int>(nullable: false),
                    description = table.Column<string>(nullable: false),
                    estimated_time = table.Column<double>(nullable: false),
                    limit_time = table.Column<double>(nullable: false),
                    TargetId = table.Column<int>(nullable: true),
                    SourceId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_interaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_interaction_tbl_simple_asset_SourceId",
                        column: x => x.SourceId,
                        principalTable: "tbl_simple_asset",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tbl_interaction_tbl_simple_asset_TargetId",
                        column: x => x.TargetId,
                        principalTable: "tbl_simple_asset",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_interaction_SourceId",
                table: "tbl_interaction",
                column: "SourceId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_interaction_TargetId",
                table: "tbl_interaction",
                column: "TargetId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_interaction");

            migrationBuilder.DropTable(
                name: "tbl_simple_asset");
        }
    }
}
