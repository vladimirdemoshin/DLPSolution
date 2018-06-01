using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Utility;
using System.Numerics;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using DLPAlgorithm;


namespace ConsoleTest
{
    public static class TestDLPAlgorithm
    {
        public static void TestBabyStepGiantStep(int startBitLength, int finishBitLength, int count)
        {
            BigIntegerRandom rand = new BigIntegerRandom();
           // var dataSheet = new List<string[]>();
            for(int i = startBitLength; i<= finishBitLength; i++)
            {
                var primes = FileUtility.ReadArrayFromFile(String.Format(@"..\..\..\TestUtility\primes{0}bits.txt", i));
                var generators = FileUtility.ReadArrayFromFile(String.Format(@"..\..\..\TestUtility\generators{0}bits.txt", i));
                var text = "";
                for(int j = 0; j < count; j++)
                {
                    var p = primes[j];
                    var g = generators[j];

                    var log = rand.Next(0, p-1);
                    var h = BigInteger.ModPow(g, log, p);

                    var startTime = System.Diagnostics.Stopwatch.StartNew();
                    var x = DLPAlgorithm.BabyStepGiantStep.SolveDLP(g, h, p);
                    startTime.Stop();
                    var resultTime = startTime.Elapsed;
                    string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:000}",
                        resultTime.Hours,
                        resultTime.Minutes,
                        resultTime.Seconds,
                        resultTime.Milliseconds);
                    var line = i.ToString() + " " + p.ToString() + " " + g.ToString() + " " + h.ToString() + " " + log.ToString() + " " + x.ToString() + " " + elapsedTime;
                    text += line + Environment.NewLine + Environment.NewLine;          
                    //var dataColumn = new string[] { i.ToString(), p.ToString(), g.ToString(), h.ToString(), log.ToString(), x.ToString(), elapsedTime };
                    //dataSheet.Add(dataColumn);
                }
                FileUtility.WriteStringInFile(String.Format(@"..\..\..\DLPUtility\babystepgiantstep{0}bits.txt", i), text);   
            }
            //var excelFile = new ExcelFile();
            //excelFile.ExcelFilePath = String.Format(@"D:\BabyStepGiantStep.xlsx");
            //excelFile.OpenExcel();
            //foreach(var dataColumn in dataSheet)
            //{
            //    excelFile.AddDataToExcel(dataColumn);
            //}
            //excelFile.CloseExcel();
        }

        public static void TestRhoPollard(int startBitLength, int finishBitLength, int count)
            {
            BigIntegerRandom rand = new BigIntegerRandom();
           // var dataSheet = new List<string[]>();
            for(int i = startBitLength; i<= finishBitLength; i++)
            {
                var primes = FileUtility.ReadArrayFromFile(String.Format(@"..\..\..\TestUtility\primes{0}bits.txt", i));
                var generators = FileUtility.ReadArrayFromFile(String.Format(@"..\..\..\TestUtility\generators{0}bits.txt", i));
                var text = "";
                for(int j = 0; j < count; j++)
                {
                    var p = primes[j];
                    var g = generators[j];

                    var log = rand.Next(0, p-1);
                    var h = BigInteger.ModPow(g, log, p);

                    var startTime = System.Diagnostics.Stopwatch.StartNew();
                    var x = DLPAlgorithm.RhoPollard.SolveDLP(g, h, p);
                    startTime.Stop();
                    var resultTime = startTime.Elapsed;
                    string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:000}",
                        resultTime.Hours,
                        resultTime.Minutes,
                        resultTime.Seconds,
                        resultTime.Milliseconds);
                    var line = i.ToString() + " " + p.ToString() + " " + g.ToString() + " " + h.ToString() + " " + log.ToString() + " " + x.ToString() + " " + elapsedTime;
                    text += line + Environment.NewLine + Environment.NewLine;          
                    //var dataColumn = new string[] { i.ToString(), p.ToString(), g.ToString(), h.ToString(), log.ToString(), x.ToString(), elapsedTime };
                    //dataSheet.Add(dataColumn);
                }
                FileUtility.WriteStringInFile(String.Format(@"..\..\..\DLPUtility\rhopollard{0}bits.txt", i), text);   
            }
            //var excelFile = new ExcelFile();
            //excelFile.ExcelFilePath = String.Format(@"D:\BabyStepGiantStep.xlsx");
            //excelFile.OpenExcel();
            //foreach(var dataColumn in dataSheet)
            //{
            //    excelFile.AddDataToExcel(dataColumn);
            //}
            //excelFile.CloseExcel();
        }




        //public static void TestIndexCalculus(int startBitLength, int finishBitLength, int count)
        //{
        //    BigIntegerRandom rand = new BigIntegerRandom();
        //    for (int i = startBitLength; i <= finishBitLength; i++)
        //    {
        //        Console.WriteLine(i + " bits");
        //        var primes = FileUtility.ReadArrayFromFile(String.Format(@"..\..\..\TestUtility\primes{0}bits.txt", i));
        //        var generators = FileUtility.ReadArrayFromFile(String.Format(@"..\..\..\TestUtility\generators{0}bits.txt", i));
        //        var text = "";
        //        for (int j = 0; j < count; j++)
        //        {
        //            var p = primes[j];
        //            var g = generators[j];

        //            var log = rand.Next(0, p - 1);
        //            var h = BigInteger.ModPow(g, log, p);

        //            System.Diagnostics.Stopwatch startTime = System.Diagnostics.Stopwatch.StartNew();
        //            var x = DLPAlgorithm.IndexCalculus.SolveDLP(g, h, p);
        //            var resultTime = startTime.Elapsed;

        //            Console.WriteLine(IndexCalculus.FactorBaseSize + " = " +  IndexCalculus.ReducedFactorBaseSize);
        //            Console.WriteLine(x + " = " + log);
        //            Console.WriteLine(resultTime);

        //            Console.WriteLine();
        //        }
                
        //    }
        //}


        public static void TestIndexCalculus(int startBitLength, int finishBitLength, int count)
        {
            BigIntegerRandom rand = new BigIntegerRandom();
            int k = 1;
            int step = 20;
            int start = 100;
            int cofLinearCount = 4;
            for (int i = startBitLength; i <= finishBitLength; i++)
            {
                var primes = FileUtility.ReadArrayFromFile(String.Format(@"..\..\..\TestUtility\primes{0}bits.txt", i));
                var generators = FileUtility.ReadArrayFromFile(String.Format(@"..\..\..\TestUtility\generators{0}bits.txt", i));
                var text = "";
                for (int j = 0; j < count; j++)
                {
                    var p = primes[j];
                    var g = generators[j];

                    var log = rand.Next(0, p - 1);
                    var h = BigInteger.ModPow(g, log, p);

                    IndexCalculus.LinearEquatationsCount = cofLinearCount * IndexCalculus.FactorBaseSize;

                    System.Diagnostics.Stopwatch startTime = null;
                    BigInteger x = -1;
                    BigInteger startFactBase = 1;
                    while(x == -1)
                    {
                        startTime = System.Diagnostics.Stopwatch.StartNew();
                        startFactBase = IndexCalculus.FactorBaseSize;
                        x = DLPAlgorithm.IndexCalculus.SolveDLP(g, h, p);
                        if(x == -1) IndexCalculus.FactorBaseSize += 5;
                    }
                    startTime.Stop();

                    var resultTime = startTime.Elapsed;

                    string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:000}",
                        resultTime.Hours,
                        resultTime.Minutes,
                        resultTime.Seconds,
                        resultTime.Milliseconds);

                    var line = i.ToString() + " bits,  p = " + p.ToString() + ", g = " + g.ToString() +
                        ", h = " + h.ToString() + " , log = " + log.ToString() + " , x = " + x.ToString() +
                        " , time = " + elapsedTime + " , startFactorBaseSize = " + startFactBase +
                        " , reducedFactorBaseSize = " + IndexCalculus.ReducedFactorBaseSize;

                    text += line + Environment.NewLine + Environment.NewLine;

                    k++;
                }
                FileUtility.WriteStringInFile(String.Format(@"..\..\..\IndexCalculusUtility\indexcalculus{0}bits.txt", i), text);
            }
        }



        //public static void TestIndexCalculus(int startBitLength, int finishBitLength, int count)
        //{
        //    BigIntegerRandom rand = new BigIntegerRandom();
        //    int k = 1;
        //    int step = 10;
        //    int start = 100;
        //    for (int i = startBitLength; i <= finishBitLength; i++)
        //    {
        //        var primes = FileUtility.ReadArrayFromFile(String.Format(@"..\..\..\TestUtility\primes{0}bits.txt", i));
        //        var generators = FileUtility.ReadArrayFromFile(String.Format(@"..\..\..\TestUtility\generators{0}bits.txt", i));
        //        var text = "";
        //        for (int j = 0; j < count; j++)
        //        {
        //            var p = primes[j];
        //            var g = generators[j];

        //            var log = rand.Next(0, p - 1);
        //            var h = BigInteger.ModPow(g, log, p);

        //           c

        //            int fbLength = 3;
        //            int z = 2;
        //            BigInteger x = 1;
        //            for (; fbLength < p; fbLength++)
        //            {
        //                bool find = false;
        //                for (; z <= 5; z++)
        //                {
        //                    startTime = System.Diagnostics.Stopwatch.StartNew();
        //                    IndexCalculus.LinearEquatationsCount = z * fbLength;
        //                    IndexCalculus.FactorBaseSize = fbLength;
        //                    x = DLPAlgorithm.IndexCalculus.SolveDLP(g, h, p);
        //                    startTime.Stop();
        //                    if (x != -1)
        //                    {
        //                        find = true;
        //                        break;
        //                    }
        //                }
        //                if (find)
        //                    break;

        //            }

        //            var resultTime = startTime.Elapsed;


        //            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:000}",
        //                resultTime.Hours,
        //                resultTime.Minutes,
        //                resultTime.Seconds,
        //                resultTime.Milliseconds);
        //            var line = i.ToString() + " " + p.ToString() + " " + g.ToString() +
        //                " " + h.ToString() + " " + log.ToString() + " " + x.ToString() +
        //                " " + elapsedTime + " " + fbLength + " " + z;
        //            text += line + Environment.NewLine + Environment.NewLine;
        //            //var dataColumn = new string[] { i.ToString(), p.ToString(), g.ToString(), h.ToString(), log.ToString(), x.ToString(), elapsedTime };
        //            //dataSheet.Add(dataColumn);
        //        }
        //        FileUtility.WriteStringInFile(String.Format(@"..\..\..\IndexCalculusUtility\indexcalculus{0}bits.txt", i), text);
        //    }
        //}

        

    }
}
