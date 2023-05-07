using System.Threading.Tasks;
using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;


public class FoodModel
{
    [JsonPropertyName("Name")]
    public string Name { get; set; }

    [JsonPropertyName("Cost")]
    public double Cost { get; set; }

    public FoodModel(string name, double cost)
    {
        Name = name;
        Cost = cost;
    }
}