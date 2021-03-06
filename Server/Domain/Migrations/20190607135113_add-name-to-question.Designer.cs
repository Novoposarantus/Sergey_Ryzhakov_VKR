﻿// <auto-generated />
using System;
using Domain.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Domain.Migrations
{
    [DbContext(typeof(RepositoryContext))]
    [Migration("20190607135113_add-name-to-question")]
    partial class addnametoquestion
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Models.Models.AnswerModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsRight");

                    b.Property<int>("QuestionId");

                    b.Property<string>("Text");

                    b.HasKey("Id");

                    b.HasIndex("QuestionId");

                    b.ToTable("Answers");
                });

            modelBuilder.Entity("Models.Models.AssignmentModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateCreate");

                    b.Property<int?>("Duration");

                    b.Property<double?>("Result");

                    b.Property<int>("TestModelId");

                    b.Property<int>("UserModelId");

                    b.HasKey("Id");

                    b.HasIndex("TestModelId");

                    b.HasIndex("UserModelId");

                    b.ToTable("Assignments");
                });

            modelBuilder.Entity("Models.Models.QuestionModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code");

                    b.Property<string>("Name");

                    b.Property<int>("QuestionTypeId");

                    b.Property<string>("Text");

                    b.HasKey("Id");

                    b.HasIndex("QuestionTypeId");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("Models.Models.QuestionToTestModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Difficulty");

                    b.Property<int>("QuestionId");

                    b.Property<int>("ReferenceResponseSeconds");

                    b.Property<int>("TestId");

                    b.HasKey("Id");

                    b.HasIndex("QuestionId");

                    b.HasIndex("TestId");

                    b.ToTable("QuestionToTests");
                });

            modelBuilder.Entity("Models.Models.QuestionTypeModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("QuestionTypes");
                });

            modelBuilder.Entity("Models.Models.QuestionsResultAnswersModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AnswerModelId");

                    b.Property<int>("AssignmentModelId");

                    b.HasKey("Id");

                    b.HasIndex("AnswerModelId");

                    b.HasIndex("AssignmentModelId");

                    b.ToTable("QuestionsResultAnswers");
                });

            modelBuilder.Entity("Models.Models.RoleModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "admin"
                        },
                        new
                        {
                            Id = 2,
                            Name = "user"
                        });
                });

            modelBuilder.Entity("Models.Models.TestModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Tests");
                });

            modelBuilder.Entity("Models.Models.TestResultQuestionsModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AssignmentModelId");

                    b.Property<int>("Milliseconds");

                    b.Property<int>("QuestionModelId");

                    b.HasKey("Id");

                    b.HasIndex("AssignmentModelId");

                    b.HasIndex("QuestionModelId");

                    b.ToTable("TestResultQuestions");
                });

            modelBuilder.Entity("Models.Models.UserModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Password");

                    b.Property<int>("RoleId");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Password = "pmWkWSBCL51Bfkhn79xPuKBKHz//H6B+mY6G9/eieuM=",
                            RoleId = 1,
                            UserName = "admin"
                        },
                        new
                        {
                            Id = 2,
                            Password = "pmWkWSBCL51Bfkhn79xPuKBKHz//H6B+mY6G9/eieuM=",
                            RoleId = 2,
                            UserName = "user"
                        });
                });

            modelBuilder.Entity("Models.Models.AnswerModel", b =>
                {
                    b.HasOne("Models.Models.QuestionModel", "Question")
                        .WithMany("Answers")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Models.Models.AssignmentModel", b =>
                {
                    b.HasOne("Models.Models.TestModel", "Test")
                        .WithMany()
                        .HasForeignKey("TestModelId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Models.Models.UserModel", "User")
                        .WithMany()
                        .HasForeignKey("UserModelId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Models.Models.QuestionModel", b =>
                {
                    b.HasOne("Models.Models.QuestionTypeModel", "QuestionType")
                        .WithMany()
                        .HasForeignKey("QuestionTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Models.Models.QuestionToTestModel", b =>
                {
                    b.HasOne("Models.Models.QuestionModel", "Question")
                        .WithMany("QuestionToTests")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Models.Models.TestModel", "Test")
                        .WithMany("QuestionToTests")
                        .HasForeignKey("TestId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Models.Models.QuestionsResultAnswersModel", b =>
                {
                    b.HasOne("Models.Models.AnswerModel", "Answer")
                        .WithMany()
                        .HasForeignKey("AnswerModelId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Models.Models.AssignmentModel", "Assignment")
                        .WithMany("QuestionsResults")
                        .HasForeignKey("AssignmentModelId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Models.Models.TestResultQuestionsModel", b =>
                {
                    b.HasOne("Models.Models.AssignmentModel", "Assignment")
                        .WithMany("TestResults")
                        .HasForeignKey("AssignmentModelId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Models.Models.QuestionModel", "Question")
                        .WithMany()
                        .HasForeignKey("QuestionModelId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Models.Models.UserModel", b =>
                {
                    b.HasOne("Models.Models.RoleModel", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
