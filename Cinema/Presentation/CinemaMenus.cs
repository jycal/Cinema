using System.Security;

public static class CinemaMenus
{
    public static AccountsLogic _accountsLogic = new AccountsLogic();
    public static AccountModel _account = null!;

    // instantiated when needed
    public static GuestLogic _guestLogic = new GuestLogic();
    public static FilmsLogic _filmsLogic = null!;
    public static FoodsLogic _foodsLogic = null!;
    public static ReservationsLogic _reservationsLogic = null!;
    public static RevenueLogic _revenueLogic = null!;
    public static TicketLogic _ticketLogic = null!;

    public static void Start()
    {
        RunMainMenu();
    }

    private static void RunMainMenu()
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
                RunMainMenu();
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

    private static void ExitCinema()
    {
        Console.WriteLine("Thank you for visiting Starlight Cinema. We hope to see you again soon!");
        Console.WriteLine("Press any key to exit...");
        Console.ReadKey(true);
        Console.Clear();
        Environment.Exit(0);
    }

    private static void DisplayContactInfo()
    {
        Console.WriteLine(@"============================================
|                                              |
|            Cinema information                |
|                                              |
================================================
|                                              |
| Phone number:  +31-655-574-244.              |
| Location:      Wijnhaven 107,                |
|                3011 WN  in Rotterdam         |
| Email:         CinemaStarlightinfo@gmail.com |
|                                              |
================================================
");
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey(true);
        Console.Clear();
    }

    private static void Login()
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
                    RunMenusMenu();
                    break;
                }
            }
            else
            {
                tries--;
                Console.WriteLine("Wrong password or email! Please try again...");
            }
        }
        Console.WriteLine("0 tries left. Please try again later...");
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey(true);
        Console.Clear();
    }

    private static void Register()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(@"============================================
|                                          |
|                Register                  |
|                                          |
============================================
");
        Console.ResetColor();
        Console.WriteLine("Email address:");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("-----------------------------");
        Console.WriteLine("|  Email must contain a @   |");
        Console.WriteLine("-----------------------------");
        Console.ResetColor();
        string email = Console.ReadLine()!;
        int EmailAttempts = 0;
        while (_accountsLogic.EmailFormatCheck(email) == false && EmailAttempts < 3)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Wrong format! Please enter Email in the correct format...");
            Console.ResetColor();
            Console.Write("Email address: ");
            string Email = Console.ReadLine()!;
            email = Email;
            EmailAttempts += 1;
        }
        if (EmailAttempts > 3)
        {
            Console.Clear();
        }
        Console.WriteLine("Please enter your password:");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("------------------------------------------------------------------------------------------");
        Console.WriteLine(@"|   Must contain at least one special character(%!@#$%^&*()?/>.<,:;'\|}]{[_~`+=-         |");
        Console.WriteLine("|   Must be longer than 6 characters                                                     |");
        Console.WriteLine("|   Must contain at least one number                                                     |");
        Console.WriteLine("|   One upper case                                                                       |");
        Console.WriteLine("|   Atleast one lower case                                                               |");
        Console.WriteLine("------------------------------------------------------------------------------------------");
        Console.ResetColor();
        SecureString pass = _accountsLogic.HashedPass();
        string password = new System.Net.NetworkCredential(string.Empty, pass).Password;
        int PasswordAttempts = 0;
        while (_accountsLogic.PasswordFormatCheck(password) == false && PasswordAttempts < 3)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Wrong format! Please enter Password in the correct format...");
            Console.ResetColor();
            SecureString passs = _accountsLogic.HashedPass();
            string Password = new System.Net.NetworkCredential(string.Empty, pass).Password;
            password = Password;
            PasswordAttempts += 1;
        }
        if (PasswordAttempts > 3)
        {
            Console.Clear();
        }
        Console.WriteLine();
        Console.WriteLine("Please enter your fullname");
        string firstName = Console.ReadLine()!;
        Console.WriteLine("Please enter your phone number");
        string phoneNumber = Console.ReadLine()!;
        Console.ForegroundColor = ConsoleColor.Yellow;
        // ✰⍟✰
        Console.WriteLine($"Congratulations!!\nYour account has been made!\nEnjoy your time at Starlight Cinema");
        Console.ResetColor();
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
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey(true);
    }

    private static void RunMenusMenu()
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

    private static void RunMovieMenu()
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
View movies and order.
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

    private static void ShowMovies(int type)
    {
        switch (type)
        {
            case 0:
                _filmsLogic.MovieOverview();
                OrderSeatConfirm();
                break;
            case 1:
                _filmsLogic.MovieSortedByABCTitle();
                OrderSeatConfirm();
                break;
            case 2:
                Console.Write("Please enter the title: ");
                string title = Console.ReadLine()!;
                _filmsLogic.SearchByTitle(title);
                OrderSeatConfirm();
                break;
            case 3:
                Console.Write("Please enter the genre: ");
                string genre = Console.ReadLine()!;
                _filmsLogic.SearchByGenre(genre);
                OrderSeatConfirm();
                break;
        }
    }

    private static void OrderSeatConfirm()
    {
        Console.Write("\nDo you want to order seats? (Y/N): ");
        string answer = Console.ReadLine()!.ToUpper();
        if (answer == "Y")
        {
            EnterRoom.Start(_account);
        }
        else if (answer == "N")
        {
            RunMovieMenu();
        }
        else
        {
            Console.WriteLine("Invalid input!");
            OrderSeatConfirm();
        }
    }

    private static void RunCateringMenu()
    {
        if (_foodsLogic == null)
        {
            _foodsLogic = new FoodsLogic();
        }

        if (_revenueLogic == null)
        {
            _revenueLogic = new RevenueLogic();
        }

        string prompt = @"============================================
|                                          |
|              Catering Menu               |
|                                          |
============================================
View menu and order.
";
        string[] options = { "Show current menu", "Search product by name", "Go back" };
        Menu cateringMenu = new Menu(prompt, options);
        int selectedIndex = cateringMenu.Run();

        switch (selectedIndex)
        {
            case 0:
                _foodsLogic.DisplayFoodMenu();
                OrderFoodConfirm();
                RunCateringMenu();
                break;
            case 1:
                SearchProduct();
                OrderFoodConfirm();
                RunCateringMenu();
                break;
            case 2:
                RunMenusMenu();
                break;
        }
    }

    private static void SearchProduct()
    {
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
    }

    private static void OrderFoodConfirm()
    {
        Console.Write("\nDo you want to order food? (Y/N): ");
        string answer = Console.ReadLine()!.ToUpper();
        if (answer == "Y")
        {
            OrderFood();
        }
        else if (answer == "N")
        {
            RunCateringMenu();
        }
        else
        {
            Console.WriteLine("Invalid input!");
            OrderFoodConfirm();
        }
    }

    private static void OrderFood()
    {
        Console.Write("Enter name of item: ");
        string name = Console.ReadLine()!;
        FoodModel food = _foodsLogic.GetByName(name);
        if (food is FoodModel)
        {
            int temp_id = _revenueLogic._revenueList!.Count > 0 ? _revenueLogic._revenueList.Max(x => x.Id) + 1 : 1;
            RevenueModel revenue = new RevenueModel(temp_id, Convert.ToDecimal(food.Cost));
            _revenueLogic.UpdateList(revenue);

            string code = EnterRoom.ReservationCodeMaker();
            Console.WriteLine($"Your reservation code is: {code}");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey(true);
        }
        else
        {
            Console.WriteLine("No food with that name was found!");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey(true);
        }
    }

    private static void RunTicketMenu()
    {
        if (_reservationsLogic == null)
        {
            _reservationsLogic = new ReservationsLogic();
        }

        if (_guestLogic == null)
        {
            _guestLogic = new GuestLogic();
        }

        if (_revenueLogic == null)
        {
            _revenueLogic = new RevenueLogic();
        }

        string prompt = @"============================================
|                                          |
|              Ticket Menu                 |
|                                          |
============================================
";
        string[] options = { "Cancel ticket", "Go back" };
        Menu ticketMenu = new Menu(prompt, options);
        int selectedIndex = ticketMenu.Run();

        switch (selectedIndex)
        {
            case 0:
                CancelTicket();
                RunTicketMenu();
                break;
            case 1:
                RunMenusMenu();
                break;
        }
    }

    private static void CancelTicket()
    {
        Console.WriteLine("What is your reservation code?");
        string? reservationCode = Console.ReadLine();
        // var guestCheck = _guestLogic.GetByCode(reservationCode!);
        var accCheck = _reservationsLogic.GetByCode(reservationCode!);
        if (accCheck != null)
        {
            _reservationsLogic.DeleteReservationByCode(reservationCode!);

        }
        else if (accCheck == null)
        {
            _guestLogic.DeleteReservation(reservationCode!);
            // Console.Clear();

        }
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
        Console.Clear();
    }

    private static void RunAdvancedMenu()
    {
        string prompt = @"============================================
|                                          |
|             Advanced Menu                |
|                                          |
============================================
";
        string[] options = { "Advanced Movie Menu", "Advanced Seat Menu", "Advanced Food Menu", "Advanced Reservation Menu", "Advanced Revenue Menu", "Go back" };
        Menu advancedMenu = new Menu(prompt, options);
        int selectedIndex = advancedMenu.Run();

        switch (selectedIndex)
        {
            case 0:
                RunAdvancedMovieMenu();
                break;
            case 1:
                RunAdvancedSeatMenu();
                break;
            case 2:
                RunAdvancedFoodMenu();
                break;
            case 3:
                RunAdvancedReservationMenu();
                break;
            case 4:
                RunAdvancedRevenueMenu();
                break;
            case 5:
                RunMenusMenu();
                break;
        }
    }

    private static void RunAdvancedMovieMenu()
    {
        if (_filmsLogic == null)
        {
            _filmsLogic = new FilmsLogic();
        }

        string prompt = @"============================================
|                                          |
|           Advanced Movie Menu            |
|                                          |
============================================
";
        string[] options = { "View all movies", "Add a movie", "Delete a movie", "Go back" };
        Menu advancedMovieMenu = new Menu(prompt, options);
        int selectedIndex = advancedMovieMenu.Run();

        switch (selectedIndex)
        {
            case 0:
                _filmsLogic.MovieOverview();
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey(true);
                RunAdvancedMovieMenu();
                break;
            case 1:
                AddMovie();
                RunAdvancedMovieMenu();
                break;
            case 2:
                _filmsLogic.MovieOverview();
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey(true);
                DeleteMovie();
                RunAdvancedMovieMenu();
                break;
            case 3:
                RunAdvancedMenu();
                break;
        }
    }

    private static void AddMovie()
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

    private static void DeleteMovie()
    {
        Console.Write("Enter the id of the movie: ");
        string input = Console.ReadLine()!;
        if (string.IsNullOrEmpty(input))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Movie not found");
            Console.ResetColor();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey(true);
            return;
        }
        int id = Convert.ToInt32(input);
        bool check = _filmsLogic.DeleteFilm(id);
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

    private static void RunAdvancedSeatMenu()
    {
        if (_ticketLogic == null)
        {
            _ticketLogic = new TicketLogic();
        }

        string prompt = @"============================================
|                                          |
|            Advanced Seat Menu            |
|                                          |
============================================
";
        string[] options = { "View all seats", "Change a seat price", "Go back" };
        Menu advancedSeatMenu = new Menu(prompt, options);
        int selectedIndex = advancedSeatMenu.Run();

        switch (selectedIndex)
        {
            case 0:
                ShowSeatPrices();
                RunAdvancedSeatMenu();
                break;
            case 1:
                ShowSeatPrices();
                SetSeatPrice();
                RunAdvancedSeatMenu();
                break;
            case 2:
                RunAdvancedMenu();
                break;
        }
    }

    private static void ShowSeatPrices()
    {
        _ticketLogic.DisplayAll();
        Console.WriteLine();
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey(true);
    }

    private static void SetSeatPrice()
    {
        Console.WriteLine();
        Console.Write("Enter the name of the seat: ");
        string name = Console.ReadLine()!;
        Console.Write("Enter the new price of the seat: ");
        string input = Console.ReadLine()!;
        if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(input))
        {
            Console.WriteLine("Name or price cannot be empty!");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey(true);
            RunAdvancedMenu();
        }
        double price = Convert.ToDouble(input);
        TicketModel ticket = _ticketLogic.GetByName(name);
        if (ticket is TicketModel)
        {
            ticket.Cost = price;
            _ticketLogic.UpdateList(ticket);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Seat price changed!");
            Console.ResetColor();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey(true);
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("No seat with that name was found!");
            Console.ResetColor();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey(true);
        }
    }

    private static void RunAdvancedFoodMenu()
    {
        if (_foodsLogic == null)
        {
            _foodsLogic = new FoodsLogic();
        }

        string prompt = @"============================================
|                                          |
|            Advanced Food Menu            |
|                                          |
============================================
";
        string[] options = { "View all food", "Change a food price", "Go back" };
        Menu advancedFoodMenu = new Menu(prompt, options);
        int selectedIndex = advancedFoodMenu.Run();

        switch (selectedIndex)
        {
            case 0:
                _foodsLogic.DisplayFoodMenu();
                RunAdvancedFoodMenu();
                break;
            case 1:
                _foodsLogic.DisplayFoodMenu();
                SetFoodPrice();
                RunAdvancedFoodMenu();
                break;
            case 2:
                RunAdvancedMenu();
                break;
        }
    }

    private static void SetFoodPrice()
    {
        _foodsLogic.ChangePrice();
    }

    private static void RunAdvancedReservationMenu()
    {
        if (_reservationsLogic == null)
        {
            _reservationsLogic = new ReservationsLogic();
        }

        string prompt = @"============================================
|                                          |
|         Advanced Reservation Menu        |
|                                          |
============================================
";
        string[] options = { "View all reservations", "Search a reservation", "Delete a reservation", "Go back" };
        Menu advancedReservationMenu = new Menu(prompt, options);
        int selectedIndex = advancedReservationMenu.Run();

        switch (selectedIndex)
        {
            case 0:
                ShowReservations();
                RunAdvancedReservationMenu();
                break;
            case 1:
                SearchReservation();
                RunAdvancedReservationMenu();
                break;
            case 2:
                DeleteReservation();
                RunAdvancedReservationMenu();
                break;
            case 3:
                RunAdvancedMenu();
                break;
        }
    }

    private static void ShowReservations()
    {
        ReservationsLogic.ReservationOverview();
        Console.WriteLine();
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey(true);
    }

    private static void SearchReservation()
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

    private static void DeleteReservation()
    {
        Console.Write("Enter id: ");
        string input = Console.ReadLine()!;
        if (string.IsNullOrEmpty(input))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Reservation not found");
            Console.ResetColor();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey(true);
            return;
        }
        int id = Convert.ToInt32(input);
        var reservation = _reservationsLogic.GetById(id);
        if (reservation != null)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            _reservationsLogic.DeleteReservation(id);
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

    private static void RunAdvancedRevenueMenu()
    {
        if (_revenueLogic == null)
        {
            _revenueLogic = new RevenueLogic();
        }

        string prompt = @"============================================
|                                          |
|                 Revenue                  |
|                                          |
============================================
";
        string[] options = { "View revenue", "Go back" };
        Menu advancedReservationMenu = new Menu(prompt, options);
        int selectedIndex = advancedReservationMenu.Run();

        switch (selectedIndex)
        {
            case 0:
                ShowRevenue();
                RunAdvancedRevenueMenu();
                break;
            case 1:
                RunAdvancedMenu();
                break;
        }
    }

    private static void ShowRevenue()
    {
        decimal revenue = _revenueLogic.TotalRevenue();
        Console.WriteLine($"Total revenue: {revenue}");
        Console.WriteLine();
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey(true);
    }

}