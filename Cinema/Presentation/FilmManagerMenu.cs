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
        Console.WriteLine("[1] View Movies");
        Console.WriteLine("[2] View Sorted Movies");
        Console.WriteLine("[3] Search by Title");
        Console.WriteLine("[4] Search by Genre");
        Console.WriteLine("[5] Add new Movie");
        Console.WriteLine("[6] Delete Movie");
        // Console.WriteLine("Please enter your email address");
        string answer = Console.ReadLine()!;
        if (answer == "1")
        {
            FilmsLogic.MovieOverview();
        }
        else if (answer == "2")
        {
            FilmsLogic.MovieSortedByABCTitle();
        }
        else if (answer == "3")
        {
            Console.WriteLine("enter title to search");
            string title = Console.ReadLine()!;
            FilmsLogic.SearchByTitle(title);
        }
        else if (answer == "4")
        {
            Console.WriteLine("enter genre to search");
            string genre = Console.ReadLine()!;
            FilmsLogic.SearchByGenre(genre);
        }
        
        else if (answer == "5")
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
        else if (answer == "6")
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

