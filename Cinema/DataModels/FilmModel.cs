using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


public class FilmModel
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("duration")]
    public int Duration { get; set; }

    [JsonPropertyName("genre")]
    public List<string> Genre { get; set; }

    [JsonPropertyName("imageURL")]
    public string ImageURL { get; set; }

    public FilmModel(int id, string title, string description, int duration, List<string> genre, string imageURL)
    {
        Id = id;
        Title = title;
        Description = description;
        Duration = duration;
        Genre = genre;
        ImageURL = imageURL;
    }
}