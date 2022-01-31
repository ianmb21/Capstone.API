using Capstone.Data.Entities.Models;
using Capstone.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Repositories.Classes
{
    public class UserRepository : IUserRepository
    {
        #region Private Properties
        private readonly CapstoneDbContext _context;
        #endregion

        #region Constructor
        public UserRepository(CapstoneDbContext context)
        {
            _context = context;
        }
        #endregion

        #region Public Methods
        public async Task<User> GetById(int id)
        {
            return await _context.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.UserId == id);
        }

        public async Task<User> GetByUsername(string username)
        {
            return await _context.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<User> Register(User user)
        {
            _context.Users.Add(user);
            user.UserId = await _context.SaveChangesAsync();

            return user;
        }

        public async Task<Role> GetRoleByRoleName(string roleName)
        {
            var role = await _context.Roles.FirstOrDefaultAsync(r => r.RoleName == roleName);

            return role;
        }
        #endregion
    }
}
