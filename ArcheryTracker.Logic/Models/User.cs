using System;
using System.Collections.Generic;

namespace ArcheryTracker.Logic.Models
{
    public class User
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime LastLogin { get; set; }
        public User(string id, string name, string email)
        {
            Id = id;
            Name = name;
            Email = email;
        }
    }
}