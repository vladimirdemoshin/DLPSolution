using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;
using DLPAlgorithm;
using System.Numerics;
using Excel = Microsoft.Office.Interop.Excel;
namespace Test
{
    public static class TestIndexCalculusDLP
    {
        #region Properties
        public static string ElapsedTime { get; set; }
        public static string PrimesFolderPath { get; set; }
        public static string GeneratorsFolderPath { get; set; }
        public static int StartFactorBaseSize{get;set;}
        public static int ReducedFactorBaseSize { get; set; }
        public static double TimeInSec { get; set; }
        #endregion
        static TestIndexCalculusDLP()
        {
            PrimesFolderPath = @"..\..\..\TestUtility\";
            GeneratorsFolderPath = @"..\..\..\TestUtility\";
        }
        public static BigInteger SolveDLP(BigInteger g, BigInteger h, BigInteger p)
        {
            StartFactorBaseSize = IndexCalculus.FactorBaseSize;
            var startTime = System.Diagnostics.Stopwatch.StartNew();
            var x = IndexCalculus.SolveDLP(g, h, p);
            startTime.Stop();
            var resultTime = startTime.Elapsed;
            TimeInSec = resultTime.Hours * 3600 + resultTime.Minutes * 60 + resultTime.Seconds + (resultTime.Milliseconds * 1.0) / (1.0 * 100);
            ElapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:000}", resultTime.Hours, resultTime.Minutes, resultTime.Seconds, resultTime.Milliseconds);
            ReducedFactorBaseSize = IndexCalculus.ReducedFactorBaseSize;
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
            int row = 2;
            for (int i = startBitLength; i <= finishBitLength; i++)
            {
                var primes = FileUtility.ReadArrayFromFile(String.Format(@"C:\Utility\Primes\primes{0}bits.txt", i));
                var generators = FileUtility.ReadArrayFromFile(String.Format(@"C:\Utility\DLPUtility\Generators\generators{0}bits.txt", i));
                var logs = FileUtility.ReadArrayFromFile(String.Format(@"C:\Utility\DLPUtility\Logs\x{0}bits.txt", i));
                var values = FileUtility.ReadArrayFromFile(String.Format(@"C:\Utility\DLPUtility\Values\h{0}bits.txt", i));
                double average = 0;
                for (int j = 0; j < count; j++)
                {
                    var p = primes[j];
                    var g = generators[j];
                    var x = logs[j];
                    var h = values[j];
                    var log = SolveDLP(g, h, p);
                    workSheet.Cells[row, 1] = i.ToString();
                    workSheet.Cells[row, 2] = p.ToString();
                    workSheet.Cells[row, 3] = g.ToString();
                    workSheet.Cells[row, 4] = h.ToString();
                    workSheet.Cells[row, 5] = x.ToString();
                    workSheet.Cells[row, 6] = log.ToString();
                    workSheet.Cells[row, 7] = TimeInSec.ToString();
                    workSheet.Cells[row, 8] = IndexCalculus.LinearEquatationsCount.ToString();
                    workSheet.Cells[row, 9] = StartFactorBaseSize.ToString();
                    workSheet.Cells[row, 10] = ReducedFactorBaseSize.ToString();
                    row++;
                    average += TimeInSec;
                }
                workSheet.Cells[row, 11] = average / (count * 1.0);
                row++;
            }
        }
    }
}
