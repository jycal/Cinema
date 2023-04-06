public class Menu
{
    private AccountsLogic _accountsLogic = new AccountsLogic();
    private AccountModel _account = null!;
    private FilmsLogic _filmsLogic = new FilmsLogic();

    // Intro screen
    public void Start()
    {
        Console.WriteLine("====================================");
        Console.WriteLine("|                                  |");
        Console.WriteLine("|   Welcome to Star Light Cinema   |");
        Console.WriteLine("|                                  |");
        Console.WriteLine("====================================");
        Console.WriteLine();
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
        Console.Clear();
        PreLogin();
    }
    public void PreLogin()
    {
        Console.WriteLine("====================================");
        Console.WriteLine("|                                  |");
        Console.WriteLine("|   Welcome to Star Light Cinema   |");
        Console.WriteLine("|                                  |");
        Console.WriteLine("====================================");
        Console.WriteLine();
        Console.WriteLine("Please select an option:");
        Console.WriteLine("1. Login as member");
        Console.WriteLine("2. Register as member");
        Console.WriteLine("3. Continue as guest");
        Console.WriteLine("4. Continue with advanced acces");
        Console.WriteLine();
        Console.WriteLine("5. Cinema information");
        Console.WriteLine();
        Console.WriteLine("6. Exit program");
        Console.Write("Enter your choice: ");
        string input = Console.ReadLine()!;
        Console.Clear();
        if (input == "1")
        {
            Login(1);
        }
        else if (input == "2")
        {
            MemberRegister();
        }
        else if (input == "3")
        {
            Guest();
        }
        else if (input == "4")
        {
            AdvancedAccess();
        }
        else if (input == "5")
        {
            CinemaInfo();
            PreLogin();
        }
        else if (input == "6")
        {
            Environment.Exit(0);
        }
        else
        {
            Console.WriteLine("Invalid input");
            PreLogin();
        }
    }

    // Login and register method
    public void Login(int type)
    {
        Console.WriteLine("====================================");
        Console.WriteLine("|                                  |");
        Console.WriteLine("|    Welcome to the login page     |");
        Console.WriteLine("|                                  |");
        Console.WriteLine("====================================");
        Console.WriteLine();
        Console.WriteLine("Please enter your email address");
        string email = Console.ReadLine()!;
        Console.WriteLine("Please enter your password");
        string password = Console.ReadLine()!;
        Console.Clear();

        AccountModel aL = _accountsLogic.CheckLogin(type, email, password);
        _account = aL;
        if (aL != null)
        {
            Console.WriteLine("You have been logged in");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Console.Clear();
            MainMenu();
        }
        else
        {
            Console.WriteLine("Invalid credentials");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Console.Clear();
            PreLogin();
        }

    }
    public void MemberRegister()
    {
        Console.WriteLine("====================================");
        Console.WriteLine("|                                  |");
        Console.WriteLine("|   Welcome to the register page   |");
        Console.WriteLine("|                                  |");
        Console.WriteLine("====================================");
        Console.WriteLine();
        Console.WriteLine("Please enter your email address");
        string email = Console.ReadLine()!;
        Console.WriteLine("Please enter your password");
        string password = Console.ReadLine()!;
        Console.WriteLine("Please enter your fullname");
        string firstName = Console.ReadLine()!;
        Console.WriteLine("Please enter your phone number");
        string phoneNumber = Console.ReadLine()!;
        Console.Clear();

        int id = 0;
        while (true)
        {
            AccountModel account_T = _accountsLogic.GetById(id);
            if (account_T is AccountModel)
            {
                id += 1;
            }
            else
            {
                break;
            }
        }
        AccountModel account = new AccountModel(id, 1, email, password, firstName);
        _accountsLogic.UpdateList(account);
        PreLogin();
    }

    // Guest screen
    public void Guest()
    {
        Console.WriteLine("====================================");
        Console.WriteLine("|                                  |");
        Console.WriteLine("|      Continueing as a guest      |");
        Console.WriteLine("|                                  |");
        Console.WriteLine("====================================");
        Console.WriteLine();
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
        Console.Clear();
        MainMenu();
    }

    // Login as employee or manager
    public void AdvancedAccess()
    {
        Console.WriteLine("====================================");
        Console.WriteLine("|                                  |");
        Console.WriteLine("|         Advanced access          |");
        Console.WriteLine("|                                  |");
        Console.WriteLine("====================================");
        Console.WriteLine();
        Console.WriteLine("Please select an option:");
        Console.WriteLine("1. Login in as employee");
        Console.WriteLine("2. Login in as manager");
        Console.WriteLine();
        Console.Write("Enter your choice: ");
        string input = Console.ReadLine()!;
        Console.Clear();
        if (input == "1")
        {
            Login(2);
        }
        else if (input == "2")
        {
            Login(3);
        }
        else
        {
            Console.WriteLine("Invalid input");
            AdvancedAccess();
        }
    }

    // Cinema information
    public void CinemaInfo()
    {
        Console.WriteLine("============================================");
        Console.WriteLine("|                                          |");
        Console.WriteLine("|             Cinema information           |");
        Console.WriteLine("|                                          |");
        Console.WriteLine("============================================");
        Console.WriteLine("|                                          |");
        Console.WriteLine("| Phone number:  +31-655-574-244.          |");
        Console.WriteLine("| Location:      Wijnhaven 107,            |");
        Console.WriteLine("|                3011 WN  in Rotterdam     |");
        Console.WriteLine("| Email:         StartLightCinema@STC.com  |");
        Console.WriteLine("|                                          |");
        Console.WriteLine("============================================");
        Console.WriteLine();
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
        Console.Clear();
    }

    // Main menu
    public void MainMenu()
    {
        Console.WriteLine("====================================");
        Console.WriteLine("|                                  |");
        Console.WriteLine("|            Main menu             |");
        Console.WriteLine("|                                  |");
        Console.WriteLine("====================================");
        Console.WriteLine();
        Console.WriteLine("Please select an option:");
        Console.WriteLine("1. Movie menu");
        Console.WriteLine("2. Catering menu");
        Console.WriteLine("3. Account menu");
        Console.WriteLine("4. Cinema information");
        Console.WriteLine("5. Exit program");
        Console.WriteLine();
        Console.Write("Enter your choice: ");
        string input = Console.ReadLine()!;
        Console.Clear();
        if (input == "1")
        {
            MovieMenu();
        }
        else if (input == "2")
        {
            // CateringMenu();
            throw new Exception("Catering menu not implemented yet");
        }
        else if (input == "3")
        {
            // AccountMenu();
            throw new Exception("Account menu not implemented yet");
        }
        else if (input == "4")
        {
            CinemaInfo();
            MainMenu();
        }
        else if (input == "5")
        {
            Environment.Exit(0);
        }
        else
        {
            Console.WriteLine("Invalid input");
            MainMenu();
        }
    }

    // Movie Menu
    public void MovieMenu()
    {
        Console.WriteLine("====================================");
        Console.WriteLine("|                                  |");
        Console.WriteLine("|            Movie menu            |");
        Console.WriteLine("|                                  |");
        Console.WriteLine("====================================");
        Console.WriteLine();
        Console.WriteLine("Please select an option:");
        Console.WriteLine("1. View all movies menu");
        Console.WriteLine("2. Search for a movie");
        if (_account != null)
        {
            if (_account.Type == 3)
            {
                Console.WriteLine("3. Add a movie");
                Console.WriteLine("4. Delete a movie");
                Console.WriteLine("5. Go back to main menu");
            }
            else
            {
                Console.WriteLine("3. Go back to main menu");
            }
        }
        else
        {
            Console.WriteLine("3. Go back to main menu");
        }
        Console.WriteLine();
        Console.Write("Enter your choice: ");
        string input = Console.ReadLine()!;
        Console.Clear();
        if (input == "1")
        {
            ViewAllMovies();
        }
        else if (input == "2")
        {
            SearchMovie();
        }
        else if (_account != null)
        {
            if (_account.Type == 3)
            {
                if (input == "3")
                {
                    AddMovie();
                }
                else if (input == "4")
                {
                    DeleteMovie();
                }
                else if (input == "5")
                {
                    MainMenu();
                }
                else
                {
                    Console.WriteLine("Invalid input");
                    MovieMenu();
                }
            }
            else
            {
                if (input == "3")
                {
                    MainMenu();
                }
                else
                {
                    Console.WriteLine("Invalid input");
                    MovieMenu();
                }
            }
        }
        else
        {
            if (input == "3")
            {
                MainMenu();
            }
            else
            {
                Console.WriteLine("Invalid input");
                MovieMenu();
            }
        }
    }
    public void ViewAllMovies()
    {
        Console.WriteLine("====================================");
        Console.WriteLine("|                                  |");
        Console.WriteLine("|          View all movies         |");
        Console.WriteLine("|                                  |");
        Console.WriteLine("====================================");
        Console.WriteLine();
        Console.WriteLine("Please select an option:");
        Console.WriteLine("1. View all movies");
        Console.WriteLine("2. View all movies sorted by title");
        Console.WriteLine("3. Order ticket");
        Console.WriteLine("4. Go back to movie menu");
        Console.WriteLine();
        Console.Write("Enter your choice: ");
        string input = Console.ReadLine()!;
        Console.Clear();
        if (input == "1")
        {
            FilmsLogic.MovieOverview();
            Console.WriteLine();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Console.Clear();
            ViewAllMovies();
        }
        else if (input == "2")
        {
            FilmsLogic.MovieSortedByABCTitle();
            Console.WriteLine();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Console.Clear();
            ViewAllMovies();
        }
        else if (input == "3")
        {
            throw new Exception("Order ticket not implemented yet");
        }
        else if (input == "4")
        {
            MovieMenu();
        }
        else
        {
            Console.WriteLine("Invalid input");
            ViewAllMovies();
        }
    }
    public void SearchMovie()
    {
        Console.WriteLine("====================================");
        Console.WriteLine("|                                  |");
        Console.WriteLine("|        Search for a movie        |");
        Console.WriteLine("|                                  |");
        Console.WriteLine("====================================");
        Console.WriteLine();
        Console.WriteLine("Please select an option:");
        Console.WriteLine("1. Search by title");
        Console.WriteLine("2. Search by genre");
        Console.WriteLine("3. Go back to movie menu");
        Console.WriteLine();
        Console.Write("Enter your choice: ");
        string input = Console.ReadLine()!;
        Console.Clear();
        if (input == "1")
        {
            Console.WriteLine("====================================");
            Console.WriteLine("|                                  |");
            Console.WriteLine("|          Search by title         |");
            Console.WriteLine("|                                  |");
            Console.WriteLine("====================================");
            Console.WriteLine();
            Console.Write("Enter the title of the movie: ");
            string title = Console.ReadLine()!;
            Console.Clear();
            FilmsLogic.SearchByTitle(title);
            Console.WriteLine();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Console.Clear();
            SearchMovie();
        }
        else if (input == "2")
        {
            Console.WriteLine("====================================");
            Console.WriteLine("|                                  |");
            Console.WriteLine("|         Search by genre          |");
            Console.WriteLine("|                                  |");
            Console.WriteLine("====================================");
            Console.WriteLine();
            Console.Write("Enter the genre of the movie: ");
            string genre = Console.ReadLine()!;
            Console.Clear();
            FilmsLogic.SearchByGenre(genre);
            Console.WriteLine();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Console.Clear();
            SearchMovie();
        }
        else if (input == "3")
        {
            MovieMenu();
        }
        else
        {
            Console.WriteLine("Invalid input");
            SearchMovie();
        }
    }
    public void AddMovie()
    {
        Console.WriteLine("====================================");
        Console.WriteLine("|                                  |");
        Console.WriteLine("|            Add a movie           |");
        Console.WriteLine("|                                  |");
        Console.WriteLine("====================================");
        Console.WriteLine();
        Console.Write("Enter the title of the movie: ");
        string title = Console.ReadLine()!;
        Console.Write("Enter the description of the movie: ");
        string description = Console.ReadLine()!;
        Console.Write("Enter the duration of the movie: ");
        int duration = Convert.ToInt32(Console.ReadLine()!);
        Console.Write("Enter the genre genre of the movie: ");
        List<string> genres = new List<string>();
        string genre = Console.ReadLine()!;
        genres.Add(genre);
        Console.Clear();

        int id = 0;
        while (true)
        {
            FilmModel movie = _filmsLogic.GetById(id);
            if (movie is FilmModel)
            {
                id += 1;
            }
            else
            {
                break;
            }
        }
        FilmModel film = new FilmModel(id, title, description, duration, genres);
        _filmsLogic.UpdateList(film);
        MovieMenu();
    }
    public void DeleteMovie()
    {
        Console.WriteLine("====================================");
        Console.WriteLine("|                                  |");
        Console.WriteLine("|           Delete a movie         |");
        Console.WriteLine("|                                  |");
        Console.WriteLine("====================================");
        Console.WriteLine();
        Console.Write("Enter the title of the movie: ");
        string title = Console.ReadLine()!;
        Console.Clear();
        bool check = _filmsLogic.DeleteFilm(title);
        if (check == true)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("====================================");
            Console.WriteLine("|                                  |");
            Console.WriteLine("|           Movie deleted          |");
            Console.WriteLine("|                                  |");
            Console.WriteLine("====================================");
            Console.ResetColor();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("====================================");
            Console.WriteLine("|                                  |");
            Console.WriteLine("|           Movie not found        |");
            Console.WriteLine("|                                  |");
            Console.WriteLine("====================================");
            Console.ResetColor();
        }
        Console.WriteLine();
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
        Console.Clear();
        MovieMenu();
    }
}