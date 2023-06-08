using System.Text.Json;

public class TicketAccess
{
    public static string path = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/ticket.json"));

    public static List<TicketModel> LoadAll()
    {
        string json = File.ReadAllText(path);
        return JsonSerializer.Deserialize<List<TicketModel>>(json)!;
    }

    public static void WriteAll(List<TicketModel> room)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(room, options);
        File.WriteAllText(path, json);
    }
}
