using System.Text.Json;

public static class GuestAccess
{
    public static string path = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/Guest.json"));

    public static List<ReservationModel> LoadAll()
    {
        string json = File.ReadAllText(path);
        return JsonSerializer.Deserialize<List<ReservationModel>>(json)!;
    }

    public static void WriteAll(List<ReservationModel> emails)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(emails, options);
        File.WriteAllText(path, json);
    }
}