using System;
using System.Collections.Generic;
using FindMin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FindMinTest
{
    [TestClass]
    public class MethodOptimizationTest
    {
        [TestMethod]
        public void TestFibonacciNumber()
        {
            Assert.AreEqual(5, FibonacciNumber.GetNumber(4));
            Assert.AreEqual(8, FibonacciNumber.GetNumber(5));
        }

        [TestMethod]
        public void TestMethodFibonacci()
        {
            var SinAbsKX = new Example(ETypeFunction.SinAbsKX, -0.74, 0.5, 2);
            var Pow4XPlusE = new Example(ETypeFunction.Pow4XPlusE, 0, 1);
            var Pow2kXminusA = new Example(ETypeFunction.Pow2kXminusA, 0, 2, 4, 1);
            double errorBase = 0.001;

            int iteration;
            var res = MethodFibonacci.FindFuncMinimum(Pow4XPlusE, errorBase, out iteration);
            Assert.AreEqual(0.5281, res, 0.001);
        }
    }
}
