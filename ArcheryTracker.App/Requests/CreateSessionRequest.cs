using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ArcheryTracker.Logic;
using ArcheryTracker.Logic.Models;

namespace ArcheryTracker.App.Requests
{
    public class CreateSessionRequest
    {
        [Required]
        public DateTime Date { get; set; }
        
        [Required]
        [Range(1, 1000, ErrorMessage = "Invalid Range Provided: Must be between 1 and 1000")]
        public int Range { get; set; }
        
        public CreateSessionRequest() {}
        
        public Session ToSession()
        {
            var id = IdService.GetId(typeof(Session));
            var session = new Session(id, Date, Range);
            session.RoundScores = new List<Round>();
            return session;
        }
    }
}