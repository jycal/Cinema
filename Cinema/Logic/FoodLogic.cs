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
                foreach (FoodModel item in _foods.ToList())
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
    List<FoodModel> orderedFood = new();
        Console.Clear();
        string[] options = _foods.Select(f => f.Name).ToArray();
        string prompt = "Please select a food item to order:";
        Menu mainMenu = new Menu(prompt, options);
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
            var food = GetByName(options[selectedIndex]);
            Console.WriteLine($"You selected {food.Name}.");
            Console.WriteLine($"Please set the quantity (maximum 4):");
            int quantity = 1;
            ConsoleKeyInfo keyInfo;
            do
            {
                Console.Write(quantity);
                keyInfo = Console.ReadKey();
                if (keyInfo.Key == ConsoleKey.UpArrow)
                {
                    if (quantity < 4)
                    {
                        quantity++;
                        Console.Write("\b \b");
                    }
                }
                else if (keyInfo.Key == ConsoleKey.DownArrow)
                {
                    if (quantity > 1)
                    {
                        quantity--;
                        Console.Write("\b \b");
                    }
                }
                else if (keyInfo.Key == ConsoleKey.Enter)
                {
                    Console.WriteLine();
                    break;
                }
        } while (keyInfo.Key != ConsoleKey.Enter);
        food.Quantity = quantity;
        orderedFood.Add(food);
        SnacksTotal += food.Cost * quantity;
        Console.WriteLine($"{quantity} {food.Name} added to your order.");
        Console.WriteLine();
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
    // else
    // {
    //     Console.WriteLine("Wrong input.");
    //     BuyFood();
    // }
    return SnacksTotal;
}
}
