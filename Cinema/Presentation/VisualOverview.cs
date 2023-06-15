public class VisualOverview
{
    static private RoomsLogic? _roomsLogic;
    static private FilmsLogic? _filmsLogic;
    static private ReservationsLogic? _reservationsLogic;
    static private RevenueLogic? _revenueLogic;
    static private AccountModel _account = null!;
    static private AccountsLogic? _accountsLogic;
    static private GuestLogic? _guestLogic;
    static private TicketLogic? _ticketLogic;

    public VisualOverview()
    {
        if (CinemaMenus._roomsLogic == null)
        {
            CinemaMenus._roomsLogic = new RoomsLogic();
            _roomsLogic = CinemaMenus._roomsLogic;
        }
        else
        {
            _roomsLogic = CinemaMenus._roomsLogic;
        }

        if (CinemaMenus._filmsLogic == null)
        {
            CinemaMenus._filmsLogic = new FilmsLogic();
            _filmsLogic = CinemaMenus._filmsLogic;
        }
        else
        {
            _filmsLogic = CinemaMenus._filmsLogic;
        }

        if (CinemaMenus._reservationsLogic == null)
        {
            CinemaMenus._reservationsLogic = new ReservationsLogic();
            _reservationsLogic = CinemaMenus._reservationsLogic;
        }
        else
        {
            _reservationsLogic = CinemaMenus._reservationsLogic;
        }

        if (CinemaMenus._revenueLogic == null)
        {
            CinemaMenus._revenueLogic = new RevenueLogic();
            _revenueLogic = CinemaMenus._revenueLogic;
        }
        else
        {
            _revenueLogic = CinemaMenus._revenueLogic;
        }

        if (CinemaMenus._accountsLogic == null)
        {
            CinemaMenus._accountsLogic = new AccountsLogic();
            _accountsLogic = CinemaMenus._accountsLogic;
        }
        else
        {
            _accountsLogic = CinemaMenus._accountsLogic;
        }

        if (CinemaMenus._guestLogic == null)
        {
            CinemaMenus._guestLogic = new GuestLogic();
            _guestLogic = CinemaMenus._guestLogic;
        }
        else
        {
            _guestLogic = CinemaMenus._guestLogic;
        }

        if (CinemaMenus._ticketLogic == null)
        {
            CinemaMenus._ticketLogic = new TicketLogic();
            _ticketLogic = CinemaMenus._ticketLogic;
        }
        else
        {
            _ticketLogic = CinemaMenus._ticketLogic;
        }
    }

    public static void Start(AccountModel account)
    {
        _account = account;
        Console.WriteLine("Welcome to the movie rooms page");

        int movieId = 0;
        do
        {
            Console.WriteLine("Please enter the movie id");
            string id = Console.ReadLine()!;
            if (CinemaMenus.IsNumber(id) == false)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine();
                Console.WriteLine($"No film found with that id\n");
                Console.ResetColor();
                Console.WriteLine("Press any key to go back to the menu");
                Console.ReadKey();
                Console.Clear();
                return;
            }
            if (string.IsNullOrEmpty(id) || Convert.ToInt32(id) < 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine();
                Console.WriteLine($"No film found with that id\n");
                Console.ResetColor();
                Console.WriteLine("Press any key to go back to the menu");
                Console.ReadKey();
                Console.Clear();
                return;
            }
            else if (Convert.ToInt32(id) > 0)
            {
                movieId = Convert.ToInt32(id);
            }
            if (CinemaMenus._filmsLogic.GetById(movieId) == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine();
                Console.WriteLine($"No film found with that id\n");
                Console.ResetColor();
                Console.WriteLine("Press any key to go back to the menu");
                Console.ReadKey();
                Console.Clear();
                return;
            }

        } while (movieId <= 0);

        FilmModel film = _filmsLogic!.GetById(movieId);
        DateTime chosenDate = film.Dates[0];
        int index = 0;

        if (film.Age == 16 || film.Age == 18)
        {
            System.Console.WriteLine();
            System.Console.Write($"Mind your age! You will be checked for ID! Trust you will be dealt with >:[".Orange());
            System.Console.WriteLine();
            Console.ResetColor();
        }
        else if (film.Age == 13)
        {
            System.Console.WriteLine();
            System.Console.Write("We recommend Parental Guidance when visiting this movie\n".Orange());
            System.Console.WriteLine();
            Console.ResetColor();
        }

        int count = 1;
        foreach (var date in film.Dates)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"{count}. {date}");
            Console.ResetColor();
            count++;
        }

        int dateSelection = 0;
        do
        {
            System.Console.WriteLine();
            Console.WriteLine("Please choose the date you want to see the movie:\n");
            string dateChoice = Console.ReadLine()!;
            if (dateChoice.All(Char.IsLetter))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Please enter a valid date\n");
                Console.ResetColor();
                Start(account);
            }
            else if (CinemaMenus.IsNumber(dateChoice) == false)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Please enter a valid date\n");
                Console.ResetColor();
                Start(account);
            }
            else if (string.IsNullOrEmpty(dateChoice) || Convert.ToInt32(dateChoice) <= 0 || Convert.ToInt32(dateChoice) > count)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine("Please enter a valid date");
                Console.ResetColor();
                Start(account);
            }
            else if (!(Convert.ToInt32(dateChoice) < 0) && !(Convert.ToInt32(dateChoice) > count - 1))
            {
                dateSelection = Convert.ToInt32(dateChoice);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine("Please enter a valid date");
                Console.ResetColor();
                Start(account);
            }
        } while (dateSelection <= 0);

        chosenDate = film.Dates[dateSelection - 1];
        index = dateSelection - 1;

        // Console.WriteLine("Please enter the room number\n");
        int number = film.Rooms[index];
        RoomModel room = _roomsLogic!.CheckEnter(number);
        if (room.Seats.Count() == room.MaxSeats)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Movie fully booked full\n");
            Console.ResetColor();
            Console.WriteLine("Press any key to go back to the menu");
            Console.ReadKey();
            Console.Clear();
            Start(account);
        }
        if (room != null)
        {
            Run(room, film, chosenDate);
        }
        else
        {
            Console.WriteLine("No room found with that hall");
        }
    }

    public static void Run(RoomModel room, FilmModel film, DateTime chosenDate)
    {


        List<int[]> selectedSeats = new List<int[]>();// final selected seats

        // special seats
        List<Tuple<int, int, DateTime, int>> reservedSeats = room.Seats;
        List<int> vipSeats = room.VipSeats;
        List<int> disabledSeats = room.DisabledSeats;
        List<int> comfortSeats = room.ComfortSeats;
        // niet selecteerbare plaatsen
        List<int> dontPrint = room.NoSeats;
        int roomWidth = room.RoomWidth;
        int roomLength = room.RoomLength;

        Box[,] box = new Box[roomLength, roomWidth];
        int cursorRow = 0; // Cursor's row position
        int cursorCol = 0; // Cursor's column position
        int boxesSelected = 0; // Number of boxes currently selected
        List<int[]> selectedBoxes = new List<int[]>(); // List of selected boxes
        Console.SetCursorPosition(0, 0);
        Console.CursorVisible = false;
        Console.Clear();



        // Print the initial box to the console
        PrintBox();

        bool exitLoop = false;
        while (!exitLoop)
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            Console.CursorVisible = false;
            // move through seats
            switch (keyInfo.Key)
            {
                case ConsoleKey.UpArrow:
                    if (cursorRow > 0)
                    {
                        cursorRow--;
                        PrintBox();
                    }
                    break;

                case ConsoleKey.DownArrow:
                    if (cursorRow < roomLength - 1)
                    {
                        cursorRow++;
                        PrintBox();
                    }
                    break;

                case ConsoleKey.LeftArrow:
                    if (cursorCol > 0)
                    {
                        cursorCol--;
                        PrintBox();
                    }
                    break;

                case ConsoleKey.RightArrow:
                    if (cursorCol < roomWidth - 1)
                    {
                        cursorCol++;
                        PrintBox();
                    }
                    break;

                case ConsoleKey.Enter:
                    // Check if the selected square is in the reserved list
                    Tuple<int, int, DateTime, int> seat = new Tuple<int, int, DateTime, int>(film.Id, room.Id, chosenDate, cursorRow * roomWidth + cursorCol);
                    if (reservedSeats.Contains(seat))
                    {
                        break;
                    }
                    else if (dontPrint.Contains(cursorRow * roomWidth + cursorCol))
                    {
                        // break so you cant select white square
                        break;
                    }
                    else if (selectedBoxes.Any(box => box[0] == cursorRow && box[1] == cursorCol))
                    {
                        // Deselect the box
                        selectedBoxes.Remove(selectedBoxes.First(box => box[0] == cursorRow && box[1] == cursorCol)); // remove from selectedboxes so it is not green anymore
                        boxesSelected--;
                        PrintBox();
                    }
                    else
                    {
                        // Select the box at the cursor's position
                        selectedBoxes.Add(new int[] { cursorRow, cursorCol });
                        boxesSelected++;
                        PrintBox();
                    }
                    break;
                case ConsoleKey.Spacebar: // end selecting
                    // 0 check
                    if (selectedBoxes.Count() == 0)
                    {
                        System.Console.WriteLine();
                        string prompt1 = $"No seats selected".Red() + $"\n" + $"\nWould you like to return to the main menu or continue?";
                        string[] options1 = { "Yes, return to menu", "No, try again" };
                        Menu mainMenu1 = new Menu(prompt1, options1);
                        int selectedIndex1 = mainMenu1.Run();

                        switch (selectedIndex1)
                        {
                            case 0:
                                CinemaMenus.RunMenusMenu();
                                break;
                            case 1:
                                Console.Clear();
                                PrintBox();
                                break;
                        }
                    }
                    else
                    {
                        // if conformation yes exit loop
                        exitLoop = true;
                        // seats aan seatlist toevoegen
                        Console.WriteLine($"\nYou have selected {boxesSelected} seats:");
                        foreach (int[] item in selectedBoxes)
                        {
                            Console.WriteLine($"- Row {item[0] + 1}, Seat {item[1] + 1}");
                            selectedSeats.Add(item);
                        }
                        Console.WriteLine("Press ENTER to continue...");
                        Console.ReadKey(true);
                        // confirmation vragen
                        string prompt = "Are you sure you want to reserve these seats?";
                        string[] options = { "Yes", "No, try again", "Return to main menu" };
                        Menu mainMenu = new Menu(prompt, options);
                        int selectedIndex = mainMenu.Run();

                        switch (selectedIndex)
                        {
                            case 0:
                                Reserve(room, film, selectedSeats, chosenDate);
                                break;
                            case 1:
                                // make final seatlist empty again
                                Console.Clear();
                                Console.WriteLine("Returning to the seat selection process. Press ENTER to continue".Orange());
                                Console.ReadKey(true);
                                selectedSeats.Clear();
                                Console.Clear();
                                // go back into loop
                                exitLoop = false;
                                PrintBox();
                                break;
                            case 2:
                                break;

                        }
                    }
                    break;
            }
        }
        void PrintBox()
        {
            Console.SetCursorPosition(0, 0);
            Console.CursorVisible = false;
            // Console.Clear();

            Console.Write("■: Unreserved Seat".Blue() + "\n");
            Console.Write("■: Reserved Seat".DarkGray() + "\n");
            Console.Write("■: VIP Seat".Red() + "\n");
            Console.Write("■: Comfort Seat".Orange() + "\n");
            Console.Write("▲: Disability Seat".DarkMagenta() + "\n");
            Console.Write("[]: Exit Doors".Green() + $"\n");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"\nUse arrow keys to navigate. Press ENTER to select a seat and press SPACE to confirm seat selection.\n");
            if (room.Id == 3)
            { Console.WriteLine("                                   |  Screen  |\n"); }
            else if (room.Id == 2)
            {
                Console.WriteLine("                         |  Screen  |\n");
            }
            else if (room.Id == 1)
            {
                Console.WriteLine("                  |  Screen  |\n");
            }
            // Print column numbers
            // Console.Write("   ");
            // for (int j = 0; j < roomWidth; j++)
            // {
            //     Console.Write($"{(j + 1)} ");
            // }
            if (room.Id == 3)
            {
                Console.WriteLine("\n----------------------------------------------------------------------------------");
                Console.Write("[]                                                                              []".Green() + $"\n");
                Console.WriteLine("----------------------------------------------------------------------------------\n");
            }
            else if (room.Id == 2)
            {
                Console.WriteLine("\n--------------------------------------------------------");
                Console.Write("[]                                                   []".Green() + $"\n");
                Console.WriteLine("--------------------------------------------------------\n");
            }
            else if (room.Id == 1)
            {
                Console.WriteLine("\n-----------------------------------------");

                Console.Write("[]                                      []".Green() + $"\n");
                Console.WriteLine("-----------------------------------------\n");
            }



            // print visual overview
            for (int i = 0; i < roomLength; i++)
            {
                // Print row number to the left of the row
                Console.Write($"|{"Row: " + (i + 1),10} |");

                for (int j = 0; j < roomWidth; j++)
                {

                    int seatNumber = i * roomWidth + j;
                    Tuple<int, int, DateTime, int> seatChoice = new Tuple<int, int, DateTime, int>(film.Id, room.Id, chosenDate, seatNumber);
                    if (i == cursorRow && j == cursorCol)
                    {
                        Console.Write("■".Yellow() + " ");
                    }

                    else if (reservedSeats.Contains(seatChoice))
                    {
                        if (vipSeats.Contains(seatNumber) || comfortSeats.Contains(seatNumber) || disabledSeats.Contains(seatNumber))
                        {
                            Console.Write("■".DarkGray() + " ");
                        }
                        else
                        {
                            Console.Write("■".DarkGray() + " ");
                        }
                    }
                    // check for special seats etc.
                    else if (selectedBoxes.Any(box => box[0] == i && box[1] == j)) // make selected box green
                    {
                        Console.Write("■".Green() + " ");
                    }
                    else if (comfortSeats.Contains(seatNumber) && selectedBoxes.Any(box => box[0] == i && box[1] == j))
                    {
                        Console.Write("■".Green() + " ");
                    }
                    else if (vipSeats.Contains(seatNumber) && selectedBoxes.Any(box => box[0] == i && box[1] == j))
                    {
                        Console.Write("■".Green() + " ");
                    }
                    else if (disabledSeats.Contains(seatNumber) && selectedBoxes.Any(box => box[0] == i && box[1] == j))
                    {
                        Console.Write("▲".Green() + " ");
                    }
                    else if (disabledSeats.Contains(seatNumber))
                    {
                        Console.Write("▲".DarkMagenta() + " ");
                    }
                    else if (comfortSeats.Contains(seatNumber))
                    {
                        Console.Write("■".Orange() + " ");
                    }
                    else if (vipSeats.Contains(seatNumber))
                    {
                        Console.Write("■".Red() + " ");
                    }
                    else if (dontPrint.Contains(seatNumber))
                    {
                        Console.Write("■".White() + " ");
                    }
                    else
                    {
                        Console.Write("■".Blue() + " ");
                    }
                }
                Console.WriteLine();
            }
        }
        // empty line for spacing
        Console.WriteLine();




    }
    public static void Reserve(RoomModel room, FilmModel film, List<int[]> seatList, DateTime chosenDate)
    {
        Console.Clear();
        // FoodsLogic foodLogic = new();
        double food = GetFoodAmount();

        // reserve as guest
        if (_account == null)
        {
            // gegevens vragen
            Console.Clear();
            // System.Console.WriteLine(food);
            Console.WriteLine("Email address:");
            // Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("-----------------------------".BrightYellow());
            Console.WriteLine("|".BrightYellow() + "   Email must contain an @".BrightWhite() + "   |".BrightYellow());
            Console.WriteLine("-----------------------------".BrightYellow());
            // Console.ResetColor();
            string email = Console.ReadLine()!;
            // int EmailAttempts = 0;


            while (_accountsLogic!.EmailFormatCheck(email) == false)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Wrong format! Please enter Email in the correct format...");
                Console.ResetColor();
                Console.Write("Email address: ");
                string Email = Console.ReadLine()!;
                email = Email;
                // EmailAttempts += 1;
            }

            string fullName = null!;
            do
            {
                Console.Write("Please enter your full name: ");
                string tickets = Console.ReadLine()!;
                if (string.IsNullOrEmpty(tickets))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    System.Console.WriteLine($"\nMust enter a valid name\n");
                    Console.ResetColor();
                    continue;
                }
                else
                {
                    fullName = tickets;
                }

            } while (fullName == null);

            // payment 
            Console.WriteLine("Press ENTER to continue...");
            Console.ReadLine();
            Console.Clear();
            string prompt = "Do you want to proceed with the reservation?";
            string[] options = { "Yes", "No" };
            Menu mainMenu = new Menu(prompt, options);
            int selectedIndex = mainMenu.Run();
            switch (selectedIndex)
            {
                case 0:
                    Console.WriteLine("\n--------------------------------".BrightCyan());
                    Console.WriteLine("         PAYMENT OPTIONS         ".BrightWhite());
                    Console.WriteLine("--------------------------------".BrightCyan());
                    Console.WriteLine("1. Paypal".BrightWhite());
                    Console.WriteLine("2. Ideal".BrightWhite());
                    Console.WriteLine("--------------------------------".BrightCyan());
                    SelectPayment();
                    Console.Clear();

                    // info naar guest json sturen
                    string title = film.Title;
                    // prchase test
                    List<int> seats = new();
                    List<Tuple<int, int, DateTime, int>> seatsInfo = new();
                    foreach (var seat in seatList)
                    {
                        int seatNumber = (seat[0]) * room.RoomWidth + seat[1];
                        seats.Add(seatNumber);
                        Tuple<int, int, DateTime, int> dateSeat = new Tuple<int, int, DateTime, int>(film.Id, room.Id, chosenDate, seatNumber);
                        seatsInfo.Add(dateSeat);
                        room.Seats.Add(dateSeat);
                        _roomsLogic?.UpdateList(room);
                    }

                    double ticketTotal = _ticketLogic!.TicketPurchase(room, seats);
                    // revenue vastmeten
                    RevenueModel rev = _revenueLogic!.GetById(1);
                    rev.Money += ticketTotal + food;
                    _revenueLogic.UpdateList(rev);

                    //total amount krijgen
                    // double totalAmount = ticketTotal + food;
                    double totalAmount = ticketTotal + food;
                    // guest naar json sturen
                    string reservationCode = ReservationCodeMaker();
                    ReservationModel guest = new(1, reservationCode, fullName, email, title, seatList.Count, food, ticketTotal, room.Id, seatList, seatsInfo, totalAmount, chosenDate);
                    _guestLogic!.UpdateList(guest);
                    // mail verzenden
                    bool account = false;
                    Console.Clear();
                    System.Console.Write("Sending conformation email please wait...".Orange() + $"\n");

                    MailConformation mailConformation = new MailConformation(email, account);
                    mailConformation.SendMailConformation();

                    break;
                case 1:
                    Console.ForegroundColor = ConsoleColor.Green;
                    System.Console.WriteLine();
                    Console.WriteLine($"Reservation has been cancelled.");
                    System.Console.WriteLine();
                    Console.ResetColor();
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey(true);
                    return;

            }
        }

        // reserve as login
        else if (_account != null)
        {
            string prompt = "Do you want to proceed with the reservation?";
            string[] options = { "Yes", "No" };
            Menu mainMenu = new Menu(prompt, options);
            int selectedIndex = mainMenu.Run();

            switch (selectedIndex)
            {
                case 0:
                    // System.Console.WriteLine(food);
                    Console.WriteLine("\n--------------------------------".BrightCyan());
                    Console.WriteLine("         PAYMENT OPTIONS         ".BrightWhite());
                    Console.WriteLine("--------------------------------".BrightCyan());
                    Console.WriteLine("1. Paypal".BrightWhite());
                    Console.WriteLine("2. Ideal".BrightWhite());
                    Console.WriteLine("--------------------------------".BrightCyan());
                    SelectPayment();
                    Console.Clear();


                    string title = film.Title;

                    // prchase test
                    List<int> seats = new();
                    List<Tuple<int, int, DateTime, int>> seatsInfo = new();
                    foreach (var seat in seatList)
                    {
                        int seatNumber = (seat[0]) * room.RoomWidth + seat[1];
                        seats.Add(seatNumber);
                        Tuple<int, int, DateTime, int> dateSeat = new Tuple<int, int, DateTime, int>(film.Id, room.Id, chosenDate, seatNumber);
                        room.Seats.Add(dateSeat);
                        seatsInfo.Add(dateSeat);
                        _roomsLogic!.UpdateList(room);
                    }
                    double ticketTotal = _ticketLogic!.TicketPurchase(room, seats);
                    // revenue vastmeten
                    RevenueModel rev = _revenueLogic!.GetById(1);
                    rev.Money += ticketTotal + food;
                    _revenueLogic.UpdateList(rev);
                    // naar json versturen
                    string reservationCode = ReservationCodeMaker();
                    //total amount krijgen
                    double totalAmount = ticketTotal + food;

                    int temp_id = _reservationsLogic!._reservations!.Count > 0 ? _reservationsLogic._reservations.Max(x => x.Id) + 1 : 1;

                    ReservationModel reservation = new(1, reservationCode, _account.FullName, _account.EmailAddress, title, seatList.Count, food, ticketTotal, room.Id, seatList, seatsInfo, totalAmount, chosenDate);
                    _reservationsLogic!.UpdateList(reservation);
                    bool account = true;
                    Console.Clear();
                    System.Console.Write("Sending conformation email please wait...".Orange() + $"\n");
                    MailConformation mailConformation = new MailConformation(_account.EmailAddress, account);
                    mailConformation.SendMailConformation();
                    _account.TicketList.Add(temp_id);
                    _accountsLogic!.UpdateList(_account);
                    break;
                case 1:
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Reservation has been cancelled.");
                    Console.ResetColor();
                    System.Console.WriteLine();
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey(true);
                    return;
            }
        }
        Console.ForegroundColor = ConsoleColor.Green;
        System.Console.WriteLine("Your reservation has been confirmed and is now guaranteed.");
        System.Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey(true);
    }

    public static double GetFoodAmount()
    {
        //foodlogic oproepen
        FoodsLogic foodLogic = new();
        string prompt = "Would you like to order food?";
        string[] options = { "Yes", "No" };
        Menu mainMenu = new Menu(prompt, options);
        int selectedIndex = mainMenu.Run();

        switch (selectedIndex)
        {
            case 0:
                double food = 0;
                double snacksTotal = foodLogic.BuyFood();
                food += snacksTotal;
                Console.WriteLine(snacksTotal);
                Console.WriteLine();
                food = NewFoodAmount(food);
                return food;
            case 1:
                return 0;
        }
        return 0;

    }

    public static void SelectPayment()
    {
        string prompt = "Select your choice of payment";
        string[] options = { "Paypal", "iDeal" };
        Menu mainMenu = new Menu(prompt, options);
        int selectedIndex = mainMenu.Run();

        switch (selectedIndex)
        {
            case 0:
                Payment.PaymentWithPayPal();
                break;

            case 1:
                Payment.PaymentWithIdeal();
                break;
        }
    }

    public static double NewFoodAmount(double current) // food  conformation
    {
        FoodsLogic foodLogic = new();
        string prompt = "Do you wish to change your snack selection?";
        string[] options = { "Yes", "No" };
        Menu mainMenu = new Menu(prompt, options);
        int selectedIndex = mainMenu.Run();
        switch (selectedIndex)
        {
            case 0:
                Console.Clear();
                double food = GetFoodAmount();
                return food;
            case 1:
                return current;
        }
        return current;
    }


    public static string ReservationCodeMaker()
    {
        Random random = new Random();
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        int length = 7;
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }
}
class Box // class voor vakjes seats
{
    public string? Value { get; set; }

}

public static class ConsoleExtensions
{
    // class voor de juiste kleuren zodat het sneller gaat met kleur oproepen(minder flikker)
    public static string Yellow(this string str)
    {
        return $"\u001b[33m{str}\u001b[0m";
    }

    public static string DarkMagenta(this string str)
    {
        return $"\u001b[35m{str}\u001b[0m";
    }

    public static string Magenta(this string str)
    {
        return $"\u001b[35m{str}\u001b[0m";
    }

    public static string Red(this string str)
    {
        return $"\u001b[31m{str}\u001b[0m";
    }

    public static string Green(this string str)
    {
        return $"\u001b[32m{str}\u001b[0m";
    }

    public static string White(this string str)
    {
        return $"\u001b[37m{str}\u001b[0m";
    }

    public static string Blue(this string str)
    {
        return $"\u001b[34m{str}\u001b[0m";
    }
    public static string Orange(this string str)
    {
        return $"\u001b[38;5;208m{str}\u001b[0m";
    }
    public static string DarkGray(this string str)
    {
        return $"\u001b[90m{str}\u001b[0m";
    }
    public static string LightBlue(this string str)
    {
        return $"\u001b[36m{str}\u001b[0m";
    }
    public static string LightRed(this string str)
    {
        return "\u001b[91m" + str + "\u001b[0m";
    }

    public static string LightGreen(this string str)
    {
        return "\u001b[92m" + str + "\u001b[0m";
    }
    public static string BrightYellow(this string str)
    {
        return $"\u001b[33;1m{str}\u001b[0m";
    }
    public static string BrightCyan(this string str)
    {
        return $"\u001b[36;1m{str}\u001b[0m";
    }
    public static string Turquoise(this string str)
    {
        return $"\u001b[46;30m{str}\u001b[0m";
    }
    public static string BrightWhite(this string str)
    {
        return $"\u001b[97;1m{str}\u001b[0m";
    }

}
