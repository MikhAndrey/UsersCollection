using Microsoft.EntityFrameworkCore;
using UsersCollectionAPI.Commands;
using UsersCollectionAPI.Model.Infrastructure;
using UsersCollectionAPI.Model.Infrastructure.Interfaces;
using UsersCollectionAPI.Services;
using UsersCollectionAPI.Services.Interfaces;

namespace UsersCollectionAPI.Extensions;

public static class ServiceCollectionExtension
{
    public static void AddCommands(this IServiceCollection services)
    {
        services.AddScoped<UserCreateCommand>();
        services.AddScoped<UserInfoCommand>();
        services.AddScoped<UserRemoveCommand>();
    }

    public static void AddUserServices(this IServiceCollection services)
    {
        services.AddSingleton<UserCacheService>();
        services.AddScoped<IUserService, UserService>();
    }
    
    public static void AddDatabaseServices(this IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("DefaultConnection")!;
        services.AddDbContext<ApplicationDbContext>(options => 
            options.UseMySql(connectionString, 
                new MySqlServerVersion(new Version(8, 0, 24))));

        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}
