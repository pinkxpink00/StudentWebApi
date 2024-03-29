﻿using Microsoft.EntityFrameworkCore;
using StudentWebApi.Models;

namespace StudentWebApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Student> Students { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=testdb;Username=postgres;Password=Paradise71");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Student>().HasData(
                new Student
                {
                    Id = 1,
                    FirstName = "German",
                    LastName = "Gritsenko",
                    Email = "SomeEmail@gmail.com",
                    Gender = Gender.Other,
                    DepartmentId = 1,
                    PhotoPath = "Images/Sam.png"
                });
        }
    }
}
