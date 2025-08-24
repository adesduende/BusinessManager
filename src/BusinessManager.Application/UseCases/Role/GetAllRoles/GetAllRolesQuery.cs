using BusinessManager.Application.Dtos;
using BusinessManager.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManager.Application.UseCases.Role.GetAllRoles
{
    public class GetAllRolesQuery():IRequest<List<RoleDto>>;
}
