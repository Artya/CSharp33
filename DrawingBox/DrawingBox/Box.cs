using System;

namespace DrawingBox
{
    public static class Box
    {
        private static int x;
        private static int y;
        private static int height;
        private static int width;
        private static char borderSymbol;
        private static string message;

        public static readonly char[] ValidCharacters;

        private static void draw()
        {
            var bufferHeight = Console.BufferHeight;
            var bufferWidth = Console.BufferWidth;

            if (Box.y + Box.height > bufferHeight)
                throw new InvalidOperationException($"(top offset + height) cannot be more than buffer height: {bufferHeight}");

            if (Box.x + Box.width > bufferWidth)
                throw new InvalidOperationException($"(left offset + width) cannot be more than buffer width: {bufferWidth}");

            var borderSymbolIsValid = false;
            foreach (var symbol in Box.ValidCharacters)
            {
                if (Box.borderSymbol == symbol)
                    borderSymbolIsValid = true;
            }

            if (!borderSymbolIsValid)
                throw new InvalidOperationException($"You're not allowed to use border symbol different from this: [ *, +, . ]");

            if (Box.height < 4 && Box.width < 4)
                throw new InvalidOperationException("You cannot make box with width and height less than 4");

            Console.Clear();

            Box.drawBorder();
            Box.drawMessage();
        }
        private static void drawBorder()
        {
            Console.ForegroundColor = ConsoleColor.Red;

            Console.SetCursorPosition(Box.x, Box.y);
            Console.WriteLine(new string(Box.borderSymbol, Box.width));

            Console.SetCursorPosition(Box.x, Box.y + Box.height - 1);
            Console.WriteLine(new string(Box.borderSymbol, Box.width));

            for (int row = 1; row < Box.height - 1; row++)
            {
                Console.SetCursorPosition(Box.x, Box.y + row);
                Console.Write(Box.borderSymbol);

                Console.SetCursorPosition(Box.x + Box.width - 1, Box.y + row);
                Console.Write(Box.borderSymbol);
            }

            Console.ForegroundColor = ConsoleColor.White;
        }
        private static void drawMessage()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;

            var messageArea = (Box.height - 2) * (Box.width - 2);
            if (Box.message.Length > messageArea)
            {
                Box.message = Box.message.Substring(0, messageArea - 3);
                Box.message += "...";
            }

            var topOffset = 0;
            var leftOffset = 0;

            var rows = Box.message.Length / (Box.width - 2);
            if (Box.height - 2 != rows)
                topOffset = ((Box.height - rows) / 2) - 1;

            if (Box.message.Length < Box.width - 2)
                leftOffset = ((Box.width - 2) - Box.message.Length) / 2;

            var counter = 0;
            for (int yPosition = 1; yPosition <= messageArea / (Box.width - 2); yPosition++)
            {
                for (int xPosition = 1; xPosition <= messageArea / (Box.height - 2); xPosition++)
                {
                    if (counter == Box.message.Length)
                        break;

                    Console.SetCursorPosition(Box.x + leftOffset + xPosition, Box.y + topOffset + yPosition);
                    Console.Write(Box.message[counter]);
                    counter++;
                }
            }

            Console.SetCursorPosition(Box.x,  Box.y + Box.height);
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
        }

        static Box()
        {
            Box.ValidCharacters = new char[] { '*', '+', '.' };
        }

        public static void Draw()
        {
            Box.x = 0;
            Box.y = 0;
            Box.height = 4;
            Box.width = 10;
            Box.borderSymbol = Box.ValidCharacters[0];
            Box.message = "Default message";

            Box.draw();
        }
        public static void Draw(uint leftOffset, uint topOffset, uint height, uint width, char symbol, string message)
        {
            Box.x = (int)leftOffset;
            Box.y = (int)topOffset;
            Box.height = (int)height;
            Box.width = (int)width;
            Box.borderSymbol = symbol;
            Box.message = message;

            Box.draw();
        }
    }
}
