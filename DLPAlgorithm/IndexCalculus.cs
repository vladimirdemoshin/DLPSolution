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
        #endregion

        #region Constructors
        static IndexCalculus()
        {
            Accuracy = 10;
            FactorBaseSize = 4;
        }
        #endregion

        #region Methods
        public static BigInteger SolveDLP(BigInteger g, BigInteger h, BigInteger p)
        {
            BigInteger order = p - 1; 
            var factorBase = BigIntegerExtension.GetFactorBase(FactorBaseSize);
            var coefficients = new List<List<BigInteger>>();
            var constantTerms = new List<BigInteger>();
            FirstStep(g, h, p, order, factorBase, ref coefficients, ref constantTerms);
            var factorBaseLogs = SecondStep(order, Converter.ToTwoDimensionalBigIntegerArray(coefficients), constantTerms.ToArray());
            foreach (var a in factorBaseLogs)
                Console.WriteLine(a);


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

            
            //for (k = 1 ; k < order; k++)
            //{
            //    var temp = h * BigInteger.ModPow(g, k, p) % p;
            //    var factorBaseFactorizationExponents = Factorization.GetFactorBaseFactorizationExponents(temp, factorBase);
            //    if (factorBaseFactorizationExponents != null)
            //    {
            //        BigInteger x = 0;
            //        i = 0;
            //        foreach (var log in factorBaseLogs)
            //            x += log * factorBaseFactorizationExponents[i];
            //        x -= k;
            //        return x.ModPositive(order);
            //    }
            //}
            return -1;
        }
       

        
        public static void FirstStep(BigInteger g, BigInteger h, BigInteger p, BigInteger order, BigInteger[] factorBase, ref List<List<BigInteger>> coefficients, ref List<BigInteger> constantTerms)
        {
            BigInteger linearEquatationsCount = 4 * factorBase.Length;
            for (BigInteger k = 1; k < order; k++)
            {
                var temp = BigInteger.ModPow(g, k, p);
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
                if (coefficients.Count == linearEquatationsCount)
                {
                    break;
                }
            }
        }
        
        

        public static BigInteger[] SecondStep(BigInteger order, BigInteger[][] coefficients, BigInteger[] constantTerms)
        {
            return GaussianElimination.SolveSystemOfLinearEquatations(order, coefficients, constantTerms);
        }
        
        #endregion


    }
}












//private static RationalNumber[][] toTwoDimensionalArrayOfRationalNumbers(List<List<BigInteger>> relations)
//{
//    var matrix = new RationalNumber[relations.Count][];
//    int i = 0, j = 0;
//    foreach (var relation in relations)
//    {
//        matrix[i] = new RationalNumber[relation.Count];
//        foreach (var index in relation)
//            matrix[i][j++] = index;
//        i++;
//        j = 0;
//    }
//    return matrix;
//}

//private static List<BigInteger> toListBigInteger(List<RationalNumber> factorBaseLogsInGeneratorBase, BigInteger cyclicGroupOrder)
//{
//    var factorBaseLogsInGeneratorBaseBigInteger = new List<BigInteger>();
//    foreach (var log in factorBaseLogsInGeneratorBase)
//    {
//        var logMod = log.ToModBigInteger(cyclicGroupOrder);
//        if (logMod < 0) logMod += cyclicGroupOrder; //возможно,костыль
//        factorBaseLogsInGeneratorBaseBigInteger.Add(logMod);
//    }
//    return factorBaseLogsInGeneratorBaseBigInteger;
//}