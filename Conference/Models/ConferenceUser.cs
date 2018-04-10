using Microsoft.AspNetCore.Http;

namespace Conference.Models
{
    public class ConferenceUser {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public IFormFile Avatar { get; set; }
    }
}
