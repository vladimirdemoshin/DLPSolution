using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Numerics;
using System.Threading;
using System.Collections;
using System.IO;


namespace Utility
{
    public static class BigIntegerExtension
    {
        public static RationalNumber[] ToRationalNumberArray(BigInteger[] arr)
        {
            RationalNumber[] array = new RationalNumber[arr.Length];
            for (int i = 0; i < arr.Length; i++)
                array[i] = new RationalNumber(arr[i]);
            return array;
        }

        public static RationalNumber[][] ToTwoDimensionalRationalNumberArray(BigInteger[][] arr)
        {
            RationalNumber[][] array = new RationalNumber[arr.Length][];
            for (int i = 0; i < arr.Length; i++)
                array[i] = BigIntegerExtension.ToRationalNumberArray(arr[i]);
            return array;
        }

        public static BigInteger[] ToBigIntegerArray(RationalNumber[] arr, BigInteger MOD)
        {
            BigInteger[] array = new BigInteger[arr.Length];
            for (int i = 0; i < arr.Length; i++)
                array[i] = arr[i].ToModBigInteger(MOD);
            return array;
        }
       
        //a * x = b (mod MOD) , returns x
        public static BigInteger SolveModLinearEquatation(BigInteger a, BigInteger b, BigInteger order, BigInteger g, BigInteger h, BigInteger p)
        {
            var gcd = BigInteger.GreatestCommonDivisor(a, order);
            var reducedOrder = order / gcd;
            var x0 = (a / gcd).ModInverse(reducedOrder);
            x0 = x0 * (b / gcd);
            x0 = x0.ModPositive(reducedOrder);
            for (BigInteger m = 0; m < gcd; m++)
            {
                var x = x0 + m * reducedOrder;
                if (BigInteger.ModPow(g, x, p) == h)
                    return x;
            }
            return -1;
        }

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

        public static BigInteger[] GetFactorBase(int factorBaseSize)
        {
            return FileUtility.GetFactorBaseFromFile(factorBaseSize).ToArray();
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

        public static int JacobiSymbol(BigInteger a, BigInteger m)
        {
            a = a % m;
            var t = 1;
            while (a != 0)
            {
                while (a % 2 == 0)
                {
                    a = a / 2;
                    var tmp = m % 8;
                    if (tmp >= 3 && tmp <= 5)
                        t = -t;
                }
                BigInteger swap = a;
                a = m;
                m = swap;
                if (a % 4 == 3 && m % 4 == 3) t = -t;
                a = a % m;
            }
            if (m == 1) return t;
            return 0;
        }
    }
}












 //public static void Shuffle(ref BigInteger[] array)
 //       {
 //           FisherYatesShuffle(ref array);
 //       }
 //       private static void FisherYatesShuffle(ref BigInteger[] array)
 //       {
 //           Random rand = new Random();
 //           for(int i = array.Length - 1; i >= 1; i--)
 //           {
 //               int j = rand.Next(i + 1);
 //               var temp = array[j];
 //               array[j] = array[i];
 //               array[i] = temp;
 //           }
 //       }