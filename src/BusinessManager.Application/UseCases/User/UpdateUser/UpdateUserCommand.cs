using BusinessManager.Application.Dtos;
using BusinessManager.Application.Interfaces;
using BusinessManager.Domain.ValueObjects;

namespace BusinessManager.Application.UseCases.User.UpdateUser
{
    public record UpdateUserCommand(Guid id, string? Name, string? Surname) : IRequest<UserDto?>;
}
