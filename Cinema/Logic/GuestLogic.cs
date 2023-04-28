using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


class GuestLogic
{
    public List<ReservationModel>? _guests;

    //Static properties are shared across all instances of the class
    //This can be used to get the current logged in account from anywhere in the program
    //private set, so this can only be set by the class itself
    static public ReservationModel? CurrentReservation { get; private set; }

    public GuestLogic()
    {
        _guests = GuestAccess.LoadAll();
    }

    public void UpdateList(ReservationModel guest)
    {
        // create id
        // System.Console.WriteLine(film.Id);
        guest.Id = _guests!.Count > 0 ? _guests.Max(x => x.Id) + 1 : 1;
        // System.Console.WriteLine(film.Id);
        //Find if there is already an model with the same id
        int index = _guests.FindIndex(s => s.Id == guest.Id);

        if (index != -1)
        {
            //update existing model
            _guests[index] = guest;
        }
        else
        {
            //add new model
            _guests.Add(guest);
        }
        GuestAccess.WriteAll(_guests);

    }

    public void DeleteReservation(string reservationCode)
    {
        //Find if there is already an model with the same id

        var check = GetByCode(reservationCode!);
        if (check != null)
        {
            var reservation = _guests!.Find(r => r.ReservationCode == reservationCode);
            int index = _guests.FindIndex(s => s.Id == reservation!.Id);

            if (index != -1)
            {
                //update existing model
                // _films[index] = film;
                _guests.RemoveAt(index);
                _guests.ForEach((x) => { if (x.Id > reservation!.Id) x.Id = x.Id - 1; });
                GuestAccess.WriteAll(_guests);
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

    public ReservationModel GetByEmail(string email)
    {
        return _guests!.Find(i => i.Email == email)!;
    }

    public ReservationModel GetByCode(string reservationCode)
    {
        return _guests!.Find(i => i.ReservationCode == reservationCode)!;
    }


}
