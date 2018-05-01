using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Numerics;
using EllipticCurveUtility;
using Utility;

namespace ECDLPAlgorithm
{
    public static class BabyStepGiantStep
    {
        public static BigInteger SolveDLP(AffinePoint P, AffinePoint Q)
        {
            BigInteger m = BigIntegerExtension.Sqrt(P.E.HasseTheorem())+1;
            var babySteps = new Dictionary<AffinePoint, BigInteger>();
            AffinePoint babyStep;
            for (BigInteger i = 1; i < m; i++)
            {
                babyStep = i * P;
                babySteps.Add(babyStep, i);
                Console.WriteLine(babyStep);
            }
            AffinePoint giantStep = -(m * P);
            Console.WriteLine();
            AffinePoint temp;
            for (BigInteger j = 0; j < m; j++)
            {
                temp = Q + j * giantStep;
                Console.WriteLine(temp);
                BigInteger i = -1;
                try
                {
                    i = babySteps[temp];
                }
                catch (KeyNotFoundException e) { }
                if (i != -1)
                    return i + j * m;  //тут еще по модулю порядка эл кривой надо брать
            }
            return -1;
        }
    }
}
