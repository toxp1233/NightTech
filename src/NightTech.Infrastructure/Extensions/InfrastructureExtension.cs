using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NightTech.Domain.Interfaces;
using NightTech.Infrastructure.Helper;
using NightTech.Infrastructure.Repositories;
using NightTech.Infrastructure.Seeders;
using NightTech.Infrastructure.Services;

namespace NightTech.Infrastructure.Extensions;

public static class InfrastructureExtension
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<NightTechDbContext>(options
            => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        services.AddScoped<IPasswordHasherBcrypt, PasswordHasher>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<INightTechSeeder, NightTechSeeder>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ICartRepository, CartRepository>();
        services.AddScoped<ICartItemsRepository, CartItemsRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IOrderItemsRepository, OrderItemsRepository>();
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<IStripeService, StripeService>();
        services.AddScoped<IEmailService, EmailSerivce>();
        return services;
    }
}
