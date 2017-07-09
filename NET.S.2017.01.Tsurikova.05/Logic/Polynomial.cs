using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class Polynomial : IEquatable<Polynomial>
    {
        private readonly double[] coefficients;

        #region ctors

        /// <summary>
        /// ctor for params double[]
        /// </summary>
        /// <param name="array">array of coefficients</param>
        /// <exception cref="ArgumentNullException">throws when array is null</exception>
        public Polynomial(params double[] array)
        {
            if (ReferenceEquals(array, null)) throw new ArgumentNullException($"{nameof(array)} is null");
            coefficients = new double[array.Length];
            Array.Copy(array, coefficients, array.Length);
        }
        
        /// <summary>
        /// ctor for polynomial
        /// </summary>
        /// <param name="p">polynomial</param>
        /// <exception cref="ArgumentNullException">throws when p is null</exception>
        public Polynomial(Polynomial p)
        {
            if (ReferenceEquals(p, null)) throw new ArgumentNullException($"{nameof(p)} is null");
            coefficients = new double[p.Degree + 1];
            Array.Copy(p.coefficients, coefficients, p.Degree + 1);
        }

        /// <summary>
        /// ctor for IEnumerable
        /// </summary>
        /// <param name="other">ienumeration of coefficients</param>
        /// <exception cref="ArgumentNullException">throws when other is null</exception>
        public Polynomial(IEnumerable<double> other)
        {
            if (ReferenceEquals(other, null)) throw new ArgumentNullException($"{nameof(other)} is null");
            coefficients = new double[other.Count()];
            int i = 0;
            foreach (var item in other)
            {
                coefficients[i++] = item;
            }
        }

        #endregion

        #region Indexer

        public double this[int i]
        {
            get
            {
                if (i < 0 || i > Degree) throw new IndexOutOfRangeException($"{nameof(i)} is invalid");
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

        #endregion

        #region Override methods

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            for (int i = Degree; i >= 0; i--)
            {
                if (coefficients[i] == 0) continue;
                if (i != 0)
                    sb.Append($"{coefficients[i]}x^{i}+");
                else
                    sb.Append($"{coefficients[i]}x^{i}");
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

        public static bool operator ==(Polynomial p1, Polynomial p2)
        {
            if (ReferenceEquals(p1, null) && ReferenceEquals(p2, null)) return true;
            if (ReferenceEquals(p1, null)) return false;
            if (ReferenceEquals(p2, null)) return false;
            return p1.Equals(p2);
        }

        public static bool operator !=(Polynomial p1, Polynomial p2) => !(p1 == p2);

        public static Polynomial operator +(Polynomial p1, Polynomial p2) => Add(p1, p2);

        public static Polynomial operator -(Polynomial p1, Polynomial p2) => Subtraction(p1, p2);

        public static Polynomial operator *(Polynomial p1, Polynomial p2) => Multiplication(p1, p2);

        #endregion

        #region basic operations

        /// <summary>
        /// calculates sum of 2 polynomial
        /// </summary>
        /// <param name="p1">first summand</param>
        /// <param name="p2">second summand</param>
        /// <returns>new polynomial for sum</returns>
        public static Polynomial Add(Polynomial p1, Polynomial p2)
        {
            if (ReferenceEquals(p1, null)) throw new ArgumentNullException($"{nameof(p1)} is null");
            if (ReferenceEquals(p2, null)) throw new ArgumentNullException($"{nameof(p2)} is null");

            double[] array = new double[Math.Max(p1.Degree + 1, p2.Degree + 1)];

            int i;
            for (i = 0; i <= Math.Min(p1.Degree, p2.Degree); i++)
            {
                array[i] = p1[i] + p2[i];
            }

            if (p1.Degree > p2.Degree)
            {
                for (; i <= p1.Degree; i++)
                    array[i] = p1[i];
            }
            if (p2.Degree > p1.Degree)
            {
                for (; i <= p2.Degree; i++)
                    array[i] = p2[i];
            }

            return new Polynomial(array);
        }

        /// <summary>
        /// calculates difference between 2 polynomial
        /// </summary>
        /// <param name="p1">minuend</param>
        /// <param name="p2">subtrahend</param>
        /// <returns>new polynomial for differnce</returns>
        public static Polynomial Subtraction(Polynomial p1, Polynomial p2)
        {
            if (ReferenceEquals(p1, null)) throw new ArgumentNullException($"{nameof(p1)} is null");
            if (ReferenceEquals(p2, null)) throw new ArgumentNullException($"{nameof(p2)} is null");

            double[] array = new double[Math.Max(p1.Degree + 1, p2.Degree + 1)];

            int i;
            for (i = 0; i <= Math.Min(p1.Degree, p2.Degree); i++)
            {
                array[i] = p1[i] - p2[i];
            }

            if (p1.Degree > p2.Degree)
            {
                for (; i <= p1.Degree; i++)
                    array[i] = p1[i];
            }
            if (p2.Degree > p1.Degree)
            {
                for (; i <= p2.Degree; i++)
                    array[i] = -p2[i];
            }

            return new Polynomial(array);
        }

        /// <summary>
        /// calculates product of 2 polynomial
        /// </summary>
        /// <param name="p1">first polynomial</param>
        /// <param name="p2">second polynomial</param>
        /// <returns>new polynomial for product</returns>
        public static Polynomial Multiplication(Polynomial p1, Polynomial p2)
        {
            if (ReferenceEquals(p1, null)) throw new ArgumentNullException($"{nameof(p1)} is null");
            if (ReferenceEquals(p2, null)) throw new ArgumentNullException($"{nameof(p2)} is null");

            double[] array = new double[p1.Degree + p2.Degree + 1];

            for (int i = 0; i <= p1.Degree; i++)
            {
                for (int j = 0; j <= p2.Degree; j++)
                {
                    array[i + j] += p1[i] * p2[j];
                }
            }

            return new Polynomial(array);
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
                result += coefficients[i] * Math.Pow(value, i);
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
                if (!(Math.Abs(coefficients[i]- other.coefficients[i])<0.0001)) return false;
            }

            return true;
        } 

        #endregion
    }
}
