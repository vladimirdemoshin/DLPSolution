using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public static class GaussianElimination
    {
        #region Public Methods
        /// <summary>
        /// Return true, if vectors in matrix are linear independent, false otherwise
        /// </summary>
        /// <param name="mtrx"></param>
        /// <returns></returns>
        public static bool IsLinearIndependent(RationalNumber[][] mtrx)
        {
            var triangularMatrix = ToTriangularForm(mtrx);
            RationalNumber[] lastRow = new RationalNumber[triangularMatrix[0].Length - 1];
            for (int i = 0; i < lastRow.Length; i++) lastRow[i] = triangularMatrix[triangularMatrix.Length - 1][i];
            RationalNumber[] nullVector = new RationalNumber[lastRow.Length];
            for (int i = 0; i < nullVector.Length; i++) nullVector[i] = 0;
            for (int i = 0; i < lastRow.Length; i++)
            {
                if (lastRow[i] != nullVector[i])
                    return true;
            }
            return false;
        }

        //mtrx is in rectangular form n x n+1
        //mtrx is not linear dependent, i.e. solution exists (single)
        public static List<RationalNumber> SolveSystemOfLinearEquatations(RationalNumber[][] mtrx)
        {
            var matrix = ToTriangularForm(mtrx);
            //solve equation for an upper triangular matrix
            var X = new List<RationalNumber>();
            int n = matrix.Length;
            int m = matrix[0].Length;
            for (int i = n - 1; i >= 0; i--)
            {
                X.Add(matrix[i][m - 1] / matrix[i][i]);
                for (int k = i - 1; k >= 0; k--)
                {
                    matrix[k][m - 1] = matrix[k][m - 1] - matrix[k][i] * X[n - 1 - i];
                }
            }
            X.Reverse();
            return X;
        }


        public static RationalNumber[][] ToTriangularForm(RationalNumber[][] matrix)
        {

            Console.WriteLine("Before");
            for (int i = 0; i < matrix.Length; i++)
            {
                for (int j = 0; j < matrix[i].Length; j++)
                {
                    Console.Write(matrix[i][j] + "  ");
                }
                Console.WriteLine();
            }
            Console.WriteLine(); Console.WriteLine();



            int n = matrix.Length; //count of linear equatations
            int m = matrix[0].Length; // count of variables
            int currentColumn = 0;
            int currentRow = 0;
            while (currentColumn < m && currentRow < n)
            {
                //step 1
                int leadingRow = -1;
                RationalNumber leadingElement = 1;
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
                    matrix[currentRow][tempColumn] /= leadingElement;
                }
                if (currentRow == n - 1) break;
                //step 3
                for (int tempRow = currentRow + 1; tempRow < n; tempRow++)
                {
                    RationalNumber c = (-1) * (matrix[tempRow][currentColumn]);
                    for (int tempColumn = currentColumn; tempColumn < m; tempColumn++)
                    {
                        matrix[tempRow][tempColumn] += c * matrix[currentRow][tempColumn];
                    }
                }
                //step 4
                currentRow++;
                currentColumn++;
            }

            Console.WriteLine("After");
            for (int i = 0; i < matrix.Length; i++)
            {
                for (int j = 0; j < matrix[i].Length; j++)
                {
                    Console.Write(matrix[i][j] + "  ");
                }
                Console.WriteLine();
            }
            Console.WriteLine(); Console.WriteLine();
            return matrix;
        }
        #endregion
    }


    //public static RationalNumber[][] ToTriangularForm(RationalNumber[][] matrix)
    //{
    //    int n = matrix.Length; //count of linear equatations
    //    int m = matrix[0].Length; // count of variables
    //    for (int i = 0; i < n; i++)
    //    {
    //        //search for maximum in this column
    //        RationalNumber maxElement = RationalNumber.Abs(matrix[i][i]);
    //        int maxRow = i;
    //        for (int k = i + 1; k < n; k++)
    //        {
    //            if (matrix[k][i] > maxElement)
    //            {
    //                maxElement = matrix[k][i];
    //                maxRow = k;
    //            }
    //        }
    //        //swap maximum row with current row
    //        if (maxRow != i)
    //        {
    //            for (int k = i; k < m; k++)
    //            {
    //                var tmp = matrix[maxRow][k];
    //                matrix[maxRow][k] = matrix[i][k];
    //                matrix[i][k] = tmp;
    //            }
    //        }
    //        //Make all rows below this one 0 in current column
    //        for (int k = i + 1; k < n; k++)
    //        {
    //            var c = (-1) * (matrix[k][i] / matrix[i][i]);
    //            matrix[k][i] = new RationalNumber(0, 1);
    //            for (int j = i + 1; j < m; j++)
    //                matrix[k][j] = matrix[k][j] + c * matrix[i][j];
    //        }
    //    }
    //    return matrix;
    //}
}
