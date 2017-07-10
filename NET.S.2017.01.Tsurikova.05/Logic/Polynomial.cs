using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Globalization;
using System.Threading.Tasks;

namespace Logic
{
    public class Polynomial : IEquatable<Polynomial>, ICloneable
    {
        private static double epsilon;
        private readonly double[] coefficients;

        #region ctors

        static Polynomial()
        {
            epsilon = double.Parse(ConfigurationManager.AppSettings["epsilon"], CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// ctor for params double[]
        /// </summary>
        /// <param name="array">array of coefficients</param>
        /// <exception cref="ArgumentNullException">throws when array is null</exception>
        public Polynomial(params double[] array)
        {
            if (ReferenceEquals(array, null)) throw new ArgumentNullException($"{nameof(array)} is null");
            coefficients = new double[Array.FindLastIndex(array, d => Math.Abs(d - 0.0) > Epsilon) + 1];
            Array.Copy(array, coefficients, coefficients.Length);
        }

        #endregion

        #region Indexer

        public double this[int i]
        {
            get
            {
                if (i < 0 || i > Degree) throw new ArgumentOutOfRangeException($"{nameof(i)} is invalid");
                return coefficients[i];
            }
        }

        #endregion

        #region Properties

        public bool IsReadOnly => true;

        /// <summary>
        /// degree of polynomial
        /// </summary>
        public int Degree => coefficients.Length - 1;

        private static double Epsilon => epsilon;

        #endregion

        #region Override methods

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            for (int i = Degree; i >= 0; i--)
            {
                if (Math.Abs(this[i] - 0.0) < Epsilon) continue;
                sb.Append(i != 0 ? $"{this[i]}x^{i}+" : $"{this[i]}x^{i}");
            }

            return sb.ToString();
        }

        public override int GetHashCode()
        {
            return coefficients.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Polynomial)) return false;
            return Equals((Polynomial)obj);
        }

        #endregion

        #region operators

        public static bool operator ==(Polynomial lhs, Polynomial rhs)
        {
            if (ReferenceEquals(lhs, rhs)) return true;
            if (ReferenceEquals(lhs, null)) return false;
            return lhs.Equals(rhs);
        }

        public static Polynomial operator -(Polynomial p) => p.MultiplyByNumber(-1);

        public static bool operator !=(Polynomial lhs, Polynomial rhs) => !(lhs == rhs);

        public static Polynomial operator +(Polynomial lhs, Polynomial rhs) => Add(lhs, rhs);

        public static Polynomial operator -(Polynomial lhs, Polynomial rhs) => Subtraction(lhs, rhs);

        public static Polynomial operator *(Polynomial lhs, Polynomial rhs) => Multiplication(lhs, rhs);

        #endregion

        #region basic operations

        /// <summary>
        /// calculates sum of 2 polynomial
        /// </summary>
        /// <param name="lhs">first summand</param>
        /// <param name="rhs">second summand</param>
        /// <returns>new polynomial for sum</returns>
        public static Polynomial Add(Polynomial lhs, Polynomial rhs)
        {
            if (ReferenceEquals(lhs, null)) throw new ArgumentNullException($"{nameof(lhs)} is null");
            if (ReferenceEquals(rhs, null)) throw new ArgumentNullException($"{nameof(rhs)} is null");

            double[] array = new double[Math.Max(lhs.Degree + 1, rhs.Degree + 1)];

            int i;
            for (i = 0; i <= Math.Min(lhs.Degree, rhs.Degree); i++)
            {
                array[i] = lhs[i] + rhs[i];
            }

            if (lhs.Degree > rhs.Degree)
            {
                for (; i <= lhs.Degree; i++)
                    array[i] = lhs[i];
            }
            if (rhs.Degree > lhs.Degree)
            {
                for (; i <= rhs.Degree; i++)
                    array[i] = rhs[i];
            }

            return new Polynomial(array);
        }

        /// <summary>
        /// calculates difference between 2 polynomial
        /// </summary>
        /// <param name="lhs">minuend</param>
        /// <param name="rhs">subtrahend</param>
        /// <returns>new polynomial for differnce</returns>
        public static Polynomial Subtraction(Polynomial lhs, Polynomial rhs)
        {
            if (ReferenceEquals(lhs, null)) throw new ArgumentNullException($"{nameof(lhs)} is null");
            if (ReferenceEquals(rhs, null)) throw new ArgumentNullException($"{nameof(rhs)} is null");

            return lhs + -rhs;
        }

        /// <summary>
        /// calculates product of 2 polynomial
        /// </summary>
        /// <param name="lhs">first polynomial</param>
        /// <param name="rhs">second polynomial</param>
        /// <returns>new polynomial for product</returns>
        public static Polynomial Multiplication(Polynomial lhs, Polynomial rhs)
        {
            if (ReferenceEquals(lhs, null)) throw new ArgumentNullException($"{nameof(lhs)} is null");
            if (ReferenceEquals(rhs, null)) throw new ArgumentNullException($"{nameof(rhs)} is null");

            double[] array = new double[lhs.Degree + rhs.Degree + 1];

            for (int i = 0; i <= lhs.Degree; i++)
            {
                for (int j = 0; j <= rhs.Degree; j++)
                {
                    array[i + j] += lhs[i] * rhs[j];
                }
            }

            return new Polynomial(array);
        }

        /// <summary>
        /// multiply polynomial by a given number
        /// </summary>
        /// <param name="number">number by which the polynomial is multiplied</param>
        /// <returns>new multiplied polynomial</returns>
        public Polynomial MultiplyByNumber(double number)
        {
            double[] coef = new double[Degree + 1];

            for (int i = 0; i <= Degree; i++)
            {
                coef[i] = this[i] * number;
            }

            return new Polynomial(coef);
        }

        /// <summary>
        /// calculates value of polynomial for given value
        /// </summary>
        /// <param name="value">value for substituring</param>
        /// <returns>obtaining value</returns>
        public double ValueAt(double value)
        {
            double result = 0;
            for (int i = 0; i <= Degree; i++)
            {
                result += this[i] * Math.Pow(value, i);
            }
            return result;
        }

        #endregion

        #region interface implementation

        public bool Equals(Polynomial other)
        {
            if (ReferenceEquals(other, null)) return false;
            if (ReferenceEquals(this, other)) return true;
            if (Degree != other.Degree) return false;

            for (int i = 0; i < Degree; i++)
            {
                if (!(Math.Abs(coefficients[i] - other.coefficients[i]) < Epsilon)) return false;
            }

            return true;
        }

        object ICloneable.Clone()
        {
            return Clone();
        }

        #endregion

        public Polynomial Clone()
        {
            return new Polynomial(coefficients);
        }
    }
}
