using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArcheryTracker.App.Data
{
    public class SessionService
    {
        private Random _rng;

        public SessionService()
        {
            _rng = new Random();
        }
        public Task<List<Session>> GetSessions()
        {
            var sessions = new List<Session>();
            for (var z = 0; z < 20; z++)
            {
                sessions.Add(GetSession(z));
            }
            
            return Task.FromResult(sessions);
        }

        public Session GetSession(int id)
        {
            var numberOfRounds = _rng.Next(1, 20);
            var roundScores = new List<Round>();
            for (var i = 0; i < numberOfRounds; i++)
            {
                var scores = new List<int>();
                for (var j = 0; j < 6; j++)
                {
                    scores.Add(_rng.Next(3));
                }

                roundScores.Add(new Round(scores));
            }
            
            return new Session(id, DateTime.Now, "20 ft", roundScores);
        }
    }
}