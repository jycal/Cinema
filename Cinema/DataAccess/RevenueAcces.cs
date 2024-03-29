using System.Text.Json;

public static class RevenueAcces
{
    public static string path = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/revenue.json"));


    public static List<RevenueModel> LoadAll()
    {
        string json = File.ReadAllText(path);
        return JsonSerializer.Deserialize<List<RevenueModel>>(json)!;
    }


    public static void WriteAll(List<RevenueModel> revenueList)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(revenueList, options);
        File.WriteAllText(path, json);
    }
}