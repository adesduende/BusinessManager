using BusinessManager.Domain.Entities;
using BusinessManager.Domain.Repositories;
using BusinessManager.Domain.ValueObjects;
using BusinessManager.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace BusinessManager.Infrastructure.Repositories.SqlServerRepository
{
    internal class UserRepository : IUserRepository
    {
        private readonly BusinessContext _context;

        public UserRepository(BusinessContext context)
        {
            _context = context;
        }

        public async Task AddUserAsync(User user)
        {
            await _context.AddAsync<User>(user);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteUserAsync(User user)
        {
            await Task.Run(() => _context.Remove<User>(user));
        }
        public async Task DeleteUserByIdAsync(string id)
        {
            var res = await _context.FindAsync<User>(id);
            if (res != null)
            {
                _context.Remove<User>(res);
            }
        }
        public async Task DeleteUsersAsync(IEnumerable<User> users)
        {
            foreach (var user in users)
            {
                await DeleteUserAsync(user);
            }
        }
        public async Task<IEnumerable<User>> FindUsersByExpressionAsync(UserSearchExpression userSearchExpression)
        {
            return await _context.Set<User>()
                .Where(
                        u => (string.IsNullOrEmpty(userSearchExpression.name) || u.Name.Contains(userSearchExpression.name)) &&
                             (string.IsNullOrEmpty(userSearchExpression.surname) || u.Surname.Contains(userSearchExpression.surname)) &&
                             (string.IsNullOrEmpty(userSearchExpression.nif) || u.Nif.Value.Contains(userSearchExpression.nif)) &&
                             (string.IsNullOrEmpty(userSearchExpression.email) || u.Email.Value.Contains(userSearchExpression.email)) &&
                             (userSearchExpression.role == null || u.Roles.Contains(userSearchExpression.role)))
                .ToListAsync<User>();
        }
        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Users.Include(u => u.Roles).ToListAsync();
        }
        public async Task<User?> GetUserByIdAsync(Guid id)
        {
            try
            {
                return await _context.Set<User>().Include(r=>r.Roles).FirstAsync(u=>u.Id==id);
            }
            catch (Exception ex)
            {
                // Log the exception (not implemented here)
                throw new InvalidOperationException("An error occurred while retrieving the user.", ex);
            }
        }
        public async Task<User?> GetUserByEmailAsync(Email email)
        {
            return await _context.Set<User>().Include(r=>r.Roles).FirstOrDefaultAsync(u => u.Email == email);
        }
        public async Task<User?> GetUserByEmailAndPasword(Email email, string password)
        {
            return await _context.Set<User>().Include(r=>r.Roles).FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
        }
        public async Task UpdateUserAsync(User user)
        {
            _context.Set<User>().Update(user);
            await _context.SaveChangesAsync();

        }
        public async Task<User?> UpdateUserRolesAsync(Guid userId, IEnumerable<Role> roles)
        {
            var user = await _context.Set<User>().FirstOrDefaultAsync(u => u.Id == userId);
            //!TODO: Handle update only the roles
            return user;

        }
    }
}
