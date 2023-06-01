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
        bool logIn = false;
        while (tries > 0)
        {
            Console.Write("Please enter your email address: ");
            string email = Console.ReadLine()!;
            string password = string.Empty;
            SecureString pass = _accountsLogic.HashedPass();
            password = new System.Net.NetworkCredential(string.Empty, pass).Password;
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
                    logIn = true;
                    Console.WriteLine("\nWelcome back " + _account.FullName);
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
        if (!logIn)
        {
            Console.WriteLine("0 tries left. Please try again later...");
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
        if (_accountsLogic.GetByMail(email) != null)
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
        Console.WriteLine("Please enter your password (You have 3 attempts):");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("------------------------------------------------------------------------------------------");
        Console.WriteLine(@"|   Must contain at least one special character(%!@#$%^&*()?/>.<,:;'\|}]{[_~`+=-         |");
        Console.WriteLine("|   Must be longer than 6 characters                                                     |");
        Console.WriteLine("|   Must contain at least one number                                                     |");
        Console.WriteLine("|   One upper case                                                                       |");
        Console.WriteLine("|   Atleast one lower case                                                               |");
        Console.WriteLine("------------------------------------------------------------------------------------------");
        Console.ResetColor();

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
        MailConformation mail = new(email);
        mail.SendRegistrationConformation();
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
|              Catering Menu               |
|                                          |
============================================
View menu.
";
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
|              Ticket Menu                 |
|                                          |
============================================
";
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
            Console.WriteLine("You are not logged in!");
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
|                My Tickets                |
|                                          |
============================================");
            foreach (ReservationModel res in reservations)
            {
                int ID = res.Id;
                string Movie = res.Movie;
                string Date = res.Date.ToString("dd/MM/yyyy");
                int RoomNumber = res.RoomNumber;
                string code = res.ReservationCode;
                string FullName = res.FullName;
                string Email = res.Email;
                double ticketAmount = res.TicketAmount;
                double snackAmount = res.SnackAmount;
                double TicketAmount = res.TicketAmount;
                double totalCost = res.TotalAmount;
                string selectedSeats = string.Join(", ", res.Seats.Select(seat => $"(Row {seat[0] + 1} Seat {seat[1] + 1})"));
                double TotalAmount = res.TotalAmount;
                string Overview = $@"
    ID: {ID}    
    Movie: {Movie}
    Dates: {Date}
    Room Number: {RoomNumber}
    Reservation Code: {code}
    Full Name: {FullName}
    Email: {Email}
    Ticket Amount: {TicketAmount}
    Snack Amount: {snackAmount}
    Total Cost: {totalCost}
    Seats: {selectedSeats}
    Total Money Amount: {TotalAmount}

============================================";
                Console.WriteLine(Overview);
            }
            Console.WriteLine("\nPress any key to continue...");
            Console.ResetColor();
            Console.ReadKey(true);
        }
    }

    private static void CancelTicket()
    {
        Console.WriteLine("What is your reservation code?");
        string? reservationCode = Console.ReadLine();
        // var guestCheck = _guestLogic.GetByCode(reservationCode!);
        Console.WriteLine("Are you sure you want to delete the reservation? (Y/N):");
        string? input = Console.ReadLine()!.ToUpper();
        if (input == "Y")
        {
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
        }
        else if (input == "N")
        {
            return;
        }
        else
        {
            Console.WriteLine("Invalid input");
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
        else
        {
            string prompt = @"============================================
|                                          |
|             Advanced Menu                |
|                                          |
============================================
";
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
            Console.Write("Enter the duration of the movie: ");
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
            Console.Write("Enter the genre of the movie: ");
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
            Console.WriteLine("Enter the age rating of the movie: ");
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
            Console.Write("Enter the image url: ");
            imageURL = Console.ReadLine()!;
            if (string.IsNullOrEmpty(imageURL))
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


        Console.ForegroundColor = ConsoleColor.Magenta;
        string selectedSeats = string.Join(", ", reservation.Seats.Select(seat => $"Row {seat[0] + 1}, Seat {seat[1] + 1}"));
        string Overview = $@"
  Movie: {reservation.Movie}
  Full Name: {reservation.FullName}
  Email: {reservation.Email}
  Ticket Amount: {reservation.TicketAmount}
  Seats: {selectedSeats}
  Total Money Amount: {reservation.TotalAmount}

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
        RevenueModel revenue = _revenueLogic.GetById(1);
        Console.WriteLine($"Total revenue: {revenue.Money}");
        Console.WriteLine();
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey(true);
    }

}