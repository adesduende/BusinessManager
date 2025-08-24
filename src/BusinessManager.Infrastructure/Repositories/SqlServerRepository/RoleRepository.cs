using BusinessManager.Domain.Entities;
using BusinessManager.Domain.Repositories;
using BusinessManager.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;

namespace BusinessManager.Infrastructure.Repositories.SqlServerRepository
{
    internal class RoleRepository : IRoleRepository
    {
        private readonly BusinessContext _context;

        public RoleRepository(BusinessContext context)
        {
            _context = context;
        }

        public async Task CreateRoleAsync(Role role)
        {
            await _context.Set<Role>().AddAsync(role);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRoleAsync(Role role)
        {
            _context.Set<Role>().Remove(role);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Role>> GetAllRolesAsync()
        {
            try
            {
                return await _context.Set<Role>().AsQueryable().ToListAsync();
            }
            catch (Exception ex)
            {
                // Log the exception (not implemented here)
                throw new InvalidOperationException("An error occurred while retrieving roles.", ex);
            }
        }
        public async Task<Role?> GetRoleByIdAsync(Guid id)
        {
            return await _context.Set<Role>().FindAsync(id);
        }

        public async Task<Role?> GetRoleByNameAsync(string name)
        {
            return await _context.Roles.FirstAsync(r => r.Name == name);
        }
    }
}
