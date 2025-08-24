using BusinessManager.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManager.Application.Mappers
{
    public static class RoleMapper
    {
        public static RoleDto ToDto(this Domain.Entities.Role role)
        {
            if (role == null) throw new ArgumentNullException(nameof(role));
            return new RoleDto
            {
                Name = role.Name,
                Description = role.Description
            };
        }
    }
}
