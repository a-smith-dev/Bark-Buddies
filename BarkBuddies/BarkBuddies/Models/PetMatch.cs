using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BarkBuddies.Models
{
    public class PetMatch
    {
        [JsonPropertyName("id")]
        public int PetMatchId { get; set; } //animal ID from petfinder
        public string Name { get; set; }
        public string Age { get; set; }
        public string Gender { get; set; }
        public string Size { get; set; }
        public string Breed { get; set; }
        public string Url { get; set; }
        public string PhotoUrl { get; set; }
        public string Organization { get; set; }
        [JsonPropertyName("good_with_children")]
        public bool GoodWithChildren { get; set; }
        [JsonPropertyName("good_with_dogs")]
        public bool GoodWithDogs { get; set; }
        [JsonPropertyName("good_with_cats")]
        public bool GoodWithCats { get; set; }
    }
}
