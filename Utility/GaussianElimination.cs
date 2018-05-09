using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Numerics;

namespace Utility
{
    public static class GaussianElimination
    {
        #region Methods
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

        public static BigInteger[] SolveSystemOfLinearEquatations(BigInteger order, BigInteger[][] coefficients, BigInteger[] constantTerms)
        {
            var augmentedMatrix = ToAugmentedMatrix(coefficients, constantTerms);
            var convertedAugmentedMatrix = Converter.ToTwoDimensionalModRationalNumberArray(augmentedMatrix);
            convertedAugmentedMatrix = ToTriangularForm(convertedAugmentedMatrix);
            return null;

            //var X = new List<RationalNumber>();
            //int n = matrix.Length;
            //int m = matrix[0].Length;

            //for (int i = n - 1; i >= 0; i--)
            //{
            //    var constantTerm = matrix[i][m - 1].ToModBigInteger(order);
            //    var Ci = matrix[i][i].ToModBigInteger(order);
            //    var gcd = BigInteger.GreatestCommonDivisor(Ci, constantTerm);
            //    if (gcd == 1)
            //        X.Add(BigIntegerExtension.ModPositive(constantTerm * Ci.ModInverse(order), order));
            //    else
            //    {
            //        var reducedMOD = order / gcd;
            //        var x0 = (Ci / gcd).ModInverse(reducedMOD);
            //        x0 = x0 * (constantTerm / gcd);
            //        x0 = x0.ModPositive(reducedMOD);
            //        for (BigInteger j = 0; j < gcd; j++)
            //        {
            //            var x = x0 + j * reducedMOD;
            //            if (BigInteger.ModPow(g, x, p) == factorBase[i])
            //            {
            //                X.Add(x.ModPositive(order));
            //                break;
            //            }
            //        }
            //    }

            //    X.Add(matrix[i][m - 1] / matrix[i][i]);
            //    for (int k = i - 1; k >= 0; k--)
            //    {
            //        matrix[k][m - 1] = matrix[k][m - 1] - matrix[k][i] * X[n - 1 - i];
            //    }
            //}
            //X.Reverse();
            //return BigIntegerExtension.ToBigIntegerArray(X.ToArray(), order);
        }


        public static BigInteger[][] ToAugmentedMatrix(BigInteger[][] coefficients, BigInteger[] constantTerms)
        {
            var temp = new BigInteger[coefficients.Length][];
            for (int i = 0; i < temp.Length; i++)
                temp[i] = coefficients[i].Concat(new BigInteger[] { constantTerms[i] }).ToArray();
            return temp;
        }

        public static ModRationalNumber[][] ToTriangularForm(ModRationalNumber[][] matrix)
        {
            int n = matrix.Length; //count of linear equatations
            int m = matrix[0].Length; // count of variables
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
                    ModRationalNumber c = (-1) * (matrix[tempRow][currentColumn]);
                    for (int tempColumn = currentColumn; tempColumn < m; tempColumn++)
                    {
                        matrix[tempRow][tempColumn] = matrix[tempRow][tempColumn] + c * matrix[currentRow][tempColumn];
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

       


        //public static BigInteger[][] ToTriangularForm(BigInteger[][] matrix, BigInteger order)
        //{

        //    //Console.WriteLine("Before");
        //    //for (int i = 0; i < matrix.Length; i++)
        //    //{
        //    //    for (int j = 0; j < matrix[i].Length; j++)
        //    //    {
        //    //        Console.Write(matrix[i][j] + "  ");
        //    //    }
        //    //    Console.WriteLine();
        //    //}
        //    //Console.WriteLine(); Console.WriteLine();



        //    int n = matrix.Length; //count of linear equatations
        //    int m = matrix[0].Length; // count of variables
        //    int currentColumn = 0;
        //    int currentRow = 0;
        //    while (currentColumn < m && currentRow < n)
        //    {
        //        //step 1
        //        int leadingRow = -1;
        //        BigInteger leadingElement = 1;
        //        for (int tempRow = currentRow; tempRow < n; tempRow++)
        //            if (matrix[tempRow][currentColumn] != 0)
        //            {
        //                leadingRow = tempRow;
        //                leadingElement = matrix[tempRow][currentColumn];
        //                break;
        //            }
        //        if (leadingRow == -1)
        //        {
        //            currentColumn++;
        //            continue;
        //        }
        //        if (leadingRow != currentRow)
        //        {
        //            for (int tempColumn = currentColumn; tempColumn < m; tempColumn++)
        //            {
        //                var tmp = matrix[leadingRow][tempColumn];
        //                matrix[leadingRow][tempColumn] = matrix[currentRow][tempColumn];
        //                matrix[currentRow][tempColumn] = tmp;
        //            }
        //        }
        //        //step 2
        //        for (int tempColumn = currentColumn; tempColumn < m; tempColumn++)
        //        {
        //            matrix[currentRow][tempColumn] /= leadingElement;
        //        }
        //        if (currentRow == n - 1) break;
        //        //step 3
        //        for (int tempRow = currentRow + 1; tempRow < n; tempRow++)
        //        {
        //            RationalNumber c = (-1) * (matrix[tempRow][currentColumn]);
        //            for (int tempColumn = currentColumn; tempColumn < m; tempColumn++)
        //            {
        //                matrix[tempRow][tempColumn] += c * matrix[currentRow][tempColumn];
        //            }
        //        }
        //        //step 4
        //        currentRow++;
        //        currentColumn++;
        //    }


            //Console.WriteLine("After");
            //for (int i = 0; i < matrix.Length; i++)
            //{
            //    for (int j = 0; j < matrix[i].Length; j++)
            //    {
            //        Console.Write(matrix[i][j] + "  ");
            //    }
            //    Console.WriteLine();
            //}
            //Console.WriteLine(); Console.WriteLine();


        //    return matrix;
        //}
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








//foreach (var a in triangularMatrix)
//{
//    foreach (var b in a)
//        Console.Write(b + " ");
//    Console.WriteLine();
//}




//RationalNumber[] lastRow = new RationalNumber[triangularMatrix[0].Length];
//for (int i = 0; i < lastRow.Length; i++) lastRow[i] = triangularMatrix[triangularMatrix.Length - 1][i];
//RationalNumber[] nullVector = new RationalNumber[lastRow.Length];
//for (int i = 0; i < nullVector.Length; i++) nullVector[i] = 0;
//for (int i = 0; i < lastRow.Length; i++)
//{
//    if (lastRow[i] != nullVector[i])
//        return true;
//}
//return false;











//working version
 //public static RationalNumber[][] ToTriangularForm(RationalNumber[][] matrix)
 //       {

 //           //Console.WriteLine("Before");
 //           //for (int i = 0; i < matrix.Length; i++)
 //           //{
 //           //    for (int j = 0; j < matrix[i].Length; j++)
 //           //    {
 //           //        Console.Write(matrix[i][j] + "  ");
 //           //    }
 //           //    Console.WriteLine();
 //           //}
 //           //Console.WriteLine(); Console.WriteLine();



 //           int n = matrix.Length; //count of linear equatations
 //           int m = matrix[0].Length; // count of variables
 //           int currentColumn = 0;
 //           int currentRow = 0;
 //           while (currentColumn < m && currentRow < n)
 //           {
 //               //step 1
 //               int leadingRow = -1;
 //               RationalNumber leadingElement = 1;
 //               for (int tempRow = currentRow; tempRow < n; tempRow++)
 //                   if (matrix[tempRow][currentColumn] != 0)
 //                   {
 //                       leadingRow = tempRow;
 //                       leadingElement = matrix[tempRow][currentColumn];
 //                       break;
 //                   }
 //               if (leadingRow == -1)
 //               {
 //                   currentColumn++;
 //                   continue;
 //               }
 //               if (leadingRow != currentRow)
 //               {
 //                   for (int tempColumn = currentColumn; tempColumn < m; tempColumn++)
 //                   {
 //                       var tmp = matrix[leadingRow][tempColumn];
 //                       matrix[leadingRow][tempColumn] = matrix[currentRow][tempColumn];
 //                       matrix[currentRow][tempColumn] = tmp;
 //                   }
 //               }
 //               //step 2
 //               for (int tempColumn = currentColumn; tempColumn < m; tempColumn++)
 //               {
 //                   matrix[currentRow][tempColumn] /= leadingElement;
 //               }
 //               if (currentRow == n - 1) break;
 //               //step 3
 //               for (int tempRow = currentRow + 1; tempRow < n; tempRow++)
 //               {
 //                   RationalNumber c = (-1) * (matrix[tempRow][currentColumn]);
 //                   for (int tempColumn = currentColumn; tempColumn < m; tempColumn++)
 //                   {
 //                       matrix[tempRow][tempColumn] += c * matrix[currentRow][tempColumn];
 //                   }
 //               }
 //               //step 4
 //               currentRow++;
 //               currentColumn++;
 //           }


 //           //Console.WriteLine("After");
 //           //for (int i = 0; i < matrix.Length; i++)
 //           //{
 //           //    for (int j = 0; j < matrix[i].Length; j++)
 //           //    {
 //           //        Console.Write(matrix[i][j] + "  ");
 //           //    }
 //           //    Console.WriteLine();
 //           //}
 //           //Console.WriteLine(); Console.WriteLine();


 //           return matrix;
 //       }