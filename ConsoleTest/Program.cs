using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Numerics;
using Utility;
using EllipticCurveUtility;
using DLPAlgorithm;
//using ECDLPAlgorithm;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //var p = 37;
            //var order = p - 1;
            //var dB = 3;
            //var dA = 6;
            //var g = 2;
            //var gcd = BigInteger.GreatestCommonDivisor(3, 36);
            //var reducedOrder = order / gcd;
            //var x0 = (dB / gcd).ModInverse(reducedOrder);
            //x0 = x0 * (dA / gcd);
            //x0 = x0.ModPositive(reducedOrder);
            //for (BigInteger m = 0; m < gcd; m++)
            //{
            //    var x = x0 + m * reducedOrder;
            //    if (BigInteger.ModPow(g, x, p) == 3)
            //        Console.WriteLine(x);
            //}




            var answer = IndexCalculus.SolveDLP(2, 13, 37);
            Console.WriteLine(answer);
            //var list = new List<BigInteger>();
            //for (int i = 1; i < 36; i++)
            //    list.Add(BigInteger.ModPow(2, i, 37));
            //list.Sort();
            //foreach (var a in list) Console.Write(a+" ");

            
            //Console.WriteLine(BigInteger.ModPow(2, answer, 37));

            //var coefficients = new List<BigInteger[]>();
            //var a = new BigInteger[4]{1,2,3,4};
            //coefficients.Add(a);
            //a = new BigInteger[4]{2,4,6,8};
            //coefficients.Add(a);
            ////a = new BigInteger[3] { 5, 5, 6 };
            ////coefficients.Add(a);
            //var c = GaussianElimination.IsLinearIndependent(coefficients.ToArray());
            //Console.WriteLine(c);
            
            //EllipticCurve curve = new EllipticCurve(130, 565, 719);
            //ProjectivePoint P = new ProjectivePoint(107, 443, 1, curve);
            //ProjectivePoint Q = curve.GetRandomAffinePoint();//new ProjectivePoint(608, 427, 1, curve);
            //BigInteger answer = ECDLPAlgorithm.BabyStepGiantStep.SolveDLP(P,Q);
            //Console.WriteLine(answer);

            //BigInteger answer = DLPAlgorithm.RhoPollard.SolveDLP(10, 64, 107);
            ////BigInteger answer = DLPAlgorithm.RhoPollard.SolveDLP(2, 5, 29);
            //Console.WriteLine(answer);

           
            //var list = new SortedDictionary<BigInteger, BigInteger>();
            //for (int i = 1; i < 16; i++)
            //{
            //    BigInteger answer = DLPAlgorithm.RhoPollard.SolveDLP(3, i, 17);

            //  //  list.Add(answer, i);
            //    Console.WriteLine("h= {0} , x = {1}", i, answer);
            //    Console.WriteLine();
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

            //EllipticCurve curve = new EllipticCurve(130, 565, 719);
            //AffinePoint P = new AffinePoint(107, 443, curve);
            //AffinePoint Q = new AffinePoint(608, 427, curve);
            //BigInteger answer = ECDLPAlgorithm.BabyStepGiantStep.SolveDLP(P, Q);
            //Console.WriteLine(answer);

            //EllipticCurve curve = new EllipticCurve(130, 565, 719);
            //AffinePoint P = new AffinePoint(107, 443, curve);
            //AffinePoint Q = new AffinePoint(608, 427, curve);
            //var res = 27 * P;
            //Console.WriteLine(P + Q);
            
        }
    }
}
