using System.Text.Json.Serialization;


public class Animal
{
    [JsonPropertyName("id")]
    public int PetId { get; set; }

    [JsonPropertyName("organization_id")]
    public string OrganizationId { get; set; }
    public string Url { get; set; }
    public string Type { get; set; }
    public string Species { get; set; }
    public string Breed { get; set; } //need to get rid of this once petMatch controller updated
    public Breeds Breeds{ get; set; }
    public string Age { get; set; }
    public string Gender { get; set; }
    public string Size { get; set; }
    public string Name { get; set; }
    public Photos[] Photos { get; set; }
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
//[NotMapped]
//    public object Coat { get; set; }
//    //public Attributes Attributes { get; set; }
//    //public Environment Environment { get; set; }
//    [NotMapped]
//    //public string[] Tags { get; set; }

//    //public string Description { get; set; }
//    //public Photo[] Photos { get; set; }
//    //public Video[] Videos { get; set; }
//    //public string Status { get; set; }

//    //[JsonPropertyName("published_at")]
//    //public DateTime PublishedAt { get; set; }
//    //public Contact Contact { get; set; }
//    ////public _Links1 Links { get; set; }
//}

