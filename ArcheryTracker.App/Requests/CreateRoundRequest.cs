using System.Collections.Generic;
using ArcheryTracker.Logic.Models;

namespace ArcheryTracker.App.Requests
{
    public class CreateRoundRequest
    {
        public string SessionId { get; set; }
        public List<int> Scores { get; set; }

        public Round ToRound()
        {
            return new Round(SessionId, Scores);
        }
    }
}