using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;

namespace BarkBuddies.Models
{
    public class Pet
    {
        [Key]
        public int PetId { get; set; }
        public string Name { get; set; }
        public Age Age { get; set; }
        public string Gender { get; set; }
        public Size Size { get; set; }
        public string Breed { get; set; }

        [ForeignKey("UserId")]
        public int UserId { get; set; }
    }

    public enum Size
    {
        small = 0,

        medium,

        large,

        xlarge
    }

    public enum Age
    {
        baby = 0,

        young,
        
        adult,
        
        senior
    }
}
