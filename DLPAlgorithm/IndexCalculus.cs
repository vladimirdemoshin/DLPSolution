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
        public static BigInteger Accuracy { get; set; }
        public static int FactorBaseSize { get; set; }
        //public BigIntegerRandom rand { get; set; }
        #endregion

        #region Constructors
        static IndexCalculus()
        {
            Accuracy = 10;
            FactorBaseSize = 3;
            //rand = new BigIntegerRandom();
        }
        #endregion

        #region Methods
        public static BigInteger SolveDLP(BigInteger g, BigInteger h, BigInteger p)
        {
            BigInteger order = p - 1; 
            var factorBase = BigIntegerExtension.GetFactorBase(FactorBaseSize);
            var coefficients = new List<BigInteger[]>();
            var constantTerms = new BigInteger[FactorBaseSize];
            int i = 0;
            BigInteger k;
            for (k = 1; k < order; k++)
            {
                var temp = BigInteger.ModPow(g, k, p);
                var factorBaseFactorizationExponents = Factorization.GetFactorBaseFactorizationExponents(temp, factorBase);
                if (factorBaseFactorizationExponents != null)
                {
                    coefficients.Add(factorBaseFactorizationExponents);
                    bool isLinearIndependent = GaussianElimination.IsLinearIndependent(coefficients.ToArray());
                    if (!isLinearIndependent)
                        coefficients.RemoveAt(coefficients.Count - 1);
                    else
                        constantTerms[i++] = k;
                }
                if (coefficients.Count == factorBase.Length) break;
            }

            int j = 0;
            foreach (var a in coefficients)
            {
                foreach (var b in a)
                {
                    Console.Write(b + " ");
                }
                Console.Write(constantTerms[j++]);
                Console.WriteLine();
            }

            var factorBaseLogs = GaussianElimination.SolveSystemOfLinearEquatations(coefficients.ToArray(), constantTerms, order, g, factorBase, p);
            for (k = 1 ; k < order; k++)
            {
                var temp = h * BigInteger.ModPow(g, k, p) % p;
                var factorBaseFactorizationExponents = Factorization.GetFactorBaseFactorizationExponents(temp, factorBase);
                if (factorBaseFactorizationExponents != null)
                {
                    BigInteger x = 0;
                    i = 0;
                    foreach (var log in factorBaseLogs)
                        x += log * factorBaseFactorizationExponents[i];
                    x -= k;
                    return x.ModPositive(order);
                }
            }
            return -1;
        }
        #endregion

        #region Private Methods
        private static RationalNumber[][] toTwoDimensionalArrayOfRationalNumbers(List<List<BigInteger>> relations)
        {
            var matrix = new RationalNumber[relations.Count][];
            int i = 0, j = 0;
            foreach (var relation in relations)
            {
                matrix[i] = new RationalNumber[relation.Count];
                foreach (var index in relation)
                    matrix[i][j++] = index;
                i++;
                j = 0;
            }
            return matrix;
        }

        private static List<BigInteger> toListBigInteger(List<RationalNumber> factorBaseLogsInGeneratorBase, BigInteger cyclicGroupOrder)
        {
            var factorBaseLogsInGeneratorBaseBigInteger = new List<BigInteger>();
            foreach (var log in factorBaseLogsInGeneratorBase)
            {
                var logMod = log.ToModBigInteger(cyclicGroupOrder);
                if (logMod < 0) logMod += cyclicGroupOrder; //возможно,костыль
                factorBaseLogsInGeneratorBaseBigInteger.Add(logMod);
            }
            return factorBaseLogsInGeneratorBaseBigInteger;
        }
        #endregion


    }
}
