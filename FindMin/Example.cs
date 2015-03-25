using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace FindMin
{

    public enum EParametersNames
    {
        K,
        A
    }
    public enum ETypeFunction
    {
        Xminus1Sqr,
        SinAbsKX,
        Pow4XPlusE,
        Pow2kXminusA
    }

    static class Constants
    {
        static public Dictionary<ETypeFunction, string> TypeFuncDictionary = new Dictionary<ETypeFunction, string>();

        static Constants()
        {
            TypeFuncDictionary.Add(ETypeFunction.Xminus1Sqr, "y=(x-1)^2+1");
            TypeFuncDictionary.Add(ETypeFunction.SinAbsKX, "y=sin(|kx|)");
            TypeFuncDictionary.Add(ETypeFunction.Pow4XPlusE, "y=x^4+e^-x");
            TypeFuncDictionary.Add(ETypeFunction.Pow2kXminusA, "(x-a)^2k");
        }

    }
    public class Example
    {
        private readonly ETypeFunction _typeFunction;
        public double MinX { get; set; }
        public double MaxX { get; set; }
        public double[] Parameters { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="typeFunction"></param>
        /// <param name="minX"></param>
        /// <param name="maxX"></param>
        /// <param name="parameters">Parameters like as K, A, etc. !!Strong sequence!!</param>
        public Example(ETypeFunction typeFunction, double minX, double maxX, params double[] parameters)
        {
            _typeFunction = typeFunction;
            MinX = minX;
            MaxX = maxX;
            Parameters = parameters;
        }

        public Example()
        {
            MinX = 0;
            MaxX = 1;
        }


        public string ReturnTypeFunction()
        {
            return Constants.TypeFuncDictionary[_typeFunction];
        }
        public double ReturnY(double x)
        {
            if ((x < MinX) || (x > MaxX))
                throw new Exception("X went beyond");
            switch (_typeFunction)
            {
                case ETypeFunction.SinAbsKX:
                    return Math.Sin(Math.Abs(Parameters[0] * x));
                case ETypeFunction.Pow4XPlusE:
                    return Math.Pow(x, 4) + Math.Exp(-x);
                case ETypeFunction.Xminus1Sqr:
                    return Math.Sqrt(x - 1) + 1;
                case ETypeFunction.Pow2kXminusA:
                    return Math.Pow(x - Parameters[0], 2*Parameters[1]);
            }
            return Double.NaN;
        }

    }


}
