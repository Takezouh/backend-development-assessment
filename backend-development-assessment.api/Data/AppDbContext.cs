
using backend_development_assessment.api.Models;
using Microsoft.EntityFrameworkCore;

namespace backend_development_assessment.api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }
    public DbSet<Ticket> Tickets => Set<Ticket>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.Property(t => t.priority)
                .HasConversion<string>();
            entity.Property(t => t.status)
                .HasConversion<string>();
        });
    }

}
