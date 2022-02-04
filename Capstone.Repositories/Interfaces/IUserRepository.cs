using Capstone.Data.Entities.Models;

namespace Capstone.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User> Register(User user);
        Task<User> GetByUsername(string username);
        Task<User> GetById(int id);
        Task<Role> GetRoleByRoleName(string roleName);
    }
}
