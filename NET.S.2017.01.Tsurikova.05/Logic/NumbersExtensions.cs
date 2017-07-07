using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class NumberExtensions
    {
        #region Gcd

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

        public static int Gcd(int a, int b, int c) => Gcd(Gcd(a, b), c);

        public static int Gcd(int[] arg)
        {
            int result = arg[0];

            for (int i = 1; i < arg.Length; i++)
            {
                result = Gcd(result, arg[i]);
            }

            return result;
        }

        #endregion

        #region GcdTime

        public static int Gcd(int a, int b, out TimeSpan time)
        {
            Stopwatch watch = new Stopwatch();

            watch.Start();
            int result = Gcd(a, b);
            watch.Stop();

            time = watch.Elapsed;

            return result;
        }

        public static int Gcd(int a, int b, int c, out TimeSpan time)
        {
            Stopwatch watch = new Stopwatch();

            watch.Start();
            int result = Gcd(a, b, c);
            watch.Stop();

            time = watch.Elapsed;

            return result;
        }

        public static int Gcd(int[] arg, out TimeSpan time)
        {
            Stopwatch watch = new Stopwatch();

            watch.Start();
            int result = Gcd(arg);
            watch.Stop();

            time = watch.Elapsed;

            return result;
        }

        #endregion

        #region GcdBinary

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

        public static int GcdBinary(int a, int b, int c) => GcdBinary(GcdBinary(a, b), c);

        public static int GcdBinary(int[] arg)
        {
            int result = arg[0];

            for (int i = 1; i < arg.Length; i++)
            {
                result = GcdBinary(result, arg[i]);
            }

            return result;
        }

        #endregion

        #region GcdTimeBinary

        public static int GcdBinary(int a, int b, out TimeSpan time)
        {
            Stopwatch watch = new Stopwatch();

            watch.Start();
            int result = GcdBinary(a, b);
            watch.Stop();

            time = watch.Elapsed;

            return result;
        }

        public static int GcdBinary(int a, int b, int c, out TimeSpan time)
        {
            Stopwatch watch = new Stopwatch();

            watch.Start();
            int result = GcdBinary(a, b, c);
            watch.Stop();

            time = watch.Elapsed;

            return result;
        }

        public static int GcdBinary(int[] arg, out TimeSpan time)
        {
            Stopwatch watch = new Stopwatch();

            watch.Start();
            int result = GcdBinary(arg);
            watch.Stop();

            time = watch.Elapsed;

            return result;
        }

        #endregion
    }
}
