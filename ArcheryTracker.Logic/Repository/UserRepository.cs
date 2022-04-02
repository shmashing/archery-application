using System;
using System.Linq;
using System.Threading.Tasks;
using ArcheryTracker.Logic.Database;
using ArcheryTracker.Logic.Models;
using Microsoft.EntityFrameworkCore;

namespace ArcheryTracker.Logic.Repository
{
    public class UserRepository
    {
        private readonly DatabaseContext _databaseContext;

        public UserRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<bool> UserExists(string userId)
        {
            var user = await _databaseContext.Users.FindAsync(userId);

            return user != null;
        }

        public async Task UpdateUserLogin(string userId)
        {
            var user = await _databaseContext.Users.FindAsync(userId);
            user.LastLogin = DateTime.Now.ToUniversalTime();

            _databaseContext.Users.Update(user);
            await _databaseContext.SaveChangesAsync();
        }

        public async Task CreateUser(User user)
        {
            await _databaseContext.Users.AddAsync(user);
            await _databaseContext.SaveChangesAsync();
        }

        public async Task<User> GetUser(string userId)
        {
            return await _databaseContext.Users.FindAsync(userId);
        }
    }
}