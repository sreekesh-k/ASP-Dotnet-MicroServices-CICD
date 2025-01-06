using System;
using Microsoft.EntityFrameworkCore;
using StudentsApi.Models;

namespace StudentsApi.Data
{
    public class StudentDbContext : DbContext
    {
        public StudentDbContext(DbContextOptions<StudentDbContext> options) : base(options) { }

        public DbSet<Student> students { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>()
                .HasIndex(s => s.Email)
                .IsUnique();
        }
    }
}
