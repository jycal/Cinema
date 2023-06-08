using System.Text.Json.Serialization;

public class RevenueModel
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("money")]
    public double Money { get; set; }

    public RevenueModel(int id, string name, double money)
    {
        Id = id;
        Name = name;
        Money = money;
    }
}