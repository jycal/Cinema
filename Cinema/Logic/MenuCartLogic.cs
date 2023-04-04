public static class foodMenuCart
{
    public static List<Food> FoodList = new List<Food>();
    public static void ShowInfo()
    {
        foreach (Food food in FoodList)
        {
            string Overview = $@"
============================================
|                   MENU                   |
============================================
{food.Name} x {food.Cost}
============================================";
            Console.WriteLine(Overview);
            Console.ResetColor();
        }
    }

    public static string Edit(string answer, string Name, int Cost)
    {
        // Er moet in het menu worden gevraagd of De Manager wilt editen.
        if (true)
        {
            foreach (Food food in FoodList)
            {
                food.Name = Name;
                food.Cost = Cost;
            }
        }
        return "No edits have been made....";
    }

    public static void Add(Food food)
    {
        FoodList.Add(food);
    }

    public static void Remove(Food food)
    {
        FoodList.Remove(food);
    }
}