using  Microsoft.EntityFrameworkCore;
using mockOSApi.Controllers;
using mockOSApi.Models;

namespace mockOSApi.Data;

public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Define DbSet properties for your entities
        public DbSet<mockOSApi.Models.Thread> Threads { get; set; }
        
        public DbSet<mockOSApi.Models.Process<TestController>> Processes { get;set; }

        public DbSet<mockOSApi.Models.ProcessCounter> ProcessCounters { get;set;}
        // Add more DbSet properties for other entities as needed

        // Optionally override OnModelCreating method for entity configurations
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure entity mappings and relationships here
        }
    }
