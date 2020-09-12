
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BarkBuddies.Models
{
    public class UserProfile
    {
        [JsonPropertyName("id")]
        [Key]
        public int UserId { get; set; }
        public int ZipCode { get; set; }
        public bool HasChildren { get; set; }
        public bool HasCats { get; set; }

        //links the Indentity Id to the user profile in db
       

        [ForeignKey("Id")]
        public int Id {get; set;}

    }
}

