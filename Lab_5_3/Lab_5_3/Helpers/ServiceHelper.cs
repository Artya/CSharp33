using System;
using System.Text;

namespace Lab_5_3
{
    public static class ServiceHelper
    {
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
    }
}
