
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BarkBuddies.Models
{
    public class UserProfile
    {
        public int UserProfileId { get; set; }
        public int ZipCode { get; set; }
        public bool HasChildren { get; set; }
        public bool HasCats { get; set; }
        public Pet[] Pets { get; set; }

        //[ForeignKey("PetId")]
        //public int PetId { get; set; }
    }
}

