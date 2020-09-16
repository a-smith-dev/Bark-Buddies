using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BarkBuddies.Models

{
    public class ApiResponse
    {
        public Animal[] Animals { get; set; }
        public Pagination Pagination { get; set; }
        public Animal Animal { get; set; }
    }
}

public class _Links
{
    public Previous Previous { get; set; }
    public Next Next { get; set; }
}

public class Previous
{
    public string Href { get; set; }
}

public class Next
{
    public string Href { get; set; }
}

public class Breeds
{
    public string Primary { get; set; }
    public object Secondary { get; set; }
    public bool Mixed { get; set; }
    public bool Unknown { get; set; }
}

public class Colors
{
    public object Primary { get; set; }
    public object Secondary { get; set; }
    public object Tertiary { get; set; }
}

public class Attributes
{
    [JsonPropertyName("spayed_neutered")]
    public bool SpayedNeutered { get; set; }

    [JsonPropertyName("house_trained")]
    public bool HouseTrained { get; set; }
    [NotMapped]
    public object Declawed { get; set; }

    [JsonPropertyName("special_needs")]
    public bool SpecialNeeds { get; set; }

    [JsonPropertyName("shots_current")]
    public bool ShotsCurrent { get; set; }
}

public class Environment
{
    public bool Children { get; set; }
    public bool Dogs { get; set; }
    public bool Cats { get; set; }
}

public class Contact
{
    public string Email { get; set; }
    public string Phone { get; set; }
    public Address Address { get; set; }
}

public class Address
{
    public string Address1 { get; set; }
    public string Address2 { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Postcode { get; set; }
    public string Country { get; set; }
}

public class _Links1
{
    public Self Self { get; set; }
    public Type Type { get; set; }
    public Organization Organization { get; set; }
}

public class Self
{
    public string Href { get; set; }
}

public class Type
{
    public string Href { get; set; }
}

public class Organization
{
    public string Href { get; set; }
}

public class Photo
{
    public string Small { get; set; }
    public string Medium { get; set; }
    public string Large { get; set; }
    public string Full { get; set; }
}

public class Video
{
    public string Embed { get; set; }
}

