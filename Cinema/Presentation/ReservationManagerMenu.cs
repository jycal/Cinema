using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


public class ReservationManagerMenu
{
    public static void Start()
    {
        while (true)
        {
            ReservationsLogic res = new ReservationsLogic();
            Console.WriteLine("Enter 1 to view reservation info");
            Console.WriteLine("Enter 2 to look for specific reservation");
            Console.WriteLine("Enter 3 to delete reservation");
            // Console.WriteLine("Enter 4 to change reservation");
            Console.WriteLine("Enter 4 to go back to main menu");
            Console.WriteLine("Enter 5 to change seat price");

            string choice = Console.ReadLine()!;

            switch (choice)
            {
                case "1":
                    ReservationsLogic.ReservationOverview();
                    Console.WriteLine("");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    Console.Clear();
                    break;
                case "2":
                    System.Console.WriteLine("enter email adress");
                    string email = Console.ReadLine()!;
                    var reservation = res.GetByEmail(email);
                    if (reservation == null)
                    {
                        Console.WriteLine("Email not found");
                        continue;
                    }

                    foreach (var item in reservation.Seats)
                    {
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        int Seats = item;
                        string Overview = $@"
==================================================
|            CURRENT Reservation OVERVIEW        |
==================================================
  Movie: {reservation.Movie}
  Full Name: {reservation.FullName}
  Email: {reservation.Email}
  Ticket Amount: {reservation.TicketAmount}
  Seats: {Seats}|
  Total Money Amount: {reservation.TotalAmount}
==================================================";
                        Console.WriteLine(Overview);

                    }
                    Console.ResetColor();
                    Console.WriteLine("");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    Console.Clear();
                    break;
                case "3":
                    System.Console.WriteLine("enter email adress");
                    string email2 = Console.ReadLine()!;
                    res.DeleteReservation(email2);
                    Console.WriteLine("Reservation deleted");
                    Console.WriteLine("");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    Console.Clear();
                    break;
                // case "4":
                //     Console.WriteLine("enter current email adress");
                //     // ask old email
                //     string email4 = Console.ReadLine()!;
                //     ReservationModel oldReservation = res.GetByEmail(email4);
                //     if (oldReservation == null)
                //     {
                //         Console.WriteLine("Email not found");
                //         continue;
                //     }
                //     int id = oldReservation.Id;
                //     // make changes to rservation
                //     Console.WriteLine("enter full name");
                //     string fullName = Console.ReadLine()!;
                //     Console.WriteLine("enter email adress");
                //     string email3 = Console.ReadLine()!;
                //     Console.WriteLine("enter movie");
                //     string movie = Console.ReadLine()!;
                //     Console.WriteLine("enter ticket amount ");
                //     int ticketAmount = Convert.ToInt32(Console.ReadLine())!;
                //     List<int> seats = new List<int>();
                //     for (int i = 0; i < ticketAmount; i++)
                //     {
                //         Console.WriteLine("enter seats");
                //         int seat = Convert.ToInt32(Console.ReadLine())!;
                //         seats.Add(seat);
                //     }
                //     Console.WriteLine("enter total amount ");
                //     int totalAmount = Convert.ToInt32(Console.ReadLine())!;
                //     ReservationModel newReservation = new ReservationModel(id, fullName, email3, movie, ticketAmount, seats, totalAmount);
                //     res.UpdateList(newReservation);
                //     Console.WriteLine("Reservation changed");
                //     Console.WriteLine("");
                //     Console.WriteLine("Press any key to continue...");
                //     Console.ReadKey();
                //     Console.Clear();
                //     break;
                case "4":
                    Console.WriteLine("Going back to main menu");
                    Console.WriteLine("");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    Console.Clear();
                    return;
                case "5":
                    TicketLogic pl = new();
                    System.Console.WriteLine("1. normal\n2. comfort\n3. vip");
                    string choice1 = Console.ReadLine()!;
                    System.Console.WriteLine("enter new price");
                    double choice2 = Convert.ToDouble(Console.ReadLine()!);
                    pl.ChangeSeatPrice(choice1, choice2);
                    break;
            }
        }
    }
}