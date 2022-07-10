using Microsoft.EntityFrameworkCore;
using Wasm.Server.Persistence.Entities;

namespace Wasm.Server.Persistence;

public sealed class BlazingTrailsContext : DbContext
{
    public DbSet<Trail> Trails => Set<Trail>();
    public DbSet<RouteInstruction> RouteInstructions => Set<RouteInstruction>();
    
    public BlazingTrailsContext(DbContextOptions<BlazingTrailsContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new TrailConfig());
        modelBuilder.ApplyConfiguration(new RouteInstructionConfig());
    }
}