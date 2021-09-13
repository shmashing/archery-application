using System.Threading.Tasks;

namespace ArcheryTracker.App.Data
{
    public class UserService
    {
        private SessionService _sessionService;

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