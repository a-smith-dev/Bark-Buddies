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
        public string Status { get; set; } // Adoptability status. Defaults to adoptable. Ping the API to confirm the status of this PetMatch animal.
        public IdentityUser User { get; set; }
    }
}