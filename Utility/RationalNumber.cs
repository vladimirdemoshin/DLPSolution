using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Numerics;

namespace Utility
{
    public class RationalNumber
    {
        #region Properties
        private BigInteger numerator;
        private BigInteger denominator;
        public BigInteger Numerator
        {
            get { return numerator; }
            set { numerator = value; }
        }
        public BigInteger Denominator
        {
            get { return denominator; }
            set
            {
                if (value < 0)
                {
                    denominator = -value;
                    numerator = -numerator;
                }
                else if (value == 0) denominator = 1;
                else denominator = value;
            }
        }
        #endregion

        #region Constructors
        public RationalNumber()
        {
            Numerator = 1;
            Denominator = 1;
        }
        public RationalNumber(BigInteger numerator)
        {
            Numerator = numerator;
            Denominator = 1;
        }
        public RationalNumber(BigInteger numerator, BigInteger denominator)
        {
            if (denominator < 0)
            {
                numerator = -numerator;
                denominator = -denominator;
            }
            Numerator = numerator;
            Denominator = denominator;
        }
        #endregion

        #region Operators
        public static RationalNumber operator +(RationalNumber a, RationalNumber b)
        {
            BigInteger numerator = a.Numerator * b.Denominator + b.Numerator * a.Denominator;
            BigInteger denominator = a.Denominator * b.Denominator;
            BigInteger gcd = BigInteger.GreatestCommonDivisor(numerator, denominator);
            return new RationalNumber(numerator / gcd, denominator / gcd);
        }
        public static RationalNumber operator -(RationalNumber a, RationalNumber b)
        {
            BigInteger numerator = a.Numerator * b.Denominator - b.Numerator * a.Denominator;
            BigInteger denominator = a.Denominator * b.Denominator;
            BigInteger gcd = BigInteger.GreatestCommonDivisor(numerator, denominator);
            return new RationalNumber(numerator / gcd, denominator / gcd);
        }
        public static RationalNumber operator *(RationalNumber a, RationalNumber b)
        {
            BigInteger numerator = a.Numerator * b.Numerator;
            BigInteger denominator = a.Denominator * b.Denominator;
            BigInteger gcd = BigInteger.GreatestCommonDivisor(numerator, denominator);
            return new RationalNumber(numerator / gcd, denominator / gcd);
        }
        public static RationalNumber operator *(int a, RationalNumber b)
        {
            BigInteger numerator = a * b.Numerator;
            BigInteger gcd = BigInteger.GreatestCommonDivisor(numerator, b.Denominator);
            return new RationalNumber(numerator / gcd, b.denominator / gcd);
        }

        public static RationalNumber operator /(RationalNumber a, RationalNumber b)
        {
            BigInteger numerator = a.Numerator * b.Denominator;
            BigInteger denominator = a.Denominator * b.Numerator;
            BigInteger gcd = BigInteger.GreatestCommonDivisor(numerator, denominator);
            if (gcd == 0) gcd = 1;
            return new RationalNumber(numerator / gcd, denominator / gcd);
        }
        public static bool operator ==(RationalNumber a, RationalNumber b)
        {
            return a.Numerator * b.Denominator == b.Numerator * a.Denominator;
        }
        public static bool operator !=(RationalNumber a, RationalNumber b)
        {
            return a.Numerator * b.Denominator != b.Numerator * a.Denominator;
        }
        public static bool operator >(RationalNumber a, RationalNumber b)
        {
            return a.Numerator * b.Denominator > b.Numerator * a.Denominator;
        }
        public static bool operator <(RationalNumber a, RationalNumber b)
        {
            return a.Numerator * b.Denominator < b.Numerator * a.Denominator;
        }
        public static bool operator >=(RationalNumber a, RationalNumber b)
        {
            return a.Numerator * b.Denominator >= b.Numerator * a.Denominator;
        }
        public static bool operator <=(RationalNumber a, RationalNumber b)
        {
            return a.Numerator * b.Denominator <= b.Numerator * a.Denominator;
        }
        public static implicit operator RationalNumber(int a)
        {
            return new RationalNumber((BigInteger)a, 1);
        }
        public static implicit operator RationalNumber(BigInteger a)
        {
            return new RationalNumber(a, 1);
        }
        #endregion

        #region Methods
        public static RationalNumber Abs(RationalNumber a)
        {
            return new RationalNumber(BigInteger.Abs(a.Numerator), BigInteger.Abs(a.Denominator));
        }
        #endregion

        #region Converters
        public BigInteger ToModBigInteger(BigInteger mod)
        {
            return Numerator * Denominator.ModInverse(mod) % mod;
        }
        public override string ToString()
        {
            if (Numerator == 0 || Denominator == 1) return Numerator.ToString();
            return String.Format("{0}/{1}", Numerator, Denominator);
        }
        #endregion

        //public static RationalNumber[] ToRationalNumberArray(BigInteger[] bigIntegerArray)
        //{
        //    var rationalNumberArray = new RationalNumber[bigIntegerArray.Length];
        //    int i = 0;
        //    foreach(var bigInteger in bigIntegerArray)
        //        rationalNumberArray[i] = new RationalNumber(bigInteger, 1);
        //    return rationalNumberArray;
        //}
    }
}
