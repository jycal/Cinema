using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;


public class RevenueModel
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("money")]
    public decimal Money { get; set; }

    public RevenueModel(int id, decimal money)
    {
        Id = id;
        Money = money;
    }
}