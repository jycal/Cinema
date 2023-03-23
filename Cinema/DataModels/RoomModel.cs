using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Text.Json.Serialization;
class RoomModel
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("maxSeats")]
    public int MaxSeats { get; set; }

    [JsonPropertyName("hallNumber")]
    public int HallNumber { get; set; }

    [JsonPropertyName("maxPeople")]
    public int MaxPeople { get; set; }


    public RoomModel(int id, int maxSeats, int hallNumber, int fullmaxPeople)
    {
        Id = id;
        MaxSeats = maxSeats;
        HallNumber = hallNumber;
        MaxPeople = fullmaxPeople;

    }
}
