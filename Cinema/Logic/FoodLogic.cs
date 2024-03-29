using System.Globalization;
using System.Text;

public class FoodsLogic
{
    public List<FoodModel>? _foods;
    private static AccountModel _account = null!;

    //Static properties are shared across all instances of the class
    //This can be used to get the current logged in account from anywhere in the program
    //private set, so this can only be set by the class itself
    static public FoodModel? CurrentName { get; private set; }
    static public FoodModel? CurrentCost { get; private set; }
    public double TotalAmount = 0;

    public double SnacksTotal = 0;

    public FoodsLogic()
    {
        _foods = FoodAccess.LoadAll();
    }

    public void UpdateList(FoodModel acc)
    {
        int index = _foods!.FindIndex(s => s.Name == acc.Name);

        if (index != -1)
        {
            _foods[index] = acc;
        }
        else
        {
            _foods.Add(acc);
        }
        FoodAccess.WriteAll(_foods);
    }

    public void AddFood(FoodModel acc)
    {
        _foods!.Add(acc);
        FoodAccess.WriteAll(_foods);
    }

    public bool DeleteFood(FoodModel acc)
    {
        string name = acc.Name;
        var food = _foods!.Find(r => r.Name == name);
        if (food == null)
        {
            return false;
        }
        int index = _foods!.FindIndex(s => s.Name == name!);

        if (index != -1)
        {
            // _films[index] = film;
            _foods!.RemoveAt(index);
            // _foods!.ForEach((x) => { if (x.Id > film!.Id) x.Id = x.Id - 1; });
            FoodAccess.WriteAll(_foods);
            return true;
        }
        else
        {
            return false;
        }
    }

    public FoodModel GetByName(string name)
    {
        FoodModel foodItem = _foods!.Find(i => i.Name == name)!;
        return foodItem;
    }

    public FoodModel GetByPrice(double price)
    {
        return _foods!.Find(i => i.Cost == price)!;
    }

    public double GetTotalPrice(List<FoodModel> orderedFoods)
    {
        double TotalAmount = 0;
        foreach (FoodModel food in orderedFoods)
        {
            System.Console.WriteLine(food.Cost);
            TotalAmount += food.Cost;
        }
        System.Console.WriteLine(TotalAmount);
        return TotalAmount;
    }

    public void ChangePrice()
    {
        string prompt = "Do you want to change the food prices?";
        string[] options = { "Yes", "No" };
        Menu advancedFoodMenu = new Menu(prompt, options);
        int selectedIndex = advancedFoodMenu.Run();

        switch (selectedIndex)
        {
            case 0:

                string answer = "";
                do
                {
                    System.Console.WriteLine("Which food price do you want to change?");
                    string tempanswer = Console.ReadLine()!;
                    if (string.IsNullOrEmpty(tempanswer))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        System.Console.WriteLine($"Item can not be empty\n");
                        Console.ResetColor();
                        Console.ReadKey(true);
                    }
                    else
                    {
                        answer = tempanswer;
                    }
                } while (string.IsNullOrEmpty(answer) == true);
                // System.Console.WriteLine(answer);
                // Console.ReadKey(true);
                bool found = false;
                foreach (FoodModel item in _foods!.ToList())
                {
                    // System.Console.WriteLine(item);
                    if (item.Name == answer)
                    {
                        System.Console.WriteLine("Enter new price:");
                        string newPrice = Console.ReadLine()!;
                        if (CinemaMenus.IsNumber(newPrice) == false || string.IsNullOrEmpty(newPrice))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            System.Console.WriteLine("Price has to be a number");
                            Console.ResetColor();
                            Console.WriteLine("Press any key to continue...");
                            Console.ReadKey(true);
                            ChangePrice();
                        }
                        // System.Console.WriteLine(newPrice);
                        double Price = double.Parse(newPrice, CultureInfo.InvariantCulture);
                        // System.Console.WriteLine(Price);
                        Console.ReadKey(true);
                        FoodModel food = new FoodModel(answer, Price, 0, 0);
                        Console.ForegroundColor = ConsoleColor.Green;
                        System.Console.WriteLine("Price has been changed");
                        Console.ResetColor();
                        UpdateList(food);
                        found = true;
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey(true);
                        break;
                    }

                }
                if (found == false)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    System.Console.WriteLine("Item has not been found");
                    Console.ResetColor();
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey(true);
                }

                break;
            case 1:
                System.Console.WriteLine("Returning to menu");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey(true);
                break;
        }
    }

    public void DisplayFoodMenu()
    {
        Console.WriteLine(@"============================================
|                                           |
|     ".BrightYellow() + @"Advanced Reservation Menu".BrightWhite() + @"                    |
|                                           |
============================================".BrightYellow());

        foreach (var food in _foods!)
        {
            Console.WriteLine($@"
 {food.Name}: {food.Cost}

" + @"============================================".BrightYellow());
        }
        Console.WriteLine();
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey(true);
    }

    public double BuyFood()
    {
        Console.OutputEncoding = Encoding.Default;
        List<FoodModel> orderedFood = new List<FoodModel>();
        Console.Clear();
        string[] options = _foods!.Select(f => $"{f.Name} - {f.Cost:c}").ToArray(); // Voeg price toe aan options
        string prompt = $"Please select a food item to order (press enter to finish):\nUse your arrow keys (upper and lower) to navigate.\n";

        Menu mainMenu = new Menu(prompt, options);
        bool continueOrdering = true;

        while (continueOrdering)
        {
            int selectedIndex = -1;
            while (true)
            {
                selectedIndex = mainMenu.Run();
                if (selectedIndex == -1)
                {
                    // Gebruiker cancelt menu
                    break;
                }
                else
                {
                    // Gebruiker selecteerd een food item
                    break;
                }
            }

            if (selectedIndex != -1)
            {
                var food = GetByName(options[selectedIndex].Split('-')[0].Trim()); // Haal de geselcteerde food item naam eruit
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine();
                orderedFood.ForEach(x => System.Console.WriteLine($"You selected {x.Name} x {x.Quantity} - ${x.Cost}."));
                Console.WriteLine($"You have currently selected {food.Name} - {food.Cost:c}.".BrightYellow()); // Display geselcteerde snack met prijs
                Console.ResetColor();
                if (food.Age == 18)
                {
                    Console.WriteLine();
                    Console.Write("You have to show your ID at the cash register!!".Orange());
                    Console.ResetColor();
                    Console.WriteLine();
                }

                Console.WriteLine($"\nPlease set the quantity (or 'D' to deselect):".BrightWhite());
                Console.WriteLine($"Use your arrow keys: left (decrease) and right (increase) to set the quantity.\n".BrightWhite());

                double quantity = 1; // quantity begint altijd met 1
                ConsoleKeyInfo keyInfo;
                do
                {
                    Console.SetCursorPosition(0, Console.CursorTop);
                    Console.Write($"[{quantity}]");

                    keyInfo = Console.ReadKey(true);
                    if (keyInfo.Key == ConsoleKey.RightArrow)
                    {
                        quantity++;
                    }
                    else if (keyInfo.Key == ConsoleKey.LeftArrow)
                    {
                        if (quantity > 1)
                        {
                            quantity--;
                        }
                    }
                    else if (keyInfo.Key == ConsoleKey.Enter)
                    {
                        Console.WriteLine();
                        break;
                    }
                    else if (keyInfo.Key == ConsoleKey.D)
                    {
                        quantity = 0; // set quantity tot 0 om het te deselecteren
                        break;
                    }
                } while (keyInfo.Key != ConsoleKey.Enter);

                if (quantity <= 0)
                {
                    var existingFood = orderedFood.FirstOrDefault(f => f.Name == food.Name); // Check of item al bestaat
                    if (existingFood != null)
                    {
                        SnacksTotal -= existingFood.Cost * existingFood.Quantity; // Min de kosten van de bestaande quantity
                        existingFood.Quantity = 0; // Update de bestaande quantity met 0
                        orderedFood.Remove(existingFood); // Verwijder item van orderedFood
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"\n{existingFood.Name} removed from your order.");
                        Console.ResetColor();
                        Console.ReadKey(true);
                        Console.WriteLine();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"\n{food.Name} is not currently in your order.");
                        Console.ResetColor();
                        Console.ReadKey(true);
                        Console.WriteLine();
                    }
                }
                else
                {
                    var existingFood = orderedFood.FirstOrDefault(f => f.Name == food.Name); // Check of item al bestaat
                    if (existingFood != null)
                    {

                        existingFood.Quantity += quantity; // Update de bestaande quantity met de nieuwe
                        SnacksTotal += food.Cost * quantity;
                    }
                    else
                    {
                        food.Quantity = quantity;
                        SnacksTotal += food.Cost * quantity;
                        orderedFood.Add(food);
                    }

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"\n{quantity} {food.Name} added to your order.");
                    Console.ResetColor();
                    Console.WriteLine();
                    Console.ReadKey(true);
                }

                // Vraag of gebruiker wilt selecteren of deselecteren
                bool cf = conformation();
                if (!cf)
                {
                    continueOrdering = false;
                }
            }
            else
            {
                // Gebruiker cancelt zijn bestelling
                break;
            }
        }

        if (orderedFood.Count > 0)
        {
            Console.WriteLine();
            Console.WriteLine("Your order:");
            foreach (var food in orderedFood)
            {
                if (food.Quantity > 0)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"{food.Quantity} x {food.Name} = ${food.Cost * food.Quantity}");
                    Console.ResetColor();
                }
            }
            Console.WriteLine($"Total cost: ${Math.Round(SnacksTotal, 2)}");
            Console.WriteLine();
        }
        else
        {
            Console.WriteLine("No items ordered.");
        }

        Console.WriteLine("Press any key to continue...");
        Console.ReadKey(true);

        return SnacksTotal;
    }

    public static bool conformation()
    {

        string prompt = "Do you want to select/deselect another item?";
        string[] options = { "Yes", "No" };
        Menu mainMenu = new Menu(prompt, options);
        int selectedIndex = mainMenu.Run();
        switch (selectedIndex)
        {
            case 0:
                return true;
            case 1:
                return false;
        }
        return true;
    }
}