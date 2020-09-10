
﻿using System.ComponentModel.DataAnnotations.Schema;

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

