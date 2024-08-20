using back_dotnet.Db.Models;
using Microsoft.EntityFrameworkCore;

namespace back_dotnet.Db;

internal sealed class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Todo> Todos { get; init; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Setup all configurations
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        
        base.OnModelCreating(modelBuilder);
    }
};