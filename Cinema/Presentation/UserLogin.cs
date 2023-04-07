
static class UserLogin
{
    static private AccountsLogic accountsLogic = new AccountsLogic();
    static public AccountModel? CurrentPassword { get; private set; }

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
            string email = Console.ReadLine()!;
            int chances = 3;
            Console.WriteLine("Please enter your password");
            string password = Console.ReadLine()!;
            Console.WriteLine("Wrong password! Please try again...");
            if (chances == 3)
            {
                Console.WriteLine("You have 3 chances left!");
                string Password = Console.ReadLine()!;
                CurrentPassword = AccountsLogic._accounts!.Find(i => i.Password == password);
                if (CurrentPassword!.Password != Password)
                {
                    chances -= 1;
                }

            }
            else if (chances == 2)
            {
                Console.WriteLine("You have 2 chances left! Think!");
                var Password = Console.ReadLine()!;
                CurrentPassword = AccountsLogic._accounts!.Find(i => i.Password == password);
                if (CurrentPassword!.Password != Password)
                {
                    chances -= 1;
                }
            }
            else if (chances == 1)
            {
                Console.WriteLine("You have 1 chance left! Use it well!!");
                var Password = Console.ReadLine()!;
                CurrentPassword = AccountsLogic._accounts!.Find(i => i.Password == password);
                if (CurrentPassword!.Password != Password)
                {
                    chances -= 1;
                }
            }
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