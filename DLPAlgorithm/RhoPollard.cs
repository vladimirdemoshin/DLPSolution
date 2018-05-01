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
        /// <summary>
        /// Rho - method of Pollard of solving DLP
        /// </summary>
        /// <param name="g">Generator of cyclic group G</param>
        /// <param name="h">Element of cyclic group G</param>
        /// <param name="p">Prime mod of cyclic group G</param>
        /// <returns></returns>
        public static BigInteger SolveDLP(BigInteger g, BigInteger h, BigInteger p)
        {
            BigInteger a1, b1, a2, b2;
            FloydCycleFindingAlgorithm(out a1, out b1, out a2, out b2, g, h, p);
            var dA = a2 - a1;
            var dB = b1 - b2;
            dA.ModPositive(p - 1);
            dB.ModPositive(p - 1);
            var gcd = BigInteger.GreatestCommonDivisor(dA, p-1);
            var reducedMod = (p-1) / gcd;
            var temp = dB * dA.ModInverse(reducedMod);
            temp.ModPositive(reducedMod);
            if(gcd == 1)
            {
                return temp;
            }
            else
            {
                for(BigInteger m = 0; m <= gcd - 1; m++)
                {
                    temp += m * reducedMod;
                    if (BigInteger.ModPow(g, temp, p - 1) == h) return temp;
                }
            }
            return -1;
        }

        private static void FloydCycleFindingAlgorithm(ref BigInteger a1, ref BigInteger b1, ref BigInteger a2, ref BigInteger b2, BigInteger g, BigInteger h, BigInteger p)
        {
            BigInteger Y1 = 1;
            a1 = 0;
            b1 = 0;
            BigInteger Y2 = 1;
            a2 = 0;
            b2 = 0;
            while(true)
            {
                PollardIterationFunction(ref Y1, ref a1, ref b1, g, h, p);
                PollardIterationFunction(ref Y2, ref a2, ref b2, g, h, p);
                PollardIterationFunction(ref Y2, ref a2, ref b2, g, h, p);
                if (Y1 == Y2) return;
            }
        }
       

        //p - is a modulo of cyclic group G
        private static void PollardIterationFunction(ref BigInteger Y, ref BigInteger a, ref BigInteger b, BigInteger g, BigInteger h, BigInteger p)
        {
            int setNumber = (int)Y % 3 + 1;
            switch(setNumber)
            {
                case 1:
                    Y = g * Y;
                    a = a + 1;
                    break;
                case 2:
                    Y = Y * Y;
                    a = 2 * a;
                    b = 2 * b;
                    break;
                case 3:
                    Y = h * Y;
                    b = b + 1;
                    break;
            }
            Y.ModPositive(p);
            a.ModPositive(p);
            b.ModPositive(p);
        }

    }
}
