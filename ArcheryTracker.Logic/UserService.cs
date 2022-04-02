using System.Threading.Tasks;
using ArcheryTracker.Logic.Models;
using ArcheryTracker.Logic.Repository;

namespace ArcheryTracker.Logic
{
    public class UserService
    {
        private readonly UserRepository _userRepository;

        public UserService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        
        public async Task<User> GetUser(string id, string name, string email)
        {
            await WriteOrUpdateUser(id, name, email);
            return await _userRepository.GetUser(id);
        }

        private async Task WriteOrUpdateUser(string userId, string name, string email)
        {
            var userExists = await _userRepository.UserExists(userId);
            if (userExists)
            {
                await _userRepository.UpdateUserLogin(userId);
            }
            else
            {
                var user = new User(userId, name, email);
                await _userRepository.CreateUser(user);
            }
        }
    }
}