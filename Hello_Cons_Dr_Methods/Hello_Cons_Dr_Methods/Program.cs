using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hello_Cons_Dr_Methods
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //Create Box class instance
                var box = new Box();

                //Implement start position, width and height, symbol, message input
                Console.WriteLine("Enter start position X:");
                var input = Console.ReadLine();           
                box.StartPositionX = int.Parse(input);

                Console.WriteLine("Enter start position Y:");
                input = Console.ReadLine();
                box.StartPositionY = int.Parse(input);

                Console.WriteLine("Enter rectangle width:");
                input = Console.ReadLine();
                box.BoxWidth = int.Parse(input);

                Console.WriteLine("Enter rectangle height:");
                input = Console.ReadLine();
                box.BoxHeight = int.Parse(input);

                var validSymbols = "*+.";
                var bordersSymbolsNumber = 1;

                while (true)
                {
                    Console.WriteLine("Enter a border symbol, one of: " + validSymbols);
                    input = Console.ReadLine();

                    if (validSymbols.Contains(input) && input.Length == bordersSymbolsNumber)
                        break;                         

                    Console.WriteLine("Entered string is incorrect");
                } 

                box.BorderSymbol = char.Parse(input);

                Console.WriteLine("Enter message:");
                box.Message = Console.ReadLine();

                //Use  Box.Draw() method
                box.Draw();

                //Console.WriteLine("Press any key...");
                //Console.ReadLine();
            }
            catch (Exception)
            {
                Console.WriteLine("Error!");
            }
        }
    }
}
