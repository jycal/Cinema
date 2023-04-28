using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

public class TicketModel
{
    [JsonPropertyName("Name")]
    public string Name { get; set; }

    [JsonPropertyName("Cost")]
    public double Cost { get; set; }

    public TicketModel(string name, double cost)
    {
        Name = name;
        Cost = cost;
    }
}
