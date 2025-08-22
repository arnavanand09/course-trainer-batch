using CourseApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CourseApi.Context
{
    public class AppDbContext : DbContext
    {

        public DbSet<Course> Courses { get; set; }
        public DbSet<Trainer> Trainers { get; set; }
        public DbSet<Batch> Batches { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options)
          : base(options) { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Batch -> Course (many-to-one)
            modelBuilder.Entity<Batch>()
        .HasOne(b => b.Course)
        .WithMany()
        .HasForeignKey(b => b.CourseId)
        .OnDelete(DeleteBehavior.Restrict);

            // Batch -> Trainer (many-to-one)
            modelBuilder.Entity<Batch>()
        .HasOne(b => b.Trainer)
        .WithMany() // Changed to WithMany() to fix the error
                .HasForeignKey(b => b.TrainerId)
        .OnDelete(DeleteBehavior.Restrict);
        }
    }
}