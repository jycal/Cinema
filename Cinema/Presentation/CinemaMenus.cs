using System.Globalization;
using System.Security;

public static class CinemaMenus
{
    public static AccountsLogic _accountsLogic = new AccountsLogic();
    public static AccountModel _account = null!;

    // instantiated when needed
    public static GuestLogic _guestLogic = new GuestLogic();
    public static RoomsLogic _roomsLogic = null!;
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
(____/ (__)\_/\_/(__\_)\____/(__)\___/\_)(_/ (__)    \___)(__)\_)__)(____)\_)(_/\_/\_/".BrightCyan() +

@"

Welcome to Starlight Cinema. What would you like to do?
(Use arrow keys to cycle through options and press enter to select an option.)
".BrightWhite();
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
        System.Console.WriteLine();
        Console.WriteLine("Thank you for visiting Starlight Cinema. We hope to see you again soon!".BrightCyan());
        System.Console.WriteLine();
        Console.WriteLine("Press any key to exit...");
        Console.ReadKey(true);
        Console.Clear();
        Environment.Exit(0);
    }

    private static void DisplayContactInfo()
    {
        System.Console.WriteLine();
        // Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(@"===============================================
|                                              |
|            ".BrightCyan() + @"Cinema Information".BrightWhite() + @"                |
|                                              |
================================================
|                                              |
| ".BrightCyan() + @"Phone number:  +31-655-574-244.".BrightWhite() + @"              |
| ".BrightCyan() + @"Location:      Wijnhaven 107,".BrightWhite() + @"                |
|                ".BrightCyan() + @"3011 WN  in Rotterdam".BrightWhite() + @"         |
| ".BrightCyan() + @"Email:         CinemaStarlightinfo@gmail.com".BrightWhite() + @" |
|                                              |
================================================
".BrightCyan());
        Console.ResetColor();
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey(true);
        Console.Clear();
    }

    private static void Login()
    {
        Console.WriteLine(@"============================================
|                                          |
|                 ".BrightCyan() + @"Login".BrightWhite() + @"                    |
|                                          |
============================================
".BrightCyan());
        int tries = 3;
        bool logIn = false;
        Console.Write("Enter email and password, you have 3 tries to get the right password".Orange());
        System.Console.WriteLine();
        while (tries > 0)
        {
            string email = "";
            while (true)
            {
                Console.Write("Please enter your email address: ");
                string mail = Console.ReadLine()!;
                if (string.IsNullOrEmpty(mail) || _accountsLogic.GetByMail(mail) == null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    System.Console.WriteLine($"\nWrong email adress. Please try again..\n");
                    Console.ResetColor();
                }
                else
                {
                    email = mail;
                    break;
                }
            }
            string password = string.Empty;
            SecureString pass = _accountsLogic.HashedPass();
            password = new System.Net.NetworkCredential(string.Empty, pass).Password;
            AccountModel correctEmail = _accountsLogic.GetByMail(email);

            if (correctEmail != null)
            {
                if (correctEmail.Password != password)
                {
                    tries--;
                    Console.ForegroundColor = ConsoleColor.Red;
                    System.Console.WriteLine();
                    Console.WriteLine($"\nWrong password or email! Please try again...\n");
                    Console.ResetColor();
                }
                else
                {
                    _account = correctEmail;
                    logIn = true;
                    Console.ForegroundColor = ConsoleColor.Green;
                    System.Console.WriteLine();
                    Console.WriteLine($"\n" + "Welcome back " + _account.FullName);
                    Console.ResetColor();
                    System.Console.WriteLine();
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey(true);
                    Console.Clear();
                    RunMenusMenu();
                    break;
                }
            }
            // else
            // {
            //     tries--;
            //     Console.ForegroundColor = ConsoleColor.Red;
            //     Console.WriteLine($"\nWrong password or email! Please try again...\n");
            //     Console.ResetColor();
            // }
        }
        if (!logIn)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("You have 0 tries left. Please try again later...");
            Console.ResetColor();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey(true);
            Console.Clear();
        }

    }

    private static void Register()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(@"============================================
|                                          |
|                ".BrightCyan() + @"Register".BrightWhite() + @"                  |
|                                          |
============================================
".BrightCyan());
        Console.ResetColor();
        Console.WriteLine("Email address:");

        Console.WriteLine("-----------------------------".BrightYellow());
        Console.WriteLine("|".BrightYellow() + "   Email must contain an @".BrightWhite() + "   |".BrightYellow());
        Console.WriteLine("-----------------------------".BrightYellow());
        // Console.ResetColor();
        string email = Console.ReadLine()!;
        // int EmailAttempts = 0;


        while (_accountsLogic.EmailFormatCheck(email) == false)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Wrong format! Please enter Email in the correct format...");
            Console.ResetColor();
            Console.Write("Email address: ");
            string Email = Console.ReadLine()!;
            if (_accountsLogic.GetByMail(email) != null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine("email already exists try again");
                Console.ResetColor();
                System.Console.WriteLine("Press any key to continue");
                Console.ReadKey(true);
                Register();
            }
            email = Email;
            // EmailAttempts += 1;
        }
        AccountModel retrievedAcc = _accountsLogic.GetByMail(email.ToLower());
        if (retrievedAcc != null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            System.Console.WriteLine("email already exists try again");
            Console.ResetColor();
            System.Console.WriteLine("Press any key to continue");
            Console.ReadKey(true);
            Console.Clear();
            Register();
        }
        // if (EmailAttempts > 3)
        // {
        //     Console.Clear();
        // }
        Console.WriteLine($"\nPlease enter your password" + $"\n" + $"You have 3 attempts".Orange());
        // Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("------------------------------------------------------------------------------------------".BrightYellow());
        Console.WriteLine(@"|".BrightYellow() + @"Must contain at least one special character(%!@#$%^&*()?/>.<,:;'\|}]{[_~`+=-        |".BrightYellow());
        Console.WriteLine("|".BrightYellow() + @"Must be longer than 6 characters                                                     |".BrightYellow());
        Console.WriteLine("|".BrightYellow() + @"Must contain at least one number                                                     |".BrightYellow());
        Console.WriteLine("|".BrightYellow() + @"One upper case character                                                             |".BrightYellow());
        Console.WriteLine("|".BrightYellow() + @"Atleast one lower case character                                                     |".BrightYellow());
        Console.WriteLine("------------------------------------------------------------------------------------------".BrightYellow());
        // Console.ResetColor();

        int passwordAttempts = 0;
        string password = string.Empty;

        while (passwordAttempts < 3)
        {
            SecureString pass = _accountsLogic.HashedPass();
            password = new System.Net.NetworkCredential(string.Empty, pass).Password;

            if (_accountsLogic.PasswordFormatCheck(password))
            {
                break;
            }

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\nWrong format! Please enter the password in the correct format. You will be send back to the menu if you don't get the format right!");
            Console.ResetColor();
            passwordAttempts++;
        }

        if (passwordAttempts >= 3)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("No attempts left.");
            Console.ResetColor();
            Console.WriteLine("You will be send back to the menu.");
            System.Console.WriteLine("Press any key to continue");
            Console.ReadKey(true);
            Console.Clear();
            RunMainMenu();
        }

        Console.WriteLine();

        Console.WriteLine("Please enter your full name");
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
        AccountModel account = new AccountModel(id, 1, email.ToLower(), password, firstName, tickets);
        _accountsLogic.UpdateList(account);
        MailConformation mail = new(email);
        mail.SendRegistrationConformation();
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey(true);
    }

    public static void RunMenusMenu()
    {
        string prompt = @"============================================
|                                          |
|                 ".BrightCyan() + @"Menus".BrightWhite() + @"                    |
|                                          |
============================================
".BrightCyan();
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
                    RunMenusMenu();
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
|               ".BrightCyan() + @"Movie Menu".BrightWhite() + @"                 |
|                                          |
============================================
".BrightCyan();
        string[] options = { "Order Ticket", "Show all movies", "Show movies sorted by title", "Search movies by title", "Search movies by genre", "Go back" };
        Menu movieMenu = new Menu(prompt, options);
        int selectedIndex = movieMenu.Run();

        switch (selectedIndex)
        {
            case 0:
                OrderSeatConfirm();
                break;
            case 1:
                ShowMovies(0);
                RunMovieMenu();
                break;
            case 2:
                ShowMovies(1);
                RunMovieMenu();
                break;
            case 3:
                ShowMovies(2);
                RunMovieMenu();
                break;
            case 4:
                ShowMovies(3);
                RunMovieMenu();
                break;
            case 5:
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
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey(true);
                OrderSeatConfirm();
                break;
            case 1:
                _filmsLogic.MovieSortedByABCTitle();
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey(true);
                break;
            case 2:
                Console.Write("Please enter the title: ");
                string title = Console.ReadLine()!;
                _filmsLogic.SearchByTitle(title);
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey(true);
                break;
            case 3:
                Console.Write("Please enter the genre: ");
                string genre = Console.ReadLine()!;
                _filmsLogic.SearchByGenre(genre);
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey(true);
                break;
        }
    }

    private static void OrderSeatConfirm()
    {
        string prompt = "\nDo you want to order seats?";
        string[] options = { "Yes", "No, return to movie menu", "Return to main menu" };
        Menu movieMenu = new Menu(prompt, options);
        int selectedIndex = movieMenu.Run();

        switch (selectedIndex)
        {
            case 0:
                _filmsLogic.MovieOverview();
                VisualOverview vis = new VisualOverview();
                VisualOverview.Start(_account);
                RunMovieMenu();
                break;
            case 1:
                RunMovieMenu();
                break;
            case 2:
                RunMenusMenu();
                break;
            default:
                Console.WriteLine("Invalid input!");
                OrderSeatConfirm();
                break;

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
|              ".BrightCyan() + @"Catering Menu".BrightWhite() + @"               |
|                                          |
============================================
".BrightCyan();
        string[] options = { "Show current menu", "Search product by name", "Go back" };
        Menu cateringMenu = new Menu(prompt, options);
        int selectedIndex = cateringMenu.Run();

        switch (selectedIndex)
        {
            case 0:
                _foodsLogic.DisplayFoodMenu();
                // OrderFoodConfirm();
                RunCateringMenu();
                break;
            case 1:
                SearchProduct();
                // OrderFoodConfirm();
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
            RevenueModel revenue = _revenueLogic.GetById(1);
            revenue.Money += food.Cost;
            _revenueLogic.UpdateList(revenue);

            string code = VisualOverview.ReservationCodeMaker();
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
|              ".BrightCyan() + @"Ticket Menu".BrightWhite() + @"                 |
|                                          |
============================================
".BrightCyan();
        string[] options = { "My tickets", "Cancel ticket", "Go back" };
        Menu ticketMenu = new Menu(prompt, options);
        int selectedIndex = ticketMenu.Run();

        switch (selectedIndex)
        {
            case 0:
                MyTickets();
                RunTicketMenu();
                break;
            case 1:
                CancelTicket();
                RunTicketMenu();
                break;
            case 2:
                RunMenusMenu();
                break;
        }
    }

    private static void MyTickets()
    {
        if (_account == null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            System.Console.WriteLine();
            Console.WriteLine("You are not logged in!");
            Console.ResetColor();
            System.Console.WriteLine();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey(true);
            return;
        }
        else
        {
            List<ReservationModel> reservations = new List<ReservationModel>();
            foreach (int id in _account.TicketList)
            {
                ReservationModel reser = _reservationsLogic.GetById(id);
                if (reser is ReservationModel)
                {
                    reservations.Add(reser);
                }
            }
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(@"============================================
|                                          |
|                ".BrightYellow() + @"My Tickets".BrightWhite() + @"                |
|                                          |
============================================".BrightYellow());
            foreach (ReservationModel res in reservations)
            {
                int ID = res.Id;
                string Movie = res.Movie;
                string Date = res.Date.ToString("dd/MM/yyyy");
                int RoomNumber = res.RoomNumber;
                string code = res.ReservationCode;
                string FullName = res.FullName;
                string Email = res.Email;
                double ticketAmount = Math.Round(res.TicketAmount);
                double snackAmount = Math.Round(res.SnackAmount, 2);
                double TicketAmount = Math.Round(res.TicketAmount, 2);
                double totalCost = Math.Round(res.TotalAmount, 2);
                string selectedSeats = string.Join(", ", res.Seats.Select(seat => $"(Row {seat[0] + 1} Seat {seat[1] + 1})"));
                double TotalAmount = Math.Round(res.TotalAmount, 2);
                string Overview = $@"
    ID: {ID}    
    Movie: {Movie}
    Dates: {Date}
    Room Number: {RoomNumber}
    Reservation Code: {code}
    Full Name: {FullName}
    Email: {Email}
    Ticket Amount: {TicketAmount}
    Snack Amount: ${snackAmount}
    Total Cost: ${totalCost}
    Seats: {selectedSeats}
    Total Money Amount: ${TotalAmount}

" + @"============================================".BrightYellow();
                Console.WriteLine(Overview);
            }
            Console.ResetColor();
            Console.WriteLine("\nPress any key to continue...");

            Console.ReadKey(true);
        }
    }

    private static void CancelTicket()
    {
        string reservationCode = "";
        int tries = 3;
        while (true && tries > 0)
        {
            Console.WriteLine("What is your reservation code?");
            string? code = Console.ReadLine();
            if (string.IsNullOrEmpty(code))
            {
                tries--;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Description cannot be empty!");
                Console.ResetColor();
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey(true);
            }
            else
            {
                reservationCode = code;
                break;
            }
            if (tries <= 0)
            {
                Console.WriteLine("No more tries!");
                RunTicketMenu();

            }
        }


        // var guestCheck = _guestLogic.GetByCode(reservationCode!);
        string prompt = "Are you sure you want to delete the reservation?";
        string[] options = { "Yes", "No" };
        Menu mainMenu = new Menu(prompt, options);
        int selectedIndex = mainMenu.Run();

        switch (selectedIndex)
        {
            case 0:
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
                break;
            case 1:
                return;
        }

        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
        Console.Clear();
    }





    private static void RunAdvancedMenu()
    {
        if (_account.Type == 3)
        {
            string prompt = @"============================================
|                                          |
|             ".BrightCyan() + @"Advanced Menu".BrightWhite() + @"                |
|                                          |
============================================
".BrightCyan();
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
        else
        {
            string prompt = @"============================================
|                                          |
|   ".BrightCyan() + @"Advanced Menu".BrightWhite() + @"                |
|                                          |
============================================
".BrightCyan();
            string[] options = { "Advanced Reservation Menu", "Go back" };
            Menu advancedMenu = new Menu(prompt, options);
            int selectedIndex = advancedMenu.Run();

            switch (selectedIndex)
            {
                case 0:
                    RunAdvancedReservationMenu();
                    break;
                case 1:
                    RunMenusMenu();
                    break;
            }
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
|           ".BrightCyan() + @"Advanced Movie Menu".BrightWhite() + @"            |
|                                          |
============================================
".BrightCyan();
        string[] options = { "View all movies", "Add a movie", "Add an old movie back", "Delete a movie", "Go back" };
        Menu advancedMovieMenu = new Menu(prompt, options);
        int selectedIndex = advancedMovieMenu.Run();

        switch (selectedIndex)
        {
            case 0:
                _filmsLogic.MovieOverviewOld();
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey(true);
                RunAdvancedMovieMenu();
                break;
            case 1:
                AddMovie();
                RunAdvancedMovieMenu();
                break;
            case 2:
                AddOldMovie();
                RunAdvancedMovieMenu();
                break;
            case 3:
                _filmsLogic.MovieOverviewOld();
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey(true);
                DeleteMovie();
                RunAdvancedMovieMenu();
                break;
            case 4:
                RunAdvancedMenu();
                break;
        }
    }

    private static void AddMovie()
    {
        // variables
        string title;
        List<DateTime> dates = null!;
        List<int> rooms = null!;
        string description;
        int duration;
        List<string> genres;
        int age;
        string imageURL;

        // title
        while (true)
        {
            Console.Write("Enter the title of the movie: ");
            title = Console.ReadLine()!;
            if (string.IsNullOrEmpty(title))
            {
                Console.WriteLine("Title cannot be empty!");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey(true);
            }
            else
            {
                break;
            }
        }
        // dates
        bool dateDone = false;
        while (!dateDone)
        {
            Console.Write("Enter dates of the movie (Example: 01/01/2020 10:50:00, 01/01/2020 11:50:00): ");
            string datesString = Console.ReadLine()!;
            dates = new List<DateTime>();
            if (string.IsNullOrEmpty(datesString))
            {
                Console.WriteLine("Dates cannot be empty!");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey(true);
            }
            else
            {
                string[] datesArray = datesString.Split(", ");
                bool wentWrong = false;
                foreach (string date in datesArray)
                {
                    if (date.Length != 19)
                    {
                        wentWrong = true;
                        Console.WriteLine("Date format is incorrect! Must be DD/MM/YYYY HH:mm:ss");
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey(true);
                        break;
                    }
                    else
                    {
                        try
                        {
                            DateTime dateTime = DateTime.ParseExact(date, "dd/MM/yyyy HH:mm:ss", null!);
                            dates.Add(dateTime);
                        }
                        catch (Exception)
                        {
                            wentWrong = true;
                            Console.WriteLine("Date format is incorrect! Must be DD/MM/YYYY HH:mm:ss");
                            Console.WriteLine("Press any key to continue...");
                            Console.ReadKey(true);
                            break;
                        }
                    }
                }
                if (!wentWrong)
                {
                    dateDone = true;
                }
            }
        }
        // rooms
        bool roomDone = false;
        while (!roomDone)
        {
            Console.Write("Enter the room of the movie (1, 2 or 3). 1st room = 1st date, 2nd room = 2nd date, etc. (Example: 1, 2): ");
            string roomsString = Console.ReadLine()!;
            if (string.IsNullOrEmpty(roomsString))
            {
                Console.WriteLine("Rooms cannot be empty!");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey(true);
            }
            else
            {
                string[] roomsArray = roomsString.Split(", ");
                if (roomsArray.Length != dates.Count)
                {
                    Console.WriteLine("Amount of rooms must be equal to amount of dates!");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey(true);
                    continue;
                }
                rooms = new List<int>();
                bool wentWrong = false;
                foreach (string room in roomsArray)
                {
                    if (room != "1" && room != "2" && room != "3")
                    {
                        wentWrong = true;
                        Console.WriteLine("Room format is incorrect! Must be 1, 2, 3, etc.");
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey(true);
                        break;
                    }
                    else
                    {
                        int roomInt = Convert.ToInt32(room);
                        rooms.Add(roomInt);
                    }
                }
                if (!wentWrong)
                {
                    roomDone = true;
                }
            }
        }
        // description
        while (true)
        {
            Console.Write("Enter the description of the movie: ");
            description = Console.ReadLine()!;
            if (string.IsNullOrEmpty(description))
            {
                Console.WriteLine("Description cannot be empty!");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey(true);
            }
            else
            {
                break;
            }
        }
        // duration
        while (true)
        {
            Console.Write("Enter the duration of the movie (Example: 120): ");
            string input = Console.ReadLine()!;
            if (string.IsNullOrEmpty(input))
            {
                Console.WriteLine("Duration cannot be empty!");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey(true);
            }
            else
            {
                duration = Convert.ToInt32(input);
                break;
            }
        }
        // genre
        while (true)
        {
            Console.Write("Enter the genre of the movie (Example: Horror, Thriller): ");
            string input = Console.ReadLine()!;
            if (string.IsNullOrEmpty(input))
            {
                Console.WriteLine("Genre cannot be empty!");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey(true);
            }
            else
            {
                genres = new List<string>();
                genres.Add(input);
                break;
            }
        }
        while (true)
        {
            Console.WriteLine("Enter the age rating of the movie (Example: 12): ");
            string rated = Console.ReadLine()!;
            if (string.IsNullOrEmpty(rated))
            {
                Console.WriteLine("Genre cannot be empty!");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey(true);
            }
            else
            {
                age = Convert.ToInt32(rated);
                break;
            }
        }
        // image
        while (true)
        {
            Console.Write("Enter the image url (Format: https://.... ): ");
            imageURL = Console.ReadLine()!;
            if (string.IsNullOrEmpty(imageURL) && imageURL.StartsWith("https://") == false)
            {
                Console.WriteLine("Image url cannot be empty!");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey(true);
            }
            else
            {
                break;
            }
        }

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
        FilmModel film = new FilmModel(id, dates, rooms, title, description, duration, genres, age, imageURL);
        _filmsLogic.UpdateList(film);
        Console.WriteLine("Movie added!");
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey(true);
    }

    private static void AddOldMovie()
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
        FilmModel movie = _filmsLogic.GetByIdOld(id);
        if (movie is FilmModel)
        {
            List<DateTime> dates = null!;
            List<int> rooms = null!;

            // dates
            bool dateDone = false;
            while (!dateDone)
            {
                Console.Write("Enter dates of the movie (Example: 01/01/2020 10:50:00, 01/01/2020 11:50:00): ");
                string datesString = Console.ReadLine()!;
                dates = new List<DateTime>();
                if (string.IsNullOrEmpty(datesString))
                {
                    Console.WriteLine("Dates cannot be empty!");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey(true);
                }
                else
                {
                    string[] datesArray = datesString.Split(", ");
                    bool wentWrong = false;
                    foreach (string date in datesArray)
                    {
                        if (date.Length != 19)
                        {
                            wentWrong = true;
                            Console.WriteLine("Date format is incorrect! Must be DD/MM/YYYY HH:mm:ss");
                            Console.WriteLine("Press any key to continue...");
                            Console.ReadKey(true);
                            break;
                        }
                        else
                        {
                            try
                            {
                                DateTime dateTime = DateTime.ParseExact(date, "dd/MM/yyyy HH:mm:ss", null!);
                                dates.Add(dateTime);
                            }
                            catch (Exception)
                            {
                                wentWrong = true;
                                Console.WriteLine("Date format is incorrect! Must be DD/MM/YYYY HH:mm:ss");
                                Console.WriteLine("Press any key to continue...");
                                Console.ReadKey(true);
                                break;
                            }
                        }
                    }
                    if (!wentWrong)
                    {
                        dateDone = true;
                    }
                }
            }
            // rooms
            bool roomDone = false;
            while (!roomDone)
            {
                Console.Write("Enter the room of the movie (1, 2 or 3). 1st room = 1st date, 2nd room = 2nd date, etc. (Example: 1, 2): ");
                string roomsString = Console.ReadLine()!;
                if (string.IsNullOrEmpty(roomsString))
                {
                    Console.WriteLine("Rooms cannot be empty!");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey(true);
                }
                else
                {
                    string[] roomsArray = roomsString.Split(", ");
                    if (roomsArray.Length != dates.Count)
                    {
                        Console.WriteLine("Amount of rooms must be equal to amount of dates!");
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey(true);
                        continue;
                    }
                    rooms = new List<int>();
                    bool wentWrong = false;
                    foreach (string room in roomsArray)
                    {
                        if (room != "1" && room != "2" && room != "3")
                        {
                            wentWrong = true;
                            Console.WriteLine("Room format is incorrect! Must be 1, 2, 3, etc.");
                            Console.WriteLine("Press any key to continue...");
                            Console.ReadKey(true);
                            break;
                        }
                        else
                        {
                            int roomInt = Convert.ToInt32(room);
                            rooms.Add(roomInt);
                        }
                    }
                    if (!wentWrong)
                    {
                        roomDone = true;
                    }
                }
            }

            movie.Dates = dates;
            movie.Rooms = rooms;
            _filmsLogic.UpdateList(movie);
            Console.WriteLine("Movie added!");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey(true);
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Movie not found");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey(true);
            Console.ResetColor();
        }
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
|            ".BrightCyan() + @"Advanced Seat Menu".BrightWhite() + @"            |
|                                          |
============================================
".BrightCyan();
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
        string input = "";
        do
        {
            Console.Write("Enter the new price of the seat: ");
            input = Console.ReadLine()!;
            if (string.IsNullOrEmpty(input))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Name or price cannot be empty!");
                Console.ResetColor();
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey(true);
                RunAdvancedSeatMenu();
            }
            else if (IsNumber(input) == false)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine("Price has to be a number");
                Console.ResetColor();
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey(true);
                RunAdvancedSeatMenu();
            }
            else if (Convert.ToDouble(input) <= 0)
            {
                System.Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine("Price must be more than 0 and a number");
                Console.ResetColor();
                System.Console.WriteLine();
            }

        } while (Convert.ToDouble(input) <= 0);
        // System.Console.WriteLine("test");
        if (string.IsNullOrEmpty(name))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            System.Console.WriteLine();
            Console.WriteLine("Name or price cannot be empty!");
            Console.ResetColor();
            System.Console.WriteLine();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey(true);
            RunAdvancedSeatMenu();
        }
        double price = Convert.ToDouble(input);

        TicketModel ticket = _ticketLogic.GetByName(name);
        if (ticket is TicketModel)
        {
            ticket.Cost = price;
            _ticketLogic.UpdateList(ticket);
            Console.ForegroundColor = ConsoleColor.Green;
            System.Console.WriteLine();
            Console.WriteLine("Seat price changed!");
            System.Console.WriteLine();
            Console.ResetColor();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey(true);
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            System.Console.WriteLine();
            Console.WriteLine("No seat with that name was found!");
            Console.ResetColor();
            System.Console.WriteLine();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey(true);
        }
    }
    public static bool IsNumber(String s)
    {
        foreach (Char ch in s)
        {
            if (!Char.IsDigit(ch)) return false;
            if (s.Contains(".")) return true;
        }
        return true;
    }
    private static void RunAdvancedFoodMenu()
    {
        if (_foodsLogic == null)
        {
            _foodsLogic = new FoodsLogic();
        }

        string prompt = @"============================================
|                                          |
|            ".BrightCyan() + @"Advanced Food Menu".BrightWhite() + @"            |
|                                          |
============================================
".BrightCyan();
        string[] options = { "View all food", "Change a food price", "Add a snack", "Remove a snack", "Go back" };
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
                string name = string.Empty;
                string cost = string.Empty;
                string quantity = string.Empty;
                string age = string.Empty;

                Console.WriteLine("Enter a snack name: ");
                name = Console.ReadLine()!;

                if (string.IsNullOrEmpty(name))
                {
                    Console.WriteLine("Enter a snack name (last chance): ");
                    name = Console.ReadLine()!;

                    if (string.IsNullOrEmpty(name))
                    {
                        RunAdvancedFoodMenu();
                    }
                }

                Console.WriteLine("Enter a snack price: ");
                cost = Console.ReadLine()!;

                if (string.IsNullOrEmpty(cost) || !CinemaMenus.IsNumber(cost))
                {
                    Console.WriteLine("Enter a snack price (last chance): ");
                    cost = Console.ReadLine()!;

                    if (string.IsNullOrEmpty(cost) || !CinemaMenus.IsNumber(cost))
                    {
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey(true);
                        RunAdvancedFoodMenu();
                    }
                }

                Console.WriteLine("Enter a snack quantity: ");
                quantity = Console.ReadLine()!;

                if (string.IsNullOrEmpty(quantity) || !CinemaMenus.IsNumber(quantity))
                {
                    Console.WriteLine("Enter a snack quantity (last chance): ");
                    quantity = Console.ReadLine()!;

                    if (string.IsNullOrEmpty(quantity) || !CinemaMenus.IsNumber(quantity))
                    {
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey(true);
                        RunAdvancedFoodMenu();
                    }
                }

                Console.WriteLine("Enter an age rating: ");
                age = Console.ReadLine()!;

                if (string.IsNullOrEmpty(age) || !CinemaMenus.IsNumber(age))
                {
                    Console.WriteLine("Enter an age rating (last chance): ");
                    age = Console.ReadLine()!;

                    if (string.IsNullOrEmpty(age) || !CinemaMenus.IsNumber(age))
                    {
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey(true);
                        RunAdvancedFoodMenu();
                    }
                }

                FoodModel food = new FoodModel(name, double.Parse(cost, CultureInfo.InvariantCulture), double.Parse(quantity, CultureInfo.InvariantCulture), Convert.ToInt32(age));
                _foodsLogic.AddFood(food);
                // SetFoodPrice();
                RunAdvancedFoodMenu();
                break;

            case 3:
                string Name = string.Empty;
                string Cost = string.Empty;
                string Quantity = string.Empty;
                string Age = string.Empty;

                Console.WriteLine("Enter a snack name: ");
                Name = Console.ReadLine()!;

                if (string.IsNullOrEmpty(Name))
                {
                    Console.WriteLine("Enter a snack name (last chance): ");
                    Name = Console.ReadLine()!;

                    if (string.IsNullOrEmpty(Name))
                    {
                        RunAdvancedFoodMenu();
                    }
                }

                Console.WriteLine("Enter a snack price: ");
                Cost = Console.ReadLine()!;

                if (string.IsNullOrEmpty(Cost) || !CinemaMenus.IsNumber(Cost))
                {
                    Console.WriteLine("Enter a snack price (last chance): ");
                    Cost = Console.ReadLine()!;

                    if (string.IsNullOrEmpty(Cost) || !CinemaMenus.IsNumber(Cost))
                    {
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey(true);
                        RunAdvancedFoodMenu();
                    }
                }

                Console.WriteLine("Enter a snack quantity: ");
                Quantity = Console.ReadLine()!;

                if (string.IsNullOrEmpty(Quantity) || !CinemaMenus.IsNumber(Quantity))
                {
                    Console.WriteLine("Enter a snack quantity (last chance): ");
                    Quantity = Console.ReadLine()!;

                    if (string.IsNullOrEmpty(Quantity) || !CinemaMenus.IsNumber(Quantity))
                    {
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey(true);
                        RunAdvancedFoodMenu();
                    }
                }

                Console.WriteLine("Enter an age rating: ");
                Age = Console.ReadLine()!;

                if (string.IsNullOrEmpty(Age) || !CinemaMenus.IsNumber(Age))
                {
                    Console.WriteLine("Enter an age rating (last chance): ");
                    Age = Console.ReadLine()!;

                    if (string.IsNullOrEmpty(Age) || !CinemaMenus.IsNumber(Age))
                    {
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey(true);
                        RunAdvancedFoodMenu();
                    }
                }

                FoodModel Food = new FoodModel(Name, double.Parse(Cost, CultureInfo.InvariantCulture), double.Parse(Quantity, CultureInfo.InvariantCulture), Convert.ToInt32(Age));
                _foodsLogic.DeleteFood(Food);
                // SetFoodPrice();
                RunAdvancedFoodMenu();
                break;

            case 4:
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
|".BrightCyan() + @"Advanced Reservation Menu".BrightWhite() + @"        |
|                                          |
============================================
".BrightCyan();
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
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Email not found!");
            Console.ResetColor();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey(true);
            return;
        }

        Console.WriteLine(@"==================================================
|                                                 |
|                  ".BrightYellow() + @"Reservations".BrightWhite() + @"                   |
|                                                 |
==================================================".BrightYellow());


        Console.ForegroundColor = ConsoleColor.Yellow;
        string selectedSeats = string.Join(", ", reservation.Seats.Select(seat => $"Row {seat[0] + 1}, Seat {seat[1] + 1}"));
        string Overview = $@"
  Movie: {reservation.Movie}
  Full Name: {reservation.FullName}
  Email: {reservation.Email}
  Ticket Amount: {reservation.TicketAmount}
  Seats: {selectedSeats}
  Total Money Amount: {Math.Round(reservation.TotalAmount, 2)}

==================================================";
        Console.WriteLine(Overview);


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
            System.Console.WriteLine();
            Console.WriteLine("Reservation not found");
            System.Console.WriteLine();
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
            System.Console.WriteLine();
            Console.WriteLine("Reservation deleted");
            System.Console.WriteLine();
            Console.ResetColor();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            System.Console.WriteLine();
            Console.WriteLine("Reservation not found");
            System.Console.WriteLine();
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
        Console.ForegroundColor = ConsoleColor.Blue;
        string prompt = @"============================================
|                                          |
|                 ".BrightCyan() + @"Revenue".BrightWhite() + @"                  |
|                                          |
============================================
".BrightCyan();
        Console.ResetColor();
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
        RevenueModel revenue = _revenueLogic.GetById(1);
        Console.WriteLine($"Total revenue: ${revenue.Money}");
        Console.WriteLine();
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey(true);
    }

}