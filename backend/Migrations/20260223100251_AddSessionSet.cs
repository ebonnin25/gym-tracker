using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class AddSessionSet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SessionExercises_Exercises_ExerciseId",
                table: "SessionExercises");

            migrationBuilder.DropColumn(
                name: "Reps",
                table: "SessionExercises");

            migrationBuilder.DropColumn(
                name: "Sets",
                table: "SessionExercises");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "SessionExercises");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "SessionExercises",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "SessionSets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SessionId = table.Column<Guid>(type: "uuid", nullable: false),
                    ExerciseId = table.Column<Guid>(type: "uuid", nullable: false),
                    Reps = table.Column<int>(type: "integer", nullable: true),
                    Weight = table.Column<decimal>(type: "numeric", nullable: true),
                    Order = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SessionSets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SessionSets_SessionExercises_SessionId_ExerciseId",
                        columns: x => new { x.SessionId, x.ExerciseId },
                        principalTable: "SessionExercises",
                        principalColumns: new[] { "SessionId", "ExerciseId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SessionSets_SessionId_ExerciseId",
                table: "SessionSets",
                columns: new[] { "SessionId", "ExerciseId" });

            migrationBuilder.AddForeignKey(
                name: "FK_SessionExercises_Exercises_ExerciseId",
                table: "SessionExercises",
                column: "ExerciseId",
                principalTable: "Exercises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SessionExercises_Exercises_ExerciseId",
                table: "SessionExercises");

            migrationBuilder.DropTable(
                name: "SessionSets");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "SessionExercises");

            migrationBuilder.AddColumn<int>(
                name: "Reps",
                table: "SessionExercises",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Sets",
                table: "SessionExercises",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Weight",
                table: "SessionExercises",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SessionExercises_Exercises_ExerciseId",
                table: "SessionExercises",
                column: "ExerciseId",
                principalTable: "Exercises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
