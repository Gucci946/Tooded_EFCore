using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tooded_EFCore.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kategooriad",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KategooriaNimetus = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kategooriad", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tooted",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Toodenimetus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Kogus = table.Column<int>(type: "int", nullable: false),
                    Hind = table.Column<float>(type: "real", nullable: false),
                    Pilt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KategooriaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tooted", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tooted_Kategooriad_KategooriaId",
                        column: x => x.KategooriaId,
                        principalTable: "Kategooriad",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tooted_KategooriaId",
                table: "Tooted",
                column: "KategooriaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tooted");

            migrationBuilder.DropTable(
                name: "Kategooriad");
        }
    }
}
