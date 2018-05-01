using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Numerics;
using System.Threading;
using System.Collections;

namespace Utility
{
    public static class BigIntegerExtension
    {
        /// <summary>
        /// Find inverse element for num modulus mod. Condition to work properly: gcd(num, mod) == 1 
        /// </summary>
        /// <param name="num"></param>
        /// <param name="mod"></param>
        /// <returns></returns>
        public static BigInteger ModInverse(this BigInteger num, BigInteger mod)
        {
            BigInteger x, y;
            BigInteger gcd = num.ExtendedGcd(mod, out x, out y);
            if (x < 0) return x + mod;
            return x;
        }

        /// <summary>
        /// num*x+mod*y=НОД(num,mod)
        /// </summary>
        /// <param name="num"></param>
        /// <param name="mod"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static BigInteger ExtendedGcd(this BigInteger num, BigInteger mod, out BigInteger x, out BigInteger y) // 
        {
            if (num == 0) { x = 0; y = 1; return mod; }
            BigInteger x1, y1;
            BigInteger d = ExtendedGcd(mod % num, num, out x1, out y1);
            x = y1 - (mod / num) * x1;
            y = x1;
            return d;
        }

        public static BigInteger ModPositive(this BigInteger num, BigInteger mod)
        {
            num = num % mod;
            while (num < 0) num += mod;
            return num;
        }

        //i - ая степень двойки - i индекс бита в массиве
        public static BitArray GetBitArray(this BigInteger num)
        {
            if (num == 0) return new BitArray(new bool[]{false});
            if (num < 0) num = -num;
            var bits = new List<bool>();
            while(num!=0)
            {
                bits.Add(num%2!=0);
                num /= 2;
            }
            return new BitArray(bits.ToArray<bool>());
        }

        //разобраться в этой функции
        public static BigInteger Sqrt(BigInteger N)
        {
            if (N <= (BigInteger)double.MaxValue) return (BigInteger)Math.Sqrt((double)N);
            /*++
             *  Using Newton Raphson method we calculate the
             *  square root (N/g + g)/2
             */
            BigInteger rootN = N;
            int bitLength = 1; // There is a bug in finding bit length hence we start with 1 not 0
            while (rootN / 2 != 0)
            {
                rootN /= 2;
                bitLength++;
            }
            bitLength = (bitLength + 1) / 2;
            rootN = N >> bitLength;

            BigInteger lastRoot = BigInteger.Zero;
            do
            {
                lastRoot = rootN;
                rootN = (BigInteger.Divide(N, rootN) + rootN) >> 1;
            }
            while (!((rootN ^ lastRoot).ToString() == "0"));
            return rootN;
        } // SqRtN

        //понять что делать в случаях, когда нод(num, mod) != 1
        //public static BigInteger ModInverse(this BigInteger num, BigInteger mod)
        //{
        //    BigInteger gcd = BigInteger.GreatestCommonDivisor(num, mod);
        //    BigInteger x, y;
        //    BigInteger d = ExtendedGcd(num / gcd, mod / gcd, out x, out y);
        //    x = x / gcd;
        //    if (x < 0) return x + mod;   //вот тут не помню, надо уточнить
        //    return x;
        //}
    }
}
