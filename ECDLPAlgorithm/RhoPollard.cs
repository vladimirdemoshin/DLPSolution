using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

using Utility;
using EllipticCurveUtility;

namespace ECDLPAlgorithm
{
    public static class RhoPollard
    {
        public static BigInteger StepsCount { get; set; }
        public static BigInteger GCD { get; set; }
        public static BigInteger SolveDLP(ProjectivePoint P, ProjectivePoint Q)
        {
            var R = P;
            BigInteger a = 1, b = 0;
            var R2 = R;
            BigInteger a2 = a, b2 = b;
            //var MOD = P.E.P;
            var MOD = P.E.HasseTheorem();
            StepsCount = 1;
            for (int i = 1; i < MOD; i++)
            {
                R = f(P, Q, R, ref a, ref b, MOD);
                R2 = f(P, Q, R2, ref a2, ref b2, MOD);
                R2 = f(P, Q, R2, ref a2, ref b2, MOD);
                if (R == R2)
                {
                    Console.WriteLine(R);
                    Console.WriteLine(R2);
                    break;
                }
                StepsCount++;
            }
            Console.WriteLine(MOD);
            Console.WriteLine(StepsCount);
            var dA = a2 - a;
            var dB = b - b2;
            dA = dA.ModPositive(MOD);
            dB = dB.ModPositive(MOD);
            var gcd = BigInteger.GreatestCommonDivisor(dB, MOD);
            Console.WriteLine(gcd);
            GCD = gcd;
            if (gcd == 1)
            {
                BigInteger x = dA * dB.ModInverse(MOD);
                return x.ModPositive(MOD);
            }
            else
            {
                var reducedOrder = MOD / gcd;
                var x0 = (dB / gcd).ModInverse(reducedOrder);
                x0 = x0 * (dA / gcd);
                x0 = x0.ModPositive(reducedOrder);
                for (BigInteger m = 0; m < gcd; m++)
                {
                    var x = x0 + m * reducedOrder;
                    if ((x * P) == Q)
                        return x;
                }
            }
            return -1;
            // return (a2 - a) * BigIntegerExtension.ModInverse(b - b2, MOD);
        }

        private static ProjectivePoint f(ProjectivePoint P, ProjectivePoint Q, ProjectivePoint R, ref BigInteger a, ref BigInteger b, BigInteger MOD)
        {
            int set = chooseSet(R);
            if (set == 0)
            {
                b = (b + 1).ModPositive(MOD);
                return (R + Q);
            }
            else if (set == 1)
            {
                a = (2 * a).ModPositive(MOD);
                b = (2 * b).ModPositive(MOD);
                return (2 * R); //ProjectivePoint.Double(R);
            }
            else
            {
                a = (a + 1).ModPositive(MOD);
                return (R + P);
            }
        }

        private static int chooseSet(ProjectivePoint R)
        {
            //var x = R.X;
            //int countSet = 3;
            //return (int)x % countSet;

            var y = R.Y;
            int countSet = 3;
            return (int)y % countSet;
        }
    }
}
