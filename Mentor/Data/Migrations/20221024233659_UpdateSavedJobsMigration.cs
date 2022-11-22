using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mentor.Migrations
{
    public partial class UpdateSavedJobsMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_SavedJobs_SavedJobId",
                table: "Jobs");

            migrationBuilder.DropForeignKey(
                name: "FK_SavedJobs_Users_UserId",
                table: "SavedJobs");

            migrationBuilder.DropIndex(
                name: "IX_SavedJobs_UserId",
                table: "SavedJobs");

            migrationBuilder.DropIndex(
                name: "IX_Jobs_SavedJobId",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "SavedJobs");

            migrationBuilder.DropColumn(
                name: "SavedJobId",
                table: "Jobs");

            migrationBuilder.RenameColumn(
                name: "JobsId",
                table: "SavedJobs",
                newName: "UserName");

            migrationBuilder.AddColumn<int>(
                name: "JobId",
                table: "SavedJobs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "EmployeeCV",
                table: "ApplyForJobs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "HasCV",
                table: "ApplyForJobs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_SavedJobs_JobId",
                table: "SavedJobs",
                column: "JobId");

            migrationBuilder.AddForeignKey(
                name: "FK_SavedJobs_Jobs_JobId",
                table: "SavedJobs",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "JobId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SavedJobs_Jobs_JobId",
                table: "SavedJobs");

            migrationBuilder.DropIndex(
                name: "IX_SavedJobs_JobId",
                table: "SavedJobs");

            migrationBuilder.DropColumn(
                name: "JobId",
                table: "SavedJobs");

            migrationBuilder.DropColumn(
                name: "EmployeeCV",
                table: "ApplyForJobs");

            migrationBuilder.DropColumn(
                name: "HasCV",
                table: "ApplyForJobs");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "SavedJobs",
                newName: "JobsId");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "SavedJobs",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "SavedJobId",
                table: "Jobs",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SavedJobs_UserId",
                table: "SavedJobs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_SavedJobId",
                table: "Jobs",
                column: "SavedJobId");

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_SavedJobs_SavedJobId",
                table: "Jobs",
                column: "SavedJobId",
                principalTable: "SavedJobs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SavedJobs_Users_UserId",
                table: "SavedJobs",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
