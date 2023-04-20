using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;


class GuestModel
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("email")]
    public string Email { get; set; }

    [JsonPropertyName("seats")]
    public List<int> Seats { get; set; }

    public GuestModel(int id, string email, List<int> seats)
    {
        Id = id;
        Email = email;
        Seats = seats;
    }
}
