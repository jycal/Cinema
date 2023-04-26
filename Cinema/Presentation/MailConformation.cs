using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;


public class MailConformation
{
    static private ReservationsLogic _reservationsLogic = new();

    static private GuestLogic _guestLogic = new();
    static private ReservationModel? reservation;

    static private string? EmailReciever;

    public MailConformation(string emailReciever, bool account)
    {
        EmailReciever = emailReciever;
        if (account == true)
        { reservation = _reservationsLogic._reservations!.Last(); }
        else { reservation = _guestLogic._guests!.Last(); }
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
        foreach (var item in reservation.Seats)
        {
            int Seats = item;
            body = body.Replace("seatNumbers", $"{Convert.ToString(Seats)}/");
        }
        body = body.Replace("ticketAmount", Convert.ToString(reservation!.TicketAmount));
        body = body.Replace("ticketTotal", Convert.ToString(reservation!.TicketTotal));
        body = body.Replace("totalAmount", Convert.ToString(reservation!.TotalAmount));
        body = body.Replace("firstName", Convert.ToString(reservation!.FullName));
        body = body.Replace("reservationCode", reservation!.ReservationCode);
        body = body.Replace("customerId", Convert.ToString(reservation!.Id));
        body = body.Replace("{Image}", Convert.ToString(image));
        return body;

    }

    public static string GetPicture(string movietitle)
    {
        string image = "";
        switch (movietitle)
        {
            case "Magic Mike's Last Dance":
                image = "https://21818cdde6.imgdist.com/public/users/Integrators/BeeProAgency/981647_966306/Magic-Mike-s-Last-Dance_ps_1_jpg_sd-high_Copyright-2022-Warner-Bros-Entertainment-Inc-All-Rights-Reserved-Photo-Credit-Claudette-Barius.webp";
                break;
            case "Cocaine Bear":
                image = "https://21818cdde6.imgdist.com/public/_thumbs/Integrators/BeeProAgency/981647_966306/cocaine_1.jpg_thumb.png?hash=1682510761536";
                break;
            case "Avatar: The Way of Water":
                image = "https://21818cdde6.imgdist.com/public/_thumbs/Integrators/BeeProAgency/981647_966306/Afbeelding2vat.jpg_thumb.png?hash=1682510761536";
                break;
            case "Ant-Man and the Wasp: Quantumania":
                image = "https://21818cdde6.imgdist.com/public/_thumbs/Integrators/BeeProAgency/981647_966306/ant.jpg_thumb.png?hash=1682510761536";
                break;
            case "Dungeons & Dragons: Honor Among Thieves":
                image = "https://21818cdde6.imgdist.com/public/_thumbs/Integrators/BeeProAgency/981647_966306/dd.jpg_thumb.png?hash=1682510761536";
                break;
            case "Mummies(NL)":
                image = "https://21818cdde6.imgdist.com/public/_thumbs/Integrators/BeeProAgency/981647_966306/mum.jpg_thumb.png?hash=1682510761536";
                break;
            case "Scream VI":
                image = "https://21818cdde6.imgdist.com/public/_thumbs/Integrators/BeeProAgency/981647_966306/scream.jpg_thumb.png?hash=1682510761536";
                break;
            case "All Inclusive":
                image = "https://21818cdde6.imgdist.com/public/_thumbs/Integrators/BeeProAgency/981647_966306/all%20inv.jpg_thumb.png?hash=1682510761536";
                break;
            case "The Super Mario Bros. Movie (OV)":
                image = "https://21818cdde6.imgdist.com/public/_thumbs/Integrators/BeeProAgency/981647_966306/suoerrr.jpg_thumb.png?hash=1682510761536";
                break;
            case "Creed III":
                image = "https://21818cdde6.imgdist.com/public/_thumbs/Integrators/BeeProAgency/981647_966306/creed.jpg_thumb.png?hash=1682510761536";
                break;
            case "Shazam! Fury of the Gods":
                image = "https://21818cdde6.imgdist.com/public/_thumbs/Integrators/BeeProAgency/981647_966306/shazam.jpg_thumb.png?hash=1682510761536";
                break;
            case "65":
                image = "https://21818cdde6.imgdist.com/public/_thumbs/Integrators/BeeProAgency/981647_966306/65.jpg_thumb.png?hash=1682510761536";
                break;
            case "John Wick 4":
                image = "https://21818cdde6.imgdist.com/public/_thumbs/Integrators/BeeProAgency/981647_966306/john%20wick.webp_thumb.png?hash=1682510761536";
                break;
        }
        return image;
    }



}


