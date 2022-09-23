using System;
using System.Threading.Tasks;
using ArcheryTracker.Logic.Models;
using ArcheryTracker.Logic.Repository;

namespace ArcheryTracker.Logic
{
    public class UserService
    {
        private readonly UserRepository _userRepository;
        private readonly UserStatsRepository _userStatsRepository;

        public UserService(UserRepository userRepository, UserStatsRepository userStatsRepository)
        {
            _userRepository = userRepository;
            _userStatsRepository = userStatsRepository;
        }

        public async Task<User> InitializeAndGetUser(Auth0User auth0User)
        {
            if (await UserExists(auth0User.Id))
            {
                return await GetUser(auth0User.Id);
            }
            
            var newUser = new User(auth0User.Id, auth0User.Name, auth0User.Email, DateTime.Now.ToUniversalTime());
            return await _userRepository.CreateUser(newUser);
        }
        
        public async Task<User> GetUser(string id)
        {
            return await _userRepository.GetUser(id);
        }

        public async Task<User> UpdateAndGetUser(User user)
        {
            return await _userRepository.UpdateAndGetUser(user);
        }

        public async Task<bool> UserExists(string id)
        {
            return await _userRepository.UserExists(id);
        }

        public async Task<UserStats> GetUserStats(string userId)
        {
            return await _userStatsRepository.GetUsersStats(userId);
        }
    }
}