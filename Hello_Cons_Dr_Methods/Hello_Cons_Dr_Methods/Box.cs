using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hello_Cons_Dr_Methods
{
    class Box
    {
        //1.  Implement public  auto-implement properties for start position (point position)
        //auto-implement properties for width and height of the box
        //and auto-implement properties for a symbol of a given set of valid characters (*, + ,.) to be used for the border 
        //and a message inside the box
        public int StartPositionX { get; set; }
        public int StartPositionY { get; set; }
        public int BoxWidth { get; set;  }
        public int BoxHeight { get; set; }
        public char BorderSymbol { get; set; }
        public string Message { get; set; }
       

        //2.  Implement public Draw() method
        //to define start position, width and height, symbol, message  according to properties
        //Use Math.Min() and Math.Max() methods
        //Use draw() to draw the box with message
        public void Draw()
        {
            Console.Clear();

            var bordersWidth = 2;
            var minMessageLength = Math.Min(Message.Length, BoxWidth-bordersWidth);

            if (Message.Length > minMessageLength)
            {
                Message = Message.Substring(0, minMessageLength);
            }

            draw(StartPositionX, StartPositionY, BoxWidth, BoxHeight, BorderSymbol, Message);
        }

        //3.  Implement private method draw() with parameters 
        //for start position, width and height, symbol, message
        //Change the message in the method to return the Box square
        //Use Console.SetCursorPosition() method
        //Trim the message if necessary
        private void draw(int startPositionX, int startPositionY, int width, int height, char symbol, string message)
        {
            var magicZeroBecauseItStartOfAll = 0;
            var magicOneBecauseAllLoopsStartsOfZeroButLengthIsMaxIndexPlusOne = 1;
            var magicTwoBecauseHalfOfAllInThsiWorldIsLengthDividedByTwo = 2;

            for (var i = 0; i < height; i++)
            {
                Console.SetCursorPosition(startPositionX, startPositionY + i);
                Console.Write(symbol);

                Console.SetCursorPosition(startPositionX + width - magicOneBecauseAllLoopsStartsOfZeroButLengthIsMaxIndexPlusOne, startPositionY + i);
                Console.Write(symbol);
            }

            for (var i = 0; i < width; i++)
            {
                Console.SetCursorPosition(startPositionX + i, startPositionY);
                Console.Write(symbol);

                Console.SetCursorPosition(startPositionX + i, startPositionY+height- magicOneBecauseAllLoopsStartsOfZeroButLengthIsMaxIndexPlusOne);
                Console.Write(symbol);
            }

            var messageStartPositionY = (int)height / magicTwoBecauseHalfOfAllInThsiWorldIsLengthDividedByTwo + startPositionY;
            var messageStartPositionX = (int)(width - message.Length) / magicTwoBecauseHalfOfAllInThsiWorldIsLengthDividedByTwo + startPositionX;

            Console.SetCursorPosition(messageStartPositionX, messageStartPositionY);
            Console.WriteLine(message);

            Console.SetCursorPosition(magicZeroBecauseItStartOfAll, startPositionY + height);

            Console.WriteLine();
            Console.WriteLine();
        }
    }
}
