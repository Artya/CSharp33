﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hello_Operators_advstud
{
    class Program
    {
       static void Main(string[] args)
        {
            const int MyMax = 200;

            Random random = new Random();
            // random.Next(MaxValue) returns a 32-bit signed integer that is greater than or equal to 0 and less than MaxValue
            int Guess_number = random.Next(MyMax) + 1;
            // implement input of number and comparison result message in the while circle with  comparison condition
            
            var avaliableColors = new ConsoleColor[] { ConsoleColor.Yellow, ConsoleColor.Blue, ConsoleColor.Red, ConsoleColor.Magenta };
            var colorCounter = 0;

            while (true)
            {
                colorCounter++;

                if (colorCounter == (avaliableColors.Length))
                    colorCounter = 0;

                Console.ForegroundColor = avaliableColors[colorCounter];

                Console.WriteLine("Enter your guess: ");

                var inputText = Console.ReadLine();
                var tryGuess = int.Parse(inputText);

                if (tryGuess == Guess_number)
                {
                    Console.WriteLine("Congratulations!!!");
                    return;
                }

                if (tryGuess < Guess_number)
                {
                    Console.WriteLine("Too low!");
                    continue;
                }

                Console.WriteLine("Too high");
            } 
        }
    }
}
