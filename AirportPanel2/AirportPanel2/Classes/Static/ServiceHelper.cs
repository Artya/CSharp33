using System;
using System.Text;

namespace AirportPanel2
{
    public static class ServiceHelper
    {
        public static Array AddElementToArray(Array array, Type elementType, object element)
        {
            var newArrayLength = array == null ? 1 : array.Length + 1;
            var newArray = Array.CreateInstance(elementType, newArrayLength);

            if (array != null)
                Array.Copy(array, newArray, array.Length);

            newArray.SetValue(element, newArray.Length - 1);

            return newArray;
        }

        public static Array GetEnumValues(Type enumType)
        {
            return Enum.GetValues(enumType);
        }

        public static string ConvertFromCamelCaseToUsualText(string camelText)
        {
            if (camelText.Length <= 1)
                return camelText;

            var builder = new StringBuilder();

            builder.Append(camelText[0]);

            for (var i = 1; i < camelText.Length; i++)
            {

                if (camelText[i].ToString().ToLower() == camelText[i].ToString())
                {
                    builder.Append(camelText[i]);
                    continue;
                }

                builder.Append(' ');
                builder.Append(camelText[i].ToString().ToLower());
            }

            return builder.ToString();
        }

        public static Array DeleteElementFromArray(Array array, object element, Type elementType)
        {
            if (element == null)
                return array;

            var newArrayLengt = array.Length - 1;
            var newArray = Array.CreateInstance(elementType, newArrayLengt);

            var index = Array.IndexOf(array, element);

            if (index > 0)
            {
                Array.Copy(array, 0, newArray, 0, index);
            }

            if (index < (array.Length - 1))
            {
                Array.Copy(array, index+1, newArray, index, (array.Length - index -1));
            }

            return newArray;
        }
    }
}
