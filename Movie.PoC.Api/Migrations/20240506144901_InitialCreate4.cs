using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Movie.PoC.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Production",
                table: "FilmDatas");

            migrationBuilder.DropColumn(
                name: "Website",
                table: "FilmDatas");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Production",
                table: "FilmDatas",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Website",
                table: "FilmDatas",
                type: "TEXT",
                nullable: true);
        }
    }
}
