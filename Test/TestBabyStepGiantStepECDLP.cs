using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Utility;
using ECDLPAlgorithm;
using System.Numerics;
using EllipticCurveUtility;
using Excel = Microsoft.Office.Interop.Excel;

namespace Test
{
    public static class TestBabyStepGiantStepECDLP
    {
         #region Properties
        public static string ElapsedTime { get; set; }

        public static double TimeInSec { get; set; }
        public static BigInteger StepsCount { get; set; }
        public static string PrimesFolderPath { get; set; }
        public static string GeneratorsFolderPath { get; set; }
        #endregion

        static TestBabyStepGiantStepECDLP()
        {
            PrimesFolderPath = @"..\..\..\TestUtility\";
            GeneratorsFolderPath = @"..\..\..\TestUtility\";
        }

        public static BigInteger SolveDLP(ProjectivePoint P, ProjectivePoint Q)
        {
            var startTime = System.Diagnostics.Stopwatch.StartNew();
            var x = BabyStepGiantStep.SolveDLP(P, Q);
            startTime.Stop();
            var resultTime = startTime.Elapsed;
            TimeInSec = resultTime.Hours * 3600 + resultTime.Minutes * 60 + resultTime.Seconds + (resultTime.Milliseconds * 1.0) / (1.0 * 100);
            ElapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:000}", resultTime.Hours, resultTime.Minutes, resultTime.Seconds, resultTime.Milliseconds);
            return x;
        }

        public static void SolveDLPRange(int startBitLength, int finishBitLength, int count)
        {
            Excel.Application excelApp = new Excel.Application();
            Excel.Workbook workBook;
            Excel.Worksheet workSheet;
            workBook = excelApp.Workbooks.Add();
            workSheet = (Excel.Worksheet)workBook.Worksheets.get_Item(1);
            excelApp.Visible = true;

            int row = 1;
            for (int i = startBitLength; i <= finishBitLength; i++)
            {
                var primes = FileUtility.ReadArrayFromFile(String.Format(@"C:\Utility\ECDLPUtility\Сurves\p{0}bits.txt", i));
                var arrA = FileUtility.ReadArrayFromFile(String.Format(@"C:\Utility\ECDLPUtility\Сurves\A{0}bits.txt", i));
                var arrB = FileUtility.ReadArrayFromFile(String.Format(@"C:\Utility\ECDLPUtility\Сurves\B{0}bits.txt", i));

                var arrX = FileUtility.ReadArrayFromFile(String.Format(@"C:\Utility\ECDLPUtility\Points\X{0}bits.txt", i));
                var arrY = FileUtility.ReadArrayFromFile(String.Format(@"C:\Utility\ECDLPUtility\Points\Y{0}bits.txt", i));
                //var arrOrder = FileUtility.ReadArrayFromFile(String.Format(@"C:\Utility\ECDLPUtility\Points\order{0}bits.txt", i));
                //var arrOrderbitslength = FileUtility.ReadArrayFromFile(String.Format(@"C:\Utility\ECDLPUtility\Points\orderbitslength{0}bits.txt", i));

                var A = arrA[0];
                var B = arrB[0];
                var p = primes[0];
                var curve = new EllipticCurveUtility.EllipticCurve(A, B, p);
                var X = arrX[0];
                var Y = arrY[0];
               // var order = arrOrder[0];
               // var orderBitsLength = arrOrderbitslength[0];
                var point = new EllipticCurveUtility.AffinePoint(X, Y, curve);

                var rand = new BigIntegerRandom();
                for (int j = 0; j < count; j++)
                {
                    var n = rand.Next(0, p - 1);
                    var P = point.ToProjectivePoint();
                    var Q = (n * P);
                    var log = SolveDLP(P, Q);

                    workSheet.Cells[row, 1] = i.ToString();
                    workSheet.Cells[row, 2] = curve.ToString();
                    workSheet.Cells[row, 3] = P.ToString();
                    workSheet.Cells[row, 4] = Q.ToString();
                   // workSheet.Cells[row, 5] = order.ToString();
                   // workSheet.Cells[row, 6] = orderBitsLength.ToString();
                    workSheet.Cells[row, 7] = TimeInSec.ToString();
                    workSheet.Cells[row, 8] = n.ToString();
                    workSheet.Cells[row, 9] = log.ToString();

                    row++;
                }

            }
        }
    }
}
