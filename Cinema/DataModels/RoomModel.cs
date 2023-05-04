using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Text.Json.Serialization;
public class RoomModel
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
    public List<int> Seats { get; set; }

    [JsonPropertyName("vipSeats")]
    public List<int> VipSeats { get; set; }

    [JsonPropertyName("disabledSeats")]
    public List<int> DisabledSeats { get; set; }

    [JsonPropertyName("comfortSeats")]
    public List<int> ComfortSeats { get; set; }

    public RoomModel(int id, int maxSeats, int roomNumber, int maxPeople, List<int> seats, List<int> vipSeats, List<int> disabledSeats, List<int> comfortSeats)
    {
        Id = id;
        MaxSeats = maxSeats;
        RoomNumber = roomNumber;
        MaxPeople = maxPeople;
        Seats = seats;
        VipSeats = vipSeats;
        DisabledSeats = disabledSeats;
        ComfortSeats = comfortSeats;
    }
}
