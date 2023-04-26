static class EnterRoom
{
    static private RoomsLogic _roomsLogic = new();
    static private FilmsLogic _filmsLogic = new();
    static private ReservationsLogic _reservationsLogic = new();
    static private AccountModel _account = null!;
    public static void Start(AccountModel account)
    {
        _account = account;
        Console.WriteLine("Welcome to the movie rooms page");
        Console.WriteLine("Please enter the movie id\n");
        int id = int.Parse(Console.ReadLine()!);
        FilmModel film = _filmsLogic.GetById(id);
        if (film == null)
        {
            Console.WriteLine("No film found with that id");
            Console.WriteLine("Please try again.\n");
            //Write some code to go back to the menu
            Console.WriteLine("Press any key to go back to the menu");
            Console.ReadKey();
            Console.Clear();
            Menu.MainMenu();
            return;
        }
        Console.WriteLine("Please enter the room number\n");

        int number = int.Parse(Console.ReadLine()!);
        RoomModel room = _roomsLogic.CheckEnter(number);
        if (room != null)
        {
            Console.WriteLine("Welcome to room " + room.RoomNumber);
            Console.WriteLine($"This room has {room.MaxSeats} seats");

            Display(room);
            Reserve(room, id);
            //Write some code to go back to the menu
            //Menu.Start();
        }
        else
        {
            Console.WriteLine("No room found with that hall");
        }
    }

    // public static void Display(RoomModel room)
    // {
    //     // example:
    //     // 1-5:   1  2  3  4  5
    //     // 6-10:  6  7  8  9  10
    //     // 11-15: 11 12 13 14 15
    //     // 16-20: 16 17 18 19 20
    //     List<int> reservedSeats = room.Seats;
    //     List<int> vipSeats = room.VipSeats;
    //     List<int> disabledSeats = room.DisabledSeats;
    //     int amountOfSeats = room.MaxSeats;
    //     string lineSep = "\n---------------------------------------------------------------------------------------------------------------------------------------";
    //     Console.ForegroundColor = ConsoleColor.Yellow;
    //     Console.WriteLine("■: Unreserved seat");
    //     Console.ForegroundColor = ConsoleColor.DarkGray;
    //     Console.WriteLine("■: Reserved seat");
    //     Console.ForegroundColor = ConsoleColor.Red;
    //     Console.WriteLine("♥: VIP seat");
    //     Console.ForegroundColor = ConsoleColor.Green;
    //     Console.WriteLine("▲: Disability seat");
    //     Console.ForegroundColor = ConsoleColor.White;
    //     Console.WriteLine("                                                            Screen");
    //     double row = 1;
    //     double plus;
    //     // if movie is in the big room increment with 0.05 (20 seats per row)
    //     if (room.Id == 3)
    //     {
    //         plus = 0.05;
    //     }
    //     // else 15 seats per row
    //     else { plus = 0.0666666667; }
    //     for (int i = 0; i <= amountOfSeats; i++)
    //     {
    //         // increment rows
    //         row += plus;
    //         string rows = row.ToString("0");
    //         if (i % 15 == 0)
    //         {
    //             Console.ForegroundColor = ConsoleColor.White;
    //             Console.WriteLine(lineSep);
    //             string rowHeader = $"|{"Row: " + rows,15}: |";
    //             Console.Write(rowHeader);
    //             // if (i == 0)
    //             // {
    //             //     continue;
    //             // }


    //         }

    //         // check if chair in list
    //         if (reservedSeats.Contains(i))
    //         {
    //             // reserved seat = gray
    //             Console.ForegroundColor = ConsoleColor.DarkGray;
    //             var reservedSeat = "■";
    //             if (vipSeats.Contains(i))
    //             {
    //                 char vipSeat = '♥';
    //                 Console.Write($"{i,5} {vipSeat}");
    //             }
    //             else if (disabledSeats.Contains(i))
    //             {
    //                 // disability seats = green
    //                 char disabilitySeat = '▲';
    //                 Console.ForegroundColor = ConsoleColor.Green;
    //                 Console.Write($"{i,5} {disabilitySeat}");
    //             }
    //             else
    //             { Console.Write($"{i,5} {reservedSeat}"); }
    //         }
    //         else
    //         {
    //             // unreserved seat = yellow
    //             Console.ForegroundColor = ConsoleColor.Yellow;
    //             var unreservedSeat = "■";
    //             if (vipSeats.Contains(i))
    //             {
    //                 // vip seat = rood
    //                 Console.ForegroundColor = ConsoleColor.Red;
    //                 char vipSeat = '♥';
    //                 Console.Write($"{i,5} {vipSeat}");
    //             }
    //             else
    //             { Console.Write($"{i,5} {unreservedSeat}"); }
    //         }

    //     }
    //     Console.ForegroundColor = ConsoleColor.White;
    //     Console.WriteLine(lineSep);
    // }


    public static void Display(RoomModel room)
    {
        List<int> reservedSeats = room.Seats;
        List<int> vipSeats = room.VipSeats;
        List<int> disabledSeats = room.DisabledSeats;
        List<int> comfortSeats = room.ComfortSeats;
        int amountOfSeats = room.MaxSeats;
        string lineSep = "\n---------------------------------------------------------------------------------------------------------------------------------------";
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("■: Unreserved seat");
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine("■: Reserved seat");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("♥: VIP seat");
        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.WriteLine("♥: Comfort seat");
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("▲: Disability seat");
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("                                                                Screen");

        double row = 1;
        // double plus;
        // // if movie is in the big room increment with 0.05 (20 seats per row)
        // if (room.Id == 3)
        // {
        //     plus = 0.05;
        // }
        // // else 15 seats per row
        // else { plus = 0.0666666667; }

        for (int i = 1; i <= amountOfSeats; i++)
        {
            // increment rows
            // row += plus;
            // string rows = row.ToString("0");

            if (i == 1 || (i - 1) % 15 == 0)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(lineSep);
                string rowHeader = $"|{"Row: " + row,15}: |";
                Console.Write(rowHeader);
                row++;
            }

            // check if chair in list
            if (reservedSeats.Contains(i))
            {
                // reserved seat = gray
                Console.ForegroundColor = ConsoleColor.DarkGray;
                var reservedSeat = "■";
                if (vipSeats.Contains(i))
                {
                    char vipSeat = '♥';
                    Console.Write($"{i,5} {vipSeat}");
                }
                else if (comfortSeats.Contains(i))
                {
                    char comfortSeat = '♥';
                    // Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write($"{i,5} {comfortSeat}");
                }
                else if (disabledSeats.Contains(i))
                {
                    // disability seats = green
                    char disabilitySeat = '▲';
                    Console.Write($"{i,5} {disabilitySeat}");
                }
                else
                { Console.Write($"{i,5} {reservedSeat}"); }
            }
            else
            {
                // unreserved seat = yellow
                Console.ForegroundColor = ConsoleColor.Yellow;
                var unreservedSeat = "■";
                if (vipSeats.Contains(i))
                {
                    // vip seat = rood
                    Console.ForegroundColor = ConsoleColor.Red;
                    char vipSeat = '♥';
                    Console.Write($"{i,5} {vipSeat}");
                }
                else if (comfortSeats.Contains(i))
                {
                    char comfortSeat = '♥';
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.Write($"{i,5} {comfortSeat}");
                }
                else if (disabledSeats.Contains(i))
                {
                    // disability seats = green
                    char disabilitySeat = '▲';
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write($"{i,5} {disabilitySeat}");
                }
                else
                { Console.Write($"{i,5} {unreservedSeat}"); }
            }

        }
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine(lineSep);
    }


    public static void Reserve(RoomModel room, int id)
    {
        Console.WriteLine("Please enter the seat number:");
        int choice = int.Parse(Console.ReadLine()!);
        if (choice <= room.MaxSeats)
        {
            if (room.Seats.Contains(choice) && choice != 0)
            {
                Console.WriteLine("Seat is already reserved. Pick again.");
            }
            else if (choice == 0)
            {
                Console.WriteLine("error");
            }
            else
            {

                if (_account == null)
                {
                    // gegevens vragen
                    Console.Write("Enter email: ");
                    string email = Console.ReadLine()!;
                    Console.Write("Enter full name: ");
                    string fullName = Console.ReadLine()!;
                    // payment 
                    Console.WriteLine("Press enter to continue...");
                    Console.ReadLine();
                    Console.Clear();
                    Console.WriteLine("\n--------------------------------");
                    Console.WriteLine("         PAYMENT OPTIONS         ");
                    Console.WriteLine("--------------------------------");
                    Console.WriteLine("1. Paypal");
                    Console.WriteLine("2. Ideal");
                    Console.WriteLine("--------------------------------");
                    Console.Write("Enter your choice of payment: ");
                    string? answer2 = Console.ReadLine();
                    Console.Clear();
                    Payment.PaymentWithPayPal(answer2!);
                    Payment.PaymentWithIdeal(answer2!);
                    room.Seats.Add(choice);
                    _roomsLogic.UpdateList(room);
                    // info naar guest json sturen
                    List<int> seatList = new();
                    seatList.Add(choice);
                    FilmModel film = _filmsLogic.GetById(id);
                    string title = film.Title;
                    // guest naar json sturen
                    string reservationCode = ReservationCodeMaker();
                    ReservationModel guest = new(1, reservationCode, fullName, email, title, 1, 10, seatList, 10);
                    GuestLogic logic = new();
                    logic.UpdateList(guest);
                    // mail verzenden
                    bool account = false;
                    MailConformation mailConformation = new MailConformation(email, account);
                    mailConformation.SendMailConformation();
                }

                else if (_account != null)
                {
                    Console.WriteLine("\n--------------------------------");
                    Console.WriteLine("         PAYMENT OPTIONS         ");
                    Console.WriteLine("--------------------------------");
                    Console.WriteLine("1. Paypal");
                    Console.WriteLine("2. Ideal");
                    Console.WriteLine("--------------------------------");
                    Console.Write("Enter your choice of payment: ");
                    string? answer = Console.ReadLine();
                    Console.Clear();
                    Payment.PaymentWithPayPal(answer!);
                    Payment.PaymentWithIdeal(answer!);


                    room.Seats.Add(choice);
                    _roomsLogic.UpdateList(room);
                    // film moet nog aan room gekoppeld worden dus nu title handmatig
                    FilmModel film = _filmsLogic.GetById(id);
                    string title = film.Title;
                    List<int> seatList = new();
                    seatList.Add(choice);
                    string reservationCode = ReservationCodeMaker();
                    ReservationModel reservation = new(1, reservationCode, _account.FullName, _account.EmailAddress, title, 1, 10, seatList, 1);
                    _reservationsLogic.UpdateList(reservation);
                    bool account = true;
                    MailConformation mailConformation = new MailConformation(_account.EmailAddress, account);
                    mailConformation.SendMailConformation();
                }
                Console.ForegroundColor = ConsoleColor.Green;
                System.Console.WriteLine("Your reservation has been confirmed and is now guaranteed.");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
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