using System;
using System.Collections.Generic;
using System.Linq;

namespace ArcheryTracker.Logic.Models
{
    public class UserStats
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public int TotalShots { get; set; }
        public int TotalOnTarget { get; set; }
        public double ShotsPerSession { get; set; }
        public int ShotsThisMonth { get; set; }
        public int TotalBullseye { get; set; }
        public double OverallAccuracy { get; set; }
        public double PreviousOverallAccuracy { get; set; }
        public double OverallBullseyeAccuracy { get; set; }

        public UserStats(string id, string userId)
        {
            Id = id;
            UserId = userId;

            TotalShots = 0;
            TotalOnTarget = 0;
            TotalBullseye = 0;

            ShotsPerSession = 0;
            OverallAccuracy = 0.0;
            OverallBullseyeAccuracy = 0.0;
            ShotsThisMonth = 0;
        }
    }
}