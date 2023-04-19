public static class Catering
{
    public static void ShowInfo()
    {
        FoodsLogic foods = new FoodsLogic();
        foreach (var food in foods._foods!)
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
}
