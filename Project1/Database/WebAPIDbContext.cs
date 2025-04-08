using Microsoft.EntityFrameworkCore;
using Project1.Models;

namespace Project1.Database
{
    public class WebAPIDbContext : DbContext
    {
        public DbSet<Models.Task> Tasks { get; set; }
        public DbSet<Models.TaskGroup> TaskGroups { get; set; }

        public WebAPIDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.Task>()
                .HasOne(e => e.TaskGroup)
                .WithMany(e => e.Tasks)
                .HasForeignKey(e => e.TaskGroupId)
                .IsRequired(false);

            base.OnModelCreating(modelBuilder);
        }

    }
}
