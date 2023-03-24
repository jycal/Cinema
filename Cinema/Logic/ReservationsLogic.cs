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

    public void DeleteFilm(string fullName)
    {
        //Find if there is already an model with the same id
        var reservation = _reservations.Find(r => r.FullName == fullName);
        int index = _reservations.FindIndex(s => s.Id == reservation!.Id);

        if (index != -1)
        {
            //update existing model
            // _films[index] = film;
            _reservations.RemoveAt(index);
            _reservations.ForEach((x) => { if (x.Id > reservation!.Id) x.Id = x.Id - 1; });
            ReservationAccess.WriteAll(_reservations);
        }
        else
        {
            Console.WriteLine($"reservation not found");

        }

    }

    public ReservationModel GetById(int id)
    {
        return _reservations.Find(i => i.Id == id)!;
    }
}
