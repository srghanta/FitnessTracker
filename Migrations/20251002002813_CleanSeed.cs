using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FitnessTracker.Migrations
{
    /// <inheritdoc />
    public partial class CleanSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Workouts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Workouts",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "UserProfiles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "UserProfiles",
                keyColumn: "Id",
                keyValue: 2);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "UserProfiles",
                columns: new[] { "Id", "Age", "Height", "UserName", "Weight" },
                values: new object[,]
                {
                    { 1, 0, 0.0, "TestUser1", 0.0 },
                    { 2, 0, 0.0, "TestUser2", 0.0 }
                });

            migrationBuilder.InsertData(
                table: "Workouts",
                columns: new[] { "Id", "Date", "DurationMinutes", "Name", "UserProfileId" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 10, 2, 0, 18, 40, 294, DateTimeKind.Utc).AddTicks(7550), 30, "Morning Cardio", 1 },
                    { 2, new DateTime(2025, 10, 2, 0, 18, 40, 294, DateTimeKind.Utc).AddTicks(8058), 45, "Evening Strength", 2 }
                });
        }
    }
}
