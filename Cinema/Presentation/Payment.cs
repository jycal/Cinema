public static class Payment
{
    public static void PaymentWithPayPal(string answer)
    {

        if (answer == "Paypal" || answer == "1")
        {
            string bank1 = "";
            bool isValid = false;
            do
            {
                Console.WriteLine("\n--------------------------------");
                Console.WriteLine("         PAYPAL PAYMENT         ");
                Console.WriteLine("--------------------------------");
                Console.WriteLine("Please enter your bank number (4 digits): ");
                string? bank = Console.ReadLine();
                bank1 += bank;

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
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Bank number must contain 4 numbers.");
                    Console.ResetColor();
                }

            } while (bank1.Length != 4 && isValid == false);
        }
    }

    public static void PaymentWithIdeal(string answer)
    {

        if (answer == "Ideal" || answer == "2")
        {
            string bank1 = "";
            bool isValid = false;
            do
            {
                Console.WriteLine("\n--------------------------------");
                Console.WriteLine("          IDEAL PAYMENT         ");
                Console.WriteLine("--------------------------------");
                Console.WriteLine("Please enter your bank number (format: NL00):\n");
                string? bank = Console.ReadLine();
                bank1 += bank;
                if (bank!.Length == 4)
                {
                    foreach (char c in bank)
                    {
                        switch (c)
                        {
                            case 'N':
                            case 'L':
                            case char when char.IsDigit(c):
                                isValid = true;
                                break;

                            default:

                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Input has the wrong format. Please use format (NL00)");
                                Console.ResetColor();
                                isValid = false;
                                break;

                        }
                    }
                    if (isValid == true)
                    {
                        SendConfirmationMessage("iDeal");
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Bank number must contain 4 characters.");
                    Console.ResetColor();
                }

            } while (bank1.Length != 4 && isValid == false);
        }
    }

    private static void SendConfirmationMessage(string paymentMethod)
    {
        // Code to send confirmation message to member
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"\nPayment with {paymentMethod} was successful. Confirmation message sent to member.");
        Console.ResetColor();
    }

}
