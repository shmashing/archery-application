using System;
using System.Collections.Generic;
using System.Linq;

namespace ArcheryTracker.Logic.Models
{
    public class Session
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public DateTime Date { get; set; }
        public int TotalShots { get; set; }
        public double OnTargetAccuracy { get; set; }
        public Session(String id, String userId, DateTime date)
        {
            Id = id;
            UserId = userId;
            Date = date;
        }

        public void SetTotalShots(int totalShots)
        {
            TotalShots = totalShots;
        }

        public void SetOnTargetAccuracy(int scoredShots)
        {
            OnTargetAccuracy = scoredShots * 1.0 / TotalShots;
        }
    }
}