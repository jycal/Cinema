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
        return _foods!.Find(i => i.Name == name)!;
    }

    public FoodModel GetByPrice(double price)
    {
        return _foods!.Find(i => i.Cost == price)!;
    }

    public double GetTotalPrice()
    {
        double TotalAmount = 0;
        foreach (FoodModel food in _foods!)
        {
            food.Cost += TotalAmount;
            return TotalAmount;
        }
        return TotalAmount;
    }

    public void ChangePrice()
    {
        Console.Write("Do you want to change the food prices? (Y/N): ");
        string firstanswer = Console.ReadLine()!;
        if (firstanswer.ToUpper() == "Y")
        {
            Console.Write("Which food price do you want to change?: ");
            string secondanswer = Console.ReadLine()!;
            foreach (FoodModel item in _foods!.ToList())
            {
                if (item.Name == secondanswer)
                {
                    Console.Write("Enter new price: ");
                    double thirdanswer = Convert.ToDouble(Console.ReadLine())!;
                    FoodModel food = new FoodModel(secondanswer, thirdanswer);
                    UpdateList(food);
                    Console.WriteLine("Price changed");
                    Console.WriteLine();
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey(true);
                }
            }
        }
        if (firstanswer.ToUpper() == "N")
        {
            Console.WriteLine("No changes will be made.....");
            Console.WriteLine();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey(true);
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
 {food.Name}: {food.Cost}

============================================");
        }
        Console.WriteLine();
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey(true);
    }
}
