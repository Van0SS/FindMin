using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindMin
{

    public static class MethodFibonacci
    {
        static public double FindFuncMinimum(Example example, double error, out int interation)
        {
            double a = example.MinX;
            double b = example.MaxX;
            double fX1 = 0;
            double fX2 = 0;
            interation = 1;
            int N = (int) ((b - a)/(2*error))+1;

            double[] fibArray = new double[N];
            for (int i = 0; i < N; i++)
            {
                fibArray[i] = FibonacciNumber.GetNumber(i);
                if (Double.IsInfinity(fibArray[i]))
                    throw new Exception("Получается слишком большое число фибоначчи.");
            }

            //Шаг 1
            double x1 = a + fibArray[N - interation - 2] / fibArray[N - interation] * (b - a);
            double x2 = a + fibArray[N - interation - 1] / fibArray[N - interation] * (b - a);
            double curError = (b - a) / 2;
            fX1 = example.ReturnY(x1);
            fX2 = example.ReturnY(x2);

            while (true)
            {
                //Шаг 2. Проверка на окончание поиска
                if ((interation >= (N - 2)) || (curError < error))
                {
                    //Шаг 4. Окончание поиска
                    if (fX2 >= fX1)
                    {
                        b = x1;
                    }
                    else
                    {
                        a = x1;
                    }
                    break;
                }

                //Шаг 3. Переход к новому отрезку и новым пробным точкам.
                if (fX1 <= fX2)
                {
                    b = x2;
                    x2 = x1;
                    fX2 = fX1;

                    x1 = a + fibArray[N - interation - 2] / fibArray[N - interation] * (b - a);
                    fX1 = example.ReturnY(x1);
                }
                else
                {
                    a = x1;
                    x1 = x2;
                    fX1 = fX2;
                    x2 = a + fibArray[N - interation - 1] / fibArray[N - interation] * (b - a);
                    fX2 = example.ReturnY(x2);
                }
                curError = (b - a) / 2;
                interation++;
            }
            

            return (a+b)/2;
        }
    }

    public static class FibonacciNumber
    {
        static public double GetNumber(int i)
        {
            double prev = 0;
            double prev2 = 1;
            double cur = 1;

            for (int j = 0; j < i; j++)
            {
                cur = prev + prev2;
                prev = prev2;
                prev2 = cur;
            }
            return cur;
        }
    }
}
