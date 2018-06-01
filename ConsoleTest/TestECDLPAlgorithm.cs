using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Utility;
using System.Numerics;
using System.Reflection;
using DLPAlgorithm;

namespace ConsoleTest
{
    public static class TestECDLPAlgorithm
    {
        public static void TestBabyStepGiantStep(int startBitLength, int finishBitLength, int count)
        {
            BigIntegerRandom rand = new BigIntegerRandom();
            for (int i = startBitLength; i <= finishBitLength; i++)
            {
                int tempI = 27;
                BigInteger a = 19;
                BigInteger b = 7;
                BigInteger p = 121572721;
                var curve = new EllipticCurveUtility.EllipticCurve(a,b,p);
                BigInteger x = 42812350;
                BigInteger y = 114715677;
                BigInteger order = 1820539;
                var point = new EllipticCurveUtility.AffinePoint(x,y,curve);
                var text = "";
                for (int j = 0; j < count; j++)
                {
                    var n = rand.Next(0, p - 1);
                    var Q = (n * point.ToProjectivePoint()).ToAffinePoint();

                    var startTime = System.Diagnostics.Stopwatch.StartNew();
                    var log = ECDLPAlgorithm.RhoPollard.SolveDLP(point, Q);//ECDLPAlgorithm.BabyStepGiantStep.SolveDLP(point.ToProjectivePoint(), Q.ToProjectivePoint());
                    startTime.Stop();
                    var resultTime = startTime.Elapsed;
                    string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:000}",
                        resultTime.Hours,
                        resultTime.Minutes,
                        resultTime.Seconds,
                        resultTime.Milliseconds);
                    var line = tempI.ToString() + " " + n.ToString() + " " + log.ToString() + " " + elapsedTime;
                    text += line + Environment.NewLine + Environment.NewLine;
                    //FileUtility.WriteStringInFile(String.Format(@"..\..\..\ECDLPUtility\babystepgiantstep{0}bits.txt", tempI), text);
                    text = "";

                    //startTime = System.Diagnostics.Stopwatch.StartNew();
                    //log = ECDLPAlgorithm.RhoPollard.SolveDLP(point, Q);
                    //startTime.Stop();
                    //resultTime = startTime.Elapsed;
                    //elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:000}",
                    //    resultTime.Hours,
                    //    resultTime.Minutes,
                    //    resultTime.Seconds,
                    //    resultTime.Milliseconds);
                    //line = tempI.ToString() + " " + n.ToString() + " " + log.ToString() + " " + elapsedTime;
                    //text += line + Environment.NewLine + Environment.NewLine;
                    FileUtility.WriteStringInFile(String.Format(@"..\..\..\ECDLPUtility\rhopollard{0}bits.txt", tempI), text);
                }
                //FileUtility.WriteStringInFile(String.Format(@"..\..\..\ECDLPUtility\babystepgiantstep{0}bits.txt", tempI), text);
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

    }
}
