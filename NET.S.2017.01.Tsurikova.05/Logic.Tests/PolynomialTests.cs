using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Logic.Tests
{
    [TestFixture]
    class PolynomialTests
    {
        [TestCase(new[] { 1.2, 3.4, 4.5 }, 1, ExpectedResult = 3.4)]
        public static double Indexer_Index_Element(double[] array, int index)
        {
            Polynomial p = new Polynomial(array);
            return p[index];
        }

        [TestCase(new[] { 1.2, 3.4, 4.5 }, -1)]
        [TestCase(new[] { 1.2, 3.4, 4.5 }, 3)]
        public static void Indexer_InvalidIndex_IndexOutOfRangeException(double[] array, int index)
        {
            Polynomial p = new Polynomial(array);
            Assert.Throws<IndexOutOfRangeException>(() =>
            {
                var d = p[index];
            });
        }

        [TestCase(new[] { 1.2, 3.4, 4.5 }, ExpectedResult = 2)]
        public static double Degree_Array_Degree(double[] array)
        {
            Polynomial p = new Polynomial(array);
            return p.Degree;
        }

        [TestCase(new[] { 1.2, 3.4, 4.5 }, ExpectedResult = "4,5x^2+3,4x^1+1,2x^0")]
        [TestCase(new[] { 1.2, 0, 0, 4.5 }, ExpectedResult = "4,5x^3+1,2x^0")]
        public static string ToString_Array_Result(double[] array)
        {
            Polynomial p = new Polynomial(array);
            return p.ToString();
        }

        [TestCase(new[] { 1.2, 3.4, 4.5 }, new[] { 1.2, 3.4, 4.5 }, ExpectedResult = true)]
        [TestCase(new[] { 1.2, 3.4, 4.5 }, new[] { 1.2, 4.5 }, ExpectedResult = false)]
        [TestCase(new[] { 1.2, 4.5 }, new[] { 1.2, 3.4, 4.5 }, ExpectedResult = false)]
        public static bool Equals_TwoArray_Result(double[] array, double[] other)
        {
            Polynomial p = new Polynomial(array);
            Polynomial p2 = new Polynomial(other);
            return p.Equals(p2);
        }

        [TestCase(new[] { 1.2, 3.4, 4.5 }, new[] { 1.2, 3.4, 4.5 }, ExpectedResult = true)]
        [TestCase(new[] { 1.2, 3.4, 4.5 }, new[] { 1.2, 4.5 }, ExpectedResult = false)]
        [TestCase(new[] { 1.2, 4.5 }, new[] { 1.2, 3.4, 4.5 }, ExpectedResult = false)]
        public static bool Equality_TwoArray_Result(double[] array, double[] other)
        {
            Polynomial p = new Polynomial(array);
            Polynomial p2 = new Polynomial(other);
            return p == p2;
        }

        [TestCase(new[] { 1.2, 3.4, 4.5 }, new[] { 1.2, 3.4, 4.5 }, ExpectedResult = false)]
        [TestCase(new[] { 1.2, 3.4, 4.5 }, new[] { 1.2, 4.5 }, ExpectedResult = true)]
        [TestCase(new[] { 1.2, 4.5 }, new[] { 1.2, 3.4, 4.5 }, ExpectedResult = true)]
        public static bool InEquality_TwoArray_Result(double[] array, double[] other)
        {
            Polynomial p = new Polynomial(array);
            Polynomial p2 = new Polynomial(other);
            return p != p2;
        }

        [TestCase(new[] { 1.2, 3.4, 4.5 }, new[] { 1.2, 3.4, 4.5 }, new[] { 2.4, 6.8, 9 } , ExpectedResult = true)]
        [TestCase(new[] { 1.0, 3.4, 4.5 }, new[] { 1.2, 4.5 }, new[] { 2.2, 7.9, 4.5 }, ExpectedResult = true)]
        [TestCase(new[] { 1.2, 4.5 }, new[] { 1.0, 3.4, 4.5 }, new[] { 2.2, 7.9, 4.5 }, ExpectedResult = true)]
        public static bool OperatorSum_TwoArray_Result(double[] array, double[] other, double[] result)
        {
            Polynomial p = new Polynomial(array);
            Polynomial p2 = new Polynomial(other);
            Polynomial p3 = new Polynomial(result);
            return p3 == (p + p2);
        }

        [Test]
        public static void OperatorSum_PolynomialFirst_ArgumentNullException()
        {
            Polynomial p = new Polynomial(2.3, 4.4);
            Assert.Throws<ArgumentNullException>(() => p = p + null);
        }

        [Test]
        public static void OperatorSum_PolynomialSecond_ArgumentNullException()
        {
            Polynomial p = new Polynomial(2.3, 4.4);
            Assert.Throws<ArgumentNullException>(() => p = null + p);
        }

        [TestCase(new[] { 1.2, 3.4, 4.5 }, new[] { 1.2, 3.4, 4.5 }, new[] { 0.0, 0.0, 0.0 }, ExpectedResult = true)]
        [TestCase(new[] { 1.0, 3.4, 4.5 }, new[] { 1.2, 4.5 }, new[] { -0.2, -1.1, 4.5 }, ExpectedResult = true)]
        [TestCase(new[] { 1.2, 4.5 }, new[] { 1.0, 3.4, 4.5 }, new[] { 0.2, 1.1, -4.5 }, ExpectedResult = true)]
        public static bool OperatorDifference_TwoArray_Result(double[] array, double[] other, double[] result)
        {
            Polynomial p = new Polynomial(array);
            Polynomial p2 = new Polynomial(other);
            Polynomial p3 = new Polynomial(result);
            return p3 == (p - p2);
        }

        [TestCase(new[] { 1.2, 3.4, 4.5 }, new[] { 1.2, 4.5 }, new[] { 1.44, 9.48, 20.7, 20.25 }, ExpectedResult = true)]
        public static bool OperatorMultiplication_TwoArray_Result(double[] array, double[] other, double[] result)
        {
            Polynomial p = new Polynomial(array);
            Polynomial p2 = new Polynomial(other);
            Polynomial p3 = new Polynomial(result);
            return p3 == (p * p2);
        }

        [TestCase(new[] { 1.0, 3.0, 5.0 }, 1.0, 9.0)]
        public static void ValueAt_Array_Result(double[] array, double value, double result)
        {
            Polynomial p = new Polynomial(array);
            Assert.AreEqual(p.ValueAt(value), result, 0.0001); 
        }
    }
}
