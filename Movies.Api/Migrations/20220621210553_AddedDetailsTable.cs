using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Movies.Api.Migrations
{
    public partial class AddedDetailsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImdbID",
                table: "MoviesFromFakeDb");

            migrationBuilder.DropColumn(
                name: "Poster",
                table: "MoviesFromFakeDb");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "MoviesFromFakeDb");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "MoviesFromFakeDb");

            migrationBuilder.CreateTable(
                name: "FakeDbMovieDetailsEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ImdbID = table.Column<string>(type: "TEXT", nullable: true),
                    Title = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Year = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    Poster = table.Column<string>(type: "TEXT", nullable: false),
                    FakeDbMovieId = table.Column<int>(type: "INTEGER", nullable: false),
                    FakeDbMovieEntityId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FakeDbMovieDetailsEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FakeDbMovieDetailsEntity_MoviesFromFakeDb_FakeDbMovieEntityId",
                        column: x => x.FakeDbMovieEntityId,
                        principalTable: "MoviesFromFakeDb",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_FakeDbMovieDetailsEntity_FakeDbMovieEntityId",
                table: "FakeDbMovieDetailsEntity",
                column: "FakeDbMovieEntityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FakeDbMovieDetailsEntity");

            migrationBuilder.AddColumn<string>(
                name: "ImdbID",
                table: "MoviesFromFakeDb",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Poster",
                table: "MoviesFromFakeDb",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "MoviesFromFakeDb",
                type: "TEXT",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Year",
                table: "MoviesFromFakeDb",
                type: "TEXT",
                maxLength: 10,
                nullable: false,
                defaultValue: "");
        }
    }
}
