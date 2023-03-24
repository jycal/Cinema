static class EnterRoom
{
    static private RoomsLogic roomsLogic = new RoomsLogic();


    public static void Start()
    {
        Console.WriteLine("Welcome to the movie rooms page");
        Console.WriteLine("Please enter the room number");
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
        List<bool> reservedSeats = room.Seats;
        int amountOfSeats = room.MaxSeats;
        string lineSep = "\n-------------------------------------------------";
        Console.WriteLine("O: Unreserved seat, X: Reserved seat");
        for (int i = 0; i < amountOfSeats; i++)
        {
            if (i % 5 == 0)
            {
                Console.WriteLine(lineSep);
                string rowHeader = $"|{"Seats",5}: |";
                Console.Write(rowHeader);
            }
            Console.Write($"{i + 1,5}{(reservedSeats.ElementAt(i) ? " X" : " O")}|");
        }
        Console.WriteLine(lineSep);
    }

    public static void Reserve(RoomModel room)
    {
        int choice = int.Parse(Console.ReadLine()!) - 1;
        if (choice <= room.MaxSeats)
        {
            if (room.Seats[choice])
            {
                Console.WriteLine("Seat is already resverded. Pick again.");
            }
            else
            {
                room.Seats[choice] = true;
                roomsLogic.UpdateList(room);
            }
        }
    }
}