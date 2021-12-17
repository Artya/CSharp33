using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWork2_2_DrawingBox
{
    class Box
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public char Symbol { get; set; }
        public string Message { get; set; }

        public void Draw()
        {
            if (X < 0 || X > Console.WindowWidth - 6)
            {
                Console.WriteLine($"The X coordinate of the left top corner should be between 0 and { Console.WindowWidth - 6}");
                return;
            }
            if (Y < 0 || Y > Console.WindowHeight - 3)
            {
                Console.WriteLine($"The Y coordinate of the left top corner should be between 0 and { Console.WindowHeight - 3}");
                return;
            }
            if (X + Width > Console.WindowWidth) Width = Console.WindowWidth - X;
            if (Width < 5) Width = 5;
            if (Y + Height > Console.WindowHeight) Height = Console.WindowHeight - Y;
            if (Height < 3) Height = 3;

            draw(X, Y, Width, Height, Symbol, Message);
        }

        private void draw(int x, int y, int width, int height, char symbol, string message)
        {
            message = message.Trim();
            var square = (width - 2) * (height - 2);
            if (message.Length > square) message = message.Remove(square - 3) + "...";
            if (message.Length < square) message = message.PadRight(square);
            Console.SetCursorPosition(x, y);
            Console.Write(new string(symbol, width));
            for (var index = 0; index < height - 2; index++)
            {
                Console.SetCursorPosition(x, y + index + 1);
                Console.Write(symbol + message.Substring(index * (width - 2), (width - 2)) + symbol);
            }
            Console.SetCursorPosition(x, y + height - 1);
            Console.WriteLine(new string(symbol, width));
        }


    }
}
