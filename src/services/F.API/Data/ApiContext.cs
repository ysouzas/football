using F.Core.Data;
using F.Models;
using Microsoft.EntityFrameworkCore;

namespace F.API.Data;

public sealed class ApiContext : DbContext, IUnitOfWork
{
    public ApiContext(DbContextOptions<ApiContext> options) : base(options)
    {
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        ChangeTracker.AutoDetectChangesEnabled = false;
    }

    public DbSet<Player> Players { get; set; }
    public DbSet<Rank> Ranks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        foreach (var relationship in modelBuilder.Model.GetEntityTypes()
            .SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApiContext).Assembly);
    }

    public async Task<bool> Commit()
    {
        return await SaveChangesAsync() > 0;
    }
}
