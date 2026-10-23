using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Tooded_EFCore.Migrations.Tooded
{
    /// <inheritdoc />
    public partial class LisaAlgkategooriad : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Kategooriatabel",
                columns: new[] { "Id", "KategooriaNimetus" },
                values: new object[,]
                {
                    { 1, "Joogid" },
                    { 2, "Puu- ja köögiviljad" },
                    { 3, "Maiustused" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Kategooriatabel",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Kategooriatabel",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Kategooriatabel",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
