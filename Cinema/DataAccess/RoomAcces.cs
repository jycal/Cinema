using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;


static class RoomAcces
{

    static string path = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/rooms.json"));


    public static List<RoomModel> LoadAll()
    {
        string json = File.ReadAllText(path);
        return JsonSerializer.Deserialize<List<RoomModel>>(json)!;
    }


    public static void WriteAll(List<RoomModel> room)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(room, options);
        File.WriteAllText(path, json);
    }




}
