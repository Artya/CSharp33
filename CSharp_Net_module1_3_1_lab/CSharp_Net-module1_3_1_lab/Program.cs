using System;
using System.IO;

namespace CSharp_Net_module1_3_1_lab
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Hello, what kind of exception would you like?");
                Console.WriteLine($"{(int)CallingExeptionTypes.OutOfRange} - {CallingExeptionTypes.OutOfRange}");
                Console.WriteLine($"{(int)CallingExeptionTypes.DivisionByZero} - {CallingExeptionTypes.DivisionByZero}");
                Console.WriteLine($"{(int)CallingExeptionTypes.NullReference} - {CallingExeptionTypes.NullReference}");
                Console.WriteLine($"{(int)CallingExeptionTypes.NoException} - {CallingExeptionTypes.NoException}");

                var input = Console.ReadLine();
                try
                {
                    var convertedInput = int.Parse(input);

                    if (convertedInput < 1 || convertedInput > 4)
                    {
                        Console.WriteLine($"Value {convertedInput} is absent in list, try again");
                        continue;
                    }

                    var callingExceptionType = (CallingExeptionTypes)convertedInput;

                    CatchExceptionClass cec = new CatchExceptionClass();
                    cec.CatchExceptionMethod(callingExceptionType);
                    break;
                }
                catch (InvalidCastException ex)
                {
                    Console.WriteLine("Error converting, try again");
                    continue;
                }
            }

            // 11) Make some unhandled exception and study Visual Studio debugger report – 
            // read description and find the reason of exception
            //var someValue = int.Parse("100 d 500");
        }
    }
}
