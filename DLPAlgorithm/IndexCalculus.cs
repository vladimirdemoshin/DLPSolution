using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Numerics;
using Utility;

namespace DLPAlgorithm
{
    public class IndexCalculus
    {
        #region Properties
        public BigInteger Accuracy { get; set; }
        public ulong FactorBaseCount { get; set; }

        public BigIntegerRandom randomBI { get; set; }
        #endregion

        #region Constructors
        public IndexCalculus()
        {
            Accuracy = 10;
            FactorBaseCount = 3;
            randomBI = new BigIntegerRandom();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Index Calculus alg
        /// </summary>
        /// <param name="generator">Primitive root of cyclic group Zprime*</param>
        public BigInteger CountDiscreteLogarithm(BigInteger generator, BigInteger value, BigInteger prime)
        {
            //случай когда G = Zp* 
            var factorBase = new BigInteger[] { 2, 3, 5 }; //заменить на общий случай поиска факторной базы// CommonAlgorithms.SieveOfEratosthenes(FactorBaseCount);
            BigInteger cyclicGroupOrder = prime - 1; //случай, когда степени [0,prime-2] генератора создают всю группу
            var relations = new List<List<BigInteger>>();
            BigInteger cyclicGroupElement;
            BigInteger cyclicGroupElementIndex = 1;
            for (; cyclicGroupElementIndex <= cyclicGroupOrder; cyclicGroupElementIndex++)
            {
                cyclicGroupElement = BigInteger.ModPow(generator, cyclicGroupElementIndex, prime); // попробовать заменить на быстрое возведение в степень, если есть в этом смысл (узнать как работает этот метод в классе)
                var factorBaseDecompositionIndexesVector = CommonAlgorithms.Factorization(cyclicGroupElement, factorBase);
                if (factorBaseDecompositionIndexesVector != null)
                {
                    factorBaseDecompositionIndexesVector.Add(cyclicGroupElementIndex);
                    relations.Add(factorBaseDecompositionIndexesVector);
                    var matrixRelations = toTwoDimensionalArrayOfRationalNumbers(relations);
                    if (!GaussianElimination.IsLinearIndependent(matrixRelations))
                        relations.RemoveAt(relations.Count - 1);
                }
                if (relations.Count == factorBase.Length) break;
            }
            var factorBaseLogsInGeneratorBase = GaussianElimination.SolveSystemOfLinearEquatations(toTwoDimensionalArrayOfRationalNumbers(relations));
            var factorBaseLogsInGeneratorBaseBigInteger = toListBigInteger(factorBaseLogsInGeneratorBase, cyclicGroupOrder);
            cyclicGroupElementIndex = 1;
            for (; cyclicGroupElementIndex <= cyclicGroupOrder; cyclicGroupElementIndex++)
            {
                cyclicGroupElement = value * BigInteger.ModPow(generator, cyclicGroupElementIndex, prime) % prime;
                var factorBaseDecompositionIndexesVector = CommonAlgorithms.Factorization(cyclicGroupElement, factorBase);
                if (factorBaseDecompositionIndexesVector != null)
                {
                    BigInteger x = 0;
                    int i = 0;
                    foreach (var index in factorBaseDecompositionIndexesVector)
                        x += factorBaseLogsInGeneratorBaseBigInteger[i] * index;
                    x -= cyclicGroupElementIndex;
                    return x % cyclicGroupOrder;
                }
            }
            return 0;
        }
        #endregion

        #region Private Methods
        private RationalNumber[][] toTwoDimensionalArrayOfRationalNumbers(List<List<BigInteger>> relations)
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

        private List<BigInteger> toListBigInteger(List<RationalNumber> factorBaseLogsInGeneratorBase, BigInteger cyclicGroupOrder)
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
