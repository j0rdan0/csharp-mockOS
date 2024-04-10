using Microsoft.EntityFrameworkCore;
using mockOSApi.Models;

namespace mockOSApi.Data;


public class OSDbContext : DbContext
{
    public OSDbContext(DbContextOptions<OSDbContext> options) : base(options) { }

    public DbSet<MockProcess> MockProcesses { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure entity mappings and relationships here
    }
}

