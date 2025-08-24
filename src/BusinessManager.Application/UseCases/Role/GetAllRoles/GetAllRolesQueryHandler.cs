using BusinessManager.Application.Dtos;
using BusinessManager.Application.Interfaces;
using BusinessManager.Application.Mappers;
using BusinessManager.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManager.Application.UseCases.Role.GetAllRoles
{
    public class GetAllRolesQueryHandler : IRequestHandler<GetAllRolesQuery, List<RoleDto>>
    {
        private readonly IRoleRepository _roleRepository;

        public GetAllRolesQueryHandler(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<List<RoleDto>> HandleAsync(GetAllRolesQuery request)
        {
            var roles = await _roleRepository.GetAllRolesAsync();

            return roles.Select(role => RoleMapper.ToDto(role)).ToList();
        }
    }
}
