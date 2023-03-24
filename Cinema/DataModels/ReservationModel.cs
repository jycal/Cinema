using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;


class ReservationModel
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("title")]
    public string FullName { get; set; }

    [JsonPropertyName("description")]
    public string Email { get; set; }

    [JsonPropertyName("duration")]
    public string Movie { get; set; }

    [JsonPropertyName("genre")]
    public int TicketAmount { get; set; }
    [JsonPropertyName("genre")]
    public int Seats { get; set; }
    [JsonPropertyName("genre")]
    public int TotalAmount { get; set; }

    public ReservationModel(int id, string fullName, string email, string movie, int ticketAmount, int seats, int totalAmount)
    {
        Id = id;
        FullName = fullName;
        Email = email;
        Movie = movie;
        TicketAmount = ticketAmount;
        Seats = seats;
        TotalAmount = totalAmount;
    }
}
