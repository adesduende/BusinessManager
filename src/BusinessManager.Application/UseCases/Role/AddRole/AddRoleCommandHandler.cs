using BusinessManager.Application.Interfaces;
using BusinessManager.Domain.Repositories;

namespace BusinessManager.Application.UseCases.Role.AddRole
{
    internal class AddRoleCommandHandler : IRequestHandler<AddRoleCommand, Guid?>
    {
        private readonly IRoleRepository _roleRepository;

        public AddRoleCommandHandler(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<Guid?> HandleAsync(AddRoleCommand request)
        {
            BusinessManager.Domain.Entities.Role newRole = new (
                    Guid.NewGuid(),
                    request.Name,
                    request.Description
                );
            await _roleRepository.CreateRoleAsync(newRole);

            return newRole.Id;
        }
    }
}
