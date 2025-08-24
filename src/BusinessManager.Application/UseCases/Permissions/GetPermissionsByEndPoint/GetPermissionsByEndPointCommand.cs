using BusinessManager.Application.Interfaces;

namespace BusinessManager.Application.UseCases.Permissions.GetPermissionsByEndPoint
{
    public record GetPermissionsByEndpointQuery(string EndpointTag) : IRequest<IEnumerable<string>>;
}

