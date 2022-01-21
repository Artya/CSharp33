using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hello_Cons_Dr_Methods
{
    class Box
    {
        public int StartPositionX { get; set; }
        public int StartPositionY { get; set; }
        public int BoxWidth { get; set;  }
        public int BoxHeight { get; set; }
        public char BorderSymbol { get; set; }
        public string Message { get; set; }
       
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
