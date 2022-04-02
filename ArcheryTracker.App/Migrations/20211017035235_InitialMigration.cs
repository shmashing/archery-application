using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ArcheryTracker.App.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    LastLogin = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sessions",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: true),
                    Date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Range = table.Column<int>(type: "integer", nullable: false),
                    NumberOfRounds = table.Column<int>(type: "integer", nullable: false),
                    BestRoundScore = table.Column<int>(type: "integer", nullable: false),
                    AverageRoundScore = table.Column<double>(type: "double precision", nullable: false),
                    TotalShots = table.Column<int>(type: "integer", nullable: false),
                    TotalBullseye = table.Column<int>(type: "integer", nullable: false),
                    TotalOnTarget = table.Column<int>(type: "integer", nullable: false),
                    TotalMisses = table.Column<int>(type: "integer", nullable: false),
                    AccuracyOnTarget = table.Column<double>(type: "double precision", nullable: false),
                    AccuracyBullseye = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sessions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserStats",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: true),
                    TotalShots = table.Column<int>(type: "integer", nullable: false),
                    TotalOnTarget = table.Column<int>(type: "integer", nullable: false),
                    ShotsPerSession = table.Column<double>(type: "double precision", nullable: false),
                    ShotsThisMonth = table.Column<int>(type: "integer", nullable: false),
                    TotalBullseye = table.Column<int>(type: "integer", nullable: false),
                    OverallAccuracy = table.Column<double>(type: "double precision", nullable: false),
                    PreviousOverallAccuracy = table.Column<double>(type: "double precision", nullable: false),
                    OverallBullseyeAccuracy = table.Column<double>(type: "double precision", nullable: false),
                    UserId1 = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserStats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserStats_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserStats_Users_UserId1",
                        column: x => x.UserId1,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Rounds",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    SessionId = table.Column<string>(type: "text", nullable: true),
                    RoundNumber = table.Column<int>(type: "integer", nullable: false),
                    TotalScore = table.Column<int>(type: "integer", nullable: false),
                    TotalShots = table.Column<int>(type: "integer", nullable: false),
                    Scores = table.Column<List<int>>(type: "integer[]", nullable: true),
                    SessionId1 = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rounds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rounds_Sessions_SessionId",
                        column: x => x.SessionId,
                        principalTable: "Sessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Rounds_Sessions_SessionId1",
                        column: x => x.SessionId1,
                        principalTable: "Sessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rounds_SessionId",
                table: "Rounds",
                column: "SessionId");

            migrationBuilder.CreateIndex(
                name: "IX_Rounds_SessionId1",
                table: "Rounds",
                column: "SessionId1");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_Id",
                table: "Sessions",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_UserId",
                table: "Sessions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Id",
                table: "Users",
                column: "Id",
                unique: true);

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rounds");

            migrationBuilder.DropTable(
                name: "UserStats");

            migrationBuilder.DropTable(
                name: "Sessions");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
