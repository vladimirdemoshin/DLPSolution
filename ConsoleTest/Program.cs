using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Numerics;
using Utility;
using EllipticCurveUtility;
using DLPAlgorithm;
using ECDLPAlgorithm;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //var list = new SortedDictionary<BigInteger, BigInteger>();
            //for (int i = 1; i < 16; i++)
            //{
            //    BigInteger answer = DLPAlgorithm.BabyStepGiantStep.SolveDLP(3, i, 17);
               
            //    list.Add(answer, i);
            //    //Console.WriteLine("h= {0} , x = {1}", i, answer);
            //    //Console.WriteLine();
            //}
            //foreach (var item in list)
            //    Console.WriteLine(item.Key + " " + item.Value);

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

            //var a = BigIntegerExtension.Sqrt(49);
            //Console.WriteLine(a);

            //for (int i = 1; i < 16; i++)
            //{
            //    //Console.WriteLine("h={0}", i);
            //    BigInteger answer = DLPAlgorithm.RhoPollard.SolveDLP(3, i, 17);
            //    Console.WriteLine("h= {0} , x = {1}", i, answer);
            //    Console.WriteLine();
            //}

            //BigInteger u, v;
            //BigInteger a = 4;
            //a.ExtendedGcd(8, out u, out v);
            //Console.WriteLine("{0} , {1}",u,v);


            //Console.WriteLine("h={0}", 4);
            //BigInteger answer = DLPAlgorithm.RhoPollard.SolveDLP(3, 4,17);
            //Console.WriteLine(answer);
            //Console.WriteLine();

            EllipticCurve curve = new EllipticCurve(130, 565, 719);
            AffinePoint P = new AffinePoint(107, 443, curve);
            AffinePoint Q = new AffinePoint(608, 427, curve);
            BigInteger answer = ECDLPAlgorithm.BabyStepGiantStep.SolveDLP(P, Q);
            Console.WriteLine(answer);

            
        }
    }
}
