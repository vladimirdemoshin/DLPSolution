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
        public static BigInteger ModInverse(this BigInteger num, BigInteger mod)
        {
            BigInteger x, y;
            num.ExtendedGcd(mod, out x, out y);
            while (x < 0) x += mod;
            return x;
        }

        public static BigInteger ExtendedGcd(this BigInteger num, BigInteger mod, out BigInteger x, out BigInteger y)
        {
            if (num == 0) { x = 0; y = 1; return mod; }
            BigInteger x1, y1;
            var d = ExtendedGcd(mod % num, num, out x1, out y1);
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

        public static BigInteger[] GetFactorBase(int factorBaseSize)
        {
            return FileUtility.GetFactorBaseFromFile(factorBaseSize).ToArray();
        }

        ////переборная функция, просто для тестирования
        //public static BigInteger PrimitiveRoot(BigInteger p)
        //{
        //    var order = p-1;
        //    for(BigInteger g = 2; g < p; g++)
        //    {
        //        if(BigInteger.ModPow(g, order, p) == 1)
        //        {
        //            bool flag = true;
        //            for (BigInteger j = 1; j < order; j++)
        //                if (BigInteger.ModPow(g, j, p) == 1)
        //                {
        //                    flag = false;
        //                    break;
        //                }
        //            if (flag)
        //                return g;
        //        }               
        //    }
        //    return -1;
        //}

        //public static BigInteger PrimitiveRoot(BigInteger p)
        //{
        //    List<int> fact = new List<int>();
        //    BigInteger phi = p - 1, n = phi;
        //    for (int i = 2; i * i <= n; i++)
        //        if (n % i == 0)
        //        {
        //            fact.Add(i);
        //            while (n % i == 0) n /= i;
        //        }
        //    if (n > 1) fact.Add((int)n);
        //    for (BigInteger res = 2; res <= p; res++)
        //    {
        //        bool ok = true;
        //        for (int i = 0; i < fact.Count && ok; i++) ok &= powMod(res, phi / fact[i], p) != 1;
        //        if (ok) return res;
        //    }
        //    return -1;
        //}

        //Function to find smallest primitive root of n
        public static BigInteger PrimitiveRoot(BigInteger p)
        {
            var order = p - 1;
            var primeFactors = GetPrimeFactors(order);
            for (int g = 2; g < p; g++)
            {
                bool flag = false;
                foreach (var prime in primeFactors)
                {
                    if (BigInteger.ModPow(g, order / prime, p) == 1)
                    {
                        flag = true;
                        break;
                    }
                }
                if (flag == false)
                    return g;
            }
            return -1;
        }

        public static BigInteger[] GetPrimitiveRoots(BigInteger[] primeNumbers)
        {
            var primitiveRoots = new List<BigInteger>();
            foreach (var prime in primeNumbers)
                primitiveRoots.Add(PrimitiveRoot(prime));
            return primitiveRoots.ToArray<BigInteger>();
        }

        public static List<BigInteger> GetPrimeFactors(BigInteger p)
        {
            var list = new List<BigInteger>();
            if(p%2==0)
            {
                list.Add(2);
                while (p % 2 == 0)
                    p = p / 2;
            }
            for (int i = 3; i <= BigIntegerExtension.Sqrt(p)+1; i = i + 2)
            {
                if (p % i == 0)
                {
                    list.Add(i);
                    while(p%i == 0)
                    {
                        p = p / i;

                    }
                    
                }
            }
            if (p > 2) list.Add(p);
            return list;
        }

       

        private static BigInteger powMod(BigInteger a, BigInteger b, BigInteger mod)
        {
            BigInteger res = 1;
            while (b != 0)
            {
                if ((b & 1) == 1) { res = res * a % mod; b--; }
                else { a = a * a % mod; b = b >> 1; }
            }
            return res;
        }

        ////разобраться в этой функции
        //public static BigInteger Sqrt(BigInteger N)
        //{
        //    if (N <= (BigInteger)double.MaxValue) return (BigInteger)Math.Sqrt((double)N);
        //    /*++
        //     *  Using Newton Raphson method we calculate the
        //     *  square root (N/g + g)/2
        //     */
        //    BigInteger rootN = N;
        //    int bitLength = 1; // There is a bug in finding bit length hence we start with 1 not 0
        //    while (rootN / 2 != 0)
        //    {
        //        rootN /= 2;
        //        bitLength++;
        //    }
        //    bitLength = (bitLength + 1) / 2;
        //    rootN = N >> bitLength;

        //    BigInteger lastRoot = BigInteger.Zero;
        //    do
        //    {
        //        lastRoot = rootN;
        //        rootN = (BigInteger.Divide(N, rootN) + rootN) >> 1;
        //    }
        //    while (!((rootN ^ lastRoot).ToString() == "0"));
        //    return rootN;
        //} // SqRtN


        public static BigInteger RandomBigInteger(BigInteger N, Random random)
        {
            byte[] bytes = N.ToByteArray();
            BigInteger R;
            random.NextBytes(bytes);
            bytes[bytes.Length - 1] &= (byte)0x7F;
            R = new BigInteger(bytes);
            if (R > N - 1 && N != 0)
            {
                R %= N;
            }
            if (R < 0)
            {
                R += N;
            }
            return R;
        }

        //Ро- метод Полларда
        public static List<BigInteger> PollardsAlg(BigInteger n)
        {
            var result = new List<BigInteger>();
            if (BigIntegerPrimeTest.MillerRabinTest(n))
            {
                return result;
            }
            BigInteger N = n;
            BigInteger i = 1;
            BigInteger nextiToSave = 1;
            Random rand = new Random();
            BigInteger x = RandomBigInteger(n, rand);
            BigInteger y = 1, lastx = 1;
            BigInteger res = 0;
            while (N % 2 == 0)
            {
                if (!result.Contains(2))
                    result.Add(2);
                N /= 2;
            }
            while (!BigIntegerPrimeTest.MillerRabinTest(N))
            {
                BigInteger factor;
                BigInteger delta = BigInteger.Abs(x - y);
                if (delta == 0)
                {
                    x = RandomBigInteger(n, rand);
                    i = 0;
                    nextiToSave = 1;
                    y = 1;
                    lastx = 1;
                    continue;
                }
                BigInteger gcd = BigInteger.GreatestCommonDivisor(N, delta);
                lastx = x;
                x = (x * x - 1) % n;
                if (i == nextiToSave)
                {
                    nextiToSave *= 2;
                    y = lastx;
                }
                i++;

                if (gcd > 1)
                {
                    factor = gcd;
                    if (!BigIntegerPrimeTest.MillerRabinTest(factor))
                    {
                        var factorFactors = PollardsAlg(factor);
                        foreach (var item in factorFactors)
                        {
                            result.Add(item);
                        }
                    }
                    else
                    {
                        if (factor > 1)
                            result.Add(factor);
                    }
                    N /= factor;
                }
            }
            if (N > 1 && !result.Contains(N))
                result.Add(N);
            result.Sort();
            return result;
        }

        //азата
        public static BigInteger Sqrt(this BigInteger n)
        {
            if (n == 0) return 0;
            if (n > 0)
            {
                int bitLength = Convert.ToInt32(Math.Ceiling(BigInteger.Log(n, 2)));
                BigInteger root = BigInteger.One << (bitLength / 2);

                while (!isSqrt(n, root))
                {
                    root += n / root;
                    root /= 2;
                }

                return root;
            }
            return -1;
        }

        private static Boolean isSqrt(BigInteger n, BigInteger root)
        {
            BigInteger lowerBound = root * root;
            BigInteger upperBound = (root + 1) * (root + 1);

            return (n >= lowerBound && n < upperBound);
        }


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

        //i - ая степень двойки - i индекс бита в массиве
        public static BitArray GetBitArray(this BigInteger num)
        {
            if (num == 0) return new BitArray(new bool[] { false });
            if (num < 0) num = -num;
            var bits = new List<bool>();
            while (num != 0)
            {
                bits.Add(num % 2 != 0);
                num /= 2;
            }
            return new BitArray(bits.ToArray<bool>());
        }


        public static BigInteger FindElementOrder(BigInteger g, BigInteger p)
        {
            BigInteger count = 0;
            for (int i = 1; i < p; i++)
            {
                count++;
                if (BigInteger.ModPow(g, i, p) == 1)
                    return count;
            }
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