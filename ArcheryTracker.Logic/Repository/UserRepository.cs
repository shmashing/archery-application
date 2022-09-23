using System;
using System.Linq;
using System.Threading.Tasks;
using ArcheryTracker.Logic.Database;
using ArcheryTracker.Logic.Models;

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

        public async Task<User> CreateUser(User user)
        {
            await _databaseContext.Users.AddAsync(user);
            await _databaseContext.SaveChangesAsync();

            await UpdateUserLogin(user.Id);

            return await _databaseContext.Users.FindAsync(user.Id);
        }

        public async Task<User> GetUser(string userId)
        {
            return await _databaseContext.Users.FindAsync(userId);
        }

        public async Task<User> UpdateAndGetUser(User user)
        {
            var oldUserEntry = await _databaseContext.Users.FindAsync(user.Id);
            oldUserEntry.Email = user.Email;
            oldUserEntry.Name = user.Name;
            oldUserEntry.LastLogin = user.LastLogin;
            
            _databaseContext.Users.Update(oldUserEntry);
            await _databaseContext.SaveChangesAsync();

            return await _databaseContext.Users.FindAsync(user.Id);
        }
    }
}