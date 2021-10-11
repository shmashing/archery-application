using System;
using System.Collections.Generic;
using System.Linq;

namespace ArcheryTracker.Logic.Models
{
    public class UserStats
    {
        public string Id { get; set; }
        public List<Session> Sessions { get; set; }
        
        public int TotalShots { get; set; }
        public int TotalOnTarget { get; set; }
        public double ShotsPerSession { get; set; }
        public int ShotsThisMonth { get; set; }
        public int TotalBullseye { get; set; }
        public double OverallAccuracy { get; set; }
        public double PreviousOverallAccuracy { get; set; }
        public double OverallBullseyeAccuracy { get; set; }

        public UserStats(string id, List<Session> sessions)
        {
            Id = id;
            Sessions = new List<Session>();

            TotalShots = sessions.Select(session => session.TotalShots).Sum();
            TotalOnTarget = sessions.Select(session => session.TotalOnTarget).Sum();
            TotalBullseye = sessions.Select(session => session.TotalBullseye).Sum();

            ShotsPerSession = Math.Round(TotalShots * 1.0 / sessions.Count, 0);
            OverallAccuracy = Math.Round(TotalOnTarget * 100.0 / TotalShots, 2);
            OverallBullseyeAccuracy = Math.Round(TotalBullseye * 100.0 / TotalShots, 2);
            ShotsThisMonth = sessions.Where(session => session.Date.Month == DateTime.Now.Month)
                .Select(session => session.TotalShots).Sum();
        }
    }
}