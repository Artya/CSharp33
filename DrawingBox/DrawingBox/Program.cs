using System;

namespace DrawingBox
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Box.Draw();

                Console.WriteLine("Press any key to continue...");
                Console.ReadLine();

                Box.Draw(1, 5, 20, 40, '+', "Hello there!");

                Console.WriteLine("Press any key to exit...");
                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.WriteLine("Error!");
            }
        }
    }
}
