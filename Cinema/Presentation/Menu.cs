public static class Menu
{
    private static AccountsLogic _accountsLogic = new AccountsLogic();
    private static AccountModel _account = null!;
    private static FilmsLogic _filmsLogic = new FilmsLogic();

    // Intro screen
    public static void Start()
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
    public static void PreLogin()
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
        Console.WriteLine("5. Cinema information");
        Console.WriteLine("6. Exit program");
        Console.WriteLine();
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
    public static void Login(int type)
    {
        Console.WriteLine("====================================");
        Console.WriteLine("|                                  |");
        Console.WriteLine("|    Welcome to the login page     |");
        Console.WriteLine("|                                  |");
        Console.WriteLine("====================================");
        Console.WriteLine();
        bool loginSuccessful = false;
        while (!loginSuccessful)
        {
            Console.WriteLine("Please enter your email address");
            string email = Console.ReadLine()!;
            Console.WriteLine("Please enter your password");
            string password = Console.ReadLine()!;
            var correctEmail = _accountsLogic.GetByMail(email);
            if (correctEmail == null)
            {
                Console.WriteLine("Email not found! Please try again...");
                continue;
            }
            else if (correctEmail.Password != password)
            {
                Console.WriteLine("Wrong password! Please try again...");
                Console.Write("Password: ");
                string Password = Console.ReadLine()!;
                password = Password;
            }
            if (password == correctEmail.Password)
            {
                Console.WriteLine();
                Console.WriteLine("Right password!! You did it!");
                email = email;
                password = password;
            }
            AccountModel acc = _accountsLogic.CheckLogin(type, email, password);
            if (acc != null)
            {
                _account = acc;
                Console.WriteLine("\nWelcome back " + acc.FullName);
                Console.WriteLine("Your email is " + acc.EmailAddress);
                loginSuccessful = true;
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                Menu.MainMenu();
            }
            else
            {
                Console.WriteLine("No account found with that email and password");
                Console.WriteLine("Please try again.\n");
            }
        }
    }
    public static void MemberRegister()
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
        List<int> tickets = new List<int>();
        AccountModel account = new AccountModel(id, 1, email, password, firstName, tickets);
        _accountsLogic.UpdateList(account);
        PreLogin();
    }

    // Guest screen
    public static void Guest()
    {
        Console.WriteLine("====================================");
        Console.WriteLine("|                                  |");
        Console.WriteLine("|      Continuing as a guest       |");
        Console.WriteLine("|                                  |");
        Console.WriteLine("====================================");
        Console.WriteLine();
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
        Console.Clear();
        MainMenu();
    }

    // Login as employee or manager
    public static void AdvancedAccess()
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
    public static void CinemaInfo()
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
    public static void MainMenu()
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
        if (_account != null)
        {
            if (_account.Type == 3 || _account.Type == 2)
            {
                Console.WriteLine("3. Advanced Ticket menu");
                Console.WriteLine("4. Cinema information");
                Console.WriteLine("5. Exit program");
            }
            else
            {
                Console.WriteLine("3. Cinema information");
                Console.WriteLine("4. Exit program");
            }
        }
        else
        {
            Console.WriteLine("3. Cinema information");
            Console.WriteLine("4. Exit program");
        }
        Console.WriteLine();
        Console.Write("Enter your choice: ");
        string input = Console.ReadLine()!;
        Console.Clear();
        if (_account != null)
        {
            if (_account.Type == 3 || _account.Type == 2)
            {
                if (input == "1")
                {
                    MovieMenu();
                }
                else if (input == "2")
                {
                    Catering.ShowInfo();
                    MainMenu();
                }
                else if (input == "3")
                {
                    ReservationManagerMenu.Start();
                    MainMenu();
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
            else
            {
                if (input == "1")
                {
                    MovieMenu();
                }
                else if (input == "2")
                {
                    Catering.ShowInfo();
                    Console.WriteLine();
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    Console.Clear();
                    MainMenu();
                }
                else if (input == "3")
                {
                    CinemaInfo();
                    MainMenu();
                }
                else if (input == "4")
                {
                    Environment.Exit(0);
                }
                else
                {
                    Console.WriteLine("Invalid input");
                    MainMenu();
                }
            }
        }
        else
        {
            if (input == "1")
            {
                MovieMenu();
            }
            else if (input == "2")
            {
                Catering.ShowInfo();
                MainMenu();
            }
            else if (input == "3")
            {
                CinemaInfo();
                MainMenu();
            }
            else if (input == "4")
            {
                Environment.Exit(0);
            }
            else
            {
                Console.WriteLine("Invalid input");
                MainMenu();
            }
        }
    }

    // Movie Menu
    public static void MovieMenu()
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
    public static void ViewAllMovies()
    {
        GuestLogic logic = new GuestLogic();

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
        Console.WriteLine("4. Cancel reservation");
        Console.WriteLine("5. Go back to movie menu");
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
            EnterRoom.Start(_account);
            ViewAllMovies();
        }
        else if (input == "4")
        {
            Console.WriteLine("What is your email?");
            string? email = Console.ReadLine();
            logic.DeleteReservation(email);
            Console.WriteLine("Reservation has been cancelled.");
            MainMenu();
        }
        else if (input == "5")
        {
            MovieMenu();
        }
        else
        {
            Console.WriteLine("Invalid input");
            ViewAllMovies();
        }
    }
    public static void SearchMovie()
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
    public static void AddMovie()
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
    public static void DeleteMovie()
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