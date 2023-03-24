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

            //Write some code to go back to the menu
            //Menu.Start();
        }
        else
        {
            Console.WriteLine("No room found with that hall");
        }
    }
}