using System.Text.Json.Serialization;

public class ReservationModel
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("reservationCode")]
    public string ReservationCode { get; set; }

    [JsonPropertyName("fullName")]
    public string FullName { get; set; }

    [JsonPropertyName("email")]
    public string Email { get; set; }

    [JsonPropertyName("movie")]
    public string Movie { get; set; }

    [JsonPropertyName("ticketAmount")]
    public double TicketAmount { get; set; }

    [JsonPropertyName("snackAmount")]
    public double SnackAmount { get; set; }

    [JsonPropertyName("ticketTotal")]
    public double TicketTotal { get; set; }

    [JsonPropertyName("roomNumber")]
    public int RoomNumber { get; set; }

    [JsonPropertyName("seats")]
    public List<int[]> Seats { get; set; }

    [JsonPropertyName("seatsInfo")]
    public List<Tuple<int, int, DateTime, int>> SeatsInfo { get; set; }

    [JsonPropertyName("totalAmount")]
    public double TotalAmount { get; set; }

    [JsonPropertyName("date")]
    public DateTime Date { get; set; }

    public ReservationModel(int id, string reservationCode, string fullName, string email, string movie, double ticketAmount, double snackAmount, double ticketTotal, int roomNumber, List<int[]> seats, List<Tuple<int, int, DateTime, int>> seatsInfo, double totalAmount, DateTime date)
    {
        Id = id;
        ReservationCode = reservationCode;
        FullName = fullName;
        Email = email;
        Movie = movie;
        TicketAmount = ticketAmount;
        SnackAmount = snackAmount;
        TicketTotal = ticketTotal;
        RoomNumber = roomNumber;
        Seats = seats;
        SeatsInfo = seatsInfo;
        TotalAmount = totalAmount;
        Date = date;
    }
}
