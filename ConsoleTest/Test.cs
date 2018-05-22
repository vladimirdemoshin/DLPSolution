using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Numerics;
using Utility;

namespace ConsoleTest
{
    public static class Test
    {

        public static void GeneratePrimesInFiles(int startBitsLength, int finishBitsLength, int rangeCount)
        {
            BigIntegerRandom rand = new BigIntegerRandom();
            List<Task> taskList = new List<Task>();
            for (int i = startBitsLength; i <= finishBitsLength; i++)
            {
                string primesPath = String.Format(@"..\..\..\TestUtility\primes{0}bits.txt", i);
                var tempI = i;
                taskList.Add(new Task(() => PrimeThreadFunc(primesPath, tempI, rangeCount)));
            }
            foreach (var t in taskList) t.Start();
            Task.WaitAll(taskList.ToArray());
        }

        public static void PrimeThreadFunc(string filePath, int lengthBits, int count)
        {
            var primes = GeneratePrimes(lengthBits, count);
            FileUtility.WriteArrayInFile(filePath, primes);
        }

        private static BigInteger[] GeneratePrimes(int lengthBits, int count)
        {
            BigInteger start = BigInteger.Pow(2, lengthBits - 1);
            BigInteger finish = BigInteger.Pow(2, lengthBits) - 1;
            BigInteger range = finish - start + 1;
            var list = new List<BigInteger>();
            var checkedNumbers = new List<BigInteger>();
            BigIntegerRandom rand = new BigIntegerRandom();
            while (checkedNumbers.Count < range && list.Count < count)
            {
                var shift = rand.Next(0, range);
                var temp = start + shift;
                if (!checkedNumbers.Contains(temp))
                {
                    if (BigIntegerPrimeTest.MillerRabinTest(temp) && temp != 2)
                        list.Add(temp);
                    checkedNumbers.Add(temp);
                }
            }
            return list.ToArray<BigInteger>();
            //listOfArraysOfPrimes.Add(list.ToArray<BigInteger>());
        }

        public static void GenerateGeneratorsInFiles(int startBitsLength, int finishBitsLength)
        {
            BigIntegerRandom rand = new BigIntegerRandom();
            List<Task> taskList = new List<Task>();
            for (int i = startBitsLength; i <= finishBitsLength; i++)
            {
                var tempI = i;
                BigInteger[] primes = FileUtility.ReadArrayFromFile(String.Format(@"..\..\..\TestUtility\primes{0}bits.txt", tempI));
                string generatorsPath = String.Format(@"..\..\..\TestUtility\generators{0}bits.txt", tempI);
                taskList.Add(new Task(() => GenerateGenerators(generatorsPath, primes)));
            }
            foreach (var t in taskList) t.Start();
            Task.WaitAll(taskList.ToArray());
        }

        private static void GenerateGenerators(string filePath, BigInteger[] primes)
        {
            var generators = BigIntegerExtension.GetPrimitiveRoots(primes);
            FileUtility.WriteArrayInFile(filePath, generators);
        }

        public static void GeneratehAndxInFiles(int startBitsLength, int finishBitsLength, int rangeCount)
        {
            BigIntegerRandom rand = new BigIntegerRandom();
            for (int i = startBitsLength; i <= finishBitsLength; i++)
            {
                string primesPath = String.Format(@"..\..\..\TestUtility\primes{0}bits.txt", i);
                BigInteger[] primes = FileUtility.ReadArrayFromFile(primesPath);
                string generatorsPath = String.Format(@"..\..\..\TestUtility\generators{0}bits.txt", i);
                BigInteger[] generators = FileUtility.ReadArrayFromFile(generatorsPath);
                var listH = new List<BigInteger>();
                var listX = new List<BigInteger>();
                for(int j=0;j<primes.Length && j<rangeCount;j++)
                {
                    var p = primes[j];
                    var g = generators[j];
                    var x = rand.Next(0, p-1);
                    var h = BigInteger.ModPow(g,x,p);
                    listH.Add(h);
                    listX.Add(x);
                }
                string hPath = String.Format(@"..\..\..\TestUtility\h{0}bits.txt", i);
                FileUtility.WriteArrayInFile(hPath, listH.ToArray<BigInteger>());
                string xPath = String.Format(@"..\..\..\TestUtility\x{0}bits.txt", i);
                FileUtility.WriteArrayInFile(xPath, listX.ToArray<BigInteger>());
            }
        }

      
    }
}


//public static List<BigInteger[]> listOfArraysOfPrimes;
//public static List<BigInteger[]> listOfArraysOfGenerators;



//var countOfNumbers = BigInteger.Pow(2, i*8) - 10;
//                for(int j = 0;j<rangeCount;j++)
//                {   
//                    BigInteger prime;
//                    bool isNotUnique;
//                    do
//                    {
//                        prime = rand.NextPrime(i);
//                        isNotUnique = listOfPrimes.Contains(prime);
//                    }
//                    while (isNotUnique && listOfPrimes.Count < countOfNumbers);
//                    listOfPrimes.Add(prime);
//                    if (listOfPrimes.Count >= countOfNumbers)
//                        break;
//                }
//                FileUtility.WriteArrayInFile(primesPath, listOfPrimes.ToArray<BigInteger>());


//public static BigInteger[] GeneratePrimes(int lengthBits, int count)
//{
//    BigInteger start = BigInteger.Pow(2, lengthBits - 1);
//    BigInteger finish = BigInteger.Pow(2, lengthBits) - 1;
//    BigInteger range = finish - start + 1;
//    var list = new List<BigInteger>();
//    var checkedNumbers = new List<BigInteger>();
//    BigIntegerRandom rand = new BigIntegerRandom();
//    while(checkedNumbers.Count < range && list.Count < count)
//    {
//        var shift = rand.Next(0, range);
//        var temp = start + shift;
//        if(!checkedNumbers.Contains(temp))
//        {
//            if (BigIntegerPrimeTest.MillerRabinTest(temp) && temp != 2)
//                list.Add(temp);
//            checkedNumbers.Add(temp);
//        }
//    }
//    return list.ToArray<BigInteger>();
//}

//public static void GeneratePrimesInFiles(int startBytesSize, int lastBytesSize, int rangeCount)
//{
//    BigIntegerRandom rand = new BigIntegerRandom();
//    for(int i=startBytesSize;i <= lastBytesSize; i++)
//    {
//        string primesPath = String.Format(@"..\..\..\FileUtility\primes{1}bytes.txt",rangeCount, i);
//        var listOfPrimes = GeneratePrimes(i, rangeCount);
//        FileUtility.WriteArrayInFile(primesPath, listOfPrimes.ToArray<BigInteger>());
//    }
//}


//public static BigInteger[] GeneratePrimes(int sizeBytes, int count)
//{
//    BigInteger start = BigInteger.Pow(2, 8 * (sizeBytes - 1));
//    BigInteger finish = BigInteger.Pow(2, 8 * sizeBytes) - 1;
//    var list = new List<BigInteger>();
//    for (BigInteger j = start; j <= finish; j++)
//    {
//        if (BigIntegerPrimeTest.MillerRabinTest(j) && j != 2)
//            list.Add(j);
//        if (list.Count >= count)
//            break;

//    }
//    return list.ToArray<BigInteger>();
//}



//public static void GeneratePrimesInFiles(int startBitsLength, int finishBitsLength, int rangeCount)
//{
//    BigIntegerRandom rand = new BigIntegerRandom();
//    List<Task> taskList = new List<Task>();
//    listOfArraysOfPrimes = new List<BigInteger[]>();
//    for (int i = startBitsLength; i <= finishBitsLength; i++)
//    {
//        string primesPath = String.Format(@"..\..\..\TestUtility\primes{1}bits.txt", rangeCount, i);
//        var tempI = i;
//        taskList.Add(new Task(() => GeneratePrimes(tempI, rangeCount)));
//        //var listOfPrimes = GeneratePrimes(i, rangeCount);
//        //FileUtility.WriteArrayInFile(primesPath, listOfPrimes.ToArray<BigInteger>());
//    }
//    foreach (var t in taskList) t.Start();
//    Task.WaitAll(taskList.ToArray());
//    int j = startBitsLength;
//    foreach(var array in listOfArraysOfPrimes)
//    {
//        string primesPath = String.Format(@"..\..\..\TestUtility\primes{0}bits.txt", j);
//        FileUtility.WriteArrayInFile(primesPath, array);
//        j++;
//    }
//}

//private static void GeneratePrimes(int lengthBits, int count)
//{
//    BigInteger start = BigInteger.Pow(2, lengthBits - 1);
//    BigInteger finish = BigInteger.Pow(2, lengthBits) - 1;
//    BigInteger range = finish - start + 1;
//    var list = new List<BigInteger>();
//    var checkedNumbers = new List<BigInteger>();
//    BigIntegerRandom rand = new BigIntegerRandom();
//    while (checkedNumbers.Count < range && list.Count < count)
//    {
//        var shift = rand.Next(0, range);
//        var temp = start + shift;
//        if (!checkedNumbers.Contains(temp))
//        {
//            if (BigIntegerPrimeTest.MillerRabinTest(temp) && temp != 2)
//                list.Add(temp);
//            checkedNumbers.Add(temp);
//        }
//    }
//    listOfArraysOfPrimes.Add(list.ToArray<BigInteger>());
//}