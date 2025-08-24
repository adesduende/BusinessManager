using BusinessManager.Application.Dtos;
using BusinessManager.Application.Interfaces;
using BusinessManager.Application.Mappers;
using BusinessManager.Domain.Repositories;

namespace BusinessManager.Application.UseCases.User.ValidateUser
{
    public class ValidateUserCommandHandler : IRequestHandler<ValidateUserCommand, UserDto>
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;

        public ValidateUserCommandHandler(IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator)
        {
            _userRepository = userRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<UserDto> HandleAsync(ValidateUserCommand request)
        {
            var userId = _jwtTokenGenerator.ValidateToken(request.token);
            var user = await _userRepository.GetUserByIdAsync(new Guid(userId));
            if (user is null)
                throw new Exception("User not found");
            return UserMapper.ToDto(user);
        }
    }
}
