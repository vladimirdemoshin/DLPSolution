using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Numerics;

namespace Utility
{
    public static class Factorization
    {
        public static BigInteger[] GetFactorBaseFactorizationExponents(BigInteger num, IEnumerable<BigInteger> factorBase)
        {
            var factorBaseFactorizationPowersVector = new List<BigInteger>();
            foreach (var primeNum in factorBase)
            {
                BigInteger currentPow = 0;
                while (num % primeNum == 0)
                {
                    currentPow++;
                    num /= primeNum;
                }
                factorBaseFactorizationPowersVector.Add(currentPow);
            }
            if (num != 1) return null;
            return factorBaseFactorizationPowersVector.ToArray();
        }
    }
}
