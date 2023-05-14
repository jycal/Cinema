using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


public class ReservationsLogic
{
    public List<ReservationModel>? _reservations;
    public RevenueLogic _revenueLogic;

    //Static properties are shared across all instances of the class
    //This can be used to get the current logged in account from anywhere in the program
    //private set, so this can only be set by the class itself
    static public ReservationModel? CurrentFilm { get; private set; }

    public ReservationsLogic()
    {
        if (CinemaMenus._revenueLogic == null)
        {
            CinemaMenus._revenueLogic = new RevenueLogic();
            _revenueLogic = CinemaMenus._revenueLogic;
        }
        else
        {
            _revenueLogic = CinemaMenus._revenueLogic;
        }

        _reservations = ReservationAccess.LoadAll();
    }

    public void UpdateList(ReservationModel film)
    {
        // create id
        // System.Console.WriteLine(film.Id);
        film.Id = _reservations!.Count > 0 ? _reservations.Max(x => x.Id) + 1 : 1;
        // System.Console.WriteLine(film.Id);
        //Find if there is already an model with the same id
        int index = _reservations.FindIndex(s => s.Id == film.Id);

        if (index != -1)
        {
            //update existing model
            _reservations[index] = film;
        }
        else
        {
            //add new model
            _reservations.Add(film);
        }
        ReservationAccess.WriteAll(_reservations);

    }

    public void DeleteReservation(int id)
    {
        //Find if there is already an model with the same id
        var reservation = _reservations!.Find(r => r.Id == id);
        int index = _reservations.FindIndex(s => s.Id == reservation!.Id);

        if (index != -1)
        {
            //update existing model
            // _films[index] = film;
            _revenueLogic.RemoveRevenue(reservation!.RevenueId);
            _reservations.RemoveAt(index);
            _reservations.ForEach((x) => { if (x.Id > reservation!.Id) x.Id = x.Id - 1; });
            ReservationAccess.WriteAll(_reservations);
        }
        else
        {
            Console.WriteLine($"reservation not found");

        }
    }

    public void DeleteReservationByCode(string reservationCode)
    {
        var check = GetByCode(reservationCode!);
        if (check != null)
        {//Find if there is already an model with the same id
            var reservation = _reservations!.Find(r => r.ReservationCode == reservationCode);
            int index = _reservations.FindIndex(s => s.Id == reservation!.Id);

            if (index != -1)
            {
                //update existing model
                // _films[index] = film;
                _revenueLogic.RemoveRevenue(reservation!.RevenueId);
                _reservations.RemoveAt(index);
                _reservations.ForEach((x) => { if (x.Id > reservation!.Id) x.Id = x.Id - 1; });
                ReservationAccess.WriteAll(_reservations);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\nReservation deleted\n");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\nReservation not found\n");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }

    public static void ReservationOverview()
    {
        var ReservationsFromJson = ReservationAccess.LoadAll();
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine(@"==================================================
|                                                 |
|                  Reservations                   |
|                                                 |
==================================================");
        foreach (ReservationModel reservation in ReservationsFromJson)
        {


            int ID = reservation.Id;
            string Movie = reservation.Movie;
            string FullName = reservation.FullName;
            string Email = reservation.Email;
            int TicketAmount = reservation.TicketAmount;
            string selectedSeats = string.Join(", ", reservation.Seats.Select(seat => $"Row {seat[0] + 1}, Seat {seat[1] + 1}"));
            double TotalAmount = reservation.TotalAmount;
            string Overview = $@"

  ID: {ID}
  Movie: {Movie}
  Full Name: {FullName}
  Email: {Email}
  Ticket Amount: {TicketAmount}
  Seats: {selectedSeats}
  Total Money Amount: {TotalAmount}

==================================================";
            Console.WriteLine(Overview);
            Console.ResetColor();

        }
    }

    public ReservationModel GetByEmail(string email)
    {
        return _reservations!.Find(i => i.Email == email)!;
    }

    public ReservationModel GetById(int id)
    {
        return _reservations!.Find(i => i.Id == id)!;
    }

    public ReservationModel GetByCode(string reservationCode)
    {
        return _reservations!.Find(i => i.ReservationCode == reservationCode)!;
    }
}