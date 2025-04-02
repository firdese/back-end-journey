using Microsoft.EntityFrameworkCore;
using Project1.Models;

namespace Project1.Database
{
    public class WebAPIDbContext : DbContext
    {
        public WebAPIDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Models.Task> Tasks { get; set; }
    }
}
