using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Numerics;
using Utility;

namespace EllipticCurveUtility
{
    public class AffinePoint : BasePoint, ICloneable
    {
        #region Properties
        public EllipticCurve E { get; set; }
        #endregion

        #region Constructors
        public AffinePoint() : base() { }
        public AffinePoint(BigInteger X, BigInteger Y) : base(X, Y) { }
        public AffinePoint(BigInteger X, BigInteger Y, BigInteger Z) : base(X, Y, Z) { }
        public AffinePoint(BigInteger X, BigInteger Y, EllipticCurve E)
            : this(X, Y)
        {
            this.E = E;
        }
        public AffinePoint(BigInteger X, BigInteger Y, BigInteger Z, EllipticCurve E)
            : this(X, Y, E)
        {
            this.Z = Z;
        }
        #endregion

        #region Operators
        public static bool operator ==(AffinePoint P, AffinePoint Q)
        {
            return (P.X == Q.X && P.Y == Q.Y && P.Z == Q.Z && P.E == Q.E);
        }
        public static bool operator !=(AffinePoint P, AffinePoint Q)
        {
            return !(P == Q);
        }
        public static AffinePoint operator -(AffinePoint P)
        {
            return new AffinePoint(P.X, -P.Y, P.Z, P.E);
        }
        public static AffinePoint operator -(AffinePoint P, AffinePoint Q)
        {
            return P + (-Q);
        }




        // мое из крендэла померанца, не уверен что работает
        public static AffinePoint operator +(AffinePoint P, AffinePoint Q)
        {
            BigInteger X1 = P.X, Y1 = P.Y, Z1 = P.Z;
            BigInteger X2 = Q.X, Y2 = Q.Y, Z2 = Q.Z;
            BigInteger MOD = P.E.P, A = P.E.A;
            BigInteger m;
            if (Z1 == 0)
                return Q;
            if (Z2 == 0)
                return P;
            if (X1 == X2)
            {
                if (Y1 + Y2 == 0)
                    return GetInfinitePointForCurve(P.E);
                m = (3 * X1 * X1 + A) * BigIntegerExtension.ModInverse(2 * Y1, MOD);
            }
            else
            {
                m = (Y2 - Y1) * BigIntegerExtension.ModInverse((X2 - X1), MOD);
            }
            BigInteger X3 = m * m - X1 - X2;
            BigInteger Y3 = m * (X1 - X3) - Y1;
            return new AffinePoint(X3.ModPositive(MOD), Y3.ModPositive(MOD), P.E);
        }



        ////из крэнделла - померанца, многие примеры неправильно работают
        //public static AffinePoint operator *(BigInteger n, AffinePoint P)
        //{
        //    int sign = 1;
        //    if (n == 0) return GetInfinitePointForCurve(P.E);
        //    if (n == 1) return P;
        //    if (n == -1) return -P;
        //    if (n < 0) sign = -1;
        //    n = sign * n;
        //    BigInteger MOD = P.E.P;
        //    var m = 3 * n;
        //    var Q = new AffinePoint(P.X, P.Y, P.Z, P.E);
        //    var mBits = m.GetBitArray();
        //    var nBits = n.GetBitArray();
        //    for (int i = mBits.Length - 2; i >= 1; i--)
        //    {
        //        Q = Double(Q);
        //        var mBit = mBits[i];
        //        var nBit = i >= nBits.Length ? false : nBits[i];
        //        if (mBit == true && nBit == false)
        //            Q = Q + P;
        //        else if (mBit == false && nBit == true)
        //            Q = Q - P;
        //    }    
        //    return new AffinePoint(Q.X.ModPositive(MOD), sign * Q.Y.ModPositive(MOD), Q.E);
        //}




        //чувака
        public static AffinePoint operator *(BigInteger k, AffinePoint point)
        {
            if (k == 0) return GetInfinitePointForCurve(point.E);
            var temp = point;
            k--;
            while (k > 0)
            {
                if (BigInteger.Remainder(k, 2) != 0)
                {
                    if ((temp.X == point.X) && (temp.Y == point.Y))
                        temp = X2(temp);
                    else
                        temp = temp + point;
                    k--;
                }
                k = k / 2;
                // Console.WriteLine(k);
                point = X2(point);
            }
            return temp;
        }

        //чувака
        private static AffinePoint X2(AffinePoint point)
        {
            BigInteger A = 3 * point.X * point.X + point.E.A * point.Z * point.Z;
            A = BigInteger.Remainder(A, point.E.P);
            if (A < 0) { while (A < 0) { A += point.E.P; } }


            BigInteger B = 2 * point.Y * point.Z;
            B = BigInteger.Remainder(B, point.E.P);
            if (B < 0) { while (B < 0) { B += point.E.P; } }


            BigInteger X = B * (A * A - 4 * point.X * point.Y * B);
            X = BigInteger.Remainder(X, point.E.P);
            if (X < 0) { while (X < 0) { X += point.E.P; } }


            BigInteger Y = A * (6 * point.Y * point.X * B - A * A) - 2 * point.Y * point.Y * B * B;
            Y = BigInteger.Remainder(Y, point.E.P);
            if (Y < 0) { while (Y < 0) { Y += point.E.P; } }


            BigInteger Z = B * B * B;
            Z = BigInteger.Remainder(Z, point.E.P);
            if (Z < 0) { while (Z < 0) { Z += point.E.P; } }
            return new AffinePoint(X, Y, Z, point.E);
        }






        #endregion

        #region Methods
        public static AffinePoint Double(AffinePoint P)
        {
            return P + P;
        }
        public static AffinePoint GetInfinitePointForCurve(EllipticCurve E)
        {
            return new AffinePoint(0, 1, 0, E);
        }
        public object Clone()
        {
            var E = (EllipticCurve)this.E.Clone();
            return new AffinePoint(X, Y, Z, E);
        }
        public override bool Equals(object P)
        {
            return this == (AffinePoint)P;
        }
        public override int GetHashCode()
        {
            return (X + Y + Z + E.A + E.B + E.C + E.P + "").GetHashCode();
        }
        public ProjectivePoint ToProjectivePoint()
        {
            if (this == AffinePoint.GetInfinitePointForCurve(E))
                return new ProjectivePoint(0, 1, 0, E);
            return new ProjectivePoint(X, Y, 1, E);
        }
        #endregion

        #region Converters
        //public ProjectivePoint
        public override string ToString()
        {
            return String.Format("({0}, {1}, {2})", X, Y, Z);
        }
        #endregion
    }
}



















//азата
//public static AffinePoint operator *(BigInteger k, AffinePoint P)
//{
//    bool negative = false;
//    if (k < 0)
//    {
//        negative = true;
//        k = BigInteger.Abs(k);
//    }
//    BigInteger d = 1;
//    AffinePoint R = new AffinePoint(0, 1, 0, P.E);
//    //if (P.IsInfinite())
//    //{
//    //    return R;
//    //}
//    while (k > 0)
//    {
//        if (P.Z > 1)
//            return P;
//        if (k % 2 == 1)
//            R = P + R;
//        k /= 2;
//        P = P + P;
//    }
//    if (negative)
//        return new AffinePoint(BigIntegerExtension.ModPositive(R.X, R.E.P), BigIntegerExtension.ModPositive(-R.Y, R.E.P), R.Z, R.E);
//    else
//        return new AffinePoint(BigIntegerExtension.ModPositive(R.X, R.E.P), BigIntegerExtension.ModPositive(R.Y, R.E.P), R.Z, R.E);
//}


//  Console.WriteLine(m);
//            Console.WriteLine();
//            for (int i = mBits.Length - 1; i >= 0;i-- )
//            {
//                Console.Write(mBits[i] ? 1 : 0);
//            }
//            Console.WriteLine();
//            Console.WriteLine(n);
//            for (int i = nBits.Length - 1; i >= 0; i--)
//            {
//                Console.Write(nBits[i] ? 1 : 0);
//            }
//            Console.WriteLine();


//мое с сайта
//public static AffinePoint operator +(AffinePoint P, AffinePoint Q)
//{
//    BigInteger X1 = P.X, Y1 = P.Y, Z1 = P.Z;
//    BigInteger X2 = Q.X, Y2 = Q.Y, Z2 = Q.Z;
//    BigInteger MOD = P.E.P, A = P.E.A;
//    if (Z1 == 0)
//        return Q;
//    if (Z2 == 0)
//        return P;
//    var X3 = (Y2 - Y1) * (Y2 - Y1) * (X2 - X1).ModInverse(MOD) * (X2 - X1).ModInverse(MOD) - X1 - X2;
//    var Y3 = (2 * X1 + X2) * (Y2 - Y1) * (X2 - X1).ModInverse(MOD) - (Y2 - Y1) * (Y2 - Y1) * (Y2 - Y1) * (X2 - X1).ModInverse(MOD) * (X2 - X1).ModInverse(MOD) * (X2 - X1).ModInverse(MOD) - Y1;
//    return new AffinePoint(X3.ModPositive(MOD), Y3.ModPositive(MOD), P.E);
//}

//чувака
//public static AffinePoint operator *(BigInteger k, AffinePoint point)
//{
//    if (k == 0) return GetInfinitePointForCurve(point.E);
//    var temp = point;
//    k--;
//    while (k > 0)
//    {
//        if (BigInteger.Remainder(k, 2) != 0)
//        {
//            if ((temp.X == point.X) && (temp.Y == point.Y))
//                temp = X2(temp);
//            else
//                temp = temp + point;
//            k--;
//        }
//        k = k / 2;
//        // Console.WriteLine(k);
//        point = X2(point);
//    }
//    return temp;
//}


//private static AffinePoint X2(AffinePoint point)
//{
//    BigInteger A = 3 * point.X * point.X + point.E.A * point.Z * point.Z;
//    A = BigInteger.Remainder(A, point.E.P);
//    if (A < 0) { while (A < 0) { A += point.E.P; } }


//    BigInteger B = 2 * point.Y * point.Z;
//    B = BigInteger.Remainder(B, point.E.P);
//    if (B < 0) { while (B < 0) { B += point.E.P; } }


//    BigInteger X = B * (A * A - 4 * point.X * point.Y * B);
//    X = BigInteger.Remainder(X, point.E.P);
//    if (X < 0) { while (X < 0) { X += point.E.P; } }


//    BigInteger Y = A * (6 * point.Y * point.X * B - A * A) - 2 * point.Y * point.Y * B * B;
//    Y = BigInteger.Remainder(Y, point.E.P);
//    if (Y < 0) { while (Y < 0) { Y += point.E.P; } }


//    BigInteger Z = B * B * B;
//    Z = BigInteger.Remainder(Z, point.E.P);
//    if (Z < 0) { while (Z < 0) { Z += point.E.P; } }
//    return new AffinePoint(X, Y, Z, point.E);
//}