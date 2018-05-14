using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Numerics;
using Utility;

namespace DLPAlgorithm
{
    public static class RhoPollard
    {
        public static BigInteger SolveDLP(BigInteger g, BigInteger h, BigInteger p)
        {
            if (g == h) return 1;
            if (h == 1) return 0;
            var order = p - 1; // order = EulerFunc(p) == p - 1 if p - prime
            BigInteger a1, b1, a2, b2;
            FloydCycleFindingAlgorithm(out a1, out b1, out a2, out b2, g, h, p);
            var dA = a2 - a1;
            var dB = b1 - b2;
            dA = dA.ModPositive(order);
            dB = dB.ModPositive(order);
            if (dB == 0) return -1; // в этом случае метод ничем не лучше полного перебора
            var gcd = BigInteger.GreatestCommonDivisor(dB, order);
            if(gcd == 1)
            {
                BigInteger x = dA * dB.ModInverse(order);
                return x.ModPositive(order);
            }
            else
            {
                var reducedOrder = order / gcd;
                var x0 = (dB / gcd).ModInverse(reducedOrder);
                x0 = x0 * (dA / gcd);
                x0 = x0.ModPositive(reducedOrder);
                for(BigInteger m = 0; m < gcd; m++)
                {
                    var x = x0 + m * reducedOrder;
                    if (BigInteger.ModPow(g, x, p) == h)
                        return x;
                }
            }
            return -1;
        }

        private static void FloydCycleFindingAlgorithm(out BigInteger a1, out BigInteger b1, out BigInteger a2, out BigInteger b2, BigInteger g, BigInteger h, BigInteger p)
        {
            BigInteger x1 = 1;
            a1 = 0;
            b1 = 0;
            BigInteger x2 = 1;
            a2 = 0;
            b2 = 0;
            for (int i = 1; i < p - 1; i++)
            {
                PollardIterationFunction(ref x1, ref a1, ref b1, g, h, p);
                PollardIterationFunction(ref x2, ref a2, ref b2, g, h, p);
                PollardIterationFunction(ref x2, ref a2, ref b2, g, h, p);
                if (x1 == x2) return;
            }
        }

        private static void PollardIterationFunction(ref BigInteger x, ref BigInteger a, ref BigInteger b, BigInteger g, BigInteger h, BigInteger p)
        {
            BigInteger order = p - 1;
            if (x <= p / 3)
            {
                b++;
                x *= h;
            }
            else if (x > p / 3 && x <= 2 * p / 3)
            {
                a *= 2;
                b *= 2;
                x *= x;
            }
            else
            {
                a++;
                x *= g;
            }
            a = a.ModPositive(order);
            b = b.ModPositive(order);
            x = x.ModPositive(p);
        }

    }
}