using BusinessManager.Application.Interfaces;
using BusinessManager.Domain.Repositories;
using BusinessManager.Infrastructure.Auth;
using BusinessManager.Infrastructure.Context;
using BusinessManager.Infrastructure.HashPassword;
using BusinessManager.Infrastructure.Mediator;
using BusinessManager.Infrastructure.Repositories.SqlServerRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessManager.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Configure DbContext
            services.Configure<BusinessDbSettings>(configuration.GetSection("BusinessDbSettings"));

            // Register DbContext with SQL Server
            services.AddDbContext<BusinessContext>(options =>
                options.UseSqlServer(
                    configuration.GetSection("BusinessDbSettings").Get<BusinessDbSettings>()!.ConnectionString,
                    sqlOptions => sqlOptions.MigrationsAssembly(typeof(BusinessContext).Assembly.FullName)
                )
            );

            // Register repositories
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();

            // Register mediator
            services.AddScoped<IMediator, BusinessMediator>();

            // Configure JWT Settings
            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

            //Register Auth
            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

            // Register password hasher
            services.AddScoped<IPasswordHasher, PasswordHasher>();

            return services;
        }
    }
}
