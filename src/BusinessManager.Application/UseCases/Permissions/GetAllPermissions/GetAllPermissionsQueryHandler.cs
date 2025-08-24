using BusinessManager.Application.Interfaces;

namespace BusinessManager.Application.UseCases.Permissions.GetAllPermissions
{
    public class GetAllPermissionsQueryHandler : IRequestHandler<GetAllPermissionsQuery, IEnumerable<string>?>
    {
        public async Task<IEnumerable<string>?> HandleAsync(GetAllPermissionsQuery request)
        {
            return null;
        }
    }
}
