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

        public string UserId { get; set; }
        
        public CreateSessionRequest() {}
        
        public Session ToSession()
        {
            var id = IdService.GetId(typeof(Session));
            var session = new Session(id, UserId, Date.ToUniversalTime());
            return session;
        }
    }
}