﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            //var gcd = BigInteger.GreatestCommonDivisor(Denominator, Mod);
            //Numerator = (Numerator * (Denominator / gcd).ModInverse(Mod)).ModPositive(Mod);
            //Denominator = gcd;
        }
        #endregion

        //для всех операций подразумевается, что a и b имеют одинаковые модули
        #region Operators 
        public static ModRationalNumber operator +(ModRationalNumber a, ModRationalNumber b)
        {
            BigInteger numerator = a.Numerator * b.Denominator + b.Numerator * a.Denominator;
            BigInteger denominator = a.Denominator * b.Denominator;

            //var gcd = BigInteger.GreatestCommonDivisor(denominator.ModPositive(a.Mod), a.Mod);
            //if (gcd == 1)
            //    return new ModRationalNumber((numerator * denominator.ModInverse(a.Mod)).ModPositive(a.Mod), 1, a.Mod);

            return new ModRationalNumber(numerator, denominator, a.Mod);
        }
        public static ModRationalNumber operator -(ModRationalNumber a, ModRationalNumber b)
        {
            BigInteger numerator = a.Numerator * b.Denominator - b.Numerator * a.Denominator;
            BigInteger denominator = a.Denominator * b.Denominator;

            //var gcd = BigInteger.GreatestCommonDivisor(denominator.ModPositive(a.Mod), a.Mod);
            //if (gcd == 1)
            //    return new ModRationalNumber((numerator * denominator.ModInverse(a.Mod)).ModPositive(a.Mod), 1, a.Mod);

            return new ModRationalNumber(numerator, denominator, a.Mod);
        }
        public static ModRationalNumber operator *(ModRationalNumber a, ModRationalNumber b)
        {
            BigInteger numerator = a.Numerator * b.Numerator;
            BigInteger denominator = a.Denominator * b.Denominator;

            //var gcd = BigInteger.GreatestCommonDivisor(denominator.ModPositive(a.Mod), a.Mod);
            //if (gcd == 1)
            //    return new ModRationalNumber((numerator * denominator.ModInverse(a.Mod)).ModPositive(a.Mod), 1, a.Mod);

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
            //var gcd = BigInteger.GreatestCommonDivisor(b.Numerator, a.Mod);
            //if (gcd == 1)
            //    return new ModRationalNumber(a.Numerator * b.Denominator * b.Numerator, a.Denominator, a.Mod);
            //else
            //{
            //    BigInteger numerator = a.Numerator * b.Denominator;
            //    BigInteger denominator = a.Denominator * b.Numerator;
            //    return new ModRationalNumber(numerator, denominator, a.Mod);
            //}

            BigInteger numerator = a.Numerator * b.Denominator;
            BigInteger denominator = a.Denominator * b.Numerator;
            return new ModRationalNumber(numerator, denominator, a.Mod);

            ////var gcd = BigInteger.GreatestCommonDivisor(denominator.ModPositive(a.Mod), a.Mod);
            ////if (gcd == 1)
            ////    return new ModRationalNumber((numerator * denominator.ModInverse(a.Mod)).ModPositive(a.Mod), 1, a.Mod);

            //return new ModRationalNumber(numerator, denominator, a.Mod);
            
        }
        public static bool operator ==(ModRationalNumber a, ModRationalNumber b)
        {
            //return a.Numerator * b.Denominator == b.Numerator * a.Denominator && a.Mod == b.Mod;
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
        //public static bool operator >(RationalNumber a, RationalNumber b)
        //{
        //    return a.Numerator * b.Denominator > b.Numerator * a.Denominator;
        //}
        //public static bool operator <(RationalNumber a, RationalNumber b)
        //{
        //    return a.Numerator * b.Denominator < b.Numerator * a.Denominator;
        //}
        //public static bool operator >=(RationalNumber a, RationalNumber b)
        //{
        //    return a.Numerator * b.Denominator >= b.Numerator * a.Denominator;
        //}
        //public static bool operator <=(RationalNumber a, RationalNumber b)
        //{
        //    return a.Numerator * b.Denominator <= b.Numerator * a.Denominator;
        //}

        //public static implicit operator ModRationalNumber(int a)
        //{
        //    return new ModRationalNumber((BigInteger)a, 1);
        //}
        //public static implicit operator ModRationalNumber(BigInteger a)
        //{
        //    return new ModRationalNumber(a, 1);
        //}
        #endregion

        #region Methods
        //public static ModRationalNumber Abs(RationalNumber a)
        //{
        //    return new RationalNumber(BigInteger.Abs(a.Numerator), BigInteger.Abs(a.Denominator));
        //}
        //#endregion

        //#region Converters
        //public BigInteger ToModBigInteger(BigInteger mod)
        //{
        //    return Numerator * Denominator.ModInverse(mod) % mod;
        //}
        public override string ToString()
        {
            if (Numerator == 0 || Denominator == 1) return Numerator.ToString();
            return String.Format("{0}/{1}", Numerator, Denominator);
        }
        #endregion

    }
}
