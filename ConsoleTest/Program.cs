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
            var primeNumbers = FileUtility.ReadArrayFromFile(primesPath).Skip(2).ToArray<BigInteger>();

            var primitiveRoots = BigIntegerExtension.GetPrimitiveRoots(primeNumbers).ToArray<BigInteger>();

            BigIntegerRandom rand = new BigIntegerRandom();
            for (int i = 0; i < primeNumbers.Length; i++)
            {
                var p = primeNumbers[i];
                var g = primitiveRoots[i];
                var log = rand.Next(0, p);
                var h = BigInteger.ModPow(g, log, p);
                //Console.WriteLine("{0} = random log, {1} = {2}^{0} mod {3}", log, BigInteger.ModPow(g, log, p), g, p);
                var x = DLPAlgorithm.RhoPollard.SolveDLP(g, h, p);
                if (log != x) Console.WriteLine(g + " " + p);
                Console.WriteLine("Discrete logarithm x = {0}", x);
               Console.WriteLine();
                Thread.Sleep(1000);
            }
           
         

            

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
