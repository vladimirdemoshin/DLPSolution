using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Numerics;
using Utility;
using EllipticCurveUtility;
using DLPAlgorithm;
using System.Linq.Expressions;
using System.Threading;
//using ECDLPAlgorithm;

namespace ConsoleTest
{
    class Program
    { 
        public static string primesPath = @"..\..\..\FileUtility\primes1-10000.txt";
        //public static string primesPath = @"..\..\..\FileUtility\PrimeNumbers.txt";
        static void Main(string[] args)
        {
            //var rand = new BigIntegerRandom();

            //var curve = EllipticCurve.GenerateEllipticCurve(rand.NextPrime(2));
            //var P = curve.GetRandomAffinePoint().ToProjectivePoint();
            //var n = rand.Next(0, P.E.P);
            //var Q = n * P; 
            //var x = ECDLPAlgorithm.RhoPollard.SolveDLP(P,Q);
            //Console.WriteLine(n);
            //Console.WriteLine(x);


            //double[][] a = new double[3][];
            //a[0] = new double[] {4,2,-1,1};
            //a[1] = new double[] {5,3,-2,2};
            //a[2] = new double[] {3,2,-3,0};
            //GaussianElimination.ToTriangularForm(a);

            //for (int i = 0; i < a.Length; i++)
            //{
            //    for (int j = 0; j < a[i].Length; j++)
            //    {
            //        Console.Write(a[i][j] + "  ");
            //    }
            //    Console.WriteLine();
            //}
            //Console.WriteLine();

            BigInteger p = 173;
            BigInteger g = 2;
            BigInteger x = 165;
            BigInteger h = 123;

            var log = DLPAlgorithm.IndexCalculus.SolveDLP(g, h, p);
            Console.WriteLine(log);





            //var primeNumbers = FileUtility.ReadArrayFromFile(primesPath).Skip(2).ToArray<BigInteger>();

            //var primitiveRoots = BigIntegerExtension.GetPrimitiveRoots(primeNumbers).ToArray<BigInteger>();

            //BigIntegerRandom rand = new BigIntegerRandom();
            //for (int i = 0; i < primeNumbers.Length; i++)
            //{
            //    var p = primeNumbers[i];
            //    var g = primitiveRoots[i];
            //    var log = rand.Next(0, p);
            //    var h = BigInteger.ModPow(g, log, p);
            //    //Console.WriteLine("{0} = random log, {1} = {2}^{0} mod {3}", log, BigInteger.ModPow(g, log, p), g, p);
            //    var x = DLPAlgorithm.RhoPollard.SolveDLP(g, h, p);
            //    if (log != x) Console.WriteLine(g + " " + p);
            //    Console.WriteLine("Discrete logarithm x = {0}", x);
            //   Console.WriteLine();
            //    Thread.Sleep(1000);
            //}

           
         //   BigIntegerRandom rand = new BigIntegerRandom();
         //   var p = BigInteger.Parse("72057594037928017");
         //   var g = BigIntegerExtension.PrimitiveRoot(p);
         //   var log = rand.Next(0, p);
         //   var h = BigInteger.ModPow(g, log, p);
         //   Console.WriteLine("{0} = random log, {1} = {2}^{0} mod {3}", log, BigInteger.ModPow(g, log, p), g, p);
         //   var startTime = System.Diagnostics.Stopwatch.StartNew();
         //   var x = DLPAlgorithm.IndexCalculus.SolveDLP(g, h, p);
         //   startTime.Stop();
         //   var resultTime = startTime.Elapsed;

         //   // elapsedTime - строка, которая будет содержать значение затраченного времени
         //   string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:000}",
         //       resultTime.Hours,
         //       resultTime.Minutes,
         //       resultTime.Seconds,
         //       resultTime.Milliseconds);
         //  // if (log != x) Console.WriteLine(g + " " + p);
         //   Console.WriteLine("Discrete logarithm x = {0}", x);
         //   Console.WriteLine();
         ////   Thread.Sleep(1000);



            //var curve = EllipticCurve.GenerateEllipticCurve(239);
            //var point = curve.GetRandomAffinePoint();
            //var order = EllipticCurve.FindPointOrder(point);
            //Console.WriteLine(order);

            //var temp = new AffinePoint(point.X, point.Y, curve);
            //int i = 2;
            //while(temp != order)
            //{

            //}



            

            //int start = 10;
            //int finish = 10;
            //int count = 1;

            //TestECDLPAlgorithm.TestBabyStepGiantStep(start, finish, count);

            //TestDLPAlgorithm.TestIndexCalculus(start, finish, count);
            //Console.WriteLine("done");
            //Console.ReadLine();





            
            
            
            //Console.WriteLine("polladr done: " + DateTime.Now.ToLongDateString());

            //start = 39;
            //finish = 50;
            //count = 1;
            //TestDLPAlgorithm.TestBabyStepGiantStep(start, finish, count);
            //Console.WriteLine("shanks done: " + DateTime.Now.ToLongDateString());


            //start = 66;
            //finish = 66;
            //count = 1;
            //Test.GenerateGeneratorsInFiles(start, finish);
            //Test.GeneratehAndxInFiles(start, finish, count);
            //Console.WriteLine("66 bit generator done: " + DateTime.Now.ToLongDateString());

            //start = 68;
            //finish = 80;
            //count = 1;
            //Test.GenerateGeneratorsInFiles(start, finish);
            //Test.GeneratehAndxInFiles(start, finish, count);
            //Console.WriteLine("generators done: " + DateTime.Now.ToLongDateString());

           // Test.GeneratePrimesInFiles(start, finish, count);
           //Test.GenerateGeneratorsInFiles(start, finish);
           //Test.GeneratehAndxInFiles(start, finish, count);

            //Console.WriteLine("Generated data");

            
            //TestDLPAlgorithm.TestBabyStepGiantStep(start, finish, count);
            //TestDLPAlgorithm.TestIndexCalculus(start, finish, count);

            //Console.WriteLine("Done");
            //Console.ReadLine();

            //Test.GeneratePrimesInFiles(start, finish, count);
            //Test.GenerateGeneratorsInFiles(start, finish);

            //BigIntegerExtension.ListOfArraysOfPrimitiveRoots = new List<BigInteger[]>();
            //List<Task> taskList = new List<Task>();
            //for (int i = start; i <= finish; i++)
            //{
            //    BigInteger[] primes = FileUtility.ReadArrayFromFile(String.Format(@"..\..\..\TestUtility\primes{0}bits.txt", i));
            //    taskList.Add(new Task(() => BigIntegerExtension.GetPrimitiveRoots(primes)));

            //    //var startTime = System.Diagnostics.Stopwatch.StartNew();
            //   // var generators = BigIntegerExtension.GetPrimitiveRoots(primes);
            //   // var resultTime = startTime.Elapsed;
            //    //string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:000}",
            //    //    resultTime.Hours,
            //    //    resultTime.Minutes,
            //    //    resultTime.Seconds,
            //    //    resultTime.Milliseconds);
            //    //FileUtility.WriteArrayInFile(String.Format(@"..\..\..\TestUtility\generators{0}bits.txt", i), generators);
            //    //FileUtility.WriteStringInFile(String.Format(@"..\..\..\TestUtility\generatorsTime{0}bits.txt", i), elapsedTime);
            //}

            //foreach (var t in taskList) t.Start();
            //Task.WaitAll(taskList.ToArray());
            //int j = start;
            //foreach (var array in listOfArraysOfPrimitiveRoots)
            //{
            //    FileUtility.WriteArrayInFile(String.Format(@"..\..\..\TestUtility\generators{0}bits.txt", j), array);
            //    j++;
            //}

            


            //BigIntegerRandom rand = new BigIntegerRandom();
            //BigInteger p = BigInteger.Parse("179424691");
            //BigInteger g = BigIntegerExtension.PrimitiveRoot(p);
            //var log = rand.Next(0, p);
            //var h = BigInteger.ModPow(g, log, p);
            ////Console.WriteLine("{0} = random log, {1} = {2}^{0} mod {3}", log, BigInteger.ModPow(g, log, p), g, p);
            //var x = DLPAlgorithm.RhoPollard.SolveDLP(g, h, p);
            //if (log != x) Console.WriteLine(g + " " + p);
            //Console.WriteLine("Discrete logarithm x = {0}", x);
            //Console.WriteLine();
            //Thread.Sleep(1000);
            
            

            //int i = 0;
            //foreach (var a in primitiveRoots)
            //    Console.WriteLine(BigIntegerExtension.FindElementOrder(a, primeNumbers[i++]));
            //string primitiveRootsFilePath = @"..\..\..\FileUtility\PrimitiveRoots.txt";
            //var temp = FileUtility.ReadArrayFromFile(primitiveRootsFilePath, " ");

        


            //for (int i = 0; i < primeNumbers.Length;i++)
            //{
            //    var p = primeNumbers[i];
            //    var g = primitiveRoots[i];
            //    var order = BigIntegerExtension.FindElementOrder(g, p);
            //    if (order != p - 1) Console.WriteLine(g);
            //}
        }
    }
}
