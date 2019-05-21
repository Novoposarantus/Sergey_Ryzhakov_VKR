﻿using Microsoft.EntityFrameworkCore;
using Domain.Helpers;
using Models.Models;

namespace Domain.Context
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions<RepositoryContext> options) : base(options) { }

        public DbSet<UserModel> Users { get; set; }
        public DbSet<RoleModel> Roles { get; set; }
        public DbSet<QuestionModel> Questions { get; set; }
        public DbSet<AnswerModel> Answers { get; set; }
        public DbSet<QuestionTypeModel> QuestionTypes { get; set; }
        public DbSet<UserAnswerModel> UserAnswers { get; set; }
        public DbSet<TestModel> Tests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<RoleModel>().HasData(
                new RoleModel() { Id = 1, Name = "admin" },
                new RoleModel() { Id = 2, Name = "user" }
            );

            modelBuilder.Entity<UserModel>().HasData(
                new UserModel() { Id = 1, UserName = "admin", Password = AuthenticationHelper.HashPassword("123"), RoleId = 1 },
                new UserModel() { Id = 2, UserName = "user", Password = AuthenticationHelper.HashPassword("123"), RoleId = 2 }
            );
        }
    }
}