public class Program
{
    public static void Main()
    {
         Console.Clear();
            for (int a = 10; a >= 0; a--)
            {
                Console.SetCursorPosition(0, 2);
                Console.Write("Generating Preview in {0} ", a); 
                System.Threading.Thread.Sleep(1000);
            }
        
        Console.CursorVisible = false;
        CinemaMenus.Start();
    }
}