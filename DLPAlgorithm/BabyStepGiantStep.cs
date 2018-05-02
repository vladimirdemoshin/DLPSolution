using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Numerics;
using System.Collections;
using Utility;

namespace DLPAlgorithm
{
    public static class BabyStepGiantStep
    {
        public static BigInteger SolveDLP(BigInteger g, BigInteger h, BigInteger p)
        {
            var order = p - 1;
            var babySteps = new Dictionary<BigInteger, BigInteger>();
            var m = BigIntegerExtension.Sqrt(order) + 1;
            var temp = BigInteger.ModPow(g, m, p);
            var y = temp;
            for (int i = 1; i <= m; i++)
            {
                babySteps.Add(y, i);
                y = (y * temp) % p;
            }
            temp = h * g % p;
            for (int j = 1; j <= m; j++)
            {
                BigInteger i = -1;
                try
                {
                    i = babySteps[temp];
                }
                catch (KeyNotFoundException e) { }
                if (i != -1)
                    return (m * i - j).ModPositive(order);  
                temp = temp * g % p;
            }
            return -1;
        }
    }
}
