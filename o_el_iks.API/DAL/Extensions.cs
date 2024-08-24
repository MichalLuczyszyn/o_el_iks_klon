using Microsoft.EntityFrameworkCore;

namespace o_el_iks.API.DAL;

internal static class Extensions
{
    public static void AddPostgres(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("connectionString");
        services.AddDbContext<AppDbContext>(x => x.UseNpgsql(connectionString));
    }
}