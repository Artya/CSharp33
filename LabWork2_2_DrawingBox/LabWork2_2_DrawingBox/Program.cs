using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWork2_2_DrawingBox
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.Write("Input X coordinate of box starting position: ");
                var x = int.Parse(Console.ReadLine());

                Console.Write("Input Y coordinate of box starting position: ");
                var y = int.Parse(Console.ReadLine());

                Console.Write("Input width of the box: ");
                var width = int.Parse(Console.ReadLine());

                Console.Write("Input height of the box: ");
                var height = int.Parse(Console.ReadLine());

                Console.Write("Input symbol which you want to use as a border: ");
                var symbol = (char)Console.ReadLine()[0];

                Console.Write("Input message you want to be displayed inside the box: ");
                var message = Console.ReadLine();

                var Box = new Box
                {
                    X = x,
                    Y = y,
                    Width = width,
                    Height = height,
                    Symbol = symbol,
                    Message = message
                };

                Console.Clear();
                Box.Draw();

                Console.WriteLine("Press any key...");
                Console.ReadLine();
            }
            catch (Exception)
            {
                Console.WriteLine("Error!");
            }

        }
    }
}
