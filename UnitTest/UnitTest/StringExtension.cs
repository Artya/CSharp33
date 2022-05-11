using System;

namespace UnitTest
{
    public static class StringExtension
    {
        public static bool IsBaseColor(this string color)
        {
            var baseColors = new string[2] { "black", "white" };

            foreach (var item in baseColors)
            {
                if (color.Equals(item, StringComparison.CurrentCultureIgnoreCase))
                    return true;
            }

            return false;
        }
    }
}
