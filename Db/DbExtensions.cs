using Microsoft.EntityFrameworkCore;

namespace back_dotnet.Db;

internal static class DbExtensions
{
    public static void AddDb(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContextFactory<AppDbContext>(options =>
        {
            var connString = configuration.GetConnectionString("LiveDb");
            
            ArgumentException.ThrowIfNullOrEmpty(connString);
            
            options.UseNpgsql(connString, builder =>
            {
                builder.SetPostgresVersion(16, 4);
            });
        });
    }

    public static void WarmUpDb(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var appDbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        appDbContext.Database.EnsureCreated();
    }
}