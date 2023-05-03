public class CinemaMenus
{
    private AccountsLogic _accountsLogic = new AccountsLogic();
    private AccountModel _account = null!;

    // instantiated when needed
    private FilmsLogic _filmsLogic = null!;
    private FoodsLogic _foodsLogic = null!;
    private ReservationsLogic _reservationsLogic = null!;

    public void Start()
    {
        RunMainMenu();
    }

    private void RunMainMenu()
    {
        // if logged in logout
        _account = null!;

        string prompt = @" ____  ____  __   ____  __    __  ___  _  _  ____     ___  __  __ _  ____  _  _   __  
/ ___)(_  _)/ _\ (  _ \(  )  (  )/ __)/ )( \(_  _)   / __)(  )(  ( \(  __)( \/ ) / _\ 
\___ \  )( /    \ )   // (_/\ )(( (_ \) __ (  )(    ( (__  )( /    / ) _) / \/ \/    \
(____/ (__)\_/\_/(__\_)\____/(__)\___/\_)(_/ (__)    \___)(__)\_)__)(____)\_)(_/\_/\_/

Welcome to Starlight Cinema. What would you like to do?
(Use arrow keys to cycle through options and press enter to select an option.)
";
        string[] options = { "Login", "Register", "Continue as Guest", "Contact", "Exit" };
        Menu mainMenu = new Menu(prompt, options);
        int selectedIndex = mainMenu.Run();

        switch (selectedIndex)
        {
            case 0:
                Login();
                RunMenusMenu();
                break;
            case 1:
                Register();
                RunMainMenu();
                break;
            case 2:
                RunMenusMenu();
                break;
            case 3:
                DisplayContactInfo();
                RunMainMenu();
                break;
            case 4:
                ExitCinema();
                break;
        }
    }

    private void ExitCinema()
    {
        Console.WriteLine("Thank you for visiting Starlight Cinema. We hope to see you again soon!");
        Console.WriteLine("Press any key to exit...");
        Console.ReadKey(true);
        Console.Clear();
        Environment.Exit(0);
    }

    private void DisplayContactInfo()
    {
        Console.WriteLine(@"============================================
|                                          |
|            Cinema information            |
|                                          |
============================================
|                                          |
| Phone number:  +31-655-574-244.          |
| Location:      Wijnhaven 107,            |
|                3011 WN  in Rotterdam     |
| Email:         StartLightCinema@STC.com  |
|                                          |
============================================
");
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey(true);
        Console.Clear();
    }

    private void Login()
    {
        Console.WriteLine(@"============================================
|                                          |
|                 Login                    |
|                                          |
============================================
");
        int tries = 3;
        while (tries > 0)
        {
            Console.Write("Please enter your email address: ");
            string email = Console.ReadLine()!;
            Console.Write("Please enter your password: ");
            string password = Console.ReadLine()!;
            AccountModel correctEmail = _accountsLogic.GetByMail(email);

            if (correctEmail != null)
            {
                if (correctEmail.Password != password)
                {
                    tries--;
                    Console.WriteLine("Wrong password or email! Please try again...");
                }
                else
                {
                    _account = correctEmail;
                    Console.WriteLine("Welcome back " + _account.FullName);
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey(true);
                    Console.Clear();
                    break;
                }
            }
            else
            {
                tries--;
            }
            Console.WriteLine("0 tries left. Please try again later...");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey(true);
            Console.Clear();
        }
    }

    private void Register()
    {
        Console.WriteLine(@"============================================
|                                          |
|                Register                  |
|                                          |
============================================
");
        Console.Write("Please enter your email address: ");
        string email = Console.ReadLine()!;
        Console.Write("Please enter your password: ");
        string password = Console.ReadLine()!;
        Console.Write("Please enter your fullname: ");
        string firstName = Console.ReadLine()!;
        Console.Write("Please enter your phone number: ");
        string phoneNumber = Console.ReadLine()!;

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
        Console.WriteLine();
        Console.WriteLine("You have been registered!");
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey(true);
        Console.Clear();
    }

    private void RunMenusMenu()
    {
        string prompt = @"============================================
|                                          |
|                 Menus                    |
|                                          |
============================================
";
        string[] options = { "Movie Menu", "Catering Menu", "Ticket Menu", "Advanced Menu", "Contact", "Logout", "Exit" };
        List<string> tempOptions = new List<string>(options);
        if (_account == null)
        {
            tempOptions.Remove("Advanced Menu");
        }
        else
        {
            if (_account.Type == 1)
            {
                tempOptions.Remove("Advanced Menu");
            }
        }
        options = tempOptions.ToArray();

        Menu menusMenu = new Menu(prompt, options);
        int selectedIndex = menusMenu.Run();

        bool isGuestMenu = false;

        if (_account == null)
        {
            isGuestMenu = true;
        }
        else
        {
            if (_account.Type == 1)
            {
                isGuestMenu = true;
            }
        }

        if (isGuestMenu)
        {
            switch (selectedIndex)
            {
                case 0:
                    RunMovieMenu();
                    RunMenusMenu();
                    break;
                case 1:
                    RunCateringMenu();
                    break;
                case 2:
                    RunTicketMenu();
                    break;
                case 3:
                    DisplayContactInfo();
                    RunMenusMenu();
                    break;
                case 4:
                    if (_account == null)
                    {
                        Console.WriteLine("You are not logged in!");
                        Console.WriteLine("Returning to starting menu...");
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey(true);
                    }
                    RunMainMenu();
                    break;
                case 5:
                    ExitCinema();
                    break;
            }
        }
        else
        {
            switch (selectedIndex)
            {
                case 0:
                    RunMovieMenu();
                    break;
                case 1:
                    RunCateringMenu();
                    break;
                case 2:
                    RunTicketMenu();
                    break;
                case 3:
                    RunAdvancedMenu();
                    break;
                case 4:
                    DisplayContactInfo();
                    RunMainMenu();
                    break;
                case 5:
                    if (_account == null)
                    {
                        Console.WriteLine("You are not logged in!");
                        Console.WriteLine("Returning to starting menu...");
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey(true);
                    }
                    RunMainMenu();
                    break;
                case 6:
                    ExitCinema();
                    break;
            }
        }
    }

    private void RunMovieMenu()
    {
        if (_filmsLogic == null)
        {
            _filmsLogic = new FilmsLogic();
        }

        string prompt = @"============================================
|                                          |
|               Movie Menu                 |
|                                          |
============================================
View movies and order tickets.
";
        string[] options = { "Show all movies", "Show movies sorted by title", "Search movies by title", "Search movies by genre", "Go back" };
        Menu movieMenu = new Menu(prompt, options);
        int selectedIndex = movieMenu.Run();

        switch (selectedIndex)
        {
            case 0:
                ShowMovies(0);
                RunMovieMenu();
                break;
            case 1:
                ShowMovies(1);
                RunMovieMenu();
                break;
            case 2:
                ShowMovies(2);
                RunMovieMenu();
                break;
            case 3:
                ShowMovies(3);
                RunMovieMenu();
                break;
            case 4:
                RunMenusMenu();
                break;
        }
    }

    private void ShowMovies(int type)
    {
        switch (type)
        {
            case 0:
                _filmsLogic.MovieOverview();
                OrderSeats();
                break;
            case 1:
                _filmsLogic.MovieSortedByABCTitle();
                OrderSeats();
                break;
            case 2:
                Console.Write("Please enter the title: ");
                string title = Console.ReadLine()!;
                _filmsLogic.SearchByTitle(title);
                OrderSeats();
                break;
            case 3:
                Console.Write("Please enter the genre: ");
                string genre = Console.ReadLine()!;
                _filmsLogic.SearchByGenre(genre);
                OrderSeats();
                break;
        }
    }

    private void OrderSeats()
    {
        Console.Write("\nDo you want to order seats? (type yes to confirm): ");
        string answer = Console.ReadLine()!;
        if (answer == "yes")
        {
            EnterRoom.Start(_account);
        }
    }

    private void RunCateringMenu()
    {
        if (_foodsLogic == null)
        {
            _foodsLogic = new FoodsLogic();
        }

        string prompt = @"============================================
|                                          |
|              Catering Menu               |
|                                          |
============================================
";
        string[] options = { "Show current menu", "Search product by name", "Go back" };
        Menu cateringMenu = new Menu(prompt, options);
        int selectedIndex = cateringMenu.Run();

        switch (selectedIndex)
        {
            case 0:
                Catering.ShowInfo();
                RunCateringMenu();
                break;
            case 1:
                Console.Write("Please enter the name: ");
                string name = Console.ReadLine()!;
                FoodModel food = _foodsLogic.GetByName(name);
                if (food is FoodModel)
                {
                    Console.WriteLine($"{food.Name}: {food.Cost}");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey(true);
                }
                else
                {
                    Console.WriteLine("No food with that name was found!");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey(true);
                }
                RunCateringMenu();
                break;
            case 2:
                RunMenusMenu();
                break;
        }
    }

    private void RunTicketMenu()
    {
        // show not implemented
        string prompt = @"============================================
|                                          |
|              Ticket Menu                 |
|                                          |
============================================
Not implemented.
";
        string[] options = { "Go back" };
        Menu ticketMenu = new Menu(prompt, options);
        int selectedIndex = ticketMenu.Run();

        switch (selectedIndex)
        {
            case 0:
                RunMenusMenu();
                break;
        }
    }

    private void RunAdvancedMenu()
    {
        if (_filmsLogic == null)
        {
            _filmsLogic = new FilmsLogic();
        }

        if (_accountsLogic == null)
        {
            _accountsLogic = new AccountsLogic();
        }

        if (_reservationsLogic == null)
        {
            _reservationsLogic = new ReservationsLogic();
        }

        string prompt = @"============================================
|                                          |
|             Advanced Menu                |
|                                          |
============================================
";
        string[] options = { "View all movies", "Add a movie", "Delete a movie", "View all reservations", "Search a reservation", "Delete a reservation", "Go back" };
        Menu advancedMenu = new Menu(prompt, options);
        int selectedIndex = advancedMenu.Run();

        switch (selectedIndex)
        {
            case 0:
                _filmsLogic.MovieOverview();
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey(true);
                RunAdvancedMenu();
                break;
            case 1:
                AddMovie();
                RunAdvancedMenu();
                break;
            case 2:
                _filmsLogic.MovieOverview();
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey(true);
                DeleteMovie();
                RunAdvancedMenu();
                break;
            case 3:
                ShowReservations();
                RunAdvancedMenu();
                break;
            case 4:
                SearchReservation();
                RunAdvancedMenu();
                break;
            case 5:
                ShowReservations();
                DeleteReservation();
                RunAdvancedMenu();
                break;
            case 6:
                RunMenusMenu();
                break;
        }
    }

    private void AddMovie()
    {
        Console.Write("Enter the title of the movie: ");
        string title = Console.ReadLine()!;
        Console.Write("Enter the description of the movie: ");
        string description = Console.ReadLine()!;
        Console.Write("Enter the duration of the movie: ");
        int duration = Convert.ToInt32(Console.ReadLine()!);
        Console.Write("Enter the genre of the movie: ");
        List<string> genres = new List<string>();
        string genre = Console.ReadLine()!;
        genres.Add(genre);
        // image add
        Console.Write("Enter the image url: ");
        string imageURL = Console.ReadLine()!;
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
        FilmModel film = new FilmModel(id, title, description, duration, genres, imageURL);
        _filmsLogic.UpdateList(film);
        Console.WriteLine("Movie added!");
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey(true);
    }

    private void DeleteMovie()
    {
        Console.Write("Enter the title of the movie: ");
        string title = Console.ReadLine()!;
        bool check = _filmsLogic.DeleteFilm(title);
        if (check == true)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Movie deleted");
            Console.ResetColor();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Movie not found");
            Console.ResetColor();
        }
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey(true);
    }

    private void ShowReservations()
    {
        ReservationsLogic.ReservationOverview();
        Console.WriteLine();
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey(true);
    }

    private void SearchReservation()
    {
        Console.Write("Enter email adress: ");
        string email = Console.ReadLine()!;
        var reservation = _reservationsLogic.GetByEmail(email);
        if (reservation == null)
        {
            Console.WriteLine("Email not found!");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey(true);
            return;
        }

        Console.WriteLine(@"==================================================
|                                                 |
|                  Reservations                   |
|                                                 |
==================================================");

        foreach (var item in reservation!.Seats)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            int Seats = item;
            string Overview = $@"
  Movie: {reservation.Movie}
  Full Name: {reservation.FullName}
  Email: {reservation.Email}
  Ticket Amount: {reservation.TicketAmount}
  Seats: {Seats}
  Total Money Amount: {reservation.TotalAmount}

==================================================";
            Console.WriteLine(Overview);

        }
        Console.ResetColor();
        Console.WriteLine();
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey(true);
    }

    private void DeleteReservation()
    {
        Console.Write("Enter email adress: ");
        string email = Console.ReadLine()!;
        var reservation = _reservationsLogic.GetByEmail(email);
        if (reservation != null)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            _reservationsLogic.DeleteReservation(email);
            Console.WriteLine("Reservation deleted");
            Console.ResetColor();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Reservation not found");
            Console.ResetColor();
        }
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey(true);
    }
}