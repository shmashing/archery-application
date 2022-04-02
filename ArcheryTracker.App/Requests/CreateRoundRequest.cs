using System.Collections.Generic;
using ArcheryTracker.Logic;
using ArcheryTracker.Logic.Models;

namespace ArcheryTracker.App.Requests
{
    public class CreateRoundRequest
    {
        public string UserId { get; set; }
        public string SessionId { get; set; }
        public List<int> Scores { get; set; }

        public Round ToRound()
        {
            var id = IdService.GetId(typeof(Round));
            return new Round(id, UserId, SessionId, Scores);
        }
    }
}