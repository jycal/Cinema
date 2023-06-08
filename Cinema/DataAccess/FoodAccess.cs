using System.Text.Json;

public static class FoodAccess
{
    public static string path = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/Food.json"));

    public static List<FoodModel> LoadAll()
    {
        string json = File.ReadAllText(path);
        return JsonSerializer.Deserialize<List<FoodModel>>(json)!;
    }

    public static void WriteAll(List<FoodModel> foods)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(foods, options);
        File.WriteAllText(path, json);
    }
}