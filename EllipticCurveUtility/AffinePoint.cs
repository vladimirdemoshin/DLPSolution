using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Numerics;
using Utility;

namespace EllipticCurveUtility
{
    public class AffinePoint : BasePoint
    {
        #region Properties
        /// <summary>
        /// Elliptic curve which contains this Point
        /// </summary>
        public EllipticCurve E { get; set; }
        /// <summary>
        /// Infinite Point
        /// </summary>
        public static AffinePoint O = new AffinePoint(0, 1, 0);
        #endregion

        #region Constructors
        public AffinePoint() : base() { }
        public AffinePoint(BigInteger X, BigInteger Y) : base(X, Y) { }
        public AffinePoint(BigInteger X, BigInteger Y, BigInteger Z) : base(X, Y, Z) { }
        #endregion

        #region Operators
        public static AffinePoint operator -(AffinePoint p1)
        {
            return new AffinePoint(p1.X, -p1.Y, p1.Z);
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
                    return O;
                m = (3 * X1 * X1 + A) * BigIntegerExtension.ModInverse(2 * Y1, MOD);
            }
            else
            {
                m = (Y2 - Y1) * BigIntegerExtension.ModInverse((X2-X1), MOD);
            }
            BigInteger X3 = m * m - X1 - X2;
            BigInteger Y3 = m * (X1 - X3) - Y1;
            return new AffinePoint(X3.ModPositive(MOD), Y3.ModPositive(MOD), 1);
        }
        public static AffinePoint operator -(AffinePoint p1, AffinePoint p2)
        {
            return p1 + (-p2);
        }
        #endregion

        public override string ToString()
        {
            return String.Format("({0}, {1}, {2})", X, Y, Z);
        }

        //разобраться учитывается ли в битовом представлении знак
        //public AffinePoint operator *(BigInteger n, AffinePoint p1)
        //{
        //    BigInteger m = 3 * n;
        //    BigInteger mTemp = m;
        //    bool[] mBits = new bool[m.ToByteArray().Length * 8];
        //    for (int i = 0; i < mBits.Length; i++, mTemp >>= 1)
        //        if ((mTemp & 1) == 1) mBits[i] = true;
        //    BigInteger nTemp = n;
        //    bool[] nBits = new bool[m.ToByteArray().Length * 8];
        //    for (int i = 0; i < mBits.Length; i++, mTemp >>= 1)
        //        if ((mTemp & 1) == 1) mBits[i] = true;
        //    if (n == 0) return O; //return O
        //    AffinePoint p2 = p1;

        //}
    }
}
