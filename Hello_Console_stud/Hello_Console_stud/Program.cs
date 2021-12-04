using System;
using System.Threading;

namespace Hello_Console_stud
{
    class Program
    {
        static void Main(string[] args)
        {
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
                        var operation = (int)uint.Parse(Console.ReadLine());
                        switch (operation)
                        {
                            case 1:
                                MyStrings();
                                Console.WriteLine("");
                                break;
                            case 2:
                                MyBinary();
                                Console.WriteLine("");
                                break;
                            case 3:
                                MyMusic();
                                Console.WriteLine("");
                                break;
                            case 4:
                                MorseCode();
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
        static void MyBinary()
        {
            //Implement positive integer variable input
            Console.Write("Enter positive number: ");
            var input = Console.ReadLine();
            var num = uint.Parse(input);
            //Present it like binary string
            //   For example, 4 as 100
            var binStr = "";
            while (num > 0)
            {
                //Use modulus operator to obtain the remainder  (n % 2) 
                //and divide variable by 2 in the loop
                binStr += num % 2;
                num /= 2;
            }
            //Use the ToCharArray() method to transform string to chararray
            //and Array.Reverse() method
            char[] binArray = binStr.ToCharArray();
            Array.Reverse(binArray);
            Console.WriteLine(new string(binArray));
        }
        #endregion

        #region ToFromUnary
        static void MyStrings()
        {
            //Declare int and string variables for decimal and binary presentations
            Console.Write("Enter first number: ");
            var input1 = Console.ReadLine();
            Console.Write("Enter second number: ");
            var input2 = Console.ReadLine();
            //Implement two positive integer variables input
            var num1 = uint.Parse(input1);
            var num2 = uint.Parse(input2);
            //To present each of them in the form of unary string use for loop
            var valueOfDifference = (uint)Math.Abs(num1 - num2);
            //Use concatenation of these two strings 
            //Note it is necessary to use some symbol ( for example “#”) to separate
            string un1, un2, un3;
            un1 = ConvertToUnary(num1);
            un2 = ConvertToUnary(num2);
            un3 = ConvertToUnary(valueOfDifference);
            //Check the numbers on the equality 0
            //Realize the  algorithm for replacing '1#1' to '#' by using the for loop 
            //Delete the '#' from algorithm result
            Console.WriteLine($"Unary for {num1} is {un1}");
            Console.WriteLine($"Unary for {num2} is {un2}");
            Console.WriteLine($"Absolute value of the differenece between {un1}#{un2} is {un3}");
            //Output the result 
        }
        static string ConvertToUnary(uint num)
        {
            string result = "";
            for (var i=0; i < num; i++)
            {
                result += "1";
            }
            return result;
        }
        #endregion

        #region My_music
        static void MyMusic()
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
        static void MorseCode()
        {
            //Create string variable for 'sos'      
            var sos = "sos";
            //Use string array for Morse code
            string[,] Dictionary_arr = new string [,] { { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" },
            { ".-   ", "-... ", "-.-. ", "-..  ", ".    ", "..-. ", "--.  ", ".... ", "..   ", ".--- ", "-.-  ", ".-.. ", "--   ", "-.   ", "---  ", ".--. ", "--.- ", ".-.  ", "...  ", "-    ", "..-  ", "...- ", ".--  ", "-..- ", "-.-- ", "--.. ", "-----", ".----", "..---", "...--", "....-", ".....", "-....", "--...", "---..", "----." }};
            //Use ToCharArray() method for string to copy charecters to Unicode character array
            //Use foreach loop for character array in which
            foreach (var character in sos.ToCharArray())
            {
                for (var i = 0; i < Dictionary_arr.GetLength(1); i++)
                {
                    if (character == char.Parse(Dictionary_arr[0, i]))
                    {
                        foreach (var morseCodeCharacter in Dictionary_arr[1, i].ToCharArray())
                        {
                            if (morseCodeCharacter == '.')
                            {
                                //Implement Console.Beep(1000, 250) for '.'
                                Console.Beep(1000, 250);
                            }
                            else if (morseCodeCharacter == '-')
                            {
                                // and Console.Beep(1000, 750) for '-'
                                Console.Beep(1000, 750);
                            }
                            else if (morseCodeCharacter == ' ')
                            {
                                continue;
                            }
                            //Use Thread.Sleep(50) to separate sounds
                            Thread.Sleep(50);
                        }
                    }
                }
            }                  
        }

        #endregion
    }
}
