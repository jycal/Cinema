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
        int amountOfSeats = room.MaxSeats;
        string lineSep = "\n---------------------------------------------------------------------------------------------------------------------------------------";
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("■: Unreserved seat");
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine("■: Reserved seat");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("♥: VIP seat");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("▲: Disability seat");
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("                                                            Screen");

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
                else if (disabledSeats.Contains(i))
                {
                    // disability seats = green
                    char disabilitySeat = '▲';
                    Console.ForegroundColor = ConsoleColor.Green;
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
                else
                { Console.Write($"{i,5} {unreservedSeat}"); }
            }

        }
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine(lineSep);
    }


    public static void Reserve(RoomModel room, int id)
    {
        Console.WriteLine("Seat(s):");
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
                    // guest
                    Console.Write("Enter email: ");
                    string email = Console.ReadLine()!;
                    Console.WriteLine("Press enter to continue...");
                    Console.ReadLine();
                    Console.Clear();
                }
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

                System.Console.WriteLine("Your reservation has been confirmed and is now guaranteed.");
                room.Seats.Add(choice);
                _roomsLogic.UpdateList(room);
                if (_account != null)
                {
                    FilmModel film = _filmsLogic.GetById(id);
                    string title = film.Title;
                    List<int> seatList = new();
                    seatList.Add(choice);
                    ReservationModel reservation = new(1, _account.FullName, _account.EmailAddress, title, 1, seatList, 1);
                    _reservationsLogic.UpdateList(reservation);
                }
                Console.WriteLine("Reservation succeeded");
            }
        }
    }
}