using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Threading;
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
            if (g == h) return 1;
            var order = p - 1; // order = EulerFunc(p) == p - 1 if p - prime
            BigInteger a1, b1, a2, b2;
            FloydCycleFindingAlgorithm(out a1, out b1, out a2, out b2, g, h, p);
            Console.WriteLine();
            Console.WriteLine(BigInteger.ModPow(g, a1, p) * BigInteger.ModPow(h, b1, p) % p);
            Console.WriteLine(BigInteger.ModPow(g, a2, p) * BigInteger.ModPow(h, b2, p) % p);
            Console.WriteLine();
            var dA = a1 - a2;
            var dB = b2 - b1;
            //if (dB % (p - 1) == 0) return -1;
            dA = dA.ModPositive(order);
            dB = dB.ModPositive(order);
            var gcd = BigInteger.GreatestCommonDivisor(dB, order);
            if(gcd == 1)
            {
                BigInteger x = dA * dB.ModInverse(order);
                return x.ModPositive(order);
            }
            else
            {
                BigInteger u, v;
                dB.ExtendedGcd(order, out u, out v);
                var x = (u * dA).ModPositive(order);
                x = x / gcd;
                return x.ModPositive(order);
                


                //BigInteger x0 = dA * dB.ModInverse(order / gcd);
                //x0 = x0.ModPositive(order / gcd);
                //for(BigInteger m = 0; m < gcd; m++)
                //{
                //    var x = x0 + m * ((p - 1)/gcd);
                //    Console.WriteLine(x);
                //    if (BigInteger.ModPow(g, x, p - 1) == h) return x;
                //}


                //var reducedMod = (p - 1) / gcd;
                //var temp = dB * dA.ModInverse(reducedMod);
                //temp = temp.ModPositive(reducedMod);
                //Console.WriteLine("gcd = {0}", gcd);
                ////
                //for(BigInteger m = 0; m <= gcd - 1; m++)
                //{

                //    //temp += m * reducedMod;
                //    temp = dA * dB.ModInverse(p - 1) * gcd.ModInverse(p - 1);
                //    temp = temp.ModPositive(p - 1);
                //    if (BigInteger.ModPow(g, temp, p - 1) == h) return temp;
                //}
            }
            //return -1;
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

        //p - is a modulo of cyclic group G
        private static void PollardIterationFunction(ref BigInteger x, ref BigInteger a, ref BigInteger b, BigInteger g, BigInteger h, BigInteger p)
        {
            BigInteger order = p - 1;
            if (x < p / 3)
            {
                b++;
                x *= h;
            }
            else if (x >= p / 3 && x < 2 * p / 3)
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




////p - is a modulo of cyclic group G
//private static void PollardIterationFunction(ref BigInteger Y, ref BigInteger a, ref BigInteger b, BigInteger g, BigInteger h, BigInteger p)
//{
//    int setNumber = (int)(Y % 3);
//    switch(setNumber)
//    {
//        case 0:
//            Y = g * Y;
//            a = a + 1;
//            break;
//        case 2:
//            Y = Y * Y;
//            a = 2 * a;
//            b = 2 * b;
//            break;
//        case 1:
//            Y = h * Y;
//            b = b + 1;
//            break;
//    }
//    Y = Y.ModPositive(p);
//    a = a.ModPositive(p);
//    b = b.ModPositive(p);
//}