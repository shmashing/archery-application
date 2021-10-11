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
            _sessions = new List<Session>();
        }

        public async Task<string> CreateSession(Session newSession)
        {
            _sessions.Add(newSession);
            return newSession.Id;
        }

        public async Task<List<Session>> GetSessionsForUser(string userId)
        {
            var usersSessions = _sessions.Where(s => s.UserId == userId).ToList();
            usersSessions.ForEach(s => s.CalculateStats());
            return usersSessions;
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