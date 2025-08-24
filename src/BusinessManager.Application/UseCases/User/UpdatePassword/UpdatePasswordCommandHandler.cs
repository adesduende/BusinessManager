using BusinessManager.Application.Dtos;
using BusinessManager.Application.Interfaces;
using BusinessManager.Application.Mappers;
using BusinessManager.Domain.Repositories;

namespace BusinessManager.Application.UseCases.User.UpdatePassword
{
    public class UpdatePasswordCommandHandler : IRequestHandler<UpdatePasswordCommand, UserDto?>
    {
        private readonly IUserRepository _userRepository;

        public UpdatePasswordCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDto?> HandleAsync(UpdatePasswordCommand request)
        {
            try
            {
                var user = await _userRepository.GetUserByIdAsync(request.Id);
                if (user == null)
                    throw new KeyNotFoundException("Not foun any user with this id");
                user.UpdatePassword(request.Password);

                await _userRepository.UpdateUserAsync(user);
                return UserMapper.ToDto(user);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurs while attempt to change the password", ex);
            }
        }
    }
}
