using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

static class FilmManagerMenu
{
    static private FilmsLogic filmsLogic = new FilmsLogic();
    // private static int _id = 1;


    // static void generateId()
    // {
    //     _id++;
    // }


    public static void Start()
    {
        Console.WriteLine("Welcome to the login page");
        Console.WriteLine($"[1] Add new Movie\n[2] Delete Movie");
        Console.WriteLine("Please enter your email address");
        string answer = Console.ReadLine()!;
        if (answer == "1")
        {
            Console.WriteLine("enter title");
            string title = Console.ReadLine()!;
            // string title = "ds";
            Console.WriteLine("enter description");
            string description = Console.ReadLine()!;
            Console.WriteLine("enter duration");
            int duration = Convert.ToInt32(Console.ReadLine())!;
            Console.WriteLine("enter genres");
            string genre = Console.ReadLine()!;
            List<string> genres = genre.Split(',').ToList();
            FilmModel model = new FilmModel(0, title, description, duration, genres);
            filmsLogic.UpdateList(model);

        }
        else if (answer == "2")
        {
            Console.WriteLine("enter title to delete");
            string title = Console.ReadLine()!;
            filmsLogic.DeleteFilm(title);
        }
        else
        {
            Console.WriteLine("error");
        }
        // Console.WriteLine("Please enter your password");
        // string password = Console.ReadLine();
        // AccountModel acc = accountsLogic.CheckLogin(email, password);
        // if (acc != null)
        // {
        //     Console.WriteLine("Welcome back " + acc.FullName);
        //     Console.WriteLine("Your email number is " + acc.EmailAddress);

        //     //Write some code to go back to the menu
        //     //Menu.Start();
        // }
        // else
        // {
        //     Console.WriteLine("No account found with that email and password");
        // }
    }
}
