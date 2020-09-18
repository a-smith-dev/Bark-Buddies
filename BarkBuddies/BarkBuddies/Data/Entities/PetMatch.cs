using Microsoft.AspNetCore.Identity;

namespace BarkBuddies.Data.Entities
{
    public class PetMatch
    {
        public int PetMatchId { get; set; }
        public int PetId { get; set; }
        public string Name { get; set; }
        public Age Age { get; set; }
        public string Gender { get; set; }
        public Size Size { get; set; }
        public string Breed { get; set; }
        public string Status { get; set; } 
        public IdentityUser User { get; set; }
    }
}