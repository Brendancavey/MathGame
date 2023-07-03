using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MathGame.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserScoreStreak : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "highestSessionStreak",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "highestStreak",
                table: "AspNetUsers",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "highestSessionStreak",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "highestStreak",
                table: "AspNetUsers");
        }
    }
}
