using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


public class FilmModel
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    [JsonPropertyName("dates")]
    public List<DateTime> Dates { get; set; }
    [JsonPropertyName("rooms")]
    public List<int> Rooms { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("duration")]
    public int Duration { get; set; }

    [JsonPropertyName("genre")]
    public List<string> Genre { get; set; }

    [JsonPropertyName("age")]
    public int Age { get; set; }

    [JsonPropertyName("imageURL")]
    public string ImageURL { get; set; }

    public FilmModel(int id, List<DateTime> dates, List<int> rooms, string title, string description, int duration, List<string> genre, int age, string imageURL)
    {
        Id = id;
        Dates = dates;
        Rooms = rooms;
        Title = title;
        Description = description;
        Duration = duration;
        Genre = genre;
        Age = age;
        ImageURL = imageURL;
    }
}