// public class ReservationManagerMenu
// {
//     private static ReservationsLogic _res = new ReservationsLogic();

//     public static void Start()
//     {
//         // prompt
        


//         string choice = Console.ReadLine()!;

//         switch (choice)
//         {
//             case "1":
//                 ReservationsLogic.ReservationOverview();
//                 Console.WriteLine();
//                 Console.WriteLine("Press any key to continue...");
//                 Console.ReadKey(true);
//                 Console.Clear();
//                 break;
//             case "2":
//                 SearchReservation();
//             case "3":
//                 System.Console.WriteLine("enter email adress");
//                 string email2 = Console.ReadLine()!;
//                 _res.DeleteReservation(email2);
//                 Console.WriteLine("Reservation deleted");
//                 Console.WriteLine();
//                 Console.WriteLine("Press any key to continue...");
//                 Console.ReadKey(true);
//                 Console.Clear();
//                 break;
//             // case "4":
//             //     Console.WriteLine("enter current email adress");
//             //     // ask old email
//             //     string email4 = Console.ReadLine()!;
//             //     ReservationModel oldReservation = res.GetByEmail(email4);
//             //     if (oldReservation == null)
//             //     {
//             //         Console.WriteLine("Email not found");
//             //         continue;
//             //     }
//             //     int id = oldReservation.Id;
//             //     // make changes to rservation
//             //     Console.WriteLine("enter full name");
//             //     string fullName = Console.ReadLine()!;
//             //     Console.WriteLine("enter email adress");
//             //     string email3 = Console.ReadLine()!;
//             //     Console.WriteLine("enter movie");
//             //     string movie = Console.ReadLine()!;
//             //     Console.WriteLine("enter ticket amount ");
//             //     int ticketAmount = Convert.ToInt32(Console.ReadLine())!;
//             //     List<int> seats = new List<int>();
//             //     for (int i = 0; i < ticketAmount; i++)
//             //     {
//             //         Console.WriteLine("enter seats");
//             //         int seat = Convert.ToInt32(Console.ReadLine())!;
//             //         seats.Add(seat);
//             //     }
//             //     Console.WriteLine("enter total amount ");
//             //     int totalAmount = Convert.ToInt32(Console.ReadLine())!;
//             //     ReservationModel newReservation = new ReservationModel(id, fullName, email3, movie, ticketAmount, seats, totalAmount);
//             //     res.UpdateList(newReservation);
//             //     Console.WriteLine("Reservation changed");
//             //     Console.WriteLine("");
//             //     Console.WriteLine("Press any key to continue...");
//             //     Console.ReadKey();
//             //     Console.Clear();
//             //     break;
//             case "4":
//                 Console.WriteLine("Going back to main menu");
//                 Console.WriteLine("");
//                 Console.WriteLine("Press any key to continue...");
//                 Console.ReadKey(true);
//                 Console.Clear();
//                 return;
//         }
//     }
// }