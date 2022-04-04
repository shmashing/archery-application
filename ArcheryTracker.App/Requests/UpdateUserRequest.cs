using System;
using System.ComponentModel.DataAnnotations;
using ArcheryTracker.Logic.Models;

namespace ArcheryTracker.App.Requests
{
    public class UpdateUserRequest
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }

        public User ToUser()
        {
            return new User(UserId, Name, Email, DateTime.Now.ToUniversalTime());
        }
    }
}