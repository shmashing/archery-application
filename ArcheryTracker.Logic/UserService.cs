using System;
using System.Linq;
using System.Threading.Tasks;
using ArcheryTracker.Logic.Models;
using ArcheryTracker.Logic.Repository;

namespace ArcheryTracker.Logic
{
    public class UserService
    {
        private readonly UserRepository _userRepository;
        private readonly SessionRepository _sessionRepository;
        private readonly RoundRepository _roundRepository;

        public UserService(UserRepository userRepository, SessionRepository sessionRepository, RoundRepository roundRepository)
        {
            _userRepository = userRepository;
            _sessionRepository = sessionRepository;
            _roundRepository = roundRepository;
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
            var userSessions = await _sessionRepository.GetSessionsForUser(userId);
            var usersRounds = await _roundRepository.GetAllRoundsForUsers(userId);
            
            var sessionsThisMonth = userSessions.Where(s => s.Date.Month == DateTime.Now.Month).Select(s => s.Id);
            var userRoundsThisMonth = usersRounds.Where(r => sessionsThisMonth.Contains(r.SessionId));

            var totalShots = usersRounds.Sum(r => r.Scores.Count);
            var totalShotsThisMonth = userRoundsThisMonth.Sum(r => r.Scores.Count);
            var totalShotsOnTarget = usersRounds.Sum(r => r.Scores.Count(s => s > 0));
            var totalBullseye = usersRounds.Sum(r => r.Scores.Count(s => s == 2));

            var shotsPerSession = ((double)totalShots / userSessions.Count);
            var overallAccuracy = ((double)totalShotsOnTarget / totalShots);
            var overallBullseyeAccuracy = ((double)totalBullseye / totalShots);
            
            return new UserStats(
                userId, 
                totalShots,
                totalShotsThisMonth,
                totalShotsOnTarget,
                totalBullseye,
                shotsPerSession,
                overallAccuracy,
                overallBullseyeAccuracy);
        }
    }
}