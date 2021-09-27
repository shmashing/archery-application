using System.Collections.Generic;
using System.Linq;

namespace ArcheryTracker.Logic.Models
{
    public class Round
    {
        public string SessionId { get; set; }
        public int TotalScore { get; set; }
        public int TotalShots { get; set; }
        public List<int> Scores { get; set; }

        public Round(string sessionId, List<int> scores)
        {
            SessionId = sessionId;
            Scores = scores;
            TotalShots = scores.Count;
            TotalScore = scores.Sum();
        }
    }
}