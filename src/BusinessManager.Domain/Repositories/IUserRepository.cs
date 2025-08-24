using BusinessManager.Domain.Entities;
using BusinessManager.Domain.ValueObjects;

namespace BusinessManager.Domain.Repositories
{
    public interface IUserRepository 
    {
        //Create
        Task AddUserAsync(User user);

        //Read
        Task<User?> GetUserByIdAsync(Guid id);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<IEnumerable<User>> FindUsersByExpressionAsync(UserSearchExpression userSearchExpression);
        Task<User?> GetUserByEmailAndPasword(Email email, string password);
        Task<User?> GetUserByEmailAsync(Email email);


        //Update
        Task UpdateUserAsync(User user);
        Task<User?> UpdateUserRolesAsync (Guid userId, IEnumerable<Role> roles);

        //Delete
        Task DeleteUserAsync(User user);
        Task DeleteUserByIdAsync(string id);
        Task DeleteUsersAsync(IEnumerable<User> users);


    }
}
