using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


public class ReservationManagerMenu
{
    public static void Start()
    {
        Console.WriteLine("Enter 1 to view reservation info");
        Console.WriteLine("Enter 2 to look for specific reservation");
        Console.WriteLine("Enter 3 to delete reservation");
        Console.WriteLine("Enter 4 to change reservation");

        string choice = Console.ReadLine()!;

        switch (choice)
        {
            case "1":
                ReservationsLogic.ReservationOverview();
                break;
            case "2":
                System.Console.WriteLine("enter email adress");
                string email = Console.ReadLine()!;
                ReservationsLogic.GetByEmail(email);
                break;
            case "3":
                System.Console.WriteLine("enter email adress");
                string email2 = Console.ReadLine()!;
                ReservationsLogic.DeleteReservation(email2);
                break;
            case "4":
                Console.WriteLine("enter current email adress");
                // ask old email
                string email4 = Console.ReadLine()!;
                ReservationModel oldReservation = ReservationsLogic.GetByEmail(email4);
                int id = oldReservation.Id;
                // make changes to rservation
                Console.WriteLine("enter full name");
                string fullName = Console.ReadLine()!;
                Console.WriteLine("enter email adress");
                string email3 = Console.ReadLine()!;
                Console.WriteLine("enter movie");
                string movie = Console.ReadLine()!;
                Console.WriteLine("enter ticket amount ");
                int ticketAmount = Convert.ToInt32(Console.ReadLine())!;
                List<int> seats = new List<int>();
                for (int i = 0; i < ticketAmount; i++)
                {
                    Console.WriteLine("enter seats");
                    int seat = Convert.ToInt32(Console.ReadLine())!;
                    seats.Add(seat);
                }
                Console.WriteLine("enter total amount ");
                int totalAmount = Convert.ToInt32(Console.ReadLine())!;
                ReservationModel newReservation = new ReservationModel(id, fullName, email3, movie, ticketAmount, seats, totalAmount);
                ReservationsLogic.UpdateList(newReservation);
                break;

        }


    }


}
