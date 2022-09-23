using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArcheryTracker.Logic.Database;
using ArcheryTracker.Logic.Models;
using Microsoft.EntityFrameworkCore;

namespace ArcheryTracker.Logic.Repository
{
    public class RoundRepository
    {
        private readonly DatabaseContext _databaseContext;

        public RoundRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task CreateRound(Round round)
        {
            await _databaseContext.AddAsync(round);
            await _databaseContext.SaveChangesAsync();
        }

        public async Task<List<Round>> GetRoundsForSession(string sessionId)
        {
            return await _databaseContext.Rounds.Where(r => r.SessionId == sessionId).ToListAsync();
        }

        public async Task<List<Round>> GetAllRoundsForUsers(string userId)
        {
            return await _databaseContext.Rounds.Where(r => r.UserId == userId).ToListAsync();
        }
    }
}