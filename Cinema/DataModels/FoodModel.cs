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

    [JsonPropertyName("Quantity")]
    public double Quantity { get; set; }

    [JsonPropertyName("Age")]
    public int Age { get; set; }

    public FoodModel(string name, double cost, double quantity, int age)
    {
        Name = name;
        Cost = cost;
        Quantity = quantity;
        Age = age;
    }
}