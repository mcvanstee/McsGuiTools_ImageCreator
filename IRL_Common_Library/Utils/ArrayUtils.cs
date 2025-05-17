namespace IRL_Common_Library.Utils
{
    public static class ArrayUtils
    {
        public static void AppendByteToArray(ref byte[] array, byte number)
        {
            AppendToArray(ref array, new[] { number });
        }

        public static void AppendUint16ToArray(ref byte[] array, ushort number)
        {
            AppendToArray(ref array, BitConverter.GetBytes(number));
        }

        public static void AppendUint32ToArray(ref byte[] array, uint number)
        {
            AppendToArray(ref array, BitConverter.GetBytes(number));
        }

        public static void AppendFloatToArray(ref byte[] array, float number)
        {
            AppendToArray(ref array, BitConverter.GetBytes(number));
        }

        public static byte GetUInt8FromArray(byte[] array, byte startIndex = 0)
        {
            if (startIndex + 1 <= array.Length)
                return array[startIndex];

            return 0;
        }

        public static uint GetUInt32FromArray(byte[] array, byte startIndex = 0)
        {
            const byte numberOfBytesUint32 = 4;

            if ((numberOfBytesUint32 + startIndex) <= array.Length)
                return BitConverter.ToUInt32(array, startIndex);

            return 0;
        }

        public static ushort GetUInt16FromArray(byte[] array, byte startIndex = 0)
        {
            const byte numberOfBytesUint16 = 2;

            if ((numberOfBytesUint16 + startIndex) <= array.Length)
                return BitConverter.ToUInt16(array, startIndex);

            return 0;
        }

        public static float GetFloatFromArray(byte[] array, byte startIndex = 0)
        {
            const byte numberOfBytesFloat = 4;

            if ((numberOfBytesFloat + startIndex) <= array.Length)
                return BitConverter.ToSingle(array, startIndex);

            return 0;
        }

        public static void AppendToArray(ref byte[] destinationArray, byte[] sourceArray)
        {
            byte[] newArray = new byte[destinationArray.Length + sourceArray.Length];
            destinationArray.CopyTo(newArray, 0);
            sourceArray.CopyTo(newArray, destinationArray.Length);

            destinationArray = newArray;
        }
    }
}
