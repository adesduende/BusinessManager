using BusinessManager.Application.Dtos;
using BusinessManager.Application.Interfaces;

namespace BusinessManager.Application.UseCases.User.AssignRoles
{
    public record AssignRolesCommand(Guid Id, IEnumerable<string> roles) : IRequest<UserDto?>;
}
