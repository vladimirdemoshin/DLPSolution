using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Numerics;
using System.Threading;

namespace Utility
{
    public static class GaussianElimination
    {
        #region Methods

        public static BigInteger[] SolveSystemOfLinearEquatations(DLPInput input, BigInteger[] factorBase, BigInteger[][] coefficients, BigInteger[] constantTerms)
        {
            var augmentedMatrix = ToAugmentedMatrix(coefficients, constantTerms);
            var convertedAugmentedMatrix =  Converter.ToTwoDimensionalModRationalNumberArray(augmentedMatrix, input.order);
            Print(convertedAugmentedMatrix);
            convertedAugmentedMatrix = ToTriangularForm(convertedAugmentedMatrix);
            Print(convertedAugmentedMatrix);
            convertedAugmentedMatrix = RemoveNullLines(convertedAugmentedMatrix);
            if (convertedAugmentedMatrix.Length != factorBase.Length)
            {
                //ReduceFactorBase(ref factorBase, ref convertedAugmentedMatrix);
                return null;
            }  
            var solution = SolveTriangularSystemOfLinearEquatations(input, factorBase, convertedAugmentedMatrix);
            return solution;
        }

        public static BigInteger[] SolveTriangularSystemOfLinearEquatations(DLPInput input, BigInteger[] factorBase, ModRationalNumber[][] matrix)
        {
            var solution = new List<BigInteger>();
            int n = matrix.Length;
            int m = matrix[0].Length;
            for (int i = n - 1; i >= 0; i--)
            {
                var logarithmicExpression = factorBase[i];
                var constantTerm = matrix[i][m - 1];
                var coefficient = matrix[i][i];
                var a = coefficient.Numerator * constantTerm.Denominator;
                var b = coefficient.Denominator * constantTerm.Numerator;
                var mod = coefficient.Mod;
                var x = SolveLinearEquatation(input, logarithmicExpression, a.ModPositive(mod), b.ModPositive(mod));
                solution.Add(x);
                for (int k = i - 1; k >= 0; k--)
                    matrix[k][m - 1] = matrix[k][m - 1] - matrix[k][i] * solution[n - 1 - i];
            }
            solution.Reverse();
            return solution.ToArray();
        }

        public static BigInteger SolveLinearEquatation(DLPInput input, BigInteger logatithmicExpression, BigInteger a, BigInteger b)
        {
            var mod = input.order;
            var gcd = BigInteger.GreatestCommonDivisor(a, mod);
            if (gcd == -1)
                return (b * a.ModInverse(mod)).ModPositive(mod);
            else
            {
                BigInteger u, v;
                BigIntegerExtension.ExtendedGcd(a, mod, out u, out v);
                var reducedMOD = mod / gcd;
                var x0 = ((b / gcd) * u).ModPositive(reducedMOD);
                for (BigInteger j = 0; j < gcd; j++)
                {
                    var x = x0 + j * reducedMOD;
                    if (BigInteger.ModPow(input.g, x, input.p) == logatithmicExpression)
                        return x;
                }
            }
            return -1;
        }

        public static void ReduceFactorBase(ref BigInteger[] factorBase, ref ModRationalNumber[][] matrix)
        {
            var indexes = new List<int>();
            for(int i=0,j=0;i-j<matrix.Length;i++)
            {
                if (matrix[i-j][i] == 0)
                    j++;
                else
                    indexes.Add(i);
            }
            var reducedFactorBase = new BigInteger[indexes.Count];
            var reducedMatrix = new ModRationalNumber[indexes.Count][];
            for(int i=0;i<reducedMatrix.Length;i++)
                reducedMatrix[i] = new ModRationalNumber[indexes.Count+1];
            int k = 0;
            //удаляем незадействованные простые числа из факторной базы и удаляем нулевые столбцы из матрицы (точнее, создаем новые объекты на основе старых)
            foreach(var index in indexes)
            {
                reducedFactorBase[k] = factorBase[index];
                for(int j=0;j<reducedMatrix.Length;j++)
                    reducedMatrix[j][k] = factorBase[index];
                k++;
            }
            //приписываем к новой матрице справа свободные члены
            for(int i=0;i<reducedMatrix.Length;i++)
                reducedMatrix[i][reducedMatrix[i].Length-1] = matrix[i][matrix[i].Length-1];
            factorBase = reducedFactorBase;
            matrix = reducedMatrix;
        }

        public static BigInteger[][] ToAugmentedMatrix(BigInteger[][] coefficients, BigInteger[] constantTerms)
        {
            var temp = new BigInteger[coefficients.Length][];
            for (int i = 0; i < temp.Length; i++)
                temp[i] = coefficients[i].Concat(new BigInteger[] { constantTerms[i] }).ToArray();
            return temp;
        }

        public static void Print(ModRationalNumber[][] matrix)
        {
            for (int i = 0; i < matrix.Length; i++)
            {
                for (int j = 0; j < matrix[i].Length; j++)
                {
                    Console.Write(matrix[i][j] + "  ");
                }
                Console.WriteLine();
            }
            Console.WriteLine(); Console.WriteLine();
        }
        public static ModRationalNumber[][] ToTriangularForm(ModRationalNumber[][] matrix)
        {
            int n = matrix.Length;
            int m = matrix[0].Length;
            int currentColumn = 0;
            int currentRow = 0;
            while (currentColumn < m && currentRow < n)
            {
                //step 1
                int leadingRow = -1;
                ModRationalNumber leadingElement = 1;
                for (int tempRow = currentRow; tempRow < n; tempRow++)
                    if (matrix[tempRow][currentColumn] != 0)
                    {
                        leadingRow = tempRow;
                        leadingElement = matrix[tempRow][currentColumn];
                        break;
                    }
                if (leadingRow == -1)
                {
                    currentColumn++;
                    continue;
                }
                if (leadingRow != currentRow)
                {
                    for (int tempColumn = currentColumn; tempColumn < m; tempColumn++)
                    {
                        var tmp = matrix[leadingRow][tempColumn];
                        matrix[leadingRow][tempColumn] = matrix[currentRow][tempColumn];
                        matrix[currentRow][tempColumn] = tmp;
                    }
                }
                //step 2
                for (int tempColumn = currentColumn; tempColumn < m; tempColumn++)
                {
                    matrix[currentRow][tempColumn] = matrix[currentRow][tempColumn] / leadingElement;
                }
                if (currentRow == n - 1) break;
                //step 3
                for (int tempRow = currentRow + 1; tempRow < n; tempRow++)
                {
                    ModRationalNumber c = matrix[tempRow][currentColumn];
                    if (c == 0) continue;
                    for (int tempColumn = currentColumn; tempColumn < m; tempColumn++)
                    {
                        matrix[tempRow][tempColumn] = matrix[tempRow][tempColumn] - c * matrix[currentRow][tempColumn];
                    }
                }
                //step 4
                currentRow++;
                currentColumn++;
            }
            return matrix;
        }

        public static ModRationalNumber[][] RemoveNullLines(ModRationalNumber[][] convertedAugmentedMatrix)
        {
            var listNotNullLineIndexes = new List<int>();
            int lineIndex = 0;
            foreach (var line in convertedAugmentedMatrix)
            {
                bool isNullLine = true;
                foreach (var column in line)
                    if (column != 0)
                    {
                        isNullLine = false;
                        break;
                    }
                if (!isNullLine) listNotNullLineIndexes.Add(lineIndex);
                lineIndex++;
            }
            var array = new ModRationalNumber[listNotNullLineIndexes.Count][];
            int i = 0;
            foreach (var index in listNotNullLineIndexes)
                array[i++] = convertedAugmentedMatrix[index];
            return array;
        }

        #endregion
        //public static bool IsLinearIndependent(BigInteger[][] mtrx)
        //{
        //    var matrix = BigIntegerExtension.ToTwoDimensionalRationalNumberArray(mtrx);
        //    var triangularMatrix = ToTriangularForm(matrix);
        //    int lastRowIndex = triangularMatrix.Length - 1;
        //    for (int i = 0; i < triangularMatrix[0].Length; i++)
        //        if (triangularMatrix[lastRowIndex][i] != 0)
        //            return true;
        //    return false;
        //}


    }
  
}






   