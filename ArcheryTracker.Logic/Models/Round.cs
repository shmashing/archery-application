using System.Collections.Generic;
using System.Linq;

namespace ArcheryTracker.App.Data
{
    public class Round
    {
        public int TotalScore { get; set; }
        public int TotalShots { get; set; }
        public List<int> Scores { get; set; }

        public Round(List<int> scores)
        {
            Scores = scores;
            TotalShots = scores.Count;
            TotalScore = scores.Sum();
        }
    }
}