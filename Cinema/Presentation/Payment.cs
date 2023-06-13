public static class Payment
{
    public static void PaymentWithPayPal()
    {
        string bank;
        bool isValid = false;
        do
        {
            Console.WriteLine("\n--------------------------------".BrightCyan());
            Console.WriteLine("         PAYPAL PAYMENT         ".BrightWhite());
            Console.WriteLine("--------------------------------".BrightCyan());
            Console.WriteLine("To go through with the payment, please enter 6 digits of your bank number:");
            Console.WriteLine("To return to the main menu, enter '0'.");
            bank = Console.ReadLine()!;
            if (bank! == "0")
            {
                string prompt = "\nAre you sure you want to cancel your payment?:";
                string[] options = { "Yes", "No, continue with the payment", "Return to main menu" };
                Menu movieMenu = new Menu(prompt, options);
                int selectedIndex = movieMenu.Run();

                switch (selectedIndex)
                {
                    case 0:
                        CinemaMenus._filmsLogic.MovieOverview();
                        VisualOverview vis = new VisualOverview();
                        VisualOverview.Start(CinemaMenus._account);
                        CinemaMenus.Start();
                        break;
                    case 1:
                        VisualOverview.SelectPayment();
                        break;
                    case 2:
                        CinemaMenus.RunMenusMenu();
                        break;
                }
            }
            if (bank!.Length == 6)
            {
                if (int.TryParse(bank, out int bankNumber))
                {
                    isValid = true;
                    SendConfirmationMessage("PayPal");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Bank number must only consist of numbers.");
                    Console.ResetColor();
                    isValid = false;
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Bank number must contain 6 numbers.");
                Console.ResetColor();
                isValid = false;
            }

        } while (bank!.Length != 6 || isValid == false);
    }

    public static void PaymentWithIdeal()
    {

        string bank;
        bool isValid = false;
        do
        {
            Console.WriteLine("\n--------------------------------".BrightCyan());
            Console.WriteLine("          IDEAL PAYMENT         ".BrightWhite());
            Console.WriteLine("--------------------------------".BrightCyan());
            Console.WriteLine("Please enter your bank number (format: NL-000-000):\n");
            Console.WriteLine("To return to the main menu, enter '0'.");
            bank = Console.ReadLine()!;
            if (bank! == "0")
            {
                string prompt = "\nAre you sure you want to cancel your payment?:";
                string[] options = { "Yes", "No, continue with the payment", "Return to main menu" };
                Menu movieMenu = new Menu(prompt, options);
                int selectedIndex = movieMenu.Run();

                switch (selectedIndex)
                {
                    case 0:
                        CinemaMenus._filmsLogic.MovieOverview();
                        VisualOverview vis = new VisualOverview();
                        VisualOverview.Start(CinemaMenus._account);
                        CinemaMenus.Start();
                        break;
                    case 1:
                        VisualOverview.SelectPayment();
                        break;
                    case 2:
                        CinemaMenus.RunMenusMenu();
                        break;
                }
            }
            if (bank!.Length == 8)
            {
                // foreach (char c in bank)
                // {
                //     switch (c)
                if (char.ToUpper(bank[0]) == 'N' && char.ToUpper(bank[1]) == 'L' && char.IsDigit(bank[2]) && char.IsDigit(bank[3]) && char.IsDigit(bank[4]) && char.IsDigit(bank[5]) && char.IsDigit(bank[6]) && char.IsDigit(bank[7]))
                {
                    isValid = true;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Input has the wrong format. Please use format (NL-000-000)");
                    Console.ResetColor();
                    isValid = false;
                }
            }
            if (isValid == true)
            {
                SendConfirmationMessage("iDeal");
            }

            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Bank number must contain 8 characters.");
                Console.ResetColor();
                isValid = false;
            }

        } while (bank!.Length != 8 || isValid == false);

    }
    private static void SendConfirmationMessage(string paymentMethod)
    {
        // Code to send confirmation message to member
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"\nPayment with {paymentMethod} was successful. Confirmation message sent.");
        Console.ResetColor();
    }

}
