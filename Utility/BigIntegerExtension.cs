using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Numerics;

namespace Utility
{
    public static class BigIntegerExtension
    {
        /// <summary>
        /// Find inverse element for num modulus mod. condition to work properly: gcd(num, mod) == 1 
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

        public static BigInteger ExtendedGcd(this BigInteger num, BigInteger mod, out BigInteger x, out BigInteger y) // a*x+b*y=НОД(a,b)
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
