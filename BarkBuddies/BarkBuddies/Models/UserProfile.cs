
using BarkBuddies.Data.Entities;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BarkBuddies.Models
{
    public class UserProfile
    {
        public int Id { get; set; }
        public int ZipCode { get; set; }
        public bool HasChildren { get; set; }
        public bool HasCats { get; set; }
        public IdentityUser User { get; set; }
    }
}

