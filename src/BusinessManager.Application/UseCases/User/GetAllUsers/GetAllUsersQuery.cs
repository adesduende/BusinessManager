using BusinessManager.Application.Dtos;
using BusinessManager.Application.Interfaces;

namespace BusinessManager.Application.UseCases.User.GetAllUsers
{
    public record GetAllUsersQuery() : IRequest<IEnumerable<UserDto>>;
}
