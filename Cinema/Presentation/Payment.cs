public static class Payment
{
    public static void PaymentWithPayPal()
    {
        string bank;
        bool isValid = false;
        do
        {
            Console.WriteLine("\n--------------------------------");
            Console.WriteLine("         PAYPAL PAYMENT         ");
            Console.WriteLine("--------------------------------");
            Console.WriteLine("To go through with the payment, please enter 4 digits of your bank number:");
            Console.WriteLine("To return to the main menu, enter '0'.");
            bank = Console.ReadLine()!;
            if (bank! == "0")
            {
                return;
            }
            if (bank!.Length == 4)
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
                Console.WriteLine("Bank number must contain 4 numbers.");
                Console.ResetColor();
                isValid = false;
            }

        } while (bank!.Length != 4 || isValid == false);
    }

    public static void PaymentWithIdeal()
    {

        string bank;
        bool isValid = false;
        do
        {
            Console.WriteLine("\n--------------------------------");
            Console.WriteLine("          IDEAL PAYMENT         ");
            Console.WriteLine("--------------------------------");
            Console.WriteLine("Please enter your bank number (format: NL00):\n");
            bank = Console.ReadLine()!;

            if (bank!.Length == 4)
            {
                // foreach (char c in bank)
                // {
                //     switch (c)
                if (bank[0] == 'N' && bank[1] == 'L' && char.IsDigit(bank[2]) && char.IsDigit(bank[3]))
                {

                    isValid = true;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Input has the wrong format. Please use format (NL00)");
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
                Console.WriteLine("Bank number must contain 4 characters.");
                Console.ResetColor();
                isValid = false;
            }

        } while (bank!.Length != 4 || isValid == false);

    }
    private static void SendConfirmationMessage(string paymentMethod)
    {
        // Code to send confirmation message to member
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"\nPayment with {paymentMethod} was successful. Confirmation message sent.");
        Console.ResetColor();
    }

}
