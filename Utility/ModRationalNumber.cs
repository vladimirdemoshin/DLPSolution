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
        private BigInteger mod;
        private BigInteger numerator;
        private BigInteger denominator;
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
                var temp = value.ModPositive(mod);
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
                var temp = value.ModPositive(mod);
                if (temp == 0) denominator = 1;
                else denominator = temp;
            }
        }
        #endregion

        #region Constructors
        public ModRationalNumber()
        {
            Mod = 2;
            Denominator = 1;
            Numerator = 1;
        }
        public ModRationalNumber(BigInteger numerator)
        {
            Mod = 2;
            Denominator = 1;
            Numerator = numerator;
        }
        public ModRationalNumber(BigInteger numerator, BigInteger denominator)
        {
            Mod = 2;
            Denominator = denominator;
            Numerator = numerator;
        }
        public ModRationalNumber(BigInteger numerator, BigInteger denominator, BigInteger mod)
        {
            Mod = mod;
            Denominator = denominator;
            Numerator = numerator;

            //var gcd = BigInteger.GreatestCommonDivisor(denominator.ModPositive(mod), mod);
            //if (gcd == 1)
            //    Numerator = (numerator * denominator.ModInverse(mod)).ModPositive(mod);

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
            BigInteger numerator = a.Numerator * b.Denominator;
            BigInteger denominator = a.Denominator * b.Numerator;

            //var gcd = BigInteger.GreatestCommonDivisor(denominator.ModPositive(a.Mod), a.Mod);
            //if (gcd == 1)
            //    return new ModRationalNumber((numerator * denominator.ModInverse(a.Mod)).ModPositive(a.Mod), 1, a.Mod);

            return new ModRationalNumber(numerator, denominator, a.Mod);
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
        public static implicit operator ModRationalNumber(int a)
        {
            return new ModRationalNumber((BigInteger)a, 1);
        }
        public static implicit operator ModRationalNumber(BigInteger a)
        {
            return new ModRationalNumber(a, 1);
        }
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
