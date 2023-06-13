public class GuestLogic
{
    public List<ReservationModel>? _guests;
    public RevenueLogic _revenueLogic;
    public RoomsLogic roomsLogic;

    //Static properties are shared across all instances of the class
    //This can be used to get the current logged in account from anywhere in the program
    //private set, so this can only be set by the class itself
    static public ReservationModel? CurrentReservation { get; private set; }

    public GuestLogic()
    {
        if (CinemaMenus._roomsLogic == null)
        {
            CinemaMenus._roomsLogic = new RoomsLogic();
            roomsLogic = CinemaMenus._roomsLogic;
        }
        else
        {
            roomsLogic = CinemaMenus._roomsLogic;
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
                RoomModel room = roomsLogic.GetById(reservation!.RoomNumber);
                // film id, room id, date, seat number
                foreach (var seat in reservation!.SeatsInfo)
                {
                    room.Seats.Remove(seat);                   
                }
                roomsLogic.UpdateList(room);

                
                _guests.RemoveAt(index);
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