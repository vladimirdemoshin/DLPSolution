
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

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
            if (h == 1) return 0;
            if (h == g) return 1;
            var giantSteps = new Dictionary<BigInteger, BigInteger>();
            var m = BigIntegerExtension.Sqrt(order) + 1;
            var temp = BigInteger.ModPow(g, m, p);
            var y = temp;
            //Console.WriteLine("g = {0}, h = {1}, p = {2}, realOrder = {3}, m = {4}",g, h, p, BigIntegerExtension.FindElementOrder(g, p), m);
            //Console.WriteLine();
            //Console.WriteLine("Table of y = g^(m*i) mod p");
            for (int i = 1; i <= m; i++)
            {
                //babySteps.Add(i, y);

                //Console.WriteLine("y = {0}, k*i = {1}", y, i * m);
                try
                {
                    giantSteps.Add(y, i);
                }
                catch (Exception e)
                {
                    //if (BigIntegerExtension.FindElementOrder(g, p) == order)
                    //    Console.WriteLine("dublicates with generators mod" + p);
                    //babySteps[y] = i;
                }
                y = (y * temp).ModPositive(p);
            }
            temp = (h * g).ModPositive(p);
            for (int j = 1; j <= m; j++)
            {
                //if (babySteps.ContainsValue(temp))
                //    return (m * babySteps[temp] - j).ModPositive(order);

                BigInteger i = -1;
                try
                {
                    i = giantSteps[temp];
                }
                catch (KeyNotFoundException e) { }
                if (i != -1)
                    return (m * i - j).ModPositive(order);
                temp = (temp * g).ModPositive(p);
            }
            return -1;
        }
    }
}
