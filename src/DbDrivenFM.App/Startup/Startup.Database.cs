using DbDrivenFM.App.Data;
using Microsoft.EntityFrameworkCore;

namespace DbDrivenFM.App.Startup
{
    public static class StartupDatabaseExtensions
    {
        public static IServiceCollection AddStartupDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            services.AddDbContextFactory<ApplicationDbContext>(options =>
                           options.UseSqlServer(connectionString));
            services.AddDatabaseDeveloperPageExceptionFilter();

            return services;
        }
    }
}
