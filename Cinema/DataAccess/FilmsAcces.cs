using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



using System.Text.Json;
using System.IO;

static class FilmsAccess
{
    static string path = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/films.json"));


    public static List<FilmModel> LoadAll()
    {
        string json = File.ReadAllText(path);
        return JsonSerializer.Deserialize<List<FilmModel>>(json)!;
    }


    public static void WriteAll(List<FilmModel> films)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(films, options);
        File.WriteAllText(path, json);
    }



}
