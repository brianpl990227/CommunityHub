using CommunityHub.Domain.Retos;
using Microsoft.EntityFrameworkCore;

namespace CommunityHub.Infrastructure.Persistence;

public sealed class CommunityHubDbContext(DbContextOptions<CommunityHubDbContext> options) : DbContext(options)
{
    public DbSet<Reto> Retos => Set<Reto>();

    protected override void OnModelCreating(ModelBuilder modelBuilder) =>
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CommunityHubDbContext).Assembly);
}
