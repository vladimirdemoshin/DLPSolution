﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Numerics;
using Utility;

namespace EllipticCurveUtility
{
    public class Point : BasePoint
    {
        #region Properties
        /// <summary>
        /// Elliptic curve which contains this Point
        /// </summary>
        public EllipticCurve E { get; set; }
        //public const Point O = new Point(0, 1, 0);
        #endregion

        #region Constructors
        public Point() : base() { }
        public Point(BigInteger X, BigInteger Y) : base(X, Y) { }
        public Point(BigInteger X, BigInteger Y, BigInteger Z) : base(X, Y, Z) { }
        #endregion

        #region Operators
        public static Point operator -(Point P)
        {
            return new Point(P.X, -P.Y, P.Z);
        }
        //public static Point operator +(Point P1, Point P2)
        //{
        //    BigInteger X1 = P1.X, Y1 = P1.Y, Z1 = P1.Z;
        //    BigInteger X2 = P2.X, Y2 = P2.Y, Z2 = P2.Z;
        //    BigInteger MOD = P1.E.P;
        //    if (Z1 == 0) return P2;
        //    if (Z2 == 0) return P1;
        //    BigInteger U1 = X2 * Z1 * Z1;
        //    BigInteger U2 = X1 * Z2 * Z2;
        //    BigInteger S1 = Y2 * Z1 * Z1 * Z1;
        //    BigInteger S2 = Y1 * Z2 * Z2 * Z2;
        //    BigInteger W = (U1 - U2);
        //    BigInteger R = (S1 - S2);
        //    if (W == 0)
        //    {
        //        Console.WriteLine("W zero");
        //        return null;
        //        //if (R == 0) return DoublePoint(P1);
        //        //return new Point(0, 1, 0);
        //    }
        //    BigInteger T = (U1 + U2);
        //    BigInteger M = (S1 + S2);
        //    BigInteger X3 = R * R - T * W * W;
        //    BigInteger Y3 = ((T * W * W - 2 * X3) * R - M * W * W * W);
        //    // Console.WriteLine(Y3);
        //    Y3 = (Y3 / 2);// % MOD;
        //    BigInteger temp = (MOD + 1) / 2;  //обратный к двум по модулю MOD
        //   // Y3 = Y3 * temp;// % MOD;
        //    // if (Y3 < 0) Y3 += MOD;
        //    BigInteger Z3 = Z1 * Z2 * W;
        //    return new Point(X3.ModPositive(MOD), Y3.ModPositive(MOD), Z3.ModPositive(MOD));
        //}


        //public static BigInteger MyMod(BigInteger number, BigInteger modul)
        //{
        //    number %= modul;
        //    while (number < 0)
        //        number += modul;
        //    return number;
        //}


        //public static Point operator +(Point P, Point Q)
        //{
        //    //if (P.IsInfinite())
        //    //    return Q;
        //    //if (Q.IsInfinite())
        //    //    return P;
        //    //if (P == Q)
        //    //{
        //    //    return Double(P);
        //    //}
        //    var X1 = P.X;
        //    var Y1 = P.Y;
        //    var Z1 = P.Z;
        //    var X2 = Q.X;
        //    var Y2 = Q.Y;
        //    var Z2 = Q.Z;
        //    var p = P.E.P;
        //    var Y1Z2 = Y1 * Z2;
        //    var X1Z2 = X1 * Z2;
        //    var Z1Z2 = Z1 * Z2;
        //    var u = Y2 * Z1 - Y1Z2;
        //    var uu = u * u;
        //    var v = X2 * Z1 - X1Z2;
        //    var vv = v * v;
        //    var vvv = v * vv;
        //    var R = vv * X1Z2;
        //    var A = uu * Z1Z2 - vvv - 2 * R;
        //    var X3 = v * A;
        //    var Y3 = u * (R - A) - vvv * Y1Z2;
        //    var Z3 = vvv * Z1Z2;
        //    return new Point(MyMod(X3, p), MyMod(Y3, p), MyMod(Z3, p));
        //}









        //public AffinePoint operator -(AffinePoint p1, AffinePoint p2)
        //{
        //    return p1 + (-p2);
        //}
        #endregion

        #region Methods
        //public static Point DoublePoint(Point P)
        //{
        //    BigInteger X = P.X, Y = P.Y, Z = P.Z;
        //    BigInteger MOD = P.E.P;
        //    if (Y == 0 || Z == 0) return new Point(0,1,0);
        //    BigInteger M = (3 * BigInteger.ModPow(X, 2, MOD) + P.E.A * BigInteger.ModPow(Z, 4, MOD)) % MOD;
        //    if (M < 0) M += MOD;
        //    BigInteger S = 4 * X * Y * Y % MOD; if (S < 0) S += MOD;
        //    BigInteger X1 = (M * M - 2 * S) % MOD; if (X1 < 0) X1 += MOD;
        //    BigInteger Y1 = (M * (S - X1) - 8 * BigInteger.ModPow(Y, 4, MOD)) % MOD;
        //    if (Y1 < 0) Y1 += MOD;
        //    BigInteger Z1 = 2 * Y * Z % MOD;
        //    if (Z1 < 0) Z1 += MOD;
        //    return new Point(X1, Y1, Z1);
        //}
        #endregion

        #region Convert
        public override string ToString()
        {
            return String.Format("({0}, {1}, {2})", X, Y, Z);
        }
        #endregion 
    }
}













 //public Point(BigInteger x, BigInteger y, BigInteger z, EllipticCurve E)
 //       {
 //           X = x;
 //           Y = y;
 //           Z = z;
 //           this.E = E;
 //       }


 //       public static BigInteger MyMod(BigInteger number, BigInteger modul)
 //       {
 //           number %= modul;
 //           while (number < 0)
 //               number += modul;
 //           return number;
 //       }





 //       static private void ExtendedEuclid(BigInteger a, BigInteger b, out BigInteger inv, out BigInteger d)
 //       {
 //           BigInteger q, r, x1, x2, y1, y2, x, y;
 //           if (b == 0)
 //           {
 //               x = 1;
 //               y = 0;
 //               d = a;
 //               inv = x;
 //               return;
 //           }
 //           x1 = 0;
 //           x2 = 1;
 //           y1 = 1;
 //           y2 = 0;
 //           while (b > 0)
 //           {
 //               q = a / b;
 //               r = a - q * b;
 //               x = x2 - q * x1;
 //               y = y2 - q * y1;
 //               a = b;
 //               b = r;
 //               x2 = x1;
 //               x1 = x;
 //               y2 = y1;
 //               y1 = y;
 //           }
 //           x = x2;
 //           y = y2;
 //           d = a;
 //           inv = x;
 //           return;
 //       }

 //       public static Point operator +(Point P, Point Q)
 //       {
 //           BigInteger d = 1;
 //           Point R = new Point(0, 1, 0, P.E);
 //           BigInteger temp;
 //           BigInteger lambda;
 //           BigInteger x;
 //           BigInteger y;
 //           BigInteger inv;
 //           if (P.X == 0 && P.Y == 1 && P.Z == 0)
 //           {
 //               return Q;
 //           }
 //           else if (Q.X == 0 && Q.Y == 1 && Q.Z == 0)
 //           {
 //               return P;
 //           }
 //           else if (P == -Q || (P == Q && P.Y == 0))
 //           {
 //               return R;
 //           }
 //           else if (P == Q)
 //           {
 //               return Double(P);
 //           }
 //           else if (P.X == Q.X)
 //           {
 //               temp = (P.Y + Q.Y) % P.E.P;
 //               if (temp == 0)
 //               {
 //                   return R;
 //               }
 //               if (temp < 0)
 //               {
 //                   temp += P.E.P;
 //               }
 //               ExtendedEuclid(temp, P.E.P, out inv, out d);
 //               if (d > 1 && d < P.E.P)
 //               {
 //                   R = new Point(0, 0, temp, P.E);
 //                   return R;
 //               }
 //               lambda = ((3 * P.X * P.X + P.E.A) * inv) % P.E.P;
 //           }
 //           else
 //           {
 //               temp = Q.X - P.X;
 //               if (temp < 0)
 //               {
 //                   temp += P.E.P;
 //               }
 //               ExtendedEuclid(temp, P.E.P, out inv, out d);
 //               if (d > 1 && d < P.E.P)
 //               {
 //                   R = new Point(0, 0, temp, P.E);
 //                   return R;
 //               }
 //               lambda = (Q.Y - P.Y) * inv;
 //           }
 //           x = (lambda * lambda - P.X - Q.X) % P.E.P;
 //           y = (lambda * (P.X - x) - P.Y) % P.E.P;
 //           R = new Point(x, y, 1, P.E);
 //           return new Point(MyMod(R.X, R.E.P), MyMod(R.Y, R.E.P), R.Z, R.E);
 //       }




 //       public static Point Double(Point P)
 //       {
 //           BigInteger d = 1;
 //           Point R = new Point(0, 1, 0, P.E);
 //           if (P.Y == 0)
 //               return R;
 //           BigInteger temp = (2 * P.Y) % P.E.P;
 //           if (temp < 0)
 //           {
 //               temp += P.E.P;
 //           }
 //           BigInteger inv;
 //           ExtendedEuclid(temp, P.E.P, out inv, out d);
 //           if (d > 1 && d < P.E.P)
 //           {
 //               R = new Point(0, 0, temp, P.E);
 //               return R;
 //           }
 //           BigInteger lambda = (3 * P.X * P.X + P.E.A) * inv;
 //           BigInteger x = (lambda * lambda - 2 * P.X) % P.E.P;
 //           BigInteger y = (lambda * (P.X - x) - P.Y) % P.E.P;
 //           R = new Point(x, y, 1, P.E);
 //           return new Point(MyMod(R.X, R.E.P), MyMod(R.Y, R.E.P), R.Z, R.E);
 //       }