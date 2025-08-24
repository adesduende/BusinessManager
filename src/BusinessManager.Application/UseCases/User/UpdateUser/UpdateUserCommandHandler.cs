using BusinessManager.Application.Dtos;
using BusinessManager.Application.Interfaces;
using BusinessManager.Application.Mappers;
using BusinessManager.Domain.Repositories;

namespace BusinessManager.Application.UseCases.User.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UserDto?>
    {
        private readonly IUserRepository _userRepository;

        public UpdateUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDto?> HandleAsync(UpdateUserCommand request)
        {
            try
            {
                var user = await _userRepository.GetUserByIdAsync(request.id);
                if (user == null)
                {
                    throw new KeyNotFoundException($"User with ID {request.id} not found.");
                }
                // Update user properties
                if (request.Name != null) user.UpdateName(request.Name);
                if (request.Surname != null) user.UpdateSurname(request.Surname);

                await _userRepository.UpdateUserAsync(user);

                return UserMapper.ToDto(user);

            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while updating the user.", ex);
            }
        }
    }
}
