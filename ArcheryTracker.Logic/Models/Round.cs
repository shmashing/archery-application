using System.Collections.Generic;
using System.Linq;

namespace ArcheryTracker.Logic.Models
{
    public class Round
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string SessionId { get; set; }
        public List<int> Scores { get; set; }

        public Round(string id, string userId, string sessionId, List<int> scores)
        {
            Id = id;
            UserId = userId;
            SessionId = sessionId;
            Scores = scores;
        }
    }
}