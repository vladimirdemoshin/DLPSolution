using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Numerics;
using System.Security.Cryptography;

namespace Utility
{
    public class BigIntegerRandom
    {
        private static BigInteger randomizer = 1;
        public BigInteger Next(int sizeBytes)
        {
            RandomNumberGenerator randNumberGenerator = RandomNumberGenerator.Create();
            byte[] bytes = new byte[sizeBytes];
            randNumberGenerator.GetBytes(bytes);
            BigInteger result = new BigInteger(bytes);
            return BigInteger.Abs(result);
        }

        //не учитывается minValue при размере больше 4 байтов
        public BigInteger Next(BigInteger minValue, BigInteger maxValue)
        {
            if (minValue >= maxValue - 1) return minValue;
            randomizer++;
            Random rand = new Random((int)DateTime.Now.Ticks + (int)randomizer);
            int sizeBytesMaxValue = maxValue.ToByteArray().Length;
            if (sizeBytesMaxValue <= 4) return rand.Next((int)minValue, (int)maxValue);
            int sizeBytes = rand.Next(1, sizeBytesMaxValue + 1);
            BigInteger result = Next(sizeBytes);
            if (result >= maxValue)
            {
                sizeBytes = rand.Next(1, sizeBytesMaxValue);
                result = Next(sizeBytes);
            }
            return result;
        }

        public BigInteger NextPrime(int sizeBytes)
        {
            BigInteger testNum = Next(sizeBytes);
            if (testNum % 2 == 0) testNum--;
            BigInteger current = testNum;
            bool failed = false;
            while (!BigIntegerPrimeTest.MillerRabinTest(current))
            {
                if (current.ToByteArray().Length == sizeBytes)
                    current -= 2;
                else
                {
                    failed = true;
                    break;
                }
            }
            if (!failed) return current;
            else
            {
                current = testNum + 2;
                while (!BigIntegerPrimeTest.MillerRabinTest(current))
                    current += 2;
            }
            return current;
        }
    }
}
