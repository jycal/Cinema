using System.Text.Json.Serialization;


class AccountModel
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("type")]
    public int Type { get; set; }

    [JsonPropertyName("emailAddress")]
    public string EmailAddress { get; set; }

    [JsonPropertyName("password")]
    public string Password { get; set; }

    [JsonPropertyName("fullName")]
    public string FullName { get; set; }
    
    [JsonPropertyName("ticketList")]
    public List<int> TicketList { get; set; }

    public AccountModel(int id, string emailAddress, string password, string fullName, List<int> ticketList)
    {
        Id = id;
        Type = type;
        EmailAddress = emailAddress;
        Password = password;
        FullName = fullName;
        TicketList = ticketList;
    }
}