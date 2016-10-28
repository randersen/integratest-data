using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integratest.Data.Security
{
    public static class StringExtensions
    {

        public static byte[] FromBase64ToBytes(this string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            value = value.Replace('_', '/').Replace('-', '+');

            switch (value.Length % 4)
            {
                case 1: throw new FormatException("Invalid length for a Base-64 char array or string.");
                case 2: value += "=="; break;
                case 3: value += "="; break;
            }

            return Convert.FromBase64String(value);
        }

        public static int ToInt32(this byte[] value, int startIndex)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }
            if (startIndex < 0 || startIndex > value.Length - 4)
            {
                throw new ArgumentException("startIndex must be within the value length");
            }

            if (BitConverter.IsLittleEndian)
            {
                var bytes = new byte[4];
                Array.Copy(value, startIndex, bytes, 0, 4);
                Array.Reverse(bytes);
                return BitConverter.ToInt32(bytes, 0);
            }
            else
            {
                return BitConverter.ToInt32(value, startIndex);
            }
        }

        public static bool CryptoEquals(this byte[] original, byte[] value)
        {
            if (original == null)
            {
                throw new ArgumentNullException("original");
            }
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            var diff = (uint)original.Length ^ (uint)value.Length;
            for (int i = 0; i < original.Length && i < value.Length; i++)
            {
                diff |= (uint)original[i] ^ (uint)value[i];
            }
            return diff == 0;
        }

    }
}
