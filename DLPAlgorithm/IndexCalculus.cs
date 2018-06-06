using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Numerics;
using Utility;

namespace DLPAlgorithm
{
    public static class IndexCalculus
    {
        #region Properties

        public static int FactorBaseSize { get; set; }
        public static int LinearEquatationsCount { get; set; }

        public static int ReducedFactorBaseSize { get; set; }

        #endregion

        #region Constructors

        static IndexCalculus()
        {
            FactorBaseSize = 10;
            LinearEquatationsCount = 5 * FactorBaseSize;
        }

        #endregion

        #region SolveDLP Method

        public static BigInteger SolveDLP(BigInteger g, BigInteger h, BigInteger p)
        {
            BigInteger order = p - 1;
            var input = new DLPInput(g, h, p, order);
            var factorBase = BigIntegerExtension.GetFactorBase(FactorBaseSize);
            


            var coefficients = new List<List<BigInteger>>();
            var constantTerms = new List<BigInteger>();
            FirstStep(input, factorBase, ref coefficients, ref constantTerms);

            Console.WriteLine("First step done");

            var factorBaseLogs = SecondStep(input, ref factorBase, Converter.ToTwoDimensionalBigIntegerArray(coefficients), constantTerms.ToArray());

            Console.WriteLine("Second step done");

            if (factorBaseLogs == null) return -1;

            for (int i = 0; i < factorBaseLogs.Length; i++)
            {
                var l = factorBaseLogs[i];
                BigInteger pow = -1;
                if (l != -1)
                {
                    pow = BigInteger.ModPow(g, l, p);
                }
                var pr = factorBase[i];
                Console.WriteLine(pow + " = " + pr);
            }



            var x = ThirdStep(input, factorBase, factorBaseLogs);

            Console.WriteLine("Third step done");

            ReducedFactorBaseSize = factorBase.Length;
            return x;

            return 0;
        }

        #endregion

        #region Steps Methods

        public static void FirstStep(DLPInput input, BigInteger[] factorBase, ref List<List<BigInteger>> coefficients, ref List<BigInteger> constantTerms)
        {
            BigIntegerRandom rand = new BigIntegerRandom();
            for (int i = 1; i < input.order; i++)
            {
                var k = rand.Next(0, input.order);
                while(k==0)
                {
                    k = rand.Next(0, input.order);
                }
                var temp = BigInteger.ModPow(input.g, k, input.p);
                var factorBaseFactorizationExponents = Factorization.GetFactorBaseFactorizationExponents(temp, factorBase);
                if (factorBaseFactorizationExponents != null)
                {
                    coefficients.Add(factorBaseFactorizationExponents.ToList());

                    bool isLinearIndependent = GaussianElimination.IsLinearIndependent(coefficients, input.order);
                    if (!isLinearIndependent)
                        coefficients.RemoveAt(coefficients.Count - 1);
                    else
                        constantTerms.Add(k);

                    //constantTerms.Add(k);
                }
                if (coefficients.Count == factorBase.Length)
                //if (coefficients.Count == LinearEquatationsCount)
                {
                    return;
                }
            }

        }
        public static BigInteger[] SecondStep(DLPInput input,ref BigInteger[] factorBase, BigInteger[][] coefficients, BigInteger[] constantTerms)
        {
            return GaussianElimination.SolveSystemOfLinearEquatations(input,ref factorBase, coefficients, constantTerms);
        }
        public static BigInteger ThirdStep(DLPInput input, BigInteger[] factorBase, BigInteger[] factorBaseLogs)
        {
            for (BigInteger k = 1; k < input.order; k++)
            {
                var temp = (input.h * BigInteger.ModPow(input.g, k, input.p)).ModPositive(input.p);
                var factorBaseFactorizationExponents = Factorization.GetFactorBaseFactorizationExponents(temp, factorBase);
                if (factorBaseFactorizationExponents != null)
                {
                    BigInteger x = 0;
                    int i = 0;
                    foreach (var log in factorBaseLogs)
                        x += log * factorBaseFactorizationExponents[i++];
                    x -= k;
                    return x.ModPositive(input.order);
                }
            }
            return -1;
        }

        #endregion
    }
}










