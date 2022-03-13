using System;
using System.Text;

namespace AirportPanel2
{
    public static class UserInteraction
    {
        public static DateTime GetDate(string title = "Enter a value")
        {
            while (true)
            {
                Console.WriteLine(title);

                var input = Console.ReadLine();

                try
                {
                    var result = DateTime.Parse(input);
                    return result;
                }
                catch
                {
                    Console.WriteLine($"Error converting {input} to Date / Time, try again.");
                }
            }
        }

        public static double GetDouble(string title = "Enter a value")
        {
            while (true)
            {
                Console.WriteLine(title);

                var input = Console.ReadLine();

                try
                {
                    var result = double.Parse(input);
                    return result;
                }
                catch
                {
                    Console.WriteLine($"Error converting {input} to double, try again.");
                }
            }
        }

        public static string GetString(string title = "Enter a value")
        {
            Console.WriteLine(title);
            return Console.ReadLine();
        }

        public static object ChooseElemementFromArray(Array array, string title, int exceptionIndex = -1)
        {
            var builder = new StringBuilder();
            builder.AppendLine(title);

            if (exceptionIndex != -1)
                exceptionIndex++;

            var index = 0;

            foreach (var element in array)
            {
                index++;

                if (exceptionIndex == index)
                    continue;

                builder.AppendLine($"{index} - {element}");
            }

            while (true)
            {
                Console.Write(builder);

                var input = Console.ReadLine();

                try
                {
                    index = int.Parse(input);

                    if (index == 0 
                        || index > array.Length 
                        || index == exceptionIndex)
                    {
                        Console.WriteLine($"Entered value is missing in suggested list");
                        continue;
                    }

                    return array.GetValue(index - 1);
                }
                catch
                {
                    Console.WriteLine($"Can`t convert {input} to number, enter a new value... ");
                }
            }
        }

        public static int ChooseEnumValue(string title, Type enumerationType, int exceptionElement = -1)
        {
            var maxIndex = int.MinValue;
            var minIndex = int.MaxValue;

            var builder = new StringBuilder();
            builder.AppendLine(title);

            var values = ServiceHelper.GetEnumValues(enumerationType);

            foreach (var currentEnumValue in values)
            {
                var indexOfEnum = (int)currentEnumValue;
                var enumTextRepresentation = ServiceHelper.ConvertFromCamelCaseToUsualText(currentEnumValue.ToString());  

                if (indexOfEnum == exceptionElement)
                    continue;

                maxIndex = Math.Max(maxIndex, indexOfEnum);
                minIndex = Math.Min(minIndex, indexOfEnum);

                builder.AppendLine($"{indexOfEnum} - {enumTextRepresentation}");
            }

            while (true)
            {
                Console.WriteLine(builder);

                var input = Console.ReadLine();

                try
                {
                    var result = int.Parse(input);

                    if ((result < minIndex || result > maxIndex) || (exceptionElement != -1 && result == exceptionElement))
                    {
                        Console.WriteLine("Entered value is missing in suggested list");
                        continue;
                    }

                    return result;
                }
                catch
                {
                    Console.WriteLine($"Error converting {input} to number, try again.");
                }
            }
        }
    }
}
