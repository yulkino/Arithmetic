﻿// <auto-generated />
using System;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230902114011_UserLoginRenamedToEmail")]
    partial class UserLoginRenamedToEmail
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Domain.Entity.ExerciseEntities.Exercise", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("Answer")
                        .HasColumnType("float");

                    b.Property<Guid?>("GameId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("LeftOperand")
                        .HasColumnType("float");

                    b.Property<Guid>("OperationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("RightOperand")
                        .HasColumnType("float");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.HasIndex("OperationId");

                    b.ToTable("Exercises");
                });

            modelBuilder.Entity("Domain.Entity.ExerciseEntities.ResolvedExercise", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("ElapsedTime")
                        .HasColumnType("float");

                    b.Property<Guid>("ExerciseId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsCorrect")
                        .HasColumnType("bit");

                    b.Property<Guid?>("ResolvedGameId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("UserAnswer")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("ExerciseId");

                    b.HasIndex("ResolvedGameId");

                    b.ToTable("ResolvedExercises");
                });

            modelBuilder.Entity("Domain.Entity.GameEntities.Game", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("SettingsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("SettingsId");

                    b.HasIndex("UserId");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("Domain.Entity.GameEntities.ResolvedGame", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("CorrectAnswerCount")
                        .HasColumnType("int");

                    b.Property<double>("ElapsedTime")
                        .HasColumnType("float");

                    b.Property<Guid>("GameId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("StatisticId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.HasIndex("StatisticId");

                    b.ToTable("ResolvedGames");
                });

            modelBuilder.Entity("Domain.Entity.SettingsEntities.Difficulty", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("MaxDigitCount")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Difficulties");

                    b.HasData(
                        new
                        {
                            Id = new Guid("128a2a8d-ae3c-4e5e-ac35-5ec2652353b0"),
                            MaxDigitCount = 2,
                            Name = "Easy"
                        },
                        new
                        {
                            Id = new Guid("36ab3493-2778-4757-aa17-f874dcf6b990"),
                            MaxDigitCount = 3,
                            Name = "Medium"
                        },
                        new
                        {
                            Id = new Guid("31b99883-df8d-4c03-bfc5-8d1bc83d2eee"),
                            MaxDigitCount = 4,
                            Name = "Hard"
                        });
                });

            modelBuilder.Entity("Domain.Entity.SettingsEntities.Operation", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Operations");

                    b.HasData(
                        new
                        {
                            Id = new Guid("472fe3a7-b8b6-47fa-b74f-07451cd91bc0"),
                            Name = "Addition"
                        },
                        new
                        {
                            Id = new Guid("3c0ab4b3-789b-4cb6-b80f-32d5feff486b"),
                            Name = "Division"
                        },
                        new
                        {
                            Id = new Guid("e713ccff-8cb3-4437-b8fb-f6b664c0d415"),
                            Name = "Multiplication"
                        },
                        new
                        {
                            Id = new Guid("50d78436-3371-421a-8f20-7490bcd58e3a"),
                            Name = "Subtraction"
                        });
                });

            modelBuilder.Entity("Domain.Entity.SettingsEntities.Settings", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("DifficultyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ExerciseCount")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DifficultyId");

                    b.ToTable("Settings");
                });

            modelBuilder.Entity("Domain.Entity.Statistic", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Statistic");
                });

            modelBuilder.Entity("Domain.Entity.User", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("IdentityId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("IdentityId")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Domain.StatisticStaff.ExerciseProgressStatistic", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ElementCountStatistic")
                        .HasColumnType("int");

                    b.Property<Guid?>("StatisticId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("X")
                        .HasColumnType("datetime2");

                    b.Property<double>("Y")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("StatisticId");

                    b.ToTable("ExerciseProgressStatistic");
                });

            modelBuilder.Entity("Domain.StatisticStaff.GameStatistic", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("CorrectAnswersPercentage")
                        .HasColumnType("float");

                    b.Property<int>("ExerciseCount")
                        .HasColumnType("int");

                    b.Property<DateTime>("GameDate")
                        .HasColumnType("datetime2");

                    b.Property<double>("GameDuration")
                        .HasColumnType("float");

                    b.Property<Guid?>("StatisticId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("StatisticId");

                    b.ToTable("GameStatistic");
                });

            modelBuilder.Entity("Domain.StatisticStaff.OperationsStatistic", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ElementCountStatistic")
                        .HasColumnType("int");

                    b.Property<Guid?>("StatisticId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("XId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("Y")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("StatisticId");

                    b.HasIndex("XId");

                    b.ToTable("OperationsStatistic");
                });

            modelBuilder.Entity("SettingsOperations", b =>
                {
                    b.Property<Guid>("OperationsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SettingsId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("OperationsId", "SettingsId");

                    b.HasIndex("SettingsId");

                    b.ToTable("SettingsOperations");
                });

            modelBuilder.Entity("Domain.Entity.ExerciseEntities.Exercise", b =>
                {
                    b.HasOne("Domain.Entity.GameEntities.Game", null)
                        .WithMany("Exercises")
                        .HasForeignKey("GameId");

                    b.HasOne("Domain.Entity.SettingsEntities.Operation", "Operation")
                        .WithMany()
                        .HasForeignKey("OperationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Operation");
                });

            modelBuilder.Entity("Domain.Entity.ExerciseEntities.ResolvedExercise", b =>
                {
                    b.HasOne("Domain.Entity.ExerciseEntities.Exercise", "Exercise")
                        .WithMany()
                        .HasForeignKey("ExerciseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entity.GameEntities.ResolvedGame", null)
                        .WithMany("ResolvedExercises")
                        .HasForeignKey("ResolvedGameId");

                    b.Navigation("Exercise");
                });

            modelBuilder.Entity("Domain.Entity.GameEntities.Game", b =>
                {
                    b.HasOne("Domain.Entity.SettingsEntities.Settings", "Settings")
                        .WithMany()
                        .HasForeignKey("SettingsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entity.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Settings");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Entity.GameEntities.ResolvedGame", b =>
                {
                    b.HasOne("Domain.Entity.GameEntities.Game", "Game")
                        .WithMany()
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entity.Statistic", null)
                        .WithMany("ResolvedGame")
                        .HasForeignKey("StatisticId");

                    b.Navigation("Game");
                });

            modelBuilder.Entity("Domain.Entity.SettingsEntities.Settings", b =>
                {
                    b.HasOne("Domain.Entity.SettingsEntities.Difficulty", "Difficulty")
                        .WithMany()
                        .HasForeignKey("DifficultyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Difficulty");
                });

            modelBuilder.Entity("Domain.Entity.Statistic", b =>
                {
                    b.HasOne("Domain.Entity.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.StatisticStaff.ExerciseProgressStatistic", b =>
                {
                    b.HasOne("Domain.Entity.Statistic", null)
                        .WithMany("ExerciseProgressStatistic")
                        .HasForeignKey("StatisticId");
                });

            modelBuilder.Entity("Domain.StatisticStaff.GameStatistic", b =>
                {
                    b.HasOne("Domain.Entity.Statistic", null)
                        .WithMany("GameStatistic")
                        .HasForeignKey("StatisticId");
                });

            modelBuilder.Entity("Domain.StatisticStaff.OperationsStatistic", b =>
                {
                    b.HasOne("Domain.Entity.Statistic", null)
                        .WithMany("OperationsStatistic")
                        .HasForeignKey("StatisticId");

                    b.HasOne("Domain.Entity.SettingsEntities.Operation", "X")
                        .WithMany()
                        .HasForeignKey("XId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("X");
                });

            modelBuilder.Entity("SettingsOperations", b =>
                {
                    b.HasOne("Domain.Entity.SettingsEntities.Operation", null)
                        .WithMany()
                        .HasForeignKey("OperationsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entity.SettingsEntities.Settings", null)
                        .WithMany()
                        .HasForeignKey("SettingsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Entity.GameEntities.Game", b =>
                {
                    b.Navigation("Exercises");
                });

            modelBuilder.Entity("Domain.Entity.GameEntities.ResolvedGame", b =>
                {
                    b.Navigation("ResolvedExercises");
                });

            modelBuilder.Entity("Domain.Entity.Statistic", b =>
                {
                    b.Navigation("ExerciseProgressStatistic");

                    b.Navigation("GameStatistic");

                    b.Navigation("OperationsStatistic");

                    b.Navigation("ResolvedGame");
                });
#pragma warning restore 612, 618
        }
    }
}
