using Microsoft.EntityFrameworkCore.Migrations;

namespace ArcheryTracker.App.Migrations
{
    public partial class RefactorDataLayer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rounds_Sessions_SessionId1",
                table: "Rounds");

            migrationBuilder.DropForeignKey(
                name: "FK_UserStats_Users_UserId",
                table: "UserStats");

            migrationBuilder.DropForeignKey(
                name: "FK_UserStats_Users_UserId1",
                table: "UserStats");

            migrationBuilder.DropIndex(
                name: "IX_UserStats_UserId",
                table: "UserStats");

            migrationBuilder.DropIndex(
                name: "IX_UserStats_UserId1",
                table: "UserStats");

            migrationBuilder.DropIndex(
                name: "IX_Sessions_Id",
                table: "Sessions");

            migrationBuilder.DropIndex(
                name: "IX_Rounds_SessionId1",
                table: "Rounds");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "UserStats");

            migrationBuilder.DropColumn(
                name: "AccuracyBullseye",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "AccuracyOnTarget",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "AverageRoundScore",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "BestRoundScore",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "NumberOfRounds",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "Range",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "TotalBullseye",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "TotalMisses",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "TotalOnTarget",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "TotalShots",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "RoundNumber",
                table: "Rounds");

            migrationBuilder.DropColumn(
                name: "TotalScore",
                table: "Rounds");

            migrationBuilder.DropColumn(
                name: "TotalShots",
                table: "Rounds");

            migrationBuilder.RenameColumn(
                name: "SessionId1",
                table: "Rounds",
                newName: "UserId");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_Id",
                table: "Sessions",
                column: "Id",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Sessions_Id",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Rounds",
                newName: "SessionId1");

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "UserStats",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "AccuracyBullseye",
                table: "Sessions",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "AccuracyOnTarget",
                table: "Sessions",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "AverageRoundScore",
                table: "Sessions",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "BestRoundScore",
                table: "Sessions",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfRounds",
                table: "Sessions",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Range",
                table: "Sessions",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalBullseye",
                table: "Sessions",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalMisses",
                table: "Sessions",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalOnTarget",
                table: "Sessions",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalShots",
                table: "Sessions",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RoundNumber",
                table: "Rounds",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalScore",
                table: "Rounds",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalShots",
                table: "Rounds",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_UserStats_UserId",
                table: "UserStats",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserStats_UserId1",
                table: "UserStats",
                column: "UserId1",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_Id",
                table: "Sessions",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Rounds_SessionId1",
                table: "Rounds",
                column: "SessionId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Rounds_Sessions_SessionId1",
                table: "Rounds",
                column: "SessionId1",
                principalTable: "Sessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserStats_Users_UserId",
                table: "UserStats",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserStats_Users_UserId1",
                table: "UserStats",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
