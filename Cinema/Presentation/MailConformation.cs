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

    static private string? EmailReciever;

    public MailConformation(string emailReciever, bool account)
    {
        _guestLogic = new GuestLogic();
        _reservationsLogic = new ReservationsLogic();
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
        string image = GetPicture(reservation!.Movie);
        string body = MailAccess.LoadAll();
        body = body.Replace("movieName", reservation!.Movie);

        body = body.Replace("seatNumbers", $"{Convert.ToString(string.Join("/", reservation.Seats))}");

        body = body.Replace("ticketAmount", Convert.ToString(reservation!.TicketAmount));
        body = body.Replace("ticketTotal", Convert.ToString(reservation!.TicketTotal));
        body = body.Replace("totalAmount", Convert.ToString(reservation!.TotalAmount));
        body = body.Replace("firstName", Convert.ToString(reservation!.FullName));
        body = body.Replace("reservationCode", reservation!.ReservationCode);
        body = body.Replace("customerId", Convert.ToString(reservation!.Id));
        body = body.Replace("{Image}", Convert.ToString(image));
        body = body.Replace("currentDate", DateTime.Today.ToString("dd/MM/yyyy"));
        body = body.Replace("roomNumber", Convert.ToString(reservation.RoomNumber));
        return body;

    }

    public static string GetPicture(string movietitle)
    {
        //     
        FilmsLogic filmLogic = new();
        FilmModel film = filmLogic.GetByName(movietitle);
        return film.ImageURL;
    }
}


