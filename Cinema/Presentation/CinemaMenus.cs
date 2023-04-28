public class CinemaMenus
{
    private AccountsLogic _accountsLogic = new AccountsLogic();
    private AccountModel _account = null!;

    public void Start()
    {
        RunMainMenu();
    }

    private void RunMainMenu()
    {
        string prompt = @" ____  ____  __   ____  __    __  ___  _  _  ____     ___  __  __ _  ____  _  _   __  
/ ___)(_  _)/ _\ (  _ \(  )  (  )/ __)/ )( \(_  _)   / __)(  )(  ( \(  __)( \/ ) / _\ 
\___ \  )( /    \ )   // (_/\ )(( (_ \) __ (  )(    ( (__  )( /    / ) _) / \/ \/    \
(____/ (__)\_/\_/(__\_)\____/(__)\___/\_)(_/ (__)    \___)(__)\_)__)(____)\_)(_/\_/\_/

Welcome to Starlight Cinema. What would you like to do?
(Use arrow keys to cycle through options and press enter to select an option.)
";
        string[] options = { "Login", "Register", "Continue as Guest", "Contact", "Exit" };
        Menu mainMenu = new Menu(prompt, options);
        int selectedIndex = mainMenu.Run();

        switch (selectedIndex)
        {
            case 0:
                Login(1);
                // RunMenusMenu();
                break;
            case 1:
                Register();
                RunMainMenu();
                break;
            case 2:
                break;
            case 3:
                DisplayContactInfo();
                RunMainMenu();
                break;
            case 4:
                ExitCiname();
                break;
        }
    }

    private void ExitCiname()
    {
        Console.WriteLine("Thank you for visiting Starlight Cinema. We hope to see you again soon!");
        Console.WriteLine("Press any key to exit...");
        Console.ReadKey(true);
        Console.Clear();
        Environment.Exit(0);
    }

    private void DisplayContactInfo()
    {
        Console.WriteLine(@"============================================
|                                          |
|            Cinema information            |
|                                          |
============================================
|                                          |
| Phone number:  +31-655-574-244.          |
| Location:      Wijnhaven 107,            |
|                3011 WN  in Rotterdam     |
| Email:         StartLightCinema@STC.com  |
|                                          |
============================================
");
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey(true);
        Console.Clear();
    }

    private void Login(int type)
    {
        Console.WriteLine(@"============================================
|                                          |
|                 Login                    |
|                                          |
============================================
");
        int tries = 3;
        while (tries > 0)
        {
            Console.Write("Please enter your email address: ");
            string email = Console.ReadLine()!;
            Console.Write("Please enter your password: ");
            string password = Console.ReadLine()!;
            AccountModel correctEmail = _accountsLogic.GetByMail(email);

            if (correctEmail != null)
            {
                if (correctEmail.Password != password)
                {
                    tries--;
                    Console.WriteLine("Wrong password or email! Please try again...");
                }
                else
                {
                    _account = correctEmail;
                    Console.WriteLine("Welcome back " + _account.FullName);
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey(true);
                    Console.Clear();
                    break;
                }
            }
            else
            {
                tries--;
            }
            Console.WriteLine("0 tries left. Please try again later...");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey(true);
            Console.Clear();
        }
    }

    private void Register()
    {
        Console.WriteLine(@"============================================
|                                          |
|                Register                  |
|                                          |
============================================
");
        Console.Write("Please enter your email address: ");
        string email = Console.ReadLine()!;
        Console.Write("Please enter your password: ");
        string password = Console.ReadLine()!;
        Console.Write("Please enter your fullname: ");
        string firstName = Console.ReadLine()!;
        Console.Write("Please enter your phone number: ");
        string phoneNumber = Console.ReadLine()!;

        int id = 0;
        while (true)
        {
            AccountModel account_T = _accountsLogic.GetById(id);
            if (account_T is AccountModel)
            {
                id += 1;
            }
            else
            {
                break;
            }
        }
        List<int> tickets = new List<int>();
        AccountModel account = new AccountModel(id, 1, email, password, firstName, tickets);
        _accountsLogic.UpdateList(account);
        Console.WriteLine();
        Console.WriteLine("You have been registered!");
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey(true);
        Console.Clear();
    }
}