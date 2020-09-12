using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;

namespace BarkBuddies.Models
{
    public class Pet
    {
        [Key]
        public int PetId { get; set; }

        [ForeignKey("UserProfileId")]
        public int UserProfileId { get; set; }
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
        [Display(Name = "Small")]
        small = 0,

        [Display(Name = "Medium")]
        medium,

        [Display(Name = "Large")]
        large,

        [Display(Name = "Extra Large")]
        xlarge
    }

    public enum Age
    {
        [Display(Name = "Puppy")]
        baby = 0,
        
        [Display(Name = "Young")]
        young,

        [Display(Name = "Adult")]
        adult,

        [Display(Name = "Senior")]
        senior
    }
}
