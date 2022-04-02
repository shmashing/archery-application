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

        public async Task<UserStats> GetUserStats(string userId)
        {
            // TODO: create stats for user
            return new UserStats("abc", userId);
        }
    }
}