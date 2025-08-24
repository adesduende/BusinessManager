using BusinessManager.Application.Dtos;
using BusinessManager.Application.Interfaces;
using BusinessManager.Application.Mappers;
using BusinessManager.Domain.Repositories;

namespace BusinessManager.Application.UseCases.User.AssignRoles
{
    internal class AssignRolesCommandHandler : IRequestHandler<AssignRolesCommand, UserDto?>
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;

        public AssignRolesCommandHandler(IUserRepository userRepository, IRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }

        public async Task<UserDto?> HandleAsync(AssignRolesCommand request)
        {
            try
            {
                var roles = await _roleRepository.GetAllRolesAsync();
                var user = await _userRepository.GetUserByIdAsync(request.Id);
                var selectedRoles = roles.Where(r => request.roles.Contains(r.Name)).ToList();

                foreach (var role in selectedRoles)
                {
                    if (user.Roles.Any(r => r.Id == role.Id))
                    {
                        throw new InvalidOperationException($"Role {role.Name} is already assigned to the user.");
                    }
                    user.AddRole(role);
                }
                await _userRepository.UpdateUserAsync(user);

                var userDto = UserMapper.ToDto(user);

                return userDto;
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("An error occurred while assigning roles to the user.", e);
            }
        }
    }
}
