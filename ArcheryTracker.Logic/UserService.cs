using System.Threading.Tasks;
using ArcheryTracker.Logic.Models;

namespace ArcheryTracker.Logic
{
    public class UserService
    {
        private readonly SessionService _sessionService;

        public UserService(SessionService sessionService)
        {
            _sessionService = sessionService;
        }
        
        public async Task<UserStats> GetUser(int id)
        {
            var sessions = await _sessionService.GetSessions();
            return new UserStats(id, sessions);
        }
    }
}