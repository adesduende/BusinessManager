using BusinessManager.Application.Dtos;
using BusinessManager.Application.Interfaces;

namespace BusinessManager.Application.UseCases.User.CreateUser
{
    public record CreateUserCommand(
            string name,
            string surname,
            string nif,
            string email,
            string password        
        ) : IRequest<UserDto>;
}
