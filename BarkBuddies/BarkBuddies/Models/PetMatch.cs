using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BarkBuddies.Models
{
    public class PetMatch
    {
        [JsonPropertyName("PetId")]
        public int PetMatchId { get; set; } //animal ID from petfinder
        
        public string Name { get; set; }
        
        public string Age { get; set; }
        
        public string Gender { get; set; }
        
        public string Size { get; set; }
        
        public string Breed { get; set; }
        
        public string Url { get; set; }
        
        public string PhotoUrl { get; set; }
        
        public string Organization { get; set; }
    }
}
