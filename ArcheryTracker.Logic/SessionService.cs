using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArcheryTracker.Logic.Models;

namespace ArcheryTracker.Logic
{
    public class SessionService
    {
        private readonly Random _rng;
        private List<Session> _sessions;
        
        public SessionService()
        {
            _rng = new Random();
            _sessions = InitializeSessions();
        }
        public List<Session> InitializeSessions(int? numberOfSessions = null)
        {
            if (numberOfSessions == 0)
            {
                return new List<Session>();
            }

            var sessions = new List<Session>();
            var sessionCount = numberOfSessions ?? 20;
            for (var z = 0; z < sessionCount; z++)
            {
                var id = IdService.GetId(typeof(Session));
                sessions.Add(CreateSession(id));
            }
            
            return sessions;
        }
        
        public Session CreateSession(string id, int? numberOfRounds = null, List<Round> rounds = null)
        {
            var roundCount = numberOfRounds ?? _rng.Next(1, 20);
            var roundScores = new List<Round>();
            for (var i = 0; i < roundCount; i++)
            {
                var roundId = IdService.GetId(typeof(Round));
                var scores = new List<int>();
                var shotCount = _rng.Next(6, 13);
                for (var j = 0; j < shotCount; j++)
                {
                    scores.Add(_rng.Next(3));
                }
        
                roundScores.Add(new Round(roundId, scores));
            }
            
            return new Session(id, DateTime.Now, 20, roundScores);
        }

        public async Task<string> CreateSession(Session newSession)
        {
            _sessions.Add(newSession);
            return newSession.Id;
        }

        public async Task<List<Session>> GetSessions()
        {
            _sessions.ForEach(s => s.CalculateStats());
            return _sessions;
        }
        public async Task<Session> GetSession(string id)
        {
            var session = _sessions.FirstOrDefault(s => s.Id == id);

            if (session == null)
            {
                return null;
            }
            
            session.CalculateStats();
            return session;
        }

        public async Task<Session> AddRoundToSession(string sessionId, Round round)
        {
            var session = await GetSession(sessionId);
            session.RoundScores.Add(round);
            session.CalculateStats();

            return session;
        }
    }
}