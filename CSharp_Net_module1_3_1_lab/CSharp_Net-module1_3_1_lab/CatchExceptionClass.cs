using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_Net_module1_3_1_lab
{
    class CatchExceptionClass
    {
        public void CatchExceptionMethod(CallingExeptionTypes exeptionType)
        {
            MyArray myArray = new MyArray();
            try
            {                
                int[] array = new int[] { 1, 4, 8, 5, 9, 6 };

                switch (exeptionType)
                {
                    case CallingExeptionTypes.OutOfRange: 
                        myArray.Assign(array, 4);
                        break;
                    case CallingExeptionTypes.DivisionByZero:
                        // 3) replace second elevent of array by 0
                        array[2] = 0;
                        myArray.Assign(array, 5);
                        break;
                    case CallingExeptionTypes.NullReference:
                        myArray.Assign(null, 4);
                        break;
                    case CallingExeptionTypes.NoException:
                        myArray.Assign(array, 5);
                        break;
                }
            }           
                
            // 8) catch all other exceptions here
            catch (Exception ex)
            {
                // 9) print System.Exception properties:
                // HelpLink, Message, Source, StackTrace, TargetSite
                PrintExceptionDetalis(ex);
            }

            // 10) add finally block, print some message
            // explain features of block finally
            finally
            {
                myArray.ShowArray();
            }
        }

        public static void PrintExceptionDetalis(Exception ex, string tabulation = "")
        {
            Console.WriteLine($"{tabulation} {ex.GetType()} message : {ex.Message}");
            Console.WriteLine($"{tabulation}helplink: {ex.HelpLink}");
            Console.WriteLine($"{tabulation}Source: {ex.Source}");
            Console.WriteLine($"{tabulation}StackTrace: {ex.StackTrace}");
            Console.WriteLine($"{tabulation}TargetSite: {ex.TargetSite}");

            if (ex.InnerException != null)
            {
                Console.WriteLine($@"{tabulation}Inner exception <<");
                PrintExceptionDetalis(ex.InnerException, tabulation + "  ");
                Console.WriteLine($"{tabulation}>> inner exception");
            }
        }
    }
}
