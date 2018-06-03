using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Numerics;
using System.IO;

namespace Utility
{
    public static class FileUtility
    {
        public static string PrimeNumbersFilePath = @"C:\Utility\Primes\primes1-10000000.txt";
        public static IEnumerable<BigInteger> GetFactorBaseFromFile(int factorBaseSize)
        {
            var reader = new StreamReader(PrimeNumbersFilePath);
            int i = 0;
            string prime = "";
            while(i < factorBaseSize && reader.Peek() != -1)
            {
                char symbol = (char)reader.Read();
                if (Char.IsWhiteSpace(symbol))
                {
                    if (prime != "")
                    {
                        yield return BigInteger.Parse(prime);
                        prime = "";
                        i++;
                    }
                    continue;
                }
                prime += symbol;
            }
        }


        public static void WriteArrayInFile(string filePath, BigInteger[] array)
        {
            var temp = Converter.ToString(array);
            using (var sw = new StreamWriter(filePath, false, System.Text.Encoding.UTF8))
            {
                sw.WriteLine(temp);
            }
        }

        public static void WriteStringInFile(string filePath, string text)
        {
            using (var sw = new StreamWriter(filePath, false, System.Text.Encoding.UTF8))
            {
                sw.WriteLine(text);
            }
        }

        public static BigInteger[] ReadArrayFromFile(string filePath)
        {
            var temp = new List<BigInteger>();
            using (var reader = new StreamReader(filePath))
            {
                string prime = "";
                while (reader.Peek() != -1)
                {
                    char symbol = (char)reader.Read();
                    if (Char.IsWhiteSpace(symbol))
                    {
                        if (prime != "")
                        {
                            temp.Add(BigInteger.Parse(prime));
                            prime = "";
                        }
                        continue;
                    }
                    prime += symbol;
                }
                if(prime!="")
                    temp.Add(BigInteger.Parse(prime));
            }
            return temp.ToArray<BigInteger>();
        }


    }
}
