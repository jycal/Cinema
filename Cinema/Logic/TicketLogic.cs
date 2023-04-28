using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


public class TicketLogic
{
    public List<TicketModel>? _tickets;

    //Static properties are shared across all instances of the class
    //This can be used to get the current logged in account from anywhere in the program
    //private set, so this can only be set by the class itself
    static public TicketModel? CurrentName { get; private set; }
    static public TicketModel? CurrentCost { get; private set; }

    public TicketModel NormalTicket;
    public TicketModel VipTicket;
    public TicketModel ComfortTicket;

    public TicketLogic()
    {
        _tickets = TicketAccess.LoadAll();
        NormalTicket = GetByName("normalSeat");
        VipTicket = GetByName("vipSeat");
        ComfortTicket = GetByName("comfortSeat");
    }

    public void UpdateList(TicketModel acc)
    {
        //Find if there is already an model with the same id
        int index = _tickets!.FindIndex(s => s.Name == acc.Name);

        if (index != -1)
        {
            //update existing model
            _tickets[index] = acc;
        }
        else
        {
            //add new model
            _tickets.Add(acc);
        }
        TicketAccess.WriteAll(_tickets);
    }

    public TicketModel GetByName(string name)
    {
        return _tickets!.Find(i => i.Name == name)!;
    }

    public TicketModel GetByPrice(double price)
    {
        return _tickets!.Find(i => i.Cost == price)!;
    }

    // methods
    public double TicketPurchase(RoomModel room, List<int> seats)
    {
        List<int> vipSeats = room.VipSeats;
        List<int> comfortSeats = room.ComfortSeats;
        double totalAmount = 0;
        foreach (int seat in seats)
        {
            if (vipSeats.Contains(seat))
            {
                totalAmount += VipTicket.Cost;
            }
            else if (comfortSeats.Contains(seat))
            {
                totalAmount += ComfortTicket.Cost;
            }
            else { totalAmount += NormalTicket.Cost; }
        }
        return totalAmount;
    }

    public void ChangeSeatPrice(string choice, double price)
    {
        string name = "";
        if (choice == "1")
        {
            name = "normalSeat";
        }
        else if (choice == "2")
        {
            name = "comfortSeat";
        }
        else if (choice == "3")
        {
            name = "vipSeat";
        }
        else
        {
            System.Console.WriteLine("Incorrect choice");
            return;
        }

        foreach (TicketModel item in _tickets!.ToList())
        {
            if (item.Name == name)
            {
                TicketModel ticket = new TicketModel(name, price);
                UpdateList(ticket);
            }
        }

    }


    public double GetTotalAmount(double ticketTotal, double SnackTotal)
    {
        return ticketTotal += SnackTotal;
    }

}

