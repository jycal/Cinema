
static class UserLogin
{
    static private AccountsLogic accountsLogic = new AccountsLogic();

    public static void Start(ref bool isLoggedIn)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine();
        Console.WriteLine("====================================");
        Console.WriteLine("|                                  |");
        Console.WriteLine("|   Welcome to the login page      |");
        Console.WriteLine("|                                  |");
        Console.WriteLine("====================================");
        Console.ResetColor();
        Console.WriteLine();

        bool loginSuccessful = false;
        while (!loginSuccessful)
        {
            Console.WriteLine("Please enter your email address");
            string email = Console.ReadLine();
            Console.WriteLine("Please enter your password");
            string password = Console.ReadLine();
            AccountModel acc = accountsLogic.CheckLogin(email, password);
            if (acc != null)
            {
                Console.WriteLine("\nWelcome back " + acc.FullName);
                Console.WriteLine("Your email number is " + acc.EmailAddress);
                loginSuccessful = true;
                isLoggedIn = true;
                Menu.Start();
            }
            else
            {
                Console.WriteLine("No account found with that email and password");
                Console.WriteLine("Please try again.\n");
            }
        }
    }
}