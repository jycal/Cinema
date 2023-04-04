using System.IO;
static class EnterRoom
{
    static private RoomsLogic roomsLogic = new RoomsLogic();


    public static void Start()
    {
        Console.WriteLine("Welcome to the movie rooms page");
        Console.WriteLine("Please enter the room number (1)\n");
        int number = int.Parse(Console.ReadLine()!);
        RoomModel room = roomsLogic.CheckEnter(number);
        if (room != null)
        {
            Console.WriteLine("Welcome to room " + room.RoomNumber);
            Console.WriteLine($"This room has {room.MaxSeats} seats");

            Display(room);
            Reserve(room);
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
        // example:
        // 1-5:   1  2  3  4  5
        // 6-10:  6  7  8  9  10
        // 11-15: 11 12 13 14 15
        // 16-20: 16 17 18 19 20
        List<int> reservedSeats = room.Seats;
        int amountOfSeats = room.MaxSeats;
        string lineSep = "\n---------------------------------------------------------------------------------------------------------------------------------------";
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("■: Unreserved seat");
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine("■: Reserved seat");
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("                                                            Screen");
        double row = 1;
        double plus;
        // if movie is in the big room increment with 0.05 (20 seats per row)
        if (room.Id == 3)
        {
            plus = 0.05;
        }
        // else 15 seats per row
        else { plus = 0.0666666667; }
        for (int i = 0; i <= amountOfSeats; i++)
        {
            // increment rows
            row += plus;
            string rows = row.ToString("0");
            if (i % 15 == 0)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(lineSep);
                string rowHeader = $"|{"Row: " + rows,16}: |";
                Console.Write(rowHeader);

                if (i == 0)
                {
                    continue;
                }
            }

            // check if chair in list
            if (reservedSeats.Contains(i))
            {
                // reserved seat = gray
                Console.ForegroundColor = ConsoleColor.DarkGray;
                var reservedSeat = "■";
                Console.Write($"{i,5} {reservedSeat}");
            }
            else
            {
                // unreserved seat = yellow
                Console.ForegroundColor = ConsoleColor.Yellow;
                var unreservedSeat = "■";
                Console.Write($"{i,5} {unreservedSeat}");
            }
        }
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine(lineSep);
    }


    public static void Reserve(RoomModel room)
    {
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
                room.Seats.Add(choice);
                roomsLogic.UpdateList(room);
            }
        }
    }
}