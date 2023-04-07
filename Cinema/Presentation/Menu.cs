static class Menu
{
    private static bool isLoggedIn = false;

    static public void Start()
    {
        Console.WriteLine();
        Console.WriteLine("====================================");
        Console.WriteLine("|                                  |");
        Console.WriteLine("|            Main Menu             |");
        Console.WriteLine("|                                  |");
        Console.WriteLine("====================================");
        Console.WriteLine();
        Console.WriteLine("Please select an option:");
        if (!isLoggedIn)
        {
            Console.WriteLine("1. Login as member");
            Console.WriteLine("2. Login as employee");
            Console.WriteLine("3. Login as admin");
            Console.WriteLine("4. View movie info");
            Console.WriteLine("5. View cinema info");
        }
        else
        {
            Console.WriteLine("1. View movie info");
            Console.WriteLine("2. Add/delete film\n");
            Console.WriteLine("3. View cinema info");
            Console.WriteLine("4. Logout");
            Console.Write("Enter your choice: ");
        }

        string input = Console.ReadLine();

        if (!isLoggedIn)
        {
            if (input == "1")
            {
                UserLogin.Start(ref isLoggedIn);
            }
            else if (input == "2")
            {
                UserLogin.Start(ref isLoggedIn);
            }
            else if (input == "3")
            {
                UserLogin.Start(ref isLoggedIn);
            }
            else if (input == "4")
            {
                // Hier moet de connecttie komen naar de movieoverview
                // FilmManagerMenu.MovieOverview();
                EnterRoom.Start();
            }
            else if (input == "5")
            {
                Console.WriteLine(InformationDisplay.overview);
            }
            else
            {
                Console.WriteLine("Invalid input");
                Start();
            }
        }
        else
        {
            if (input == "1")
            {
                // Hier moet de connecttie komen naar de movieoverview
                // FilmManagerMenu.MovieOverview();

            }
            else if (input == "2")
            {
                FilmManagerMenu.Start();
            }
            else if (input == "3")
            {
                Console.WriteLine(InformationDisplay.overview);
            }
            else if (input == "4")
            {
                isLoggedIn = false;
                Console.WriteLine("You have been logged out.");

            }
            else
            {
                Console.WriteLine("Invalid input");
                Start();
            }
        }
    }
}
