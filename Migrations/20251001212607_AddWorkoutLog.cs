using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitnessTracker.Migrations
{
    /// <inheritdoc />
    public partial class AddWorkoutLog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserProfile",
                table: "Workouts");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Workouts_UserProfiles_UserProfileId",
                table: "Workouts");

            migrationBuilder.DropIndex(
                name: "IX_Workouts_UserProfileId",
                table: "Workouts");

            migrationBuilder.AddColumn<string>(
                name: "UserProfile",
                table: "Workouts",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
