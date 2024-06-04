using Microsoft.EntityFrameworkCore;
using mockOSApi.Models;


namespace mockOSApi.Data;

public class OSDbContext : DbContext
{   
    public OSDbContext(DbContextOptions<OSDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MockProcess>()
            .HasOne(m => m.User)           // MockProcess has one User
            .WithMany()
                       // User can be associated with many MockProcesses
            .HasForeignKey(m => m.UserUid)
          
             .OnDelete(DeleteBehavior.Cascade); // Foreign key property in MockProcess
    }

    public DbSet<MockProcess> MockProcesses { get; set; }
    public DbSet<User> User { get; set; }
    public DbSet<MockThread> MockThreads { get; set; }
    public DbSet<MockThreadStack> MockThreadStacks { get; set; }

}

