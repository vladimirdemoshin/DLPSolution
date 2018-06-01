using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Utility;
using DLPAlgorithm;
using System.Numerics;

namespace Test
{
    public static class TestRhoPollardDLP
    {
        #region Properties
        public static string ElapsedTime { get; set; }
        public static BigInteger StepsCount { get; set; }
        public static string PrimesFolderPath { get; set; }
        public static string GeneratorsFolderPath { get; set; }
        #endregion

        static TestRhoPollardDLP()
        {
            PrimesFolderPath = @"..\..\..\TestUtility\";
            GeneratorsFolderPath = @"..\..\..\TestUtility\";
        }

        public static BigInteger SolveDLP(BigInteger g, BigInteger h, BigInteger p)
        {
            var startTime = System.Diagnostics.Stopwatch.StartNew();
            var x = RhoPollard.SolveDLP(g, h, p);
            startTime.Stop();
            var resultTime = startTime.Elapsed;
            ElapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:000}", resultTime.Hours, resultTime.Minutes, resultTime.Seconds, resultTime.Milliseconds);
            StepsCount = RhoPollard.StepsCount;
            return x;
        }
        public static void SolveDLPRange(int startBitLength, int finishBitLength, int count)
        {
            BigIntegerRandom rand = new BigIntegerRandom();
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
                }
                FileUtility.WriteStringInFile(String.Format(@"..\..\..\DLPUtility\rhopollard{0}bits.txt", i), text);
            }
        }
    }
}
