using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Movies.Api.Migrations
{
    public partial class InitializeMoviesDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FakeDbMovieDetailsEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Year = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    Poster = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FakeDbMovieDetailsEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MoviesFromOmDb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Year = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    Genre = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Director = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Plot = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoviesFromOmDb", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MoviesFromFakeDb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MovieDetailsId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoviesFromFakeDb", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MoviesFromFakeDb_FakeDbMovieDetailsEntity_MovieDetailsId",
                        column: x => x.MovieDetailsId,
                        principalTable: "FakeDbMovieDetailsEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MoviesFromFakeDb_MovieDetailsId",
                table: "MoviesFromFakeDb",
                column: "MovieDetailsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MoviesFromFakeDb");

            migrationBuilder.DropTable(
                name: "MoviesFromOmDb");

            migrationBuilder.DropTable(
                name: "FakeDbMovieDetailsEntity");
        }
    }
}
