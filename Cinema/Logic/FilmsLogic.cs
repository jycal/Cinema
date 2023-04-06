using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


class FilmsLogic
{
    private static string CurrentMovieOverview = @"============================================
|                                          |
|          CURRENT MOVIE OVERVIEW          |
|                                          |
============================================";
    private List<FilmModel> _films;

    //Static properties are shared across all instances of the class
    //This can be used to get the current logged in account from anywhere in the program
    //private set, so this can only be set by the class itself
    static public FilmModel? CurrentFilm { get; private set; }

    public FilmsLogic()
    {
        _films = FilmsAccess.LoadAll();
    }


    public void UpdateList(FilmModel film)
    {
        // create id
        // System.Console.WriteLine(film.Id);
        film.Id = _films.Count > 0 ? _films.Max(x => x.Id) + 1 : 1;
        // System.Console.WriteLine(film.Id);
        //Find if there is already an model with the same id
        int index = _films.FindIndex(s => s.Id == film.Id);

        if (index != -1)
        {
            //update existing model
            _films[index] = film;
        }
        else
        {
            //add new model
            _films.Add(film);
        }
        FilmsAccess.WriteAll(_films);

    }

    public bool DeleteFilm(string title)
    {
        //Find if there is already an model with the same id
        var film = _films.Find(r => r.Title == title);
        if (film == null)
        {
            return false;
        }
        int index = _films.FindIndex(s => s.Id == film!.Id);

        if (index != -1)
        {
            //update existing model
            // _films[index] = film;
            _films.RemoveAt(index);
            _films.ForEach((x) => { if (x.Id > film!.Id) x.Id = x.Id - 1; });
            FilmsAccess.WriteAll(_films);
            return true;
        }
        else
        {
            return false;
        }

    }

    public FilmModel GetById(int id)
    {
        return _films.Find(i => i.Id == id)!;
    }

    // public AccountModel CheckLogin(string email, string password)
    // {
    //     if (email == null || password == null)
    //     {
    //         return null;
    //     }
    //     CurrentFilm = _films.Find(i => i.EmailAddress == email && i.Password == password);
    //     return CurrentFilm;
    // }

    public static void MovieOverview()
    {
        var MoviesFromJson = FilmsAccess.LoadAll();
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine(CurrentMovieOverview);
        foreach (FilmModel item in MoviesFromJson)
        {
            int ID = item.Id;
            string Title = item.Title;
            string Description = item.Description;
            int Duration = item.Duration;
            string Genre = "";
            foreach (var genre in item.Genre)
            {
                Genre += $"{genre} ";
            }
            string Overview = $@"
  ID: {ID}
  Title: {Title}
  Description: {Description}
  Duration: {Duration}
  Genre: {Genre}

============================================";
            Console.WriteLine(Overview);
        }
        Console.ResetColor();
    }

    public static void SearchByTitle(string filter)
    {
        var MoviesFromJson = FilmsAccess.LoadAll();
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine(CurrentMovieOverview);
        foreach (FilmModel item in MoviesFromJson)
        {
            if (item.Title == filter)
            {
                int ID = item.Id;
                string Title = item.Title;
                string Description = item.Description;
                int Duration = item.Duration;
                string Genre = "";
                foreach (var genre in item.Genre)
                {
                    Genre += $"{genre} ";
                }
                string Overview = $@"
  ID: {ID}
  Title: {Title}
  Description: {Description}
  Duration: {Duration}
  Genre: {Genre}

============================================";
                Console.WriteLine(Overview);
            }
        }
        Console.ResetColor();
    }
    public static void SearchByGenre(string filter)
    {
        var MoviesFromJson = FilmsAccess.LoadAll();
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine(CurrentMovieOverview);
        foreach (FilmModel item in MoviesFromJson)
        {
            foreach (var genre in item.Genre)
            {
                if (genre != filter)
                {
                    continue;
                }
                else
                {
                    int ID = item.Id;
                    string Title = item.Title;
                    string Description = item.Description;
                    int Duration = item.Duration;
                    string Genre = "";
                    foreach (var genre2 in item.Genre)
                    {
                        Genre += $"{genre2} ";
                    }
                    string Overview = $@"
  ID: {ID}
  Title: {Title}
  Description: {Description}
  Duration: {Duration}
  Genre: {Genre}

============================================";
                    Console.WriteLine(Overview);
                }
            }
        }
        Console.ResetColor();
    }

    public static void MovieSortedByABCTitle()
    {
        var MoviesFromJson = FilmsAccess.LoadAll();
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine(CurrentMovieOverview);
        var descListOb = MoviesFromJson.OrderBy(x => x.Title);
        // Console.WriteLine(descListOb);
        foreach (FilmModel item in descListOb)
        {
            int ID = item.Id;
            string Title = item.Title;
            string Description = item.Description;
            int Duration = item.Duration;
            string Genre = "";
            foreach (var genre in item.Genre)
            {
                Genre += $"{genre} ";
            }
            string Overview = $@"
  ID: {ID}
  Title: {Title}
  Description: {Description}
  Duration: {Duration}
  Genre: {Genre}

============================================";
            Console.WriteLine(Overview);
        }
        Console.ResetColor();
    }
}