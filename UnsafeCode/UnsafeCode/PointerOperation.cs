using System;

namespace UnsafeCode
{
    public static class PointerOperation
    {
        public static int? NullableInt;
        public static int NotNullableInt;

        public static byte[] ConvertToByte(int number)
        {
            var bytes = BitConverter.GetBytes(number);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(bytes);

            return bytes;
        }

        public unsafe static int Power(int* pointerToNumber, int power)
        {
            if (power == 0)
                return 1;

            var result = *pointerToNumber;
            for (var i = 1; i < power; i++)
                result *= *pointerToNumber;

            return result;
        }

        public static void SetAsNull()
        {
            NotNullableInt = NullableInt ?? 0;
        }
    }
}
