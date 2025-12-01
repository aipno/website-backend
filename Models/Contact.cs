using System.Text.Json;
using System.Text.Json.Serialization;

namespace website_backend.Models;

public class Contact
{
    public int Id { get; set; }
    public List<ContactDetail> Details { get; set; } = new List<ContactDetail>();
    public List<SocialLink> SocialLinks { get; set; } = new List<SocialLink>();
    public JoinUsInfo JoinUs { get; set; } = null!;
}

public class ContactDetail
{
    public int Id { get; set; }
    public string Type { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
    public int ContactId { get; set; }
    public Contact Contact { get; set; } = null!;
}

public class SocialLink
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public int ContactId { get; set; }
    public Contact Contact { get; set; } = null!;
}

public class JoinUsInfo
{
    public int Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public string ConditionsJson { get; set; } = JsonSerializer.Serialize(new List<string>());
    public string StepsJson { get; set; } = JsonSerializer.Serialize(new List<string>());
    
    [JsonIgnore]
    public List<string> Conditions
    {
        get => JsonSerializer.Deserialize<List<string>>(ConditionsJson) ?? new List<string>();
        set => ConditionsJson = JsonSerializer.Serialize(value);
    }
    
    [JsonIgnore]
    public List<string> Steps
    {
        get => JsonSerializer.Deserialize<List<string>>(StepsJson) ?? new List<string>();
        set => StepsJson = JsonSerializer.Serialize(value);
    }
    
    public string ApplicationUrl { get; set; } = string.Empty;
    public int ContactId { get; set; }
    public Contact Contact { get; set; } = null!;
}