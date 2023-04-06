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

    public AccountModel(int id, int type, string emailAddress, string password, string fullName)
    {
        Id = id;
        Type = type;
        EmailAddress = emailAddress;
        Password = password;
        FullName = fullName;
    }

}
