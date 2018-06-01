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
        public static BigInteger SolveDLP(AffinePoint P, AffinePoint Q)
        {
            var R = P;
            BigInteger a = 1, b = 0;
            var R2 = R;
            BigInteger a2 = a, b2 = b;
            //var MOD = P.E.P;
            var MOD = P.E.HasseTheorem();
            for (int i = 1; i < MOD; i++)
            {
                R = f(Q, P, R, ref a, ref b,MOD);
                R2 = f(Q, P, R2, ref a2, ref b2,MOD);
                R2 = f(Q, P, R2, ref a2, ref b2,MOD);
                Console.WriteLine(R);
                Console.WriteLine(R2);
                if (R == R2)
                {
                    //Console.WriteLine(R);
                    //Console.WriteLine(R2);
                    break;
                }
            }
            var dA = a2 - a;
            var dB = b - b2;
            dA = dA.ModPositive(MOD);
            dB = dB.ModPositive(MOD);
            var gcd = BigInteger.GreatestCommonDivisor(dB, MOD);
            if (gcd == 1)
            {
                BigInteger x = dA * dB.ModInverse(MOD);
                return x.ModPositive(MOD);
            }
            else
            {
                Console.WriteLine(gcd);
                var reducedOrder = MOD / gcd;
                var x0 = (dB / gcd).ModInverse(reducedOrder);
                x0 = x0 * (dA / gcd);
                x0 = x0.ModPositive(reducedOrder);
                for (BigInteger m = 0; m < gcd; m++)
                {
                    var x = x0 + m * reducedOrder;
                    if ((x * P.ToProjectivePoint()).ToAffinePoint() == Q)
                        return x;
                }
            }
            return -1;
           // return (a2 - a) * BigIntegerExtension.ModInverse(b - b2, MOD);
        }

        private static AffinePoint f(AffinePoint Q, AffinePoint P, AffinePoint R, ref BigInteger a, ref BigInteger b, BigInteger MOD)
        {
            int set = chooseSet(R);
            if (set == 0)
            {
                a = a;
                b = (b + 1).ModPositive(MOD);
                return (Q.ToProjectivePoint() + R.ToProjectivePoint()).ToAffinePoint();
            }
            else if (set == 1)
            {
                a = (2 * a).ModPositive(MOD);
                b = (2 * b).ModPositive(MOD);
                return (2 * R.ToProjectivePoint()).ToAffinePoint();
            }
            else
            {
                a = (a + 1).ModPositive(MOD);
                b = b;
                return (P.ToProjectivePoint() + R.ToProjectivePoint()).ToAffinePoint();
            }
        }

        private static int chooseSet(AffinePoint R)
        {
            var x = R.X;
            int countSet = 3;
            return (int)x % countSet;
        }
    }
}
