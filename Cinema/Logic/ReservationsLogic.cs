using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


class ReservationsLogic
{
    private List<ReservationModel> _reservations;

    //Static properties are shared across all instances of the class
    //This can be used to get the current logged in account from anywhere in the program
    //private set, so this can only be set by the class itself
    static public ReservationModel? CurrentFilm { get; private set; }

    public ReservationsLogic()
    {
        _reservations = ReservationAccess.LoadAll();
    }

    public void UpdateList(ReservationModel film)
    {
        // create id
        // System.Console.WriteLine(film.Id);
        film.Id = _reservations.Count > 0 ? _reservations.Max(x => x.Id) + 1 : 1;
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

    public bool DeleteReservation(int id)
    {
        //Find if there is already an model with the same id
        int? index = _reservations.FindIndex(s => s.Id == id);
        if (index == null)
        {
            return false;
        }
        else
        {
            if (index != -1)
            {
                //update existing model
                _reservations.RemoveAt(index.Value);
                ReservationAccess.WriteAll(_reservations);
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public static void ReservationOverview()
    {
        var ReservationsFromJson = ReservationAccess.LoadAll();
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine(@"============================================
|                                          |
|          CURRENT RESERVATIONS            |
|                                          |
============================================");
        foreach (ReservationModel reservation in ReservationsFromJson)
        {
            DisplayTicket(reservation);
        }
        Console.ResetColor();
    }

    public static void DisplayTicket(ReservationModel ticket)
    {
        Console.ForegroundColor = ConsoleColor.Magenta;
        int ID = ticket.Id;
        string Movie = ticket.Movie;
        string FullName = ticket.FullName;
        string Email = ticket.Email;
        int TicketAmount = ticket.TicketAmount;
        string Seats = String.Join(", ", ticket.Seats);
        int TotalAmount = ticket.TotalAmount;
        string Overview = $@"
  ID: {ID}
  Movie: {Movie}
  Full Name: {FullName}
  Email: {Email}
  Ticket Amount: {TicketAmount}
  Seats: {Seats}
  Total Money Amount: {TotalAmount}

=================================================";
        Console.WriteLine(Overview);
        Console.ResetColor();
    }

    public ReservationModel GetById(int id)
    {
        return _reservations.Find(i => i.Id == id)!;
    }
}
