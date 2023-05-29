using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


public class VisualOverview
{
    static private RoomsLogic _roomsLogic = new();
    static private FilmsLogic? _filmsLogic;
    static private ReservationsLogic? _reservationsLogic;
    static private RevenueLogic? _revenueLogic;
    static private AccountModel _account = null!;
    static private AccountsLogic? _accountsLogic;
    static private GuestLogic? _guestLogic;
    static private TicketLogic? _ticketLogic;

    public VisualOverview()
    {

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
        Console.WriteLine("Please enter the movie id\n");
        int id = int.Parse(Console.ReadLine()!);
        FilmModel film = _filmsLogic!.GetById(id);
        if (film == null)
        {
            Console.WriteLine("No film found with that id");
            Console.WriteLine("Please try again.\n");
            //Write some code to go back to the menu
            Console.WriteLine("Press any key to go back to the menu");
            Console.ReadKey();
            Console.Clear();
            return;
        }
        DateTime chosenDate = film.Dates[0];
        int index = 0;
        if (film.Dates.Count > 0)
        {
            Console.WriteLine("Please choose the date you want to see the movie\n");
            int count = 1;
            foreach (var date in film.Dates)
            {
                Console.WriteLine($"{count}. {date}");
                count++;
            }
            int dateChoice = int.Parse(Console.ReadLine()!);
            if (dateChoice > film.Dates.Count || dateChoice < 1)
            {
                Console.WriteLine("Please enter a valid date");
                dateChoice = int.Parse(Console.ReadLine()!);
            }
            chosenDate = film.Dates[dateChoice - 1];
            index = dateChoice - 1;
        }
        // Console.WriteLine("Please enter the room number\n");

        // int number = int.Parse(Console.ReadLine()!);
        int number = film.Rooms[index];
        RoomModel room = _roomsLogic.CheckEnter(number);
        if (room != null)
        {
            // Ask the user for a number input
            int numBoxesToSelect = 0;
            do
            {
                Console.WriteLine("How many tickets would you like to order?");
                numBoxesToSelect = int.Parse(Console.ReadLine()!);
                if (String.IsNullOrEmpty(Convert.ToString(numBoxesToSelect)))
                {
                    System.Console.WriteLine("must enter number");
                    continue;
                }

            } while (numBoxesToSelect <= 0);
            Console.Clear();

            Run(room, film, chosenDate, numBoxesToSelect);
        }
        else
        {
            Console.WriteLine("No room found with that hall");
        }
    }

    public static void Run(RoomModel room, FilmModel film, DateTime chosenDate, int numBoxesToSelect)
    {

        // RoomModel room = _roomsLogic.CheckEnter(1);
        // FilmModel film = _filmsLogic!.GetById(3);

        List<int[]> selectedSeats = new List<int[]>();
        // List<int> rowList = new List<int>();

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
        // int numBoxesToSelect = 0; // Number of boxes to select
        int boxesSelected = 0; // Number of boxes currently selected
        List<int[]> selectedBoxes = new List<int[]>(); // List of selected boxes
        Console.SetCursorPosition(0, 0);
        Console.CursorVisible = false;
        Console.Clear();

        // Fill the box with black square symbols
        for (int i = 0; i < roomLength; i++)
        {
            for (int j = 0; j < roomWidth; j++)
            {
                // normale stoelen
                box[i, j] = new Box { Value = "■", IsBlue = false };
            }
        }

        // Print the initial box to the console
        PrintBox();

        // Move the cursor or select/deselect a box when keys are pressed
        while (boxesSelected < numBoxesToSelect)
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            Console.CursorVisible = false;

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
                        // The selected square is in the reserved list, so do nothing.
                        break;
                    }
                    else if (dontPrint.Contains(cursorRow * roomWidth + cursorCol))
                    {
                        // The selected square is in the dontprint list, so do nothing.
                        break;
                    }
                    else if (selectedBoxes.Any(box => box[0] == cursorRow && box[1] == cursorCol))
                    {
                        // Deselect the box
                        selectedBoxes.Remove(selectedBoxes.First(box => box[0] == cursorRow && box[1] == cursorCol));
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
            Console.WriteLine($"\nUse arrow keys to navigate. Press ENTER to select a seat.\n");
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



            // Print the rest of the box with 12 rows and 14 columns
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
                    else if (selectedBoxes.Any(box => box[0] == i && box[1] == j))
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

        // seats aan seatlist toevoegen
        Console.WriteLine($"\nYou have selected {boxesSelected} seats:");
        foreach (int[] seat in selectedBoxes)
        {
            Console.WriteLine($"- Row {seat[0] + 1}, Seat {seat[1] + 1}");
            selectedSeats.Add(seat);
            // if (!rowList.Contains(seat[0]))
            // { rowList.Add(seat[0]); }
        }
        Console.WriteLine("Press enter to continue...");
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
                Start(_account);
                break;
            case 2:
                break;

        }


    }
    public static void Reserve(RoomModel room, FilmModel film, List<int[]> seatList, DateTime chosenDate)
    {
        Console.Clear();
        FoodsLogic foodLogic = new();
        double food = GetFoodAmount();


        if (_account == null)
        {
            // gegevens vragen
            Console.Clear();
            // System.Console.WriteLine(food);
            Console.WriteLine("Email address:");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("-----------------------------");
            Console.WriteLine("|  Email must contain a @   |");
            Console.WriteLine("-----------------------------");
            Console.ResetColor();
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
            Console.Write("Please enter your full name: ");
            string fullName = Console.ReadLine()!;

            // payment 
            Console.WriteLine("Press enter to continue...");
            Console.ReadLine();
            Console.Clear();
            string prompt = "Do you want to proceed with the reservation?";
            string[] options = { "Yes", "No" };
            Menu mainMenu = new Menu(prompt, options);
            int selectedIndex = mainMenu.Run();
            switch (selectedIndex)
            {
                case 0:
                    Console.WriteLine("\n--------------------------------");
                    Console.WriteLine("         PAYMENT OPTIONS         ");
                    Console.WriteLine("--------------------------------");
                    Console.WriteLine("1. Paypal");
                    Console.WriteLine("2. Ideal");
                    Console.WriteLine("--------------------------------");
                    SelectPayment();
                    Console.Clear();

                    // info naar guest json sturen
                    string title = film.Title;
                    // prchase test
                    List<int> seats = new();
                    foreach (var seat in seatList)
                    {
                        int seatNumber = (seat[0]) * room.RoomWidth + seat[1];
                        seats.Add(seatNumber);
                        Tuple<int, int, DateTime, int> dateSeat = new Tuple<int, int, DateTime, int>(film.Id, room.Id, chosenDate, seatNumber);
                        room.Seats.Add(dateSeat);
                        _roomsLogic.UpdateList(room);
                    }
                    // foreach (int seat in seats)
                    // {
                    //     System.Console.WriteLine(seat);
                    // }
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
                    ReservationModel guest = new(1, reservationCode, fullName, email, title, seatList.Count, food, ticketTotal, room.Id, seatList, totalAmount, chosenDate);
                    _guestLogic!.UpdateList(guest);
                    // mail verzenden
                    bool account = false;
                    Console.Clear();

                    System.Console.Write("Sending conformation email please wait".Orange() + $"\n");
                    Console.Clear();
                    MailConformation mailConformation = new MailConformation(email, account);
                    mailConformation.SendMailConformation();

                    break;
                case 1:
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Reservation has been cancelled.");
                    Console.ResetColor();
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey(true);
                    return;

            }
        }

        else if (_account != null)
        {
            string prompt = "Do you want to proceed with the reservation?";
            string[] options = { "Yes", "No" };
            Menu mainMenu = new Menu(prompt, options);
            int selectedIndex = mainMenu.Run();

            switch (selectedIndex)
            {
                case 0:
                    System.Console.WriteLine(food);
                    Console.WriteLine("\n--------------------------------");
                    Console.WriteLine("         PAYMENT OPTIONS         ");
                    Console.WriteLine("--------------------------------");
                    Console.WriteLine("1. Paypal");
                    Console.WriteLine("2. Ideal");
                    Console.WriteLine("--------------------------------");
                    SelectPayment();
                    Console.Clear();

                    // film moet nog aan room gekoppeld worden dus nu title handmatig
                    string title = film.Title;

                    // prchase test
                    List<int> seats = new();
                    foreach (var seat in seatList)
                    { seats.Add(seat[1]); }
                    double ticketTotal = _ticketLogic!.TicketPurchase(room, seats);
                    // revenue vastmeten
                    RevenueModel rev = _revenueLogic!.GetById(1);
                    rev.Money += ticketTotal + food;
                    _revenueLogic.UpdateList(rev);
                    // naar json versturen
                    string reservationCode = ReservationCodeMaker();
                    //total amount krijgen
                    double totalAmount = ticketTotal + food;
                    int temp_id = film.Id = _reservationsLogic!._reservations!.Count > 0 ? _reservationsLogic._reservations.Max(x => x.Id) + 1 : 1;

                    ReservationModel reservation = new(temp_id, reservationCode, _account.FullName, _account.EmailAddress, title, seatList.Count, food, ticketTotal, room.Id, seatList, totalAmount, chosenDate);
                    _reservationsLogic.UpdateList(reservation);
                    bool account = true;
                    MailConformation mailConformation = new MailConformation(_account.EmailAddress, account);
                    mailConformation.SendMailConformation();
                    _account.TicketList.Add(temp_id);
                    _accountsLogic!.UpdateList(_account);
                    break;
                case 1:
                    Console.WriteLine("Reservation cancelled.");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey(true);
                    return;
            }
        }
        Console.ForegroundColor = ConsoleColor.Green;
        System.Console.WriteLine("Your reservation has been confirmed and is now guaranteed.");
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

    public static double NewFoodAmount(double current)
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
class Box
{
    public string? Value { get; set; }
    public bool IsBlue { get; set; }
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

}
