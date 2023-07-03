using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MathGame.Migrations
{
    /// <inheritdoc />
    public partial class addPrevSessionScore : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "sessionScore",
                table: "AspNetUsers",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "sessionScore",
                table: "AspNetUsers");
        }
    }
}
