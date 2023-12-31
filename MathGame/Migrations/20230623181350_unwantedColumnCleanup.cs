﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MathGame.Migrations
{
    /// <inheritdoc />
    public partial class unwantedColumnCleanup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "score",
                table: "mathQuestions");

            migrationBuilder.AlterColumn<int>(
                name: "answer",
                table: "mathQuestions",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "answer",
                table: "mathQuestions",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "score",
                table: "mathQuestions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
