static class EnterRoom
{
    static private RoomsLogic _roomsLogic = new();
    static private FilmsLogic _filmsLogic = new();
    static private ReservationsLogic _reservationsLogic = new();
    static private AccountModel _account = null!;
    private static int id;

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

            int currentSeat = 0;

            Display(room);
            // Reserve(room, id, currentSeat);
            //Write some code to go back to the menu
            //Menu.Start();
        }
        else
        {
            Console.WriteLine("No room found with that hall");
        }
    }

    public static void Display(RoomModel room)
    {

        List<int> reservedSeats = room.Seats;
        List<int> vipSeats = room.VipSeats;
        List<int> disabledSeats = room.DisabledSeats;
        int amountOfSeats = room.MaxSeats;
        int currentSeat = -1;

        Console.CursorVisible = false;

        Console.WriteLine("How many tickets would you like to order?");
        int numTickets = int.Parse(Console.ReadLine()!);

        List<int> selectedSeats = new List<int>();
        while (selectedSeats.Count <= numTickets)
        {
            Console.Clear();

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
            double plus;
            if (room.Id == 3)
            {
                plus = 0.05;
            }
            else
            {
                plus = 0.0666666667;
            }
            for (int i = 0; i < amountOfSeats; i++)
            {
                row += plus;
                string rows = row.ToString("0");

                if (i % 15 == 0)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("\n---------------------------------------------------------------------------------------------------------------------------------------");
                    string rowHeader = $"|{"Row: " + rows,15}: |";
                    Console.Write(rowHeader);
                }

                if (i == currentSeat)
                {
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                }

                if (reservedSeats.Contains(i))
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    var reservedSeat = "■";

                    if (vipSeats.Contains(i))
                    {
                        char vipSeat = '♥';
                        Console.Write($"{i,5} {vipSeat}");
                    }
                    else if (disabledSeats.Contains(i))
                    {
                        char disabilitySeat = '▲';
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write($"{i,5} {disabilitySeat}");
                    }
                    else
                    {
                        Console.Write($"{i,5} {reservedSeat}");
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    var unreservedSeat = "■";

                    if (vipSeats.Contains(i))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        char vipSeat = '♥';
                        Console.Write($"{i,5} {vipSeat}");
                    }
                    else
                    {
                        Console.Write($"{i,5} {unreservedSeat}");
                    }
                }

                Console.BackgroundColor = ConsoleColor.Black;
            }

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\n---------------------------------------------------------------------------------------------------------------------------------------");

            Console.WriteLine("Use arrow keys to navigate. Press ENTER to reserve the selected seat.");

            ConsoleKeyInfo keyInfo = Console.ReadKey(true);

            switch (keyInfo.Key)
            {
                case ConsoleKey.UpArrow:
                    if (currentSeat >= 15)
                    {
                        currentSeat -= 15;
                    }
                    break;
                case ConsoleKey.DownArrow:
                    if (currentSeat < amountOfSeats - 15)
                    {
                        currentSeat += 15;
                    }
                    break;
                case ConsoleKey.LeftArrow:
                    if (currentSeat > 0)
                    {
                        currentSeat--;
                    }
                    break;
                case ConsoleKey.RightArrow:
                    if (currentSeat < amountOfSeats - 1)
                    {
                        currentSeat++;
                    }
                    break;
                case ConsoleKey.Enter:
                    if (!reservedSeats.Contains(currentSeat))
                    {
                        selectedSeats.Add(currentSeat);
                        System.Console.WriteLine(selectedSeats);
                        if (currentSeat >= 0 && currentSeat < amountOfSeats)
                        {
                            if (selectedSeats.Count == numTickets)
                            {
                                Console.Clear();
                                Console.WriteLine($"You have selected the following seats: {string.Join(",", selectedSeats)}");

                                Console.WriteLine("\nPress any key to confirm your reservation.");
                                Console.ReadKey(true);

                                // foreach (var seat in selectedSeats)
                                // {
                                //     Reserve(room, id, seat);
                                //     room.Seats.Add(seat);
                                //     _roomsLogic.UpdateList(room);
                                // }
                                Reserve(room, id, selectedSeats);
                                foreach (var seat in selectedSeats)
                                { room.Seats.Add(seat); }
                                _roomsLogic.UpdateList(room);

                                Console.WriteLine("Reservation successful! Press any key to return to the main menu.");
                                Console.ReadKey(true);

                                return;
                            }


                        }
                    }
                    else if (reservedSeats.Contains(currentSeat))
                    {
                        Console.WriteLine("Seat is already reserved. Pick again.");
                    }
                    break;
            }
        }
    }

    public static void Reserve(RoomModel room, int id, List<int> seatList)
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

            // info naar guest json sturen

            FilmModel film = _filmsLogic.GetById(id);
            string title = film.Title;
            // guest naar json sturen
            string reservationCode = ReservationCodeMaker();
            ReservationModel guest = new(1, reservationCode, fullName, email, title, 1, 10, room.Id, seatList, 10);
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


            // film moet nog aan room gekoppeld worden dus nu title handmatig
            FilmModel film = _filmsLogic.GetById(id);
            string title = film.Title;

            string reservationCode = ReservationCodeMaker();
            ReservationModel reservation = new(1, reservationCode, _account.FullName, _account.EmailAddress, title, 1, 10, room.Id, seatList, 1);
            _reservationsLogic.UpdateList(reservation);
            bool account = true;
            MailConformation mailConformation = new MailConformation(_account.EmailAddress, account);
            mailConformation.SendMailConformation();
        }

        Console.ForegroundColor = ConsoleColor.Green;
        System.Console.WriteLine("Your reservation has been confirmed and is now guaranteed.");
        Console.ForegroundColor = ConsoleColor.White;

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
