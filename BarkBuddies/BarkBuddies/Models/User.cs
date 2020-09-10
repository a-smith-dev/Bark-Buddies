using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BarkBuddies.Models
{
    public class User
    {
        public int UserId { get; set; }
        public int ZipCode { get; set; }
        public bool HasChildren { get; set; }
        public bool HasCats { get; set; }

        [ForeignKey("PetId")]
        public int PetId { get; set; }
    }
}

