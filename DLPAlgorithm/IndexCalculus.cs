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

        #endregion

        #region Constructors

        static IndexCalculus()
        {
            FactorBaseSize = 30;
            LinearEquatationsCount = 4 * FactorBaseSize;
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
            var factorBaseLogs = SecondStep(input, factorBase, Converter.ToTwoDimensionalBigIntegerArray(coefficients), constantTerms.ToArray());
            if (factorBaseLogs == null) return -1;
            var x = ThirdStep(input, factorBase, factorBaseLogs);
            return x;
        }

        #endregion

        #region Steps Methods

        public static void FirstStep(DLPInput input, BigInteger[] factorBase, ref List<List<BigInteger>> coefficients, ref List<BigInteger> constantTerms)
        {
            BigIntegerRandom rand = new BigIntegerRandom();
            for (BigInteger k = 1; k < input.order; k++)
            {
                k = rand.Next(0, input.order);
                var temp = BigInteger.ModPow(input.g, k, input.p);
                var factorBaseFactorizationExponents = Factorization.GetFactorBaseFactorizationExponents(temp, factorBase);
                if (factorBaseFactorizationExponents != null)
                {
                    coefficients.Add(factorBaseFactorizationExponents.ToList());

                    //bool isLinearIndependent = GaussianElimination.IsLinearIndependent(coefficients.ToArray());
                    //if (!isLinearIndependent)
                    //    coefficients.RemoveAt(coefficients.Count - 1);
                    //else
                    //    constantTerms.Add(k);

                    constantTerms.Add(k);
                }
                if (coefficients.Count == LinearEquatationsCount)
                {
                    break;
                }
            }
        }
        public static BigInteger[] SecondStep(DLPInput input, BigInteger[] factorBase, BigInteger[][] coefficients, BigInteger[] constantTerms)
        {
            return GaussianElimination.SolveSystemOfLinearEquatations(input, factorBase, coefficients, constantTerms);
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










