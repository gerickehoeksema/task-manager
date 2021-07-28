using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManager.Application.Models;

namespace TaskManager.Application.Interfaces
{
    public interface IIdentityUserService
    {
        Task<bool> IsInRoleAsync(long userId, string role);
        Task<(long userId, string username, string email, string name, string surname, string passwordHash, IList<string> roles, bool isActive, bool isMember)> FindUserAsync(long userId);
        Task<(long userId, string username, string email, string name, string surname, string passwordHash, IList<string> roles, bool isActive, bool isMember)> FindUserAsync(string email);
        Task<(long userId, string username, string email, string name, string surname, string passwordHash, IList<string> roles, bool isActive, bool isMember)> FindUserByUsernameAsync(string username);
        Task<(Result Result, long UserId)> CreateUserAsync(string username, string email, string name, string surname, bool isActive, bool isMember, string password);
        Task<Result> UpdateUserAsync(long userId, string email, string firstName, string lastName, bool isActive);
        Task<Result> DeleteUserAsync(long userId);
        Task<string> HashPasswordAsync(long userId, string password);
        Task<Result> UpdatePasswordAsync(long userId, string passwordHash);
        List<string> GetRoles();
        Task<Result> AddUserToRoleAsync(long userId, string role);
        Task<Result> AddUserToRolesAsync(long userId, IList<string> roles);
        Task<bool> ValidateUser(string username, string password);
    }
}
