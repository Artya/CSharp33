using System;
using System.Threading;

namespace Hello_Console_stud
{
    class Program
    {
        static void Main(string[] args)
        {
            int a;
            try
            {
                do
                {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine(@"Please,  type the number:
                    1.  f(a,b) = |a-b| (unary)
                    2.  f(a) = a (binary)
                    3.  music
                    4.  morse sos
          
                    ");
                    try
                    {
                        a = (int)uint.Parse(Console.ReadLine());
                        switch (a)
                        {
                            case 1:
                                My_strings();
                                Console.WriteLine("");
                                break;
                            case 2:
                                My_Binary();
                                Console.WriteLine("");
                                break;
                            case 3:
                                My_music();
                                Console.WriteLine("");
                                break;
                            case 4:
                                Morse_code();
                                Console.WriteLine("");
                                break;                   
                            default:
                                Console.WriteLine("Exit");
                                break;
                        }

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Error"+e.Message);
                    }                   
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Press Spacebar to exit; press any key to continue");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                while (Console.ReadKey().Key != ConsoleKey.Spacebar);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        #region ToFromBinary
        static void My_Binary()
        {
            //Implement positive integer variable input
            Console.WriteLine("Enter a number:");
            var input = Console.ReadLine();
            var enteredNumber = uint.Parse(input);

            //Present it like binary string
            //   For example, 4 as 100
            //Use modulus operator to obtain the remainder  (n % 2) 
            //and divide variable by 2 in the loop

            
            var binaryString = "";

            while (enteredNumber > 0)
            {
                var oneZero = enteredNumber % 2;
                binaryString += oneZero;
                enteredNumber = enteredNumber / 2; 
            }

            //Use the ToCharArray() method to transform string to chararray
            //and Array.Reverse() method
            var charArray = binaryString.ToCharArray();
            Array.Reverse(charArray);

            Console.WriteLine(charArray);
        }
        #endregion

        #region ToFromUnary
        static void My_strings()
        {
            //Declare int and string variables for decimal and binary presentations
            //Implement two positive integer variables input

            Console.WriteLine("Enter first number:");
            var input = Console.ReadLine();

            var firstNumber = uint.Parse(input);

            Console.WriteLine("Enter second number:");
            input = Console.ReadLine();

            var secondNumber = uint.Parse(input);

            //To present each of them in the form of unary string use for loop
            var firstStringRepresentation = GetUnaryStringPreserntation(firstNumber);
            var secondStringRepresentation = GetUnaryStringPreserntation(secondNumber);

            //Use concatenation of these two strings 
            //Note it is necessary to use some symbol ( for example “#”) to separate
            var resultString = firstStringRepresentation + "#" + secondStringRepresentation;

            Console.WriteLine(resultString);

            //Check the numbers on the equality 0
            //Realize the  algorithm for replacing '1#1' to '#' by using the for loop 
            //Delete the '#' from algorithm result
            //Output the result 

            if (firstNumber == 0)
                resultString = secondStringRepresentation;
            else if (secondNumber == 0)
                resultString = firstStringRepresentation;
            else
            {
                var lessNumber = firstNumber > secondNumber ? secondNumber : firstNumber;

                for (var i = 0; i < lessNumber; i++)
                {
                    resultString = resultString.Replace("1#1", "#");
                }

                var sharpArray = new char[] {'#'};

                resultString = resultString.Trim(sharpArray);
            }

            Console.WriteLine("Absolute difference is: " + resultString.Length + " unary: " + resultString);
        }

        public static string GetUnaryStringPreserntation(uint number)
        {
            var resStr = "";

            for (var i = 0; i < number; i++)
                resStr += "1";

            return resStr;
        }
        #endregion

        #region My_music
        static void My_music()
        {
            //HappyBirthday
            Thread.Sleep(2000);
            Console.Beep(264, 125);
            Thread.Sleep(250);
            Console.Beep(264, 125);
            Thread.Sleep(125);
            Console.Beep(297, 500);
            Thread.Sleep(125);
            Console.Beep(264, 500);
            Thread.Sleep(125);
            Console.Beep(352, 500);
            Thread.Sleep(125);
            Console.Beep(330, 1000);
            Thread.Sleep(250);
            Console.Beep(264, 125);
            Thread.Sleep(250);
            Console.Beep(264, 125);
            Thread.Sleep(125);
            Console.Beep(297, 500);
            Thread.Sleep(125);
            Console.Beep(264, 500);
            Thread.Sleep(125);
            Console.Beep(396, 500);
            Thread.Sleep(125);
            Console.Beep(352, 1000);
            Thread.Sleep(250);
            Console.Beep(264, 125);
            Thread.Sleep(250);
            Console.Beep(264, 125);
            Thread.Sleep(125);
            Console.Beep(2642, 500);
            Thread.Sleep(125);
            Console.Beep(440, 500);
            Thread.Sleep(125);
            Console.Beep(352, 250);
            Thread.Sleep(125);
            Console.Beep(352, 125);
            Thread.Sleep(125);
            Console.Beep(330, 500);
            Thread.Sleep(125);
            Console.Beep(297, 1000);
            Thread.Sleep(250);
            Console.Beep(466, 125);
            Thread.Sleep(250);
            Console.Beep(466, 125);
            Thread.Sleep(125);
            Console.Beep(440, 500);
            Thread.Sleep(125);
            Console.Beep(352, 500);
            Thread.Sleep(125);
            Console.Beep(396, 500);
            Thread.Sleep(125);
            Console.Beep(352, 1000);
        }
        #endregion

        #region Morse
        static void Morse_code()
        {
            const int symbolRow = 0;
            const int morseRow = 1;
            const int soundFrequency = 1000;
            const int dotDurationInMiliSeconds = 250;
            const int dashDurationInMiliSeconds = 750;
            const int timeOutInMiliSeconds = 50;
           
            //Create string variable for 'sos
            Console.WriteLine("Enter some text");
            var word = Console.ReadLine();
            
            //Use string array for Morse code
            string[,] Dictionary_arr = new string [,] { { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" },
            { ".-   ", "-... ", "-.-. ", "-..  ", ".    ", "..-. ", "--.  ", ".... ", "..   ", ".--- ", "-.-  ", ".-.. ", "--   ", "-.   ", "---  ", ".--. ", "--.- ", ".-.  ", "...  ", "-    ", "..-  ", "...- ", ".--  ", "-..- ", "-.-- ", "--.. ", "-----", ".----", "..---", "...--", "....-", ".....", "-....", "--...", "---..", "----." }};

            //Use ToCharArray() method for string to copy charecters to Unicode character array
            var vordAsArray = word.ToCharArray();
            //Use foreach loop for character array in which

            foreach (var symbol in vordAsArray)
            {
                for (var i = 0; i < Dictionary_arr.GetLength(1); i++)
                {
                    if (symbol.ToString() == Dictionary_arr[symbolRow, i])
                    {
                        var morseReppresentation = Dictionary_arr[morseRow, i].Trim().ToCharArray();

                        Console.WriteLine(symbol + " " + Dictionary_arr[morseRow, i]);

                        foreach (var dotOrDash in morseReppresentation)
                        {
                            switch (dotOrDash)
                            {
                                case '.':
                                    Console.Beep(soundFrequency, dotDurationInMiliSeconds);
                                    break;
                                case '-':
                                    Console.Beep(soundFrequency, dashDurationInMiliSeconds);
                                    break;
                            }
                            Thread.Sleep(timeOutInMiliSeconds);
                        }
                    }
                }

            }

                //Implement Console.Beep(1000, 250) for '.'
                // and Console.Beep(1000, 750) for '-'
                //Use Thread.Sleep(50) to separate sounds
            //                  
        }

        #endregion
    }
}
