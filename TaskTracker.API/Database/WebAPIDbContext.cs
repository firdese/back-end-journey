using Microsoft.EntityFrameworkCore;
using TaskTrackerAPI.Models;
using Task = TaskTrackerAPI.Models.Task;

namespace TaskTrackerAPI.Database
{
    public class WebAPIDbContext : DbContext
    {
        public DbSet<Models.Task> Tasks { get; set; }
        public DbSet<Models.TaskGroup> TaskGroups { get; set; }

        public WebAPIDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Task>().ToTable("tasks", "public");
            modelBuilder.Entity<TaskGroup>().ToTable("taskgroups", "public");

            modelBuilder.Entity<Models.Task>()
                .HasOne(e => e.TaskGroup)
                .WithMany(e => e.Tasks)
                .HasForeignKey(e => e.TaskGroupId)
                .IsRequired(false);

            base.OnModelCreating(modelBuilder);
        }

    }
}
