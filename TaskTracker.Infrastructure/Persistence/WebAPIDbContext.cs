using Microsoft.EntityFrameworkCore;
using TaskTracker.Domain.Models;
using Task = TaskTracker.Domain.Models.Task;

namespace TaskTracker.Infrastructure.Persistence
{
    public class WebAPIDbContext : DbContext
    {
        public DbSet<Task> Tasks { get; set; }
        public DbSet<TaskGroup> TaskGroups { get; set; }

        public WebAPIDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaskTracker.Domain.Models.Task>().ToTable("tasks", "public");
            modelBuilder.Entity<TaskTracker.Domain.Models.TaskGroup>().ToTable("taskgroups", "public");

            modelBuilder.Entity<Task>()
                .HasOne(e => e.TaskGroup)
                .WithMany(e => e.Tasks)
                .HasForeignKey(e => e.TaskGroupId)
                .IsRequired(false);

            base.OnModelCreating(modelBuilder);
        }

    }
}
