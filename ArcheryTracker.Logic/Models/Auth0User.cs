namespace ArcheryTracker.Logic.Models
{
    public class Auth0User
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public Auth0User(string id, string name, string email)
        {
            Id = id;
            Name = name;
            Email = email;
        }
    }
}