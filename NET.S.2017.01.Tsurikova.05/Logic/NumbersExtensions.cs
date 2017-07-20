using System;
using System.Collections.Generic;
using System.Configuration.Internal;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    /// <summary>
    /// class provides additional methods for numbers
    /// </summary>
    public class NumberExtensions
    {
        #region Gcd

        /// <summary>
        /// calculate greatest common divisor for a and b
        /// </summary>
        /// <param name="a">one number</param>
        /// <param name="b">second number</param>
        /// <returns>greatest common divisor for a and b</returns>
        public static int Gcd(int a, int b)
        {
            if (a == 0) return b;
            if (b == 0) return a;

            a = Math.Abs(a);
            b = Math.Abs(b);

            while (a != b)
            {
                if (a > b) a -= b;
                else b -= a;
            }

            return a;
        }

        /// <summary>
        /// calculate greatest common divisor for a, b and c
        /// </summary>
        /// <param name="a">first number</param>
        /// <param name="b">second number</param>
        /// <param name="c">third number</param>
        /// <returns>common divisor for a, b and c</returns>
        public static int Gcd(int a, int b, int c) => Gcd3Arg(Gcd, a, b, c);

        /// <summary>
        /// calculate greatest common divisor for more then 3 numbers
        /// </summary>
        /// <param name="arg">numbers for which calculates gcd</param>
        /// <exception cref="ArgumentNullException">throws when arg is null</exception>
        /// <returns>greatest common divisor for arg</returns>
        public static int Gcd(int[] arg) => GcdParamsArg(Gcd, arg);

        #endregion

        #region GcdTime

        /// <summary>
        /// calculate gcd and define time elapsed
        /// </summary>
        /// <param name="a">first number</param>
        /// <param name="b">second number</param>
        /// <param name="time">time elapsed</param>
        /// <returns>gcd for a and b</returns>
        public static int Gcd(int a, int b, out long time) => GcdTime2Arg(Gcd, a, b, out time);

        /// <summary>
        /// calculate gcd and define time elapsed
        /// </summary>
        /// <param name="a">first number</param>
        /// <param name="b">second number</param>
        /// <param name="c">third number</param>
        /// <param name="time">time elapsed</param>
        /// <returns>gcd for a, b and c</returns>
        public static int Gcd(int a, int b, int c, out long time) => GcdTime3Arg(Gcd, a, b, c, out time);

        /// <summary>
        /// calculate gcd and define time elapsed
        /// </summary>
        /// <param name="arg">numbers for which gcd calculates</param>
        /// <param name="time">time elapsed</param>
        /// <returns>gcd for numbers</returns>
        public static int Gcd(int[] arg, out long time) => GcdTimeParamsArg(Gcd, arg, out time);

        #endregion

        #region GcdBinary

        /// <summary>
        /// calculate greatest common divisor for a and b
        /// </summary>
        /// <param name="a">one number</param>
        /// <param name="b">second number</param>
        /// <returns>greatest common divisor for a and b</returns>
        public static int GcdBinary(int a, int b)
        {
            if (a == 0) return b;
            if (b == 0) return a;

            a = Math.Abs(a);
            b = Math.Abs(b);

            int shift, t;
            for (shift = 0; ((a | b) & 1) == 0; ++shift)
            {
                a >>= 1;
                b >>= 1;
            }

            while ((a & 1) == 0)
                a >>= 1;

            do
            {
                while ((b & 1) == 0)
                    b >>= 1;

                if (a > b) { t = b; b = a; a = t; }
                b -= a;

            } while (b != 0);

            return a << shift;
        }

        /// <summary>
        /// calculate greatest common divisor for a, b and c
        /// </summary>
        /// <param name="a">first number</param>
        /// <param name="b">second number</param>
        /// <param name="c">third number</param>
        /// <returns>common divisor for a, b and c</returns>
        public static int GcdBinary(int a, int b, int c) => Gcd3Arg(GcdBinary, a, b, c);

        /// <summary>
        /// calculate greatest common divisor for more then 3 numbers
        /// </summary>
        /// <param name="arg">numbers for which calculates gcd</param>
        /// <exception cref="ArgumentNullException">throws when arg is null</exception>
        /// <returns>greatest common divisor for arg</returns>
        public static int GcdBinary(int[] arg) => GcdParamsArg(GcdBinary, arg);

        #endregion

        #region GcdTimeBinary

        /// <summary>
        /// calculate gcd and define time elapsed
        /// </summary>
        /// <param name="a">first number</param>
        /// <param name="b">second number</param>
        /// <param name="time">time elapsed</param>
        /// <returns>gcd for a and b</returns>
        public static int GcdBinary(int a, int b, out long time) => GcdTime2Arg(GcdBinary, a, b, out time);

        /// <summary>
        /// calculate gcd and define time elapsed
        /// </summary>
        /// <param name="a">first number</param>
        /// <param name="b">second number</param>
        /// <param name="c">third number</param>
        /// <param name="time">time elapsed</param>
        /// <returns>gcd for a, b and c</returns>
        public static int GcdBinary(int a, int b, int c, out long time) => GcdTime3Arg(GcdBinary, a, b, c, out time);

        /// <summary>
        /// calculate gcd and define time elapsed
        /// </summary>
        /// <param name="arg">numbers for which gcd calculates</param>
        /// <param name="time">time elapsed</param>
        /// <returns>gcd for numbers</returns>
        public static int GcdBinary(int[] arg, out long time) => GcdTimeParamsArg(GcdBinary, arg, out time);

        #endregion

        private static int Gcd3Arg(Func<int, int, int> method, int a, int b, int c)
            => method(a, method(b, c));

        private static int GcdParamsArg(Func<int, int, int> method, int[] arg)
        {
            if (ReferenceEquals(arg, null)) throw new ArgumentNullException($"{nameof(arg)} is null");
            if (arg.Length == 0) return 0;

            int result = arg[0];
            for (int i = 1; i < arg.Length; i++)
            {
                result = method(result, arg[i]);
            }

            return result;
        }

        private static int GcdTime2Arg(Func<int, int, int> method, int a, int b, out long time)
        {
            Stopwatch watch = new Stopwatch();

            watch.Start();
            int result = method(a, b);
            watch.Stop();

            time = watch.ElapsedMilliseconds;

            return result;
        }

        private static int GcdTime3Arg(Func<int, int, int, int> method, int a, int b, int c, out long time)
        {
            Stopwatch watch = new Stopwatch();

            watch.Start();
            int result = method(a, b, c);
            watch.Stop();

            time = watch.ElapsedMilliseconds;

            return result;
        }

        private static int GcdTimeParamsArg(Func<int[], int> method, int[] arg, out long time)
        {
            Stopwatch watch = new Stopwatch();

            watch.Start();
            int result = method(arg);
            watch.Stop();

            time = watch.ElapsedMilliseconds;

            return result;
        }
    }
}
