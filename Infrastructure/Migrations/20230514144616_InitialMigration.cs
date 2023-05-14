using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Difficulties",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaxDigitCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Difficulties", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Operations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Login = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Settings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DifficultyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExerciseCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Settings_Difficulties_DifficultyId",
                        column: x => x.DifficultyId,
                        principalTable: "Difficulties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Statistic",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statistic", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Statistic_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SettingsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Games_Settings_SettingsId",
                        column: x => x.SettingsId,
                        principalTable: "Settings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Games_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SettingsOperations",
                columns: table => new
                {
                    OperationsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SettingsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SettingsOperations", x => new { x.OperationsId, x.SettingsId });
                    table.ForeignKey(
                        name: "FK_SettingsOperations_Operations_OperationsId",
                        column: x => x.OperationsId,
                        principalTable: "Operations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SettingsOperations_Settings_SettingsId",
                        column: x => x.SettingsId,
                        principalTable: "Settings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExerciseProgressStatistic",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    X = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Y = table.Column<TimeSpan>(type: "time", nullable: false),
                    ElementCountStatistic = table.Column<int>(type: "int", nullable: false),
                    StatisticId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExerciseProgressStatistic", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExerciseProgressStatistic_Statistic_StatisticId",
                        column: x => x.StatisticId,
                        principalTable: "Statistic",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "GameStatistic",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GameDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExerciseCount = table.Column<int>(type: "int", nullable: false),
                    GameDuration = table.Column<TimeSpan>(type: "time", nullable: false),
                    CorrectAnswersPercentage = table.Column<double>(type: "float", nullable: false),
                    StatisticId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameStatistic", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameStatistic_Statistic_StatisticId",
                        column: x => x.StatisticId,
                        principalTable: "Statistic",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OperationsStatistic",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    XId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Y = table.Column<TimeSpan>(type: "time", nullable: false),
                    ElementCountStatistic = table.Column<int>(type: "int", nullable: false),
                    StatisticId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperationsStatistic", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OperationsStatistic_Operations_XId",
                        column: x => x.XId,
                        principalTable: "Operations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OperationsStatistic_Statistic_StatisticId",
                        column: x => x.StatisticId,
                        principalTable: "Statistic",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Exercises",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LeftOperand = table.Column<double>(type: "float", nullable: false),
                    RightOperand = table.Column<double>(type: "float", nullable: false),
                    OperationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Answer = table.Column<double>(type: "float", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GameId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exercises", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Exercises_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Exercises_Operations_OperationId",
                        column: x => x.OperationId,
                        principalTable: "Operations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ResolvedGames",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GameId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CorrectAnswerCount = table.Column<int>(type: "int", nullable: false),
                    ElapsedTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    StatisticId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResolvedGames", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResolvedGames_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ResolvedGames_Statistic_StatisticId",
                        column: x => x.StatisticId,
                        principalTable: "Statistic",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ResolvedExercises",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserAnswer = table.Column<double>(type: "float", nullable: false),
                    ElapsedTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    IsCorrect = table.Column<bool>(type: "bit", nullable: false),
                    ExerciseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ResolvedGameId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResolvedExercises", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResolvedExercises_Exercises_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "Exercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ResolvedExercises_ResolvedGames_ResolvedGameId",
                        column: x => x.ResolvedGameId,
                        principalTable: "ResolvedGames",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Difficulties",
                columns: new[] { "Id", "MaxDigitCount", "Name" },
                values: new object[,]
                {
                    { new Guid("128a2a8d-ae3c-4e5e-ac35-5ec2652353b0"), 2, "Easy" },
                    { new Guid("31b99883-df8d-4c03-bfc5-8d1bc83d2eee"), 4, "Hard" },
                    { new Guid("36ab3493-2778-4757-aa17-f874dcf6b990"), 3, "Medium" }
                });

            migrationBuilder.InsertData(
                table: "Operations",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("3c0ab4b3-789b-4cb6-b80f-32d5feff486b"), "Division" },
                    { new Guid("472fe3a7-b8b6-47fa-b74f-07451cd91bc0"), "Addition" },
                    { new Guid("50d78436-3371-421a-8f20-7490bcd58e3a"), "Subtraction" },
                    { new Guid("e713ccff-8cb3-4437-b8fb-f6b664c0d415"), "Multiplication" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseProgressStatistic_StatisticId",
                table: "ExerciseProgressStatistic",
                column: "StatisticId");

            migrationBuilder.CreateIndex(
                name: "IX_Exercises_GameId",
                table: "Exercises",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_Exercises_OperationId",
                table: "Exercises",
                column: "OperationId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_SettingsId",
                table: "Games",
                column: "SettingsId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_UserId",
                table: "Games",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_GameStatistic_StatisticId",
                table: "GameStatistic",
                column: "StatisticId");

            migrationBuilder.CreateIndex(
                name: "IX_OperationsStatistic_StatisticId",
                table: "OperationsStatistic",
                column: "StatisticId");

            migrationBuilder.CreateIndex(
                name: "IX_OperationsStatistic_XId",
                table: "OperationsStatistic",
                column: "XId");

            migrationBuilder.CreateIndex(
                name: "IX_ResolvedExercises_ExerciseId",
                table: "ResolvedExercises",
                column: "ExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_ResolvedExercises_ResolvedGameId",
                table: "ResolvedExercises",
                column: "ResolvedGameId");

            migrationBuilder.CreateIndex(
                name: "IX_ResolvedGames_GameId",
                table: "ResolvedGames",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_ResolvedGames_StatisticId",
                table: "ResolvedGames",
                column: "StatisticId");

            migrationBuilder.CreateIndex(
                name: "IX_Settings_DifficultyId",
                table: "Settings",
                column: "DifficultyId");

            migrationBuilder.CreateIndex(
                name: "IX_SettingsOperations_SettingsId",
                table: "SettingsOperations",
                column: "SettingsId");

            migrationBuilder.CreateIndex(
                name: "IX_Statistic_UserId",
                table: "Statistic",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExerciseProgressStatistic");

            migrationBuilder.DropTable(
                name: "GameStatistic");

            migrationBuilder.DropTable(
                name: "OperationsStatistic");

            migrationBuilder.DropTable(
                name: "ResolvedExercises");

            migrationBuilder.DropTable(
                name: "SettingsOperations");

            migrationBuilder.DropTable(
                name: "Exercises");

            migrationBuilder.DropTable(
                name: "ResolvedGames");

            migrationBuilder.DropTable(
                name: "Operations");

            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "Statistic");

            migrationBuilder.DropTable(
                name: "Settings");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Difficulties");
        }
    }
}
