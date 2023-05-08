public class Menu
{
    private int _selectedIndex;
    private string[] _options;
    private string _prompt;

    public Menu(string prompt, string[] options)
    {
        _prompt = prompt;
        _options = options;
        _selectedIndex = 0;
    }

    private void Display_options()
    {
        Console.WriteLine(_prompt);
        for (int i = 0; i < _options.Length; i++)
        {
            string currentOption = _options[i];
            string prefix;

            if (i == _selectedIndex)
            {
                prefix = " ";
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.White;
            }
            else
            {
                prefix = "";
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Black;
            }
        }
    }
    public static void MemberRegister()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("====================================");
        Console.WriteLine("|                                  |");
        Console.WriteLine("|   Welcome to the register page   |");
        Console.WriteLine("|                                  |");
        Console.WriteLine("====================================");
        Console.ResetColor();
        Console.WriteLine();
        Console.WriteLine("Email address: ");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("-----------------------------");
        Console.WriteLine("|  Email must contain a @   |");
        Console.WriteLine("-----------------------------");
        Console.ResetColor();
        string email = Console.ReadLine()!;
        int EmailAttempts = 0;
        while (_accountsLogic.EmailFormatCheck(email) == false && EmailAttempts < 3)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Wrong format! Please enter Email in the correct format...");
            Console.ResetColor();
            Console.Write("Email address: ");
            string Email = Console.ReadLine()!;
            email = Email;
            EmailAttempts += 1;
        }
        if (EmailAttempts > 3)
        {
            Console.Clear();
        }
        Console.WriteLine("Please enter your password");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("------------------------------------------------------------------------------------------");
        Console.WriteLine(@"|  Must contain at least one special character(%!@#$%^&*()?/>.<,:;'\|}]{[_~`+=-         |");
        Console.WriteLine("|   Must be longer than 6 characters                                                     |");
        Console.WriteLine("|   Must contain at least one number                                                     |");
        Console.WriteLine("|   One upper case                                                                       |");
        Console.WriteLine("|   Atleast one lower case                                                               |");
        Console.WriteLine("------------------------------------------------------------------------------------------");
        Console.ResetColor();
        SecureString pass = _accountsLogic.HashedPass();
        string password = new System.Net.NetworkCredential(string.Empty, pass).Password;
        int PasswordAttempts = 0;
        while (_accountsLogic.PasswordFormatCheck(password) == false && PasswordAttempts < 3)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Wrong format! Please enter Password in the correct format...");
            Console.ResetColor();
            SecureString passs = _accountsLogic.HashedPass();
            string Password = new System.Net.NetworkCredential(string.Empty, pass).Password;
            password = Password;
            PasswordAttempts += 1;
        }
        if (PasswordAttempts > 3)
        {
            Console.Clear();
        }
        Console.ResetColor();
    }

    public int Run()
    {
        ConsoleKey keyPressed;
        do
        {
            Console.Clear();
            Display_options();

            // ReadKey(true): Hides key from terminal
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            keyPressed = keyInfo.Key;

            // Update SelectedIndex based on arrow keys.
            if (keyPressed == ConsoleKey.UpArrow)
            {
                _selectedIndex--;
                if (_selectedIndex == -1)
                {
                    _selectedIndex = _options.Length - 1;
                }
            }
            else if (keyPressed == ConsoleKey.DownArrow)
            {
                _selectedIndex++;
                if (_selectedIndex == _options.Length)
                {
                    _selectedIndex = 0;
                }
            }
        } while (keyPressed != ConsoleKey.Enter);

        return _selectedIndex;
    }
}