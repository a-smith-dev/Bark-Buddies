using System.Text.Json.Serialization;

namespace BarkBuddies.Models
{
    public class Pet
    {
        public int PetId { get; set; }
        public string Name { get; set; }
        public string Age { get; set; } // 
        public string Gender { get; set; }
        public string Size { get; set; }
        public string Breed { get; set; }
    }
}
