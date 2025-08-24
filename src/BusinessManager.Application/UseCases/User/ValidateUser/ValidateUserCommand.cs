using BusinessManager.Application.Dtos;
using BusinessManager.Application.Interfaces;

namespace BusinessManager.Application.UseCases.User.ValidateUser
{
    public record ValidateUserCommand(string token) : IRequest<UserDto>;
}
