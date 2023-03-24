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

    [JsonPropertyName("roomNumber")]
    public int RoomNumber { get; set; }

    [JsonPropertyName("maxPeople")]
    public int MaxPeople { get; set; }

    [JsonPropertyName("seats")]
    public List<bool> Seats { get; set; }

    public RoomModel(int id, int maxSeats, int roomNumber, int maxPeople, List<bool> seats)
    {
        Id = id;
        MaxSeats = maxSeats;
        RoomNumber = roomNumber;
        MaxPeople = maxPeople;
        Seats = seats;
    }
}
