using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;


//This class is not static so later on we can use inheritance and interfaces
class FoodsLogic
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

    public double GetTotalPrice(string name)
    {
        FoodModel item = GetByName(name);
        TotalAmount += item.Cost;
        // foreach (FoodModel food in _foods)
        // {
        //     food.Cost += TotalAmount;
        //     return TotalAmount;
        // }
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
                        FoodModel food = new FoodModel(secondanswer, thirdanswer);
                        UpdateList(food);
                    }
                    Console.WriteLine("Food item not found. Please check our menu.");
                }
            }
            if (firstanswer.ToUpper() == "N")
            {
                System.Console.WriteLine("No changes will be made.....");
            }
    }

}
