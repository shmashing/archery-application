using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArcheryTracker.Logic.Models;
using ArcheryTracker.Logic.Repository;

namespace ArcheryTracker.Logic
{
    public class SessionService
    {
        private readonly SessionRepository _sessionRepository;
        private readonly RoundRepository _roundRepository;

        public SessionService(SessionRepository sessionRepository, RoundRepository roundRepository)
        {
            _sessionRepository = sessionRepository;
            _roundRepository = roundRepository;
        }
        
        public async Task<string> CreateSession(Session newSession)
        {
            await _sessionRepository.CreateSession(newSession);
            return newSession.Id;
        }

        public async Task<List<Session>> GetSessionsForUser(string userId)
        {
            var sessions = await _sessionRepository.GetSessionsForUser(userId);

            foreach (var session in sessions)
            {
                await SetSessionDetails(session);
            }

            return sessions;
        }
        
        public async Task<Session> GetSession(string id)
        {
            var session = await _sessionRepository.GetSession(id);
            await SetSessionDetails(session);
            return session;
        }

        public async Task<List<Round>> GetRoundsForSession(string sessionId)
        {
            return await _roundRepository.GetRoundsForSession(sessionId);
        }

        public async Task<Session> AddRoundToSession(string sessionId, Round round)
        {
            await _roundRepository.CreateRound(round);
            return await _sessionRepository.GetSession(sessionId);
        }

        private async Task<Session> SetSessionDetails(Session session)
        {
            var sessionRounds = await _roundRepository.GetRoundsForSession(session.Id);
            session.TotalShots = sessionRounds.Sum(r => r.Scores.Count);
            session.OnTargetAccuracy = sessionRounds.Sum(r => r.Scores.Count(shot => shot != 0));

            return session;
        }
    }
}