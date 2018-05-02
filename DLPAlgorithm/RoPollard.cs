using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Numerics;

namespace DLPAlgorithm
{
    class RoPollard
    {
        public static BigInteger Pollard(BigInteger g, BigInteger t, BigInteger p)  //учитываем, что p - простой, тогда фи(p)=p-1
        {
            BigInteger a = 0, b = 0, x = 1;
            BigInteger A = a, B = b, X = x;
            for (int i = 1; i < p - 1; i++) //
            {
                refreshVariables(ref x, ref a, ref b, g, t, p);
                refreshVariables(ref X, ref A, ref B, g, t, p);
                refreshVariables(ref X, ref A, ref B, g, t, p);
                //Console.WriteLine(a+" "+b+" "+ x+" "+ A + " " + B + " " + X);
                if (x == X) break;
            }

            BigInteger dA = A - a, dB = b - B;
            BigInteger gcd = BigInteger.GreatestCommonDivisor(dA, p - 1);
            if (dA <= 0) dA += p - 1;
            if (dB <= 0) dB += p - 1;
            dA = dA / gcd;
            dB = dB / gcd;
            BigInteger u=1, v;
            //extendedGcd(dA, (p - 1) / gcd, out u, out v);
            // Console.WriteLine(gcd);

            if (gcd == 1)
            {
                return u * dB % (p - 1);
            }
            else
            {
                BigInteger freq = (p - 1) / gcd;
                BigInteger u0 = u * dB;// gcd;
                BigInteger log = u0 % (p - 1);
                for (int k = 0; BigInteger.ModPow(g, log, p) != t; k++)
                {
                    log = (u0 + freq * k) % (p - 1);

                    //Console.WriteLine(log);
                }
                return log;
            }
        }

        private static void refreshVariables(ref BigInteger x, ref BigInteger a, ref BigInteger b, BigInteger g, BigInteger t, BigInteger p)
        {
            if (x > 0 && x < p / 3)
            {
                a = (a + 1) % (p - 1);
                x = t * x % p;
            }
            else if (x > p / 3 && x < 2 * p / 3)
            {
                a = 2 * a % (p - 1);
                b = 2 * b % (p - 1);
                x = x * x % p;
            }
            else
            {
                b = (b + 1) % (p - 1);
                x = g * x % p;
            }
        }
    }
}
