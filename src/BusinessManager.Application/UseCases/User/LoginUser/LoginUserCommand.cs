using BusinessManager.Application.Dtos;
using BusinessManager.Application.Interfaces;

namespace BusinessManager.Application.UseCases.User.LoginUser
{
    public record LoginUserCommand(
        string Email,
        string Password
    ) : IRequest<LoginUserDto?>;
}
