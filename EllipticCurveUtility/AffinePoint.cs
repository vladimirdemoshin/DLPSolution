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

        /// <summary>
        /// Elliptic curve which contains this AffinePoint
        /// </summary>
        public EllipticCurve E { get; set; }
        #endregion

        #region Constructors
        public AffinePoint() : base() { }
        public AffinePoint(BigInteger X, BigInteger Y) : base(X, Y) { }
        public AffinePoint(BigInteger X, BigInteger Y, BigInteger Z) : base(X, Y, Z) { }
        public AffinePoint(BigInteger X, BigInteger Y, EllipticCurve E) : this(X,Y)
        {
            this.E = E;
        }
        public AffinePoint(BigInteger X, BigInteger Y, BigInteger Z, EllipticCurve E) : this(X,Y,E)
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
                m = (Y2 - Y1) * BigIntegerExtension.ModInverse((X2-X1), MOD);
            }
            BigInteger X3 = m * m - X1 - X2;
            BigInteger Y3 = m * (X1 - X3) - Y1;
            return new AffinePoint(X3.ModPositive(MOD), Y3.ModPositive(MOD), P.E);
        }
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

        //    for (int i = mBits.Length-2 ; i >=1; i--)
        //    {
        //        Q = Double(Q);
        //        var mBit = mBits[i];
        //        var nBit = i >= nBits.Length ? false : nBits[i];
        //        if (mBit == true && nBit == false)
        //            Q += P;
        //        else if (mBit == false && nBit == true)
        //            Q -= P;
        //    }
        //    return new AffinePoint(Q.X.ModPositive(MOD), sign * Q.Y.ModPositive(MOD), Q.E);
        //}
        public static AffinePoint operator *(BigInteger k, AffinePoint P)
        {
            bool negative = false;
            if (k < 0)
            {
                negative = true;
                k = BigInteger.Abs(k);
            }
            BigInteger d = 1;
            AffinePoint R = new AffinePoint(0, 1, 0, P.E);
            //if (P.IsInfinite())
            //{
            //    return R;
            //}
            while (k > 0)
            {
                if (P.Z > 1)
                    return P;
                if (k % 2 == 1)
                    R = P + R;
                k /= 2;
                P = P + P;
            }
            if (negative)
                return new AffinePoint(BigIntegerExtension.ModPositive(R.X, R.E.P), BigIntegerExtension.ModPositive(-R.Y, R.E.P), R.Z, R.E);
            else
                return new AffinePoint(BigIntegerExtension.ModPositive(R.X, R.E.P), BigIntegerExtension.ModPositive(R.Y, R.E.P), R.Z, R.E);
        }
        #endregion

        #region Public Methods
        public static AffinePoint Double(AffinePoint P)
        {
            return P + P;
        }
        public static AffinePoint GetInfinitePointForCurve(EllipticCurve E)
        {
            return new AffinePoint(0,1,0,E);
        }
        #endregion

        #region Interface and Override Methods
        public object Clone()
        {
            var E = (EllipticCurve)this.E.Clone();
            return new AffinePoint(X,Y,Z,E);
        }
        public override bool Equals(object P)
        {
            return this == (AffinePoint)P;
        }
        public override int GetHashCode()
        {
            return (X + Y + Z + E.A + E.B + E.C + E.P + "").GetHashCode();
        }
        #endregion

        #region Converters
        public override string ToString()
        {
            return String.Format("({0}, {1}, {2})", X, Y, Z);
        }
        #endregion
    }
}
