using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Numerics;

namespace Utility
{
    public static class BigIntegerPrimeTest
    {
        public static int MillerRabinCertainty { get; set; }

        static BigIntegerPrimeTest()
        {
            MillerRabinCertainty = 32;
        }

        public static bool MillerRabinTest(BigInteger n)
        {
            BigInteger d = n - 1;
            BigInteger s = 0;
            while ((d & 1) == 0)
            {
                d >>= 1;
                s++;
            }
            BigIntegerRandom randBI = new BigIntegerRandom();
            for (int i = 0; i < MillerRabinCertainty; i++)
            {
                bool isPrime = false;
                BigInteger a = randBI.Next(2, n - 1);
                BigInteger x = BigInteger.ModPow(a, d, n);
                if (x == 1 || x == n - 1)
                {
                    continue;
                }

                for (int r = 1; r < s; r++)
                {
                    x = BigInteger.ModPow(x, 2, n);
                    if (x == n - 1)
                    {
                        isPrime = true;
                        break;
                    }
                }
                if (!isPrime) return false;
            }
            return true;
        }
    }
}
