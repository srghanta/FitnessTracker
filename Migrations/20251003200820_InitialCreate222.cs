using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitnessTracker.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate222 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Workouts_UserProfiles_UserProfileId",
                table: "Workouts");

            migrationBuilder.DropIndex(
                name: "IX_Workouts_UserProfileId",
                table: "Workouts");

            migrationBuilder.DropColumn(
                name: "UserProfileId",
                table: "Workouts");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Workouts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Workouts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "NutritionLogs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Goals",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Workouts");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Workouts");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "NutritionLogs");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Goals");

            migrationBuilder.AddColumn<int>(
                name: "UserProfileId",
                table: "Workouts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Workouts_UserProfileId",
                table: "Workouts",
                column: "UserProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Workouts_UserProfiles_UserProfileId",
                table: "Workouts",
                column: "UserProfileId",
                principalTable: "UserProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
