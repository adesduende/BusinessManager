using BusinessManager.Application.Interfaces;
using BusinessManager.Application.UseCases.User.CreateUser;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BusinessManager.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // Register all Commands handlers
            var typesCollection = Assembly.GetAssembly(typeof(CreateUserCommandHandler))!.GetTypes();
            foreach (var type in typesCollection)
            {
                if (!type.IsInterface && !type.IsAbstract)
                {
                    var commandHandler = type.GetInterfaces().FirstOrDefault(
                        i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRequestHandler<,>));
                    if (commandHandler != null)
                        services.AddTransient(commandHandler, type);
                }
            }

            return services;
        }
    }
}
