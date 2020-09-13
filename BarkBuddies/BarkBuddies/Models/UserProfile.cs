
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BarkBuddies.Models
{
    public class UserProfile : IdentityUser
    {
        public override string Id { get; set; }
        public int ZipCode { get; set; }
        public bool HasChildren { get; set; }
        public bool HasCats { get; set; }
        public Pet[] Pets { get; set; }
    }
}

