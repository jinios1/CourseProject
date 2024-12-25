using Back.Models;

namespace Back.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User?> GetUserAsync(string id);
        Task<User> CreateUserAsync(User user);
        Task<bool> UpdateUserAsync(string id, User updatedUser);
        Task<bool> DeleteUserAsync(string id);
    }
}
