using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    /// <summary>
    /// provides extension methods for double
    /// </summary>
    public static class DoubleExtension
    {
        /// <summary>
        /// method for obtaining binary representation of double
        /// </summary>
        /// <param name="d">number to be represented</param>
        /// <returns>binary representation for d</returns>
        public static string GetBinaryRepresentation(this double d)
        {
            StringBuilder sb = new StringBuilder(Convert.ToString(BitConverter.DoubleToInt64Bits(d), 2));
            while (sb.Length < 64)
            {
                sb.Insert(0, "0");
            }
            return sb.ToString();
        }

        /// <summary>
        /// method for obtaining binary representation of double
        /// </summary>
        /// <param name="d">number to be represented</param>
        /// <returns>binary representation for d</returns>
        public static string GetBinaryRepresentationUnion(this double d)
        {
            InnerUnion sv = new InnerUnion(d);

            StringBuilder sb = new StringBuilder("");

            for (var i = 0; i < sizeof(long) * 8; i++)
                sb.Append((sv.NL & ((long)1 << (63 - i))) != 0 ? '1' : '0');

            return sb.ToString();
        }

        [StructLayout(LayoutKind.Explicit)]
        private class InnerUnion
        {
            [FieldOffset(0)]
            private long numberL;

            [FieldOffset(0)]
            private double numberD;

            public InnerUnion(double d)
            {
                numberD = d;
            }

            public long NL => numberL;
        }
    }
}
