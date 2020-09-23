using System.ComponentModel;
using System.Text.Json.Serialization;


public class Animal
{
    [JsonPropertyName("id")]
    public int PetId { get; set; }

    [JsonPropertyName("organization_id")]
    public string OrganizationId { get; set; }

    [DisplayName("Profile")]
    public string Url { get; set; }
    public string Type { get; set; }
    public string Species { get; set; }
    public string Breed { get; set; }
    public Breeds Breeds{ get; set; }
    public string Age { get; set; }
    public string Gender { get; set; }
    public string Size { get; set; }
    public string Status { get; set; }
    public string Name { get; set; }
    public Photos[] Photos { get; set; }

    [DisplayName("Location")]
    public Contact Contact { get; set; }
}

public class Breeds
{
    public string Primary { get; set; }
    public string Secondary { get; set; }
    public bool Mixed { get; set; }
    public bool Unknown { get; set; }
}

public class Photos
{
    public string Small { get; set; }
    public string Medium { get; set; }
    public string Large { get; set; }
    public string Full { get; set; }
}