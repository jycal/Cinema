//This class is not static so later on we can use inheritance and interfaces
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
        //Find if there is already an model with the same id
        int index = _foods!.FindIndex(s => s.Name == acc.Name);

        if (index != -1)
        {
            //update existing model
            _foods[index] = acc;
        }
        else
        {
            //add new model
            _foods.Add(acc);
        }
        FoodAccess.WriteAll(_foods);
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
            System.Console.WriteLine("Do you want to change the food prices? (Y/N)");
            string firstanswer = Console.ReadLine()!;
            if (firstanswer.ToUpper() == "Y")
            {
                System.Console.WriteLine("Which food price do you want to change?");
                string secondanswer = Console.ReadLine()!;
                foreach (FoodModel item in _foods!.ToList())
                {
                    if (item.Name == secondanswer)
                    {
                        System.Console.WriteLine("Enter new price:");
                        double thirdanswer = Convert.ToDouble(Console.ReadLine())!;
                        FoodModel food = new FoodModel(secondanswer, thirdanswer, 0);
                        UpdateList(food);
                    }
                }
            }
            if (firstanswer.ToUpper() == "N")
            {
                System.Console.WriteLine("No changes will be made.....");
            }
    }

    public void DisplayFoodMenu()
    {
        Console.WriteLine(@"============================================
|                                           |
|                   Menu                    |
|                                           |
============================================");

        foreach (var food in _foods!)
        {
            Console.WriteLine($@"
 {food.Name}: {food.Cost}: {food.Quantity}

============================================");
        }
        Console.WriteLine();
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey(true);
    }

    public double BuyFood()
{
    List<FoodModel> orderedFood = new List<FoodModel>();
    Console.Clear();
    string[] options = _foods!.Select(f => $"{f.Name} - {f.Cost:c}").ToArray(); // Added price to the options
    string prompt = "Please select a food item to order (press spacebar to finish):";
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
                // User cancelled the menu
                break;
            }
            else
            {
                // User selected a food item
                break;
            }
        }

        if (selectedIndex != -1)
        {
            var food = GetByName(options[selectedIndex].Split('-')[0].Trim()); // Extract the selected food item name
            Console.ForegroundColor = ConsoleColor.Red; // Set the text color to red
            Console.WriteLine($"You selected {food.Name} - {food.Cost:c}."); // Display the selected snack with price
            Console.ResetColor(); // Reset the text color

            Console.WriteLine($"Please set the quantity (maximum 5, or 'D' to deselect):");
            int quantity = 1;
            ConsoleKeyInfo keyInfo;
            do
            {
                Console.SetCursorPosition(0, Console.CursorTop);
                Console.Write($"[{quantity}]");

                keyInfo = Console.ReadKey();
                if (keyInfo.Key == ConsoleKey.RightArrow)
                {
                    if (quantity < 5)
                    {
                        quantity++;
                    }
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
                    Console.SetCursorPosition(0, Console.CursorTop);
                    Console.Write($"Enter the quantity to remove: ");
                    string input = Console.ReadLine()!;
                    if (int.TryParse(input, out int removeQuantity) && removeQuantity >= 0 && removeQuantity <= food.Quantity)
                    {
                        quantity = -removeQuantity; // Use negative value to indicate removal
                    }
                    else
                    {
                        Console.WriteLine("Invalid quantity. Please try again.");
                    }
                    break;
                }
            } while (keyInfo.Key != ConsoleKey.Enter || (quantity > 5 || quantity < 1)); // Check for valid quantity range

            if (quantity < 0)
            {
                // User deselected the item
                int removeQuantity = Math.Abs(quantity);
                if (removeQuantity == food.Quantity)
                {
                    orderedFood.RemoveAll(f => f.Name == food.Name);
                    SnacksTotal -= food.Cost * food.Quantity;
                    Console.WriteLine($"{food.Name} removed from your order.");
                }
                else
                {
                    food.Quantity -= removeQuantity;
                    SnacksTotal -= food.Cost * removeQuantity;
                    Console.WriteLine($"{removeQuantity} {food.Name} removed from your order.");
                }
                Console.WriteLine();
            }
            else
            {
                SnacksTotal -= food.Cost * food.Quantity;
                food.Quantity = quantity;
                orderedFood.RemoveAll(f => f.Name == food.Name);
                orderedFood.Add(food);
                SnacksTotal += food.Cost * quantity;
                Console.WriteLine($"{quantity} {food.Name} added to your order.");
                Console.WriteLine();
            }

            // Ask if the user wants to select/deselect another item
            Console.WriteLine("Do you want to select/deselect another item? (Y/N)");
            ConsoleKeyInfo continueKeyInfo = Console.ReadKey(true);
            if (continueKeyInfo.Key != ConsoleKey.Y)
            {
                continueOrdering = false;
            }
        }
        else
        {
            // User cancelled the menu
            break;
        }
    }

    if (orderedFood.Count > 0)
    {
        Console.WriteLine("Your order:");
        foreach (var food in orderedFood)
        {
            Console.WriteLine($"{food.Quantity} x {food.Name} = {food.Cost * food.Quantity:c}");
        }
        Console.WriteLine($"Total cost: {SnacksTotal:c}");
    }
    else
    {
        Console.WriteLine("No items ordered.");
    }

    Console.WriteLine("Press any key to continue...");
    Console.ReadKey(true);

    return SnacksTotal;
}
}
