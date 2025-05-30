namespace UserManager.API;

using Microsoft.EntityFrameworkCore;
using UserManager.API.Data;
using UserManager.API.Services;

public static class DependencyInjection
{
    public static IServiceCollection AddManualServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
                    options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserService, UserService>();

        return services;
    }
}
