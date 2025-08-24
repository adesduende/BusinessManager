using BusinessManager.Application.Interfaces;

namespace BusinessManager.Application.UseCases.Permissions.GetAllPermissions
{
    public record GetAllPermissionsQuery(Guid roleId) : IRequest<IEnumerable<string>?>;
}
