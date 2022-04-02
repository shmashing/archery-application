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
        public Session(String id, String userId, DateTime date)
        {
            Id = id;
            UserId = userId;
            Date = date;
        }
    }
}