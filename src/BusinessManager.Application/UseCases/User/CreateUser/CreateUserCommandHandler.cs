using BusinessManager.Application.Dtos;
using BusinessManager.Application.Interfaces;
using BusinessManager.Application.Mappers;
using BusinessManager.Domain.Repositories;

namespace BusinessManager.Application.UseCases.User.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;

        public CreateUserCommandHandler(IUserRepository userRepository,IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<UserDto> HandleAsync(CreateUserCommand request)
        {
            //Hash the password
            var hashedPassword = _passwordHasher.HashPassword(request.password);

            if (request == null) throw new ArgumentNullException(nameof(request));
            var user = new Domain.Entities.User(
                            Guid.NewGuid(),
                            request.name,
                            request.surname,
                            request.nif,
                            request.email,
                            hashedPassword
                        );
            await _userRepository.AddUserAsync(user);

            return UserMapper.ToDto(user);
        }
    }
}
