using System.Text.Json;
using System.Text.Json.Serialization;

namespace website_backend.Models;

public class About
{
    public int Id { get; set; }
    public string Background { get; set; } = string.Empty;
    public string MissionsJson { get; set; } = JsonSerializer.Serialize(new List<string>());
    
    [JsonIgnore]
    public List<string> Missions
    {
        get => JsonSerializer.Deserialize<List<string>>(MissionsJson) ?? new List<string>();
        set => MissionsJson = JsonSerializer.Serialize(value);
    }
    
    public List<HistoryItem> History { get; set; } = new List<HistoryItem>();
    public List<OrganizationItem> Organization { get; set; } = new List<OrganizationItem>();
}

public class HistoryItem
{
    public int Id { get; set; }
    public string Year { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int AboutId { get; set; }
    public About About { get; set; } = null!;
}

public class OrganizationItem
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int AboutId { get; set; }
    public About About { get; set; } = null!;
}