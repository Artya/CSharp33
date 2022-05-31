using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab_5_3
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

        public static decimal GetDecimal(string title = "Enter a value")
        {
            while (true)
            {
                Console.WriteLine(title);

                var input = Console.ReadLine();

                try
                {
                    var result = decimal.Parse(input);
                    return result;
                }
                catch
                {
                    Console.WriteLine($"Error converting {input} to double, try again.");
                }
            }
        }

        public static int GetInt(string title = "Enter a value")
        {
            while (true)
            {
                Console.WriteLine(title);

                var input = Console.ReadLine();

                try
                {
                    var result = int.Parse(input);
                    return result;
                }
                catch
                {
                    Console.WriteLine($"Error converting {input} to int, try again.");
                }
            }
        }

        public static string GetString(string title = "Enter a value")
        {
            Console.WriteLine(title);
            return Console.ReadLine();
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

        public static int ChooseElementFromList<T>(IEnumerable<T> elements, string title)
        {
            var list = elements.ToList();
            var index = 0;
            var builder = new StringBuilder();

            foreach (var element in list)
            {
                index++;
                builder.AppendLine($"{index} - {element}");
            }

            while (true)
            {
                Console.WriteLine(title);
                Console.WriteLine(builder);

                var input = Console.ReadLine();

                try
                {
                    var result = int.Parse(input);

                    if (result > list.Count || result <= 0)
                    {
                        Console.WriteLine($"Entered value is out of range, try again");
                        continue;
                    }

                    return result-1;
                }
                catch
                {
                    Console.WriteLine($"Error converting {input} to uint, try again.");
                }
            }
        }
    }
}
