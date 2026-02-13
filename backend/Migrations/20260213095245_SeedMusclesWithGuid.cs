using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class SeedMusclesWithGuid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Muscles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("62cc5531-ebdb-4b8e-8227-505c300285cc"), "Mollets" },
                    { new Guid("66cefe28-52e3-46bc-8fc6-2c52090b1922"), "Épaules" },
                    { new Guid("6b93f3f6-73bb-4147-83c3-401c5a698b2a"), "Abdos" },
                    { new Guid("7490b915-3115-4898-b86a-1855c095e814"), "Lombaires" },
                    { new Guid("992bf433-2f92-4569-ac29-85b8f7066ff9"), "Triceps" },
                    { new Guid("9a1fda82-7bcb-432f-85b6-d79c9bee783d"), "Biceps" },
                    { new Guid("9bb5a066-0706-4dd1-8faa-a07700e5a50f"), "Fessiers" },
                    { new Guid("bae51a45-0eb0-4e51-97ae-e70bbe009eb6"), "Pectoraux" },
                    { new Guid("c8498283-5483-491f-adbc-9dad4d591196"), "Dos" },
                    { new Guid("cf5d0331-4cca-4b0e-887d-8f453cf497a3"), "Ischios" },
                    { new Guid("fcb9abb3-2715-4d5a-b0db-30c82dc1f60a"), "Quadriceps" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Muscles",
                keyColumn: "Id",
                keyValue: new Guid("62cc5531-ebdb-4b8e-8227-505c300285cc"));

            migrationBuilder.DeleteData(
                table: "Muscles",
                keyColumn: "Id",
                keyValue: new Guid("66cefe28-52e3-46bc-8fc6-2c52090b1922"));

            migrationBuilder.DeleteData(
                table: "Muscles",
                keyColumn: "Id",
                keyValue: new Guid("6b93f3f6-73bb-4147-83c3-401c5a698b2a"));

            migrationBuilder.DeleteData(
                table: "Muscles",
                keyColumn: "Id",
                keyValue: new Guid("7490b915-3115-4898-b86a-1855c095e814"));

            migrationBuilder.DeleteData(
                table: "Muscles",
                keyColumn: "Id",
                keyValue: new Guid("992bf433-2f92-4569-ac29-85b8f7066ff9"));

            migrationBuilder.DeleteData(
                table: "Muscles",
                keyColumn: "Id",
                keyValue: new Guid("9a1fda82-7bcb-432f-85b6-d79c9bee783d"));

            migrationBuilder.DeleteData(
                table: "Muscles",
                keyColumn: "Id",
                keyValue: new Guid("9bb5a066-0706-4dd1-8faa-a07700e5a50f"));

            migrationBuilder.DeleteData(
                table: "Muscles",
                keyColumn: "Id",
                keyValue: new Guid("bae51a45-0eb0-4e51-97ae-e70bbe009eb6"));

            migrationBuilder.DeleteData(
                table: "Muscles",
                keyColumn: "Id",
                keyValue: new Guid("c8498283-5483-491f-adbc-9dad4d591196"));

            migrationBuilder.DeleteData(
                table: "Muscles",
                keyColumn: "Id",
                keyValue: new Guid("cf5d0331-4cca-4b0e-887d-8f453cf497a3"));

            migrationBuilder.DeleteData(
                table: "Muscles",
                keyColumn: "Id",
                keyValue: new Guid("fcb9abb3-2715-4d5a-b0db-30c82dc1f60a"));
        }
    }
}
