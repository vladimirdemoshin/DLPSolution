using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Numerics;
using Utility;
using EllipticCurveUtility;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //for(int k=-5;k<=5;k++)
            //{
            //    BigInteger m = k;
            //    //BigInteger m = -12;
            //    BigInteger mTemp = m;
            //    bool[] mBits = new bool[m.ToByteArray().Length * 8];
            //    for (int i = 0; i < mBits.Length; i++, mTemp >>= 1)
            //        if ((mTemp & 1) == 1) mBits[i] = true;
            //    for (int i = mBits.Length - 1; i >= 0; i--)
            //    {
            //        Console.Write(mBits[i] ? 1 : 0);
            //    }
            //    Console.WriteLine();
            //}
            //BigInteger m = k;

            //BigInteger m = 32769;
            //BigInteger mTemp = m;
            //bool[] mBits = new bool[m.ToByteArray().Length * 8 - 1]; //делаем -1, т.к. последний бит отвечает за знак, мы имеем дело с положительными числами
            //for (int i = 0; i < mBits.Length; i++, mTemp >>= 1)
            //    if ((mTemp & 1) == 1) mBits[i] = true;
            //for (int i = mBits.Length - 1; i >= 0; i--)
            //{
            //    Console.Write(mBits[i] ? 1 : 0);
            //}
            //Console.WriteLine();


            //EllipticCurve curve = new EllipticCurve(0,-71873653,0,78052081);
            //Point P = new Point(37955809, 13776099, 70829467);
            //P.E = curve;
            //Point Q = new Point(55092553, 51780512, 71316093);
            //Q.E = curve;
            //Console.WriteLine(P + Q);

            //EllipticCurve curve = new EllipticCurve(0, -34627270, 0, 78052081);
            //Point P = new Point(63968123, 10374632, 15884669);
            //P.E = curve;
            //Point Q = new Point(75640139, 53440531, 28341508);
            //Q.E = curve;
            //Console.WriteLine(P + Q);

            //BigInteger P = 14;
            //var array = P.GetBitArray();
            //for (int i = 0; i < array.Length; i++)
            //    Console.Write(array[i]?1:0);
                //foreach (var bit in array) Console.Write(bit);

            EllipticCurve curve = new EllipticCurve(2, 1, 41);
            AffinePoint P = new AffinePoint(0, 1);
            P.E = curve;
            //AffinePoint Q = new AffinePoint(3, 5);
            //Q.E = curve;
            for(int n = 1; n <=7 ;n++)
                Console.WriteLine(n * P);
            //BigInteger n = 3;
            //Console.WriteLine(n * P);
        }
    }
}
