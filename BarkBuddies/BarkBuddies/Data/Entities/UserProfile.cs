using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarkBuddies.Data.Entities
{
    public class UserProfile
    {
        public int Id { get; set; }
        public int ZipCode { get; set; }
        public bool HasChildren { get; set; }
        public bool HasCats { get; set; }
        //public List<Pet> Pets { get; set; }
        public IdentityUser User { get; set; }
    }
}
