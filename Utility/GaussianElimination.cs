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

        public static BigInteger[] SolveSystemOfLinearEquatations(DLPInput input, ref BigInteger[] factorBase, BigInteger[][] coefficients, BigInteger[] constantTerms)
        {
            //Print(coefficients);
            Console.WriteLine("Size of fb = " + factorBase.Length);
            var reducedCoefficients = RemoveNullColumns(coefficients, ref factorBase);
            Console.WriteLine("Size of reduced fb = " + factorBase.Length);

            var augmentedMatrix = ToAugmentedMatrix(reducedCoefficients, constantTerms);
            var convertedAugmentedMatrix =  Converter.ToTwoDimensionalModRationalNumberArray(augmentedMatrix, input.order);
            //Print(convertedAugmentedMatrix);


            Console.WriteLine("Count of rows before to triangular = " + convertedAugmentedMatrix.Length);
            convertedAugmentedMatrix = ToTriangularForm(convertedAugmentedMatrix);
            Console.WriteLine("Count of rows after triangular = " + convertedAugmentedMatrix.Length);

            //Print(convertedAugmentedMatrix);
            convertedAugmentedMatrix = RemoveNullLines(convertedAugmentedMatrix);
            Console.WriteLine("Count of rows after null lines removal = " + convertedAugmentedMatrix.Length);

           // Print(convertedAugmentedMatrix);
           // Console.WriteLine(convertedAugmentedMatrix.Length +" - " + factorBase.Length );
            if (convertedAugmentedMatrix.Length < factorBase.Length)
            {
               // ReduceFactorBase(ref factorBase, ref convertedAugmentedMatrix);
                //Console.WriteLine(convertedAugmentedMatrix.Length + " - " + factorBase.Length);
                return null;
            }  
           // Print(convertedAugmentedMatrix);
            var solution = SolveTriangularSystemOfLinearEquatations(input, factorBase, convertedAugmentedMatrix);
            return solution;
            //return null;
        }

        public static BigInteger[][] RemoveNullColumns(BigInteger[][] coefficients, ref BigInteger[] factorBase)
        {
            var listFactorBase = new List<BigInteger>();
            //create list of not null columns in coefficients matrix
            var listColumns = new List<List<BigInteger>>();
            for(int j=0;j<coefficients[0].Length;j++)
            {
                var temp = new List<BigInteger>();
                bool isNullColumn = true;  
                for(int i=0;i<coefficients.Length;i++)
                {
                    BigInteger tmp = coefficients[i][j];
                    if(tmp != 0) isNullColumn = false;
                    temp.Add(tmp);
                }
                if (!isNullColumn)
                {
                    listFactorBase.Add(factorBase[j]);
                    listColumns.Add(temp);
                }
            }

            //create new BigInteger[][] matrix without null columns
            var reducedCoefficients = new BigInteger[coefficients.Length][];
            for(int i=0;i<reducedCoefficients.Length;i++)
            {
                reducedCoefficients[i] = new BigInteger[listColumns.Count];
                for(int j=0;j<reducedCoefficients[i].Length;j++)
                {
                    reducedCoefficients[i][j] = listColumns[j][i];
                }
            }

            //remove primes from factor base, which has related null columns 
            factorBase = listFactorBase.ToArray();
            return reducedCoefficients;


            //int countRows = coefficients.Length;

            //var listCoefficients = new List<List<BigInteger>>();
            //for(int i=0;i<countRows;i++)
            //{
            //    listCoefficients.Add(coefficients[i].ToList());
            //}

            //for(int j=0;j<coefficients[0].Length;j++)
            //{
            //    bool toDeleteRow = true;
            //    for(int i=0;i<countRows;i++)
            //    {
            //        if(listCoefficients[i][j] != 0)
            //        {
            //            toDeleteRow = false;
            //            break;
            //        }
            //    }
            //    if(toDeleteRow)
            //}

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
                //Console.WriteLine("try to solve "+i);
                var x = SolveLinearEquatation(input, logarithmicExpression, a.ModPositive(mod), b.ModPositive(mod));
                solution.Add(x);
                for (int k = i - 1; k >= 0; k--)
                    matrix[k][m - 1] = matrix[k][m - 1] - solution[n - 1 - i] * matrix[k][i];
               // Console.WriteLine("solved + " + i);
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

                //return 1;
                Console.WriteLine(gcd);
                BigInteger u, v;
                BigIntegerExtension.ExtendedGcd(a, mod, out u, out v);
                var reducedMOD = mod / gcd;
                var x0 = ((b / gcd) * u).ModPositive(reducedMOD);
                for (BigInteger j = 0; j < gcd; j++)
                {

                    var x = x0 + j * reducedMOD;
                    // Console.WriteLine(x);
                    if (BigInteger.ModPow(input.g, x, input.p) == logatithmicExpression)
                        return x;
                }
            }
            return -1;
        }

        public static void ReduceFactorBase(ref BigInteger[] factorBase, ref ModRationalNumber[][] matrix)
        {
            var indexes = new List<int>();
            for (int i = 0, j = 0; i - j < matrix.Length; i++)
            {
                if (matrix[i - j][i].Numerator == 0)
                    j++;
                else
                    indexes.Add(i);
            }
            var reducedFactorBase = new BigInteger[indexes.Count];
            var reducedMatrix = new ModRationalNumber[indexes.Count][];
            for (int i = 0; i < reducedMatrix.Length; i++)
                reducedMatrix[i] = new ModRationalNumber[indexes.Count + 1];
            int k = 0;
            //удаляем незадействованные простые числа из факторной базы и удаляем нулевые столбцы из матрицы (точнее, создаем новые объекты на основе старых)
            foreach (var index in indexes)
            {
                reducedFactorBase[k] = factorBase[index];
                for (int j = 0; j < reducedMatrix.Length; j++)
                    reducedMatrix[j][k] = new ModRationalNumber(factorBase[index], 1, reducedMatrix[j][k].Mod);
                k++;
            }
            //приписываем к новой матрице справа свободные члены
            for (int i = 0; i < reducedMatrix.Length; i++)
                reducedMatrix[i][reducedMatrix[i].Length - 1] = matrix[i][matrix[i].Length - 1];
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


        public static void Print(BigInteger[][] matrix)
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

        ////тестовая функция
        //public static double[][] ToTriangularForm(double[][] matrix)
        //{
        //    int n = matrix.Length; //количество строк 
        //    int m = matrix[0].Length; //количество столбцов
        //    int currentColumn = 0; //текущий рассматриваемый столбец
        //    int currentRow = 0; //текущая рассматриваемая строка
        //    while (currentRow != n - 1 && currentColumn != m)  //currentColumn < m && currentRow < 
        //    {
        //        //шаг 1 - находим ненулевой элемент в текущем столбце и меняем с текущей строкой
        //        int leadingRow = -1; //ряд с ведущим элементом
        //        double leadingElement = 1; //ведущий элемент
        //        for (int tempRow = currentRow; tempRow < n; tempRow++) //идем по одному столбцу и всем строчкам
        //            if (matrix[tempRow][currentColumn] != 0)
        //            {
        //                leadingRow = tempRow;
        //                leadingElement = matrix[tempRow][currentColumn];
        //                break;
        //            }
        //        //if (leadingRow == -1) //если -1, то в столбце все нули, пропускаем столб, но у нас такого не бывает, так как все нулевые столбцы удалены
        //        //{
        //        //    currentColumn++;
        //        //    continue;
        //        //}
        //        if (leadingRow != currentRow) //если первый же элемент ненулевой, то ниче не меняем местами
        //        {
        //            for (int tempColumn = currentColumn; tempColumn < m; tempColumn++)
        //            {
        //                var tmp = matrix[leadingRow][tempColumn];
        //                matrix[leadingRow][tempColumn] = matrix[currentRow][tempColumn];
        //                matrix[currentRow][tempColumn] = tmp;
        //            }
        //        }

        //        //убираем шаг 2, он реализуется в шаге 3
        //        //шаг 2 - создаем временную строку, делим её на ведущий элемент
        //        //var tempCurrentRowCopy = new double[m];
        //        //for (int tempColumn = currentColumn; tempColumn < m; tempColumn++)
        //        //{
        //        //    tempCurrentRowCopy[tempColumn] = matrix[currentRow][tempColumn] / leadingElement;
        //        //}
        //        // if (currentRow == n - 1) break;

        //        //шаг 3 - прибавляем ко всем строкам, ниже текущей, текущую строку, деленную на ведущий элемент и умноженую на первый элемент каждой строки с противоположным знаком
        //        for (int tempRow = currentRow + 1; tempRow < n; tempRow++)
        //        {
        //            double coeff = matrix[tempRow][currentColumn] / (1.0 * leadingElement);
        //            //if (c == 0) continue;
        //            for (int tempColumn = currentColumn; tempColumn < m; tempColumn++)
        //            {

        //                //else matrix[tempRow][tempColumn] = matrix[tempRow][tempColumn] - c * matrix[currentRow][tempColumn];
        //                matrix[tempRow][tempColumn] -= coeff * matrix[currentRow][tempColumn];
        //            }
        //        }


        //        //for (int i = 0; i < m; i++)
        //        //    Console.Write(matrix[currentRow][i] + " ");

        //        //Console.WriteLine();

        //        //step 4
        //        currentRow++;
        //        currentColumn++;
        //    }
        //    //Console.WriteLine("colunn row" + currentRow + " " + currentColumn);
        //    return matrix;
        //}

        //главная функция
        public static ModRationalNumber[][] ToTriangularForm(ModRationalNumber[][] matrix)
        {
            var Mod = matrix[0][0].Mod; // делаем допущение, что все элементы берутся по одному модулю
            int n = matrix.Length; //количество строк 
            int m = matrix[0].Length; //количество столбцов
            int currentColumn = 0; //текущий рассматриваемый столбец
            int currentRow = 0; //текущая рассматриваемая строка
            while (currentRow != n - 1 && currentColumn != m)
            {
                //шаг 1 - находим ненулевой элемент в текущем столбце и меняем с текущей строкой
                int leadingRow = -1; //ряд с ведущим элементом
                var leadingElement = new ModRationalNumber(1,1,Mod); //ведущий элемент
                for (int tempRow = currentRow; tempRow < n; tempRow++) //идем по одному столбцу и всем строчкам
                    if (matrix[tempRow][currentColumn] != 0)
                    {
                        leadingRow = tempRow;
                        leadingElement = matrix[tempRow][currentColumn];
                        break;
                    }
                if (leadingRow == -1) //если -1, то в столбце все нули, пропускаем столб
                {
                    currentColumn++;
                    continue;
                }
                if (leadingRow != currentRow) //если первый же элемент ненулевой, то ниче не меняем местами
                {
                    for (int tempColumn = currentColumn; tempColumn < m; tempColumn++)
                    {
                        var tmp = matrix[leadingRow][tempColumn];
                        matrix[leadingRow][tempColumn] = matrix[currentRow][tempColumn];
                        matrix[currentRow][tempColumn] = tmp;
                    }
                }

                //убираем шаг 2, он реализуется в шаге 3
                //шаг 2 - создаем временную строку, делим её на ведущий элемент
                //var tempCurrentRowCopy = new double[m];
                //for (int tempColumn = currentColumn; tempColumn < m; tempColumn++)
                //{
                //    tempCurrentRowCopy[tempColumn] = matrix[currentRow][tempColumn] / leadingElement;
                //}
                // if (currentRow == n - 1) break;

                //шаг 3 - прибавляем ко всем строкам, ниже текущей, текущую строку, деленную на ведущий элемент и умноженую на первый элемент каждой строки с противоположным знаком
                for (int tempRow = currentRow + 1; tempRow < n; tempRow++)
                {
                    var coeff = (matrix[tempRow][currentColumn] / leadingElement);
                    for (int tempColumn = currentColumn; tempColumn < m; tempColumn++)
                    {
                        //if (tempColumn == currentColumn)
                        //{
                        //    matrix[tempRow][tempColumn] = new ModRationalNumber(0, 1, Mod);
                        //    continue;
                        //}
                        matrix[tempRow][tempColumn] -= coeff * matrix[currentRow][tempColumn];
                    }
                }


                //for (int i = 0; i < m; i++)
                //    Console.Write(matrix[currentRow][i] + " ");

                //Console.WriteLine();

                //step 4
                currentRow++;
                currentColumn++;
            }
            //Console.WriteLine("colunn row" + currentRow + " " + currentColumn);
            return matrix;
        }


       

        public static ModRationalNumber[][] RemoveNullLines(ModRationalNumber[][] convertedAugmentedMatrix)
        {
            var listNotNullLineIndexes = new List<int>();
            int lineIndex = 0;
            foreach (var line in convertedAugmentedMatrix)
            {
                bool isNullLine = true;
                for (int i = 0; i < line.Length - 1 ;i++)
                {
                    if (line[i].Numerator != 0)
                    {
                        isNullLine = false;
                        break;
                    }
                }   
                if (!isNullLine) listNotNullLineIndexes.Add(lineIndex);
                lineIndex++;
            }
            var array = new ModRationalNumber[listNotNullLineIndexes.Count][];
            int k = 0;
            foreach (var index in listNotNullLineIndexes)
                array[k++] = convertedAugmentedMatrix[index];
            return array;
        }

        #endregion
        public static bool IsLinearIndependent(List<List<BigInteger>> mtrx, BigInteger mod)
        {
            var temp = new BigInteger[mtrx.Count][];
            int i = 0;
            foreach(var row in mtrx)
            {
                temp[i] = new BigInteger[row.Count];
                for(int j=0;j<row.Count;j++)
                {
                    temp[i][j] = row[j];
                }
                i++;
            }
            var triangularMatrix = ToTriangularForm(Converter.ToTwoDimensionalModRationalNumberArray(temp, mod));
            int lastRowIndex = triangularMatrix.Length - 1;
            for (int k = 0; k < triangularMatrix[0].Length; k++)
                if (triangularMatrix[lastRowIndex][k] != 0)
                    return true;
            return false;
        }


    }
  
}






   