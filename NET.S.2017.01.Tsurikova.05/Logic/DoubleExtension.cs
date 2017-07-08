using System;
using System.Collections.Generic;
using System.Linq;
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
            => Convert.ToString(BitConverter.DoubleToInt64Bits(d), 2);
    }
}
