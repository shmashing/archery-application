using System;
using System.Collections.Generic;
using System.Linq;

namespace ArcheryTracker.Logic.Models
{
    public class UserStats
    {
        public string UserId { get; set; }
        public int TotalShots { get; set; }
        public int TotalOnTarget { get; set; }
        public double ShotsPerSession { get; set; }
        public int ShotsThisMonth { get; set; }
        public int TotalBullseye { get; set; }
        public double OverallAccuracy { get; set; }
        public double OverallBullseyeAccuracy { get; set; }

        public UserStats(
            string userId, 
            int totalShots, 
            int shotsThisMonth,
            int totalShotsOnTarget, 
            int totalBullseye,
            double shotsPerSession,
            double overallAccuracy,
            double overallBullseyeAccuracy
            )
        {
            UserId = userId;

            TotalShots = totalShots;
            ShotsThisMonth = shotsThisMonth;
            TotalOnTarget = totalShotsOnTarget;
            TotalBullseye = totalBullseye;

            ShotsPerSession = shotsPerSession;
            OverallAccuracy = overallAccuracy;
            OverallBullseyeAccuracy = overallBullseyeAccuracy;
        }
    }
}