using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Numerics;

namespace Utility
{
    public static class CommonAlgorithms
    {
        public static List<ulong> SieveOfEratosthenes(ulong n)
        {
            bool[] prime = new bool[n + 1];
            for (ulong i = 0; i < n + 1; i++) prime[i] = true;
            prime[0] = prime[1] = false;
            for (ulong i = 2; i * i <= n; ++i)
                if (prime[i])
                    for (ulong j = i * i; j <= n; j += i)
                        prime[j] = false;
            var result = new List<ulong>();
            for (ulong i = 2; i < n + 1; i++) if (prime[i]) result.Add(i);
            return result;
        }

        public static BigInteger ModInverse(BigInteger a, BigInteger mod)
        {
            BigInteger gcd = BigInteger.GreatestCommonDivisor(a, mod);
            Console.WriteLine(gcd);
            BigInteger x, y;
            BigInteger d = ExtendedGcd(a / gcd, mod / gcd, out x, out y);
            x = x / gcd;
            if (x < 0) return x + mod;   //вот тут не помню, надо уточнить
            return x;
        }


        public static BigInteger ExtendedGcd(BigInteger a, BigInteger b, out BigInteger x, out BigInteger y) // a*x+b*y=НОД(a,b)
        {
            if (a == 0) { x = 0; y = 1; return b; }
            BigInteger x1, y1;
            BigInteger d = ExtendedGcd(b % a, a, out x1, out y1);
            x = y1 - (b / a) * x1;
            y = x1;
            return d;
        }

        //if returns null, than can't factore value in factorBase
        //otherwise returns powers of each prime in factor base
        //!!! find some optimization of factorization algorithm, if exists !!!
        public static List<BigInteger> Factorization(BigInteger value, BigInteger[] factorBase)
        {
            var factorBaseFactorizationPowersVector = new List<BigInteger>();
            foreach (var primeNum in factorBase)
            {
                BigInteger currentPow = 0;
                while (value % primeNum == 0)
                {
                    currentPow++;
                    value /= primeNum;
                }
                factorBaseFactorizationPowersVector.Add(currentPow);
            }
            if (value != 1) return null;
            return factorBaseFactorizationPowersVector;
        }
    }
}
