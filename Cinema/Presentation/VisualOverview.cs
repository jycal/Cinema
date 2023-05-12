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
        RoomModel room = _roomsLogic.CheckEnter(2);
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
                    if (dontPrint.Any(box => box == cursorRow && box == cursorCol))
                    {
                        // The selected square is in the blueSquares list, so do nothing.
                        break;
                    }
                    else if (!selectedBoxes.Any(box => box[0] == cursorRow && box[1] == cursorCol))
                    {
                        // Select the box at the cursor's position
                        selectedBoxes.Add(new int[] { cursorRow, cursorCol });
                        boxesSelected++;
                        PrintBox();
                    }
                    else
                    {
                        // Deselect the box
                        selectedBoxes.Remove(selectedBoxes.First(box => box[0] == cursorRow && box[1] == cursorCol));
                        boxesSelected--;
                        PrintBox();
                    }
                    break;

                default:
                    break;
            }
        }
        void PrintBox()
        {
            Console.SetCursorPosition(0, 0);
            Console.CursorVisible = false;
            Console.Clear();

            // Console.Clear();

            // Print column numbers
            Console.Write("  ");
            Console.Write("  ");
            for (int j = 1; j < roomWidth; j++)
            {
                Console.Write($" {j}");
            }
            Console.WriteLine();

            // Print the rest of the box with 12 rows and 14 columns
            for (int i = 0; i < roomLength; i++)
            {
                // Print row number to the left of the row
                Console.Write($"|{"Row: " + i,10} |");

                for (int j = 0; j < roomWidth; j++)
                {
                    if (i == cursorRow && j == cursorCol)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("■ ");
                        Console.ResetColor();
                    }
                    else if (disabledSeats.Contains(i * roomWidth + j))
                    {
                        Console.ForegroundColor = ConsoleColor.DarkMagenta;
                        Console.Write("■ ");
                        Console.ResetColor();
                    }
                    else if (comfortSeats.Contains(i * roomWidth + j))
                    {
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.Write("■ ");
                        Console.ResetColor();
                    }
                    else if (vipSeats.Contains(i * roomWidth + j))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("■ ");
                        Console.ResetColor();
                    }
                    else if (dontPrint.Contains(i * roomWidth + j))
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("■ ");
                        Console.ResetColor();
                    }
                    else if (selectedBoxes.Any(box => box[0] == i && box[1] == j))
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("■ ");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write("■ ");
                        Console.ResetColor();
                    }
                }
                Console.WriteLine();
            }



            // Print an empty line for spacing
            Console.WriteLine();

        }
    }
    class Box
    {
        public string Value { get; set; }
        public bool IsBlue { get; set; }
    }
}
