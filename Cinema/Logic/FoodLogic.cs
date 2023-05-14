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
                        FoodModel food = new FoodModel(secondanswer, thirdanswer);
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
 {food.Name}: {food.Cost}

============================================");
        }
        Console.WriteLine();
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey(true);
    }


    public double BuyFood()
    {
        List<FoodModel> orderedFood = new();
        System.Console.WriteLine("would you like to buy some food? Y/N");
        string answer = Console.ReadLine()!;
        if (answer == "Y")
        {
            Console.Clear();
            DisplayFoodMenu();
            System.Console.WriteLine("What would you like?");
            string choice = Console.ReadLine()!;
            var food = GetByName(choice);
            // System.Console.WriteLine(food.Cost);
            // double price = GetTotalPrice(orderedFood);
            // System.Console.WriteLine(price);
            return food.Cost;

        }
        else if (answer == "N")
        {
            return 0;
        }
        else
        {
            System.Console.WriteLine("wrong input");
            BuyFood();
        }
        return 0;
    }

}
