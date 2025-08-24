using BusinessManager.Application.Dtos;
using BusinessManager.Application.Interfaces;

namespace BusinessManager.Application.UseCases.User.UpdatePassword
{
    public record UpdatePasswordCommand(Guid Id, string Password) : IRequest<UserDto?>;
}
