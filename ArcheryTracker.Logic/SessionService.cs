using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArcheryTracker.App.Data
{
    public class SessionService
    {
        private readonly Random _rng;

        public SessionService()
        {
            _rng = new Random();
        }
        public async Task<List<Session>> GetSessions()
        {
            var sessions = new List<Session>();
            for (var z = 0; z < 20; z++)
            {
                var id = $"ses_${Guid.NewGuid():N}";
                sessions.Add(await GetSession(id));
            }
            
            return sessions;
        }

        public Task<Session> GetSession(string id)
        {
            var numberOfRounds = _rng.Next(1, 20);
            var roundScores = new List<Round>();
            for (var i = 0; i < numberOfRounds; i++)
            {
                var scores = new List<int>();
                var shotCount = _rng.Next(6, 13);
                for (var j = 0; j < shotCount; j++)
                {
                    scores.Add(_rng.Next(3));
                }

                roundScores.Add(new Round(scores));
            }
            
            return Task.FromResult(new Session(id, DateTime.Now, "20 ft", roundScores));
        }
    }
}