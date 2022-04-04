using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArcheryTracker.Logic.Database;
using ArcheryTracker.Logic.Models;
using Microsoft.EntityFrameworkCore;

namespace ArcheryTracker.Logic.Repository
{
    public class SessionRepository
    {
        private readonly DatabaseContext _databaseContext;

        public SessionRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<List<Session>> GetSessionsForUser(string userId)
        {
            return await _databaseContext.Sessions.Where(s => s.UserId == userId)
                .OrderByDescending(s => s.Date)
                .ToListAsync();
        }

        public async Task<Session> GetSession(string sessionId)
        {
            return await _databaseContext.Sessions.FindAsync(sessionId);
        }

        public async Task CreateSession(Session session)
        {
            await _databaseContext.AddAsync(session);
            await _databaseContext.SaveChangesAsync();
        }
    }
}