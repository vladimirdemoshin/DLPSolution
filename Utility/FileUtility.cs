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
        public static string PrimeNumbersFilePath = @"..\..\..\FileUtility\PrimeNumbers.txt";
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
    }
}
