using BusinessManager.Application.Dtos;
using BusinessManager.Application.Interfaces;
using BusinessManager.Application.Mappers;
using BusinessManager.Domain.Repositories;

namespace BusinessManager.Application.UseCases.User.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginUserDto?>
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IPasswordHasher _passwordHasher;

        public LoginUserCommandHandler(IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
            _passwordHasher = passwordHasher;
        }

        public async Task<LoginUserDto?> HandleAsync(LoginUserCommand request)
        {
            var response = await _userRepository.GetUserByEmailAsync(request.Email);
            if (response is null)            
                return null;
            
            bool isValidPassword = _passwordHasher.VerifyPassword(response.Password, request.Password);

            if (!isValidPassword)
                return null;

            var token = _jwtTokenGenerator.GenerateToken(response);

            //!TO-DO: Store token in a database or cache

            return UserMapper.ToLoginUserDto(response,token);

        }
    }
}
