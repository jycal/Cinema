using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;


public class MailConformation
{
    static private ReservationsLogic? _reservationsLogic;

    static private GuestLogic? _guestLogic;
    static private ReservationModel? reservation;
    static private RoomsLogic? _roomsLogic;

    static private string? EmailReciever;

    public MailConformation(string emailReciever, bool account)
    {
        _guestLogic = new GuestLogic();
        _reservationsLogic = new ReservationsLogic();
        _roomsLogic = new RoomsLogic();
        EmailReciever = emailReciever;
        if (account == true)
        { reservation = _reservationsLogic!._reservations!.Last(); }
        else { reservation = _guestLogic!._guests!.Last(); }
    }
    public void SendMailConformation()
    {
        MimeMessage email = new MimeMessage();
        // zoek reservation

        email.From.Add(new MailboxAddress("Starlight Cinema", "cinemastarlightinfo@gmail.com"));
        email.To.Add(new MailboxAddress("Receiver Name", EmailReciever));

        email.Subject = $"Order conformation {reservation!.Movie} - Reference: {reservation.ReservationCode}";
        // string Body = System.IO.File.ReadAllText(HttpContext.Current.Server.MapPath("~/test.html"));
        email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
        {
            Text = CreateBody()
        };

        SmtpClient client = new SmtpClient();
        try
        {
            client.Connect("smtp.gmail.com", 465, true);
            string emailAdress = "cinemastarlightinfo@gmail.com";
            string password = "zctjlsquzyoodket";
            // Note: only needed if the SMTP server requires authentication
            client.Authenticate(emailAdress, password);

            client.Send(email);
            System.Console.WriteLine("email send");

        }
        catch (Exception x)
        {
            System.Console.WriteLine(x.Message);
        }
        finally
        {
            client.Disconnect(true);
            client.Dispose();
        }
    }
    public static string CreateBody()
    {
        string selectedSeats = string.Join("/", reservation!.Seats.Select(seat => $"Row {seat[0] + 1} Seat {seat[1] + 1}\n"));
        string image = GetPicture(reservation!.Movie);
        string body = MailAccess.LoadAll();
        body = body.Replace("movieName", reservation!.Movie);
        body = body.Replace("seatNumbers", $"{selectedSeats}");
        // body = body.Replace("rowNumber", $"{Convert.ToString(reservation.Rows)}");
        body = body.Replace("ticketAmount", Convert.ToString(reservation!.TicketAmount));
        body = body.Replace("ticketTotal", Convert.ToString(reservation!.TicketTotal));
        body = body.Replace("totalAmount", Convert.ToString(reservation!.TotalAmount));
        body = body.Replace("firstName", Convert.ToString(reservation!.FullName));
        body = body.Replace("reservationCode", reservation!.ReservationCode);
        body = body.Replace("customerId", Convert.ToString(reservation!.Id));
        body = body.Replace("{Image}", Convert.ToString(image));
        body = body.Replace("currentDate", DateTime.Today.ToString("dd/MM/yyyy"));
        body = body.Replace("roomNumber", Convert.ToString(reservation.RoomNumber));
        body = body.Replace("singleTicketPrice", Convert.ToString(GetTicketPrice(reservation.RoomNumber, reservation.Seats)));
        body = body.Replace("snackTotal", Convert.ToString(reservation.SnackAmount));
        body = body.Replace("ticketType", Convert.ToString(GetTicketType(reservation.RoomNumber, reservation.Seats)));

        return body;

    }

    public static string GetPicture(string movietitle)
    {
        //     
        FilmsLogic filmLogic = new();
        FilmModel film = filmLogic.GetByName(movietitle);
        return film.ImageURL;
    }

    public static string GetTicketType(int id, List<int[]> seats)
    {
        RoomModel room = _roomsLogic!.GetById(id);
        // Console.WriteLine($"room: {room}");

        List<int> seatList = new();
        foreach (int[] seat in seats)
        {
            int seatNumber = (seat[0]) * room.RoomWidth + seat[1];
            seatList.Add(seatNumber);
        }
        string type = "";
        foreach (int seat1 in seatList)
        {
            if (room.VipSeats.Contains(seat1))
            {
                if (!type.Contains("VIP"))
                { type += "VIP\n"; }
                continue;
            }
            else if (room.ComfortSeats.Contains(seat1))
            {
                if (!type.Contains("Comfort"))
                { type += "Comfort\n"; }
                continue;

            }
            else if (room.DisabledSeats.Contains(seat1))
            {
                if (!type.Contains("Disability"))
                { type += "Disability\n"; }
                continue;

            }
            else
            {
                if (!type.Contains("Regular"))
                { type += "Regular\n"; }
                continue;

            }
        }
        return $"{type}";
    }
    public static string GetTicketPrice(int id, List<int[]> seats)
    {
        RoomModel room = _roomsLogic!.GetById(id);
        TicketLogic ticketlogic = new();
        // Console.WriteLine($"room: {room}");

        List<int> seatList = new();
        foreach (int[] seat in seats)
        {
            int seatNumber = (seat[0]) * room.RoomWidth + seat[1];
            seatList.Add(seatNumber);
        }
        List<double> prices = new();
        foreach (int seat1 in seatList)
        {
            if (room.VipSeats.Contains(seat1))
            {
                TicketModel ticket = ticketlogic.GetByName("vipSeat");
                if (!prices.Contains(ticket.Cost))
                { prices.Add(ticket.Cost); }
                continue;
            }
            else if (room.ComfortSeats.Contains(seat1))
            {
                TicketModel ticket = ticketlogic.GetByName("comfortSeat");
                if (!prices.Contains(ticket.Cost))
                { prices.Add(ticket.Cost); }
                continue;

            }
            else
            {
                TicketModel ticket = ticketlogic.GetByName("normalSeat");
                if (!prices.Contains(ticket.Cost))
                { prices.Add(ticket.Cost); }
                continue;

            }
        }
        string price = string.Join($"\n", prices.Select(price => $"${price}\n"));
        return price;
    }
}


