using BusinessManager.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace BusinessManager.Infrastructure.Auth
{
    public class DynamicAuthorizationHandler : AuthorizationHandler<DynamicAuthorizationRequirement>
    {
        private readonly IMediator _mediator;
        private readonly IServiceProvider _serviceProvider;

        public DynamicAuthorizationHandler(IMediator mediator, IServiceProvider serviceProvider)
        {
            _mediator = mediator;
            _serviceProvider = serviceProvider;
        }

        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            DynamicAuthorizationRequirement requirement)
        {
            // Verificar que el usuario esté autenticado
            //if (!context.User.Identity.IsAuthenticated)
            //{
            //    context.Fail();
            //    return;
            //}

            // Obtener los roles del usuario desde los claims
            var userRoles = context.User.Claims
                .Where(c => c.Type == ClaimTypes.Role)
                .Select(c => c.Value)
                .ToList();

            if (!userRoles.Any())
            {
                context.Fail();
                return;
            }

            try
            {
                // Verificar si alguno de los roles del usuario tiene permisos para este endpoint
                foreach (var roleName in userRoles)
                {
                    // Aquí comprobamos si tienes permisos para ese endpoint
                    var hasPermission = await CheckRolePermissionForEndpoint(roleName, requirement.EndpointTag);

                    if (hasPermission)
                    {
                        context.Succeed(requirement);
                        return;
                    }
                }

                context.Fail();
            }
            catch
            {
                context.Fail();
            }
        }

        private async Task<bool> CheckRolePermissionForEndpoint(string roleName, string endpointTag)
        {

            // Si el usuario es Administrator, permitir acceso a todo
            if (roleName == "Administrator")
                return true;

            // Verificar si en la base de datos el rol tiene permisos para el endpoint
            string[] rolePermissions = [];
            await Task.Run(()=>
            {
                rolePermissions = [];
            });

            return rolePermissions.Contains(endpointTag);
        }
    }
}
