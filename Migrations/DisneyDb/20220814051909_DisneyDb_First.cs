using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DisneyApi.Migrations.DisneyDb
{
    public partial class DisneyDb_First : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Characters",
                columns: table => new
                {
                    Character_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Weight = table.Column<float>(type: "real", nullable: false),
                    History = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Characters", x => x.Character_Id);
                });

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    Genre_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Genre_Id);
                });

            migrationBuilder.CreateTable(
                name: "Media",
                columns: table => new
                {
                    MovieSerie_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tittle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Rate = table.Column<int>(type: "int", nullable: false),
                    Genre_Id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Media", x => x.MovieSerie_Id);
                    table.ForeignKey(
                        name: "FK_Media_Genres_Genre_Id",
                        column: x => x.Genre_Id,
                        principalTable: "Genres",
                        principalColumn: "Genre_Id");
                });

            migrationBuilder.CreateTable(
                name: "MovieSerieCharacters",
                columns: table => new
                {
                    CharactersCharacter_Id = table.Column<int>(type: "int", nullable: false),
                    MoviesAndSeriesMovieSerie_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieSerieCharacters", x => new { x.CharactersCharacter_Id, x.MoviesAndSeriesMovieSerie_Id });
                    table.ForeignKey(
                        name: "FK_MovieSerieCharacters_Characters_CharactersCharacter_Id",
                        column: x => x.CharactersCharacter_Id,
                        principalTable: "Characters",
                        principalColumn: "Character_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovieSerieCharacters_Media_MoviesAndSeriesMovieSerie_Id",
                        column: x => x.MoviesAndSeriesMovieSerie_Id,
                        principalTable: "Media",
                        principalColumn: "MovieSerie_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Media_Genre_Id",
                table: "Media",
                column: "Genre_Id");

            migrationBuilder.CreateIndex(
                name: "IX_MovieSerieCharacters_MoviesAndSeriesMovieSerie_Id",
                table: "MovieSerieCharacters",
                column: "MoviesAndSeriesMovieSerie_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MovieSerieCharacters");

            migrationBuilder.DropTable(
                name: "Characters");

            migrationBuilder.DropTable(
                name: "Media");

            migrationBuilder.DropTable(
                name: "Genres");
        }
    }
}
