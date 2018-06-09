//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
using System;
using System.Numerics;
using Utility;
namespace Utility
{
    public class ModRationalNumber
    {
        #region Properties
        #region Private
        private BigInteger mod;
        private BigInteger numerator;
        private BigInteger denominator;
        #endregion
        #region Public
        public BigInteger Mod
        {
            get { return mod; }
            set
            {
                if (value > 0)
                    mod = value;
            }
        }
        public BigInteger Numerator
        {
            get { return numerator; }
            set 
            {
                var temp = value.ModPositive(Mod);
                if (temp == 0)
                    Denominator = 1;
                numerator = temp;
            }
        }
        public BigInteger Denominator
        {
            get { return denominator; }
            set
            {
                var temp = value.ModPositive(Mod);
                if (temp == 0) denominator = 1;
                else denominator = temp;
            }
        }
        #endregion
        #endregion
        #region Constructors
        public ModRationalNumber(BigInteger numerator, BigInteger denominator, BigInteger mod)
        {
            Mod = mod;
            Denominator = denominator;
            Numerator = numerator;
        }
        #endregion
        #region Operators 
        public static ModRationalNumber operator +(ModRationalNumber a, ModRationalNumber b)
        {
            BigInteger numerator = a.Numerator * b.Denominator + b.Numerator * a.Denominator;
            BigInteger denominator = a.Denominator * b.Denominator;
            return new ModRationalNumber(numerator, denominator, a.Mod);
        }
        public static ModRationalNumber operator -(ModRationalNumber a, ModRationalNumber b)
        {
            BigInteger numerator = a.Numerator * b.Denominator - b.Numerator * a.Denominator;
            BigInteger denominator = a.Denominator * b.Denominator;
            return new ModRationalNumber(numerator, denominator, a.Mod);
        }
        public static ModRationalNumber operator *(ModRationalNumber a, ModRationalNumber b)
        {
            BigInteger numerator = a.Numerator * b.Numerator;
            BigInteger denominator = a.Denominator * b.Denominator;
            return new ModRationalNumber(numerator, denominator, a.Mod);
        }
        public static ModRationalNumber operator *(int a, ModRationalNumber b)
        {
            BigInteger numerator = a * b.Numerator;
            return new ModRationalNumber(numerator, b.Denominator, b.Mod);
        }
        public static ModRationalNumber operator *(BigInteger a, ModRationalNumber b)
        {
            BigInteger numerator = a * b.Numerator;
            return new ModRationalNumber(numerator, b.Denominator, b.Mod);
        }
        public static ModRationalNumber operator /(ModRationalNumber a, ModRationalNumber b)
        {
            BigInteger numerator = a.Numerator * b.Denominator;
            BigInteger denominator = a.Denominator * b.Numerator;
            return new ModRationalNumber(numerator, denominator, a.Mod);
        }
        public static bool operator ==(ModRationalNumber a, ModRationalNumber b)
        {
            bool condition = a.Numerator == b.Numerator && a.Denominator == b.Denominator;
            return condition;
        }
        public static bool operator !=(ModRationalNumber a, ModRationalNumber b)
        {
            return !(a == b);
        }
        public static bool operator ==(ModRationalNumber a, int b)
        {
            return a.Numerator == b && a.Denominator == 1;
        }
        public static bool operator !=(ModRationalNumber a, int b)
        {
            return !(a == b);
        }
        #endregion
        #region Methods
        public override string ToString()
        {
            if (Numerator == 0 || Denominator == 1) return Numerator.ToString();
            return String.Format("{0}/{1}", Numerator, Denominator);
        }
        #endregion
    }
}
