using System;
using System.Linq;
using System.Threading.Tasks;
using ArcheryTracker.Logic.Database;
using ArcheryTracker.Logic.Models;
using Microsoft.EntityFrameworkCore;

namespace ArcheryTracker.Logic.Repository
{
    public class UserStatsRepository
    {
        private readonly DatabaseContext _databaseContext;

        public UserStatsRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }
        
        public async Task<UserStats> GetUsersStats(string userId)
        {
            var userSessions = await _databaseContext.Sessions.Where(s => s.UserId == userId).ToListAsync();
            var usersRounds = await _databaseContext.Rounds.Where(r => r.UserId == userId).ToListAsync();
            
            var sessionsThisMonth = userSessions.Where(s => s.Date.Month == DateTime.Now.Month).Select(s => s.Id);
            var userRoundsThisMonth = usersRounds.Where(r => sessionsThisMonth.Contains(r.SessionId));

            var totalShots = usersRounds.Sum(r => r.Scores.Count);
            var totalShotsThisMonth = userRoundsThisMonth.Sum(r => r.Scores.Count);
            var totalShotsOnTarget = usersRounds.Sum(r => r.Scores.Count(s => s > 0));
            var totalBullseye = usersRounds.Sum(r => r.Scores.Count(s => s == 2));

            var shotsPerSession = ((double)totalShots / userSessions.Count());
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