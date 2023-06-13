public class FilmsLogic
{
    private string CurrentMovieOverview = @"============================================
|                                          |
|               ".BrightYellow() + @"Movie Menu".BrightWhite() + @"                 |
|                                          |
============================================".BrightYellow();
    private List<FilmModel> _films;

    //Static properties are shared across all instances of the class
    //This can be used to get the current logged in account from anywhere in the program
    //private set, so this can only be set by the class itself
    static public FilmModel? CurrentFilm { get; private set; }
    public string fileOutput = @"C:\Users\vdmil\source\repos\HTTPClient_APP\HTTPClient_APP\output.txt";

    public FilmsLogic()
    {
        _films = FilmsAccess.LoadAll();
        removeOldies();
    }

    public void removeOldies()
    {
        List<Tuple<int, DateTime>> filmIdDate = new();
        foreach (var film in _films)
        {
            foreach (var date in film.Dates)
            {
                if (date < DateTime.Now)
                {
                    var filmId = film.Id;
                    var dateRemove = date;
                    filmIdDate.Add(new Tuple<int, DateTime>(filmId, dateRemove));
                }
            }
        }
        foreach (var index in filmIdDate)
        {
            FilmModel film = _films.Find(x => x.Id == index.Item1)!;
            int indexOfDate = film.Dates.IndexOf(index.Item2);
            Console.WriteLine(indexOfDate);
            _films.Find(x => x.Id == index.Item1)!.Rooms.RemoveAt(indexOfDate);
            _films.Find(x => x.Id == index.Item1)!.Dates.Remove(index.Item2);
        }
        FilmsAccess.WriteAll(_films);
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
            // dates and room cannot exist already
            foreach (var date in film.Dates)
            {
                foreach (var room in film.Rooms)
                {
                    var film2 = _films.Find(r => r.Dates.Contains(date) && r.Rooms.Contains(room));
                    if (film2 != null)
                    {
                        Console.WriteLine("Date or room already exists");
                        return;
                    }
                }
            }

            Console.WriteLine("Film added");
            _films.Add(film);
        }
        FilmsAccess.WriteAll(_films);
        Console.WriteLine("Press any key to continue...");
    }

    public bool DeleteFilm(int id)
    {
        //Find if there is already an model with the same id
        var film = _films.Find(r => r.Id == id);
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
        FilmModel film = _films.Find(i => i.Id == id)!;
        if (film == null)
        {
            return film!;
        }

        if (film.Dates.Count() > 0)
        {
            return film;
        }
        else
        {
            return null!;
        }
    }

    public FilmModel GetByIdOld(int id)
    {
        FilmModel film = _films.Find(i => i.Id == id)!;
        if (film == null)
        {
            return film!;
        }

        if (film.Dates.Count == 0)
        {
            return film;
        }
        else
        {
            return null!;
        }
    }

    public FilmModel GetByName(string name)
    {
        return _films.Find(i => i.Title == name)!;
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

    public void MovieOverviewOld()
    {
        removeOldies();

        var MoviesFromJson = FilmsAccess.LoadAll();
        // Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(CurrentMovieOverview);
        foreach (FilmModel item in MoviesFromJson)
        {
            int ID = item.Id;
            string Dates = string.Join(", ", item.Dates);
            int dateNumber = 1;
            string Rooms = "";
            foreach (var room in item.Rooms)
            {
                Rooms += $"{dateNumber}.";
                dateNumber++;
            }
            string Title = item.Title;
            string Description = item.Description;
            int Duration = item.Duration;
            string Genre = "";
            foreach (var genre in item.Genre)
            {
                Genre += $"{genre} ";
            }
            int Age = item.Age;
            string Overview = $@"
  ID: {ID}
  Dates: {Dates}
  Rooms: {Rooms}
  Title: {Title}
  Description: {Description}
  Duration: {Duration}
  Genre: {Genre}
  Rated: {Age}

" + @"============================================".BrightYellow();
            Console.WriteLine(Overview);
        }
        Console.ResetColor();
    }

    public void MovieOverview()
    {
        removeOldies();

        var MoviesFromJson = FilmsAccess.LoadAll();
        // Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(CurrentMovieOverview);
        foreach (FilmModel item in MoviesFromJson)
        {
            if (item.Dates.Count == 0)
            {
                continue;
            }

            int ID = item.Id;
            string Dates = string.Join(", ", item.Dates);
            int dateNumber = 1;
            string Rooms = "";
            foreach (var room in item.Rooms)
            {
                Rooms += $"{dateNumber}. Date: {room} ";
                dateNumber++;
            }
            string Title = item.Title;
            string Description = item.Description;
            int Duration = item.Duration;
            string Genre = "";
            foreach (var genre in item.Genre)
            {
                Genre += $"{genre} ";
            }
            int Age = item.Age;
            string Overview = $@"
  ID: {ID}
  Dates: {Dates}
  Rooms: {Rooms}
  Title: {Title}
  Description: {Description}
  Duration: {Duration}
  Genre: {Genre}
  Rated: {Age}

" + @"============================================".BrightYellow();
            Console.WriteLine(Overview);
        }
        Console.ResetColor();
    }

    public void SearchByTitle(string filter)
    {
        removeOldies();

        var MoviesFromJson = FilmsAccess.LoadAll();
        // Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine(CurrentMovieOverview);
        foreach (FilmModel item in MoviesFromJson)
        {
            if (item.Dates.Count == 0)
            {
                // System.Console.WriteLine();
                // System.Console.WriteLine("No movies found..".Red());
                // System.Console.WriteLine();
                continue;
            }

            if (item.Title.ToLower().Contains(filter.ToLower()))
            {
                int ID = item.Id;
                string Dates = string.Join(", ", item.Dates);
                int dateNumber = 1;
                string Rooms = "";
                foreach (var room in item.Rooms)
                {
                    Rooms += $"{dateNumber}. Date: {room} ";
                    dateNumber++;
                }
                string Title = item.Title;
                string Description = item.Description;
                int Duration = item.Duration;
                string Genre = "";
                foreach (var genre in item.Genre)
                {
                    Genre += $"{genre} ";
                }
                int Age = item.Age;
                string Overview = $@"
  ID: {ID}
  Dates: {Dates}
  Rooms: {Rooms}
  Title: {Title}
  Description: {Description}
  Duration: {Duration}
  Genre: {Genre}
  Rated: {Age}

" + @"============================================".BrightYellow();
                Console.WriteLine(Overview);
            }
        }
        Console.ResetColor();
        Console.WriteLine();
        // Console.WriteLine("Press any key to continue...");
        // Console.ReadKey(true);
    }
    public void SearchByGenre(string filter)
    {
        removeOldies();

        var MoviesFromJson = FilmsAccess.LoadAll();
        // Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine(CurrentMovieOverview);
        foreach (FilmModel item in MoviesFromJson)
        {
            if (item.Dates.Count == 0)
            {
                // System.Console.WriteLine();
                // System.Console.WriteLine("No movies found..".Red());
                // System.Console.WriteLine();
                continue;
            }

            foreach (var genre in item.Genre)
            {
                if (!genre.ToLower().Contains(filter.ToLower()))
                {
                    continue;
                }
                else
                {
                    int ID = item.Id;
                    string Dates = string.Join(", ", item.Dates);
                    int dateNumber = 1;
                    string Rooms = "";
                    foreach (var room in item.Rooms)
                    {
                        Rooms += $"{dateNumber}. Date: {room} ";
                        dateNumber++;
                    }
                    string Title = item.Title;
                    string Description = item.Description;
                    int Duration = item.Duration;
                    string Genre = "";
                    foreach (var genre2 in item.Genre)
                    {
                        Genre += $"{genre2} ";
                    }
                    int Age = item.Age;
                    string Overview = $@"
  ID: {ID}
  Dates: {Dates}
  Rooms: {Rooms}
  Title: {Title}
  Description: {Description}
  Duration: {Duration}
  Genre: {Genre}
  Rated: {Age}

" + @"============================================".BrightYellow();
                    Console.WriteLine(Overview);
                }
            }
        }
        Console.ResetColor();
    }

    public void MovieSortedByABCTitle()
    {
        removeOldies();

        var MoviesFromJson = FilmsAccess.LoadAll();
        // Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine(CurrentMovieOverview);
        var descListOb = MoviesFromJson.OrderBy(x => x.Title);
        // Console.WriteLine(descListOb);
        foreach (FilmModel item in descListOb)
        {
            if (item.Dates.Count == 0)
            {
                continue;
            }

            int ID = item.Id;
            string Dates = string.Join(", ", item.Dates);
            int dateNumber = 1;
            string Rooms = "";
            foreach (var room in item.Rooms)
            {
                Rooms += $"{dateNumber}. Date: {room} ";
                dateNumber++;
            }
            string Title = item.Title;
            string Description = item.Description;
            int Duration = item.Duration;
            string Genre = "";
            foreach (var genre in item.Genre)
            {
                Genre += $"{genre} ";
            }
            int Age = item.Age;
            string Overview = $@"
  ID: {ID}
  Dates: {Dates}
  Rooms: {Rooms}
  Title: {Title}
  Description: {Description}
  Duration: {Duration}
  Genre: {Genre}
  Rated: {Age}

" + @"============================================".BrightYellow();
            Console.WriteLine(Overview);
        }
        Console.ResetColor();
    }
}