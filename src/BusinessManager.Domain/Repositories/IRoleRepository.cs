using BusinessManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManager.Domain.Repositories
{
    public interface IRoleRepository
    {
        Task CreateRoleAsync(Role role);
        Task DeleteRoleAsync(Role role);
        Task<Role?> GetRoleByIdAsync(Guid id);
        Task<Role?> GetRoleByNameAsync(string name);
        Task<IEnumerable<Role>> GetAllRolesAsync();
    }
}
