using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Logic.Tests
{
    [TestFixture]
    public class NumbersExtensionsTests
    {
        #region data

        public static IEnumerable<TestCaseData> TestDataForGcdTwoNumber
        {
            get
            {
                yield return new TestCaseData(-5, 10).Returns(5);
                yield return new TestCaseData(0, 15).Returns(15);
                yield return new TestCaseData(5, 0).Returns(5);
                yield return new TestCaseData(0, 0).Returns(0);
                yield return new TestCaseData(24, 24).Returns(24);
                yield return new TestCaseData(5, 10).Returns(5);
                yield return new TestCaseData(1, 10).Returns(1);
            }
        }

        public static IEnumerable<TestCaseData> TestDataForGcdArg
        {
            get
            {
                yield return new TestCaseData(new[] { 11, 7, 8 }).Returns(1);
                yield return new TestCaseData(new[] { 16, 10, 8, 4 }).Returns(2);
                yield return new TestCaseData(new[] { 16, 40, 8, 4 }).Returns(4);
                yield return new TestCaseData(new[] { 625, 75, 275, 575 }).Returns(25);
                yield return new TestCaseData(new[] { 33, 121, 77, 209, 253, 165 }).Returns(11);
            }
        }

        public static IEnumerable<TestCaseData> TestDataForGcdThreeNumber
        {
            get
            {
                yield return new TestCaseData(-5, 10, 15).Returns(5);
                yield return new TestCaseData(1, 10, 100).Returns(1);
                yield return new TestCaseData(2, 0, 4).Returns(2);
                yield return new TestCaseData(11, 7, 8).Returns(1);
                yield return new TestCaseData(217, 341, 713).Returns(31);
            }
        }

        #endregion

        #region Gdc

        [Test, TestCaseSource(nameof(TestDataForGcdTwoNumber))]
        public int Gcd_2number_Result(int a, int b)
        {
            return NumberExtensions.Gcd(a, b);
        }

        [Test, TestCaseSource(nameof(TestDataForGcdThreeNumber))]
        public int Gcd_3number_Result(int a, int b, int c)
        {
            return NumberExtensions.Gcd(a, b, c);
        }

        [Test, TestCaseSource(nameof(TestDataForGcdArg))]
        public int Gcd_Args_Result(int[] a)
        {
            return NumberExtensions.Gcd(a);
        }

        [Test]
        public void Gcd_Null_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => NumberExtensions.Gcd(null));
        }

        #endregion

        #region GdcBinary

        [Test, TestCaseSource(nameof(TestDataForGcdTwoNumber))]
        public int GcdBinary_2number_Result(int a, int b)
        {
            return NumberExtensions.GcdBinary(a, b);
        }

        [Test, TestCaseSource(nameof(TestDataForGcdThreeNumber))]
        public int GcdBinary_3number_Result(int a, int b, int c)
        {
            return NumberExtensions.GcdBinary(a, b, c);
        }

        [Test, TestCaseSource(nameof(TestDataForGcdArg))]
        public int GcdBinary_Args_Result(int[] a)
        {
            return NumberExtensions.GcdBinary(a);
        }

        [Test]
        public void GcdBinary_Null_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => NumberExtensions.GcdBinary(null));
        }

        #endregion
    }
}
