using System.Text.Json.Serialization;

public class RoomModel
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("maxSeats")]
    public int MaxSeats { get; set; }

    [JsonPropertyName("roomLength")]
    public int RoomLength { get; set; }

    [JsonPropertyName("roomWidth")]
    public int RoomWidth { get; set; }

    [JsonPropertyName("roomNumber")]
    public int RoomNumber { get; set; }

    [JsonPropertyName("maxPeople")]
    public int MaxPeople { get; set; }

    [JsonPropertyName("seats")]
    public List<Tuple<int, int, DateTime, int>> Seats { get; set; }

    [JsonPropertyName("vipSeats")]
    public List<int> VipSeats { get; set; }

    [JsonPropertyName("disabledSeats")]
    public List<int> DisabledSeats { get; set; }

    [JsonPropertyName("comfortSeats")]
    public List<int> ComfortSeats { get; set; }

    [JsonPropertyName("noSeats")]
    public List<int> NoSeats { get; set; }

    public RoomModel(int id, int maxSeats, int roomLength, int roomWidth, int roomNumber, int maxPeople, List<Tuple<int, int, DateTime, int>> seats, List<int> vipSeats, List<int> disabledSeats, List<int> comfortSeats, List<int> noSeats)
    {
        Id = id;
        MaxSeats = maxSeats;
        RoomLength = roomLength;
        RoomWidth = roomWidth;
        RoomNumber = roomNumber;
        MaxPeople = maxPeople;
        Seats = seats;
        VipSeats = vipSeats;
        DisabledSeats = disabledSeats;
        ComfortSeats = comfortSeats;
        NoSeats = noSeats;
    }
}