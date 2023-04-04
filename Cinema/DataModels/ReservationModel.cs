using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;


class ReservationModel
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("fullName")]
    public string FullName { get; set; }

    [JsonPropertyName("email")]
    public string Email { get; set; }

    [JsonPropertyName("movie")]
    public string Movie { get; set; }

    [JsonPropertyName("ticketAmount")]
    public int TicketAmount { get; set; }
    [JsonPropertyName("seats")]
    public List<int> Seats { get; set; }
    [JsonPropertyName("totalAmount")]
    public int TotalAmount { get; set; }

    public ReservationModel(int id, string fullName, string email, string movie, int ticketAmount, List<int> seats, int totalAmount)
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