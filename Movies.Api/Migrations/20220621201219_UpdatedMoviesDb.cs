using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Movies.Api.Migrations
{
    public partial class UpdatedMoviesDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MoviesFromFakeDb_FakeDbMovieDetailsEntity_MovieDetailsId",
                table: "MoviesFromFakeDb");

            migrationBuilder.DropIndex(
                name: "IX_MoviesFromFakeDb_MovieDetailsId",
                table: "MoviesFromFakeDb");

            migrationBuilder.DropColumn(
                name: "MovieDetailsId",
                table: "MoviesFromFakeDb");

            migrationBuilder.AddColumn<int>(
                name: "FakeDbMovieEntityId",
                table: "FakeDbMovieDetailsEntity",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FakeDbMovieId",
                table: "FakeDbMovieDetailsEntity",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_FakeDbMovieDetailsEntity_FakeDbMovieEntityId",
                table: "FakeDbMovieDetailsEntity",
                column: "FakeDbMovieEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_FakeDbMovieDetailsEntity_MoviesFromFakeDb_FakeDbMovieEntityId",
                table: "FakeDbMovieDetailsEntity",
                column: "FakeDbMovieEntityId",
                principalTable: "MoviesFromFakeDb",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FakeDbMovieDetailsEntity_MoviesFromFakeDb_FakeDbMovieEntityId",
                table: "FakeDbMovieDetailsEntity");

            migrationBuilder.DropIndex(
                name: "IX_FakeDbMovieDetailsEntity_FakeDbMovieEntityId",
                table: "FakeDbMovieDetailsEntity");

            migrationBuilder.DropColumn(
                name: "FakeDbMovieEntityId",
                table: "FakeDbMovieDetailsEntity");

            migrationBuilder.DropColumn(
                name: "FakeDbMovieId",
                table: "FakeDbMovieDetailsEntity");

            migrationBuilder.AddColumn<int>(
                name: "MovieDetailsId",
                table: "MoviesFromFakeDb",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_MoviesFromFakeDb_MovieDetailsId",
                table: "MoviesFromFakeDb",
                column: "MovieDetailsId");

            migrationBuilder.AddForeignKey(
                name: "FK_MoviesFromFakeDb_FakeDbMovieDetailsEntity_MovieDetailsId",
                table: "MoviesFromFakeDb",
                column: "MovieDetailsId",
                principalTable: "FakeDbMovieDetailsEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
