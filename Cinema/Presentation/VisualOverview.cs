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


    public static void Run()
    {
        RoomModel room = _roomsLogic.CheckEnter(1);
        FilmModel film = _filmsLogic!.GetById(3);

        List<int> selectedSeats = new List<int>();

        List<int> reservedSeats = room.Seats;
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
        int numBoxesToSelect = 0; // Number of boxes to select
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
        // Ask the user for a number input
        Console.WriteLine("How many tickets would you like to order?");
        numBoxesToSelect = int.Parse(Console.ReadLine()!);

        // Print the initial box to the console
        PrintBox();

        // Move the cursor or select/deselect a box when keys are pressed
        while (boxesSelected < numBoxesToSelect)
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);

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
                    if (reservedSeats.Contains(cursorRow * roomWidth + cursorCol))
                    {
                        // The selected square is in the reserved list, so do nothing.
                        break;
                    }
                    else if (dontPrint.Contains(cursorRow * roomWidth + cursorCol))
                    {
                        // The selected square is in the blueSquares list, so do nothing.
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
            Console.Clear();

            // Print column numbers
            Console.Write("  ");
            for (int j = 1; j < roomWidth; j++)
            {
                Console.Write($"{j} ");
            }
            Console.WriteLine();

            // Print the rest of the box with 12 rows and 14 columns
            for (int i = 0; i < roomLength; i++)
            {
                // Print row number to the left of the row
                Console.Write($"|{"Row: " + i,10} |");

                for (int j = 0; j < roomWidth; j++)
                {
                    int seatNumber = i * roomWidth + j;
                    if (i == cursorRow && j == cursorCol)
                    {
                        Console.Write("■".Yellow() + " ");
                    }
                    else if (reservedSeats.Contains(seatNumber))
                    {
                        Console.Write("■".DarkGray() + " ");
                    }
                    else if (disabledSeats.Contains(seatNumber))
                    {
                        Console.Write("■".DarkMagenta() + " ");
                    }
                    else if (comfortSeats.Contains(seatNumber))
                    {
                        Console.Write("■".Magenta() + " ");
                    }
                    else if (vipSeats.Contains(seatNumber))
                    {
                        Console.Write("■".Red() + " ");
                    }
                    else if (dontPrint.Contains(seatNumber))
                    {
                        Console.Write("■".White() + " ");
                    }
                    else if (selectedBoxes.Any(box => box[0] == i && box[1] == j))
                    {
                        Console.Write("■".Green() + " ");
                    }
                    else
                    {
                        Console.Write("■".Blue() + " ");
                    }
                }
                Console.WriteLine();
            }
        }
        // Print an empty line for spacing
        Console.WriteLine();
    }
}
class Box
{
    public string? Value { get; set; }
    public bool IsBlue { get; set; }
}

public static class ConsoleExtensions
{
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
    public static string DarkGray(this string str)
    {
        return $"\u001b[90m{str}\u001b[0m";
    }
}
