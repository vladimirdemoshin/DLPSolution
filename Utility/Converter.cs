using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Numerics;

namespace Utility
{
    public static class Converter
    {
        public static ModRationalNumber[] ToModRationalNumberArray(BigInteger[] arr)
        {
            ModRationalNumber[] array = new ModRationalNumber[arr.Length];
            for (int i = 0; i < arr.Length; i++)
                array[i] = new ModRationalNumber(arr[i]);
            return array;
        }
        public static ModRationalNumber[][] ToTwoDimensionalModRationalNumberArray(BigInteger[][] arr)
        {
            ModRationalNumber[][] array = new ModRationalNumber[arr.Length][];
            for (int i = 0; i < arr.Length; i++)
                array[i] = Converter.ToModRationalNumberArray(arr[i]);
            return array;
        }


        public static RationalNumber[] ToRationalNumberArray(BigInteger[] arr)
        {
            RationalNumber[] array = new RationalNumber[arr.Length];
            for (int i = 0; i < arr.Length; i++)
                array[i] = new RationalNumber(arr[i]);
            return array;
        }

        public static RationalNumber[][] ToTwoDimensionalRationalNumberArray(BigInteger[][] arr)
        {
            RationalNumber[][] array = new RationalNumber[arr.Length][];
            for (int i = 0; i < arr.Length; i++)
                array[i] = Converter.ToRationalNumberArray(arr[i]);
            return array;
        }

        public static BigInteger[] ToBigIntegerArray(RationalNumber[] arr, BigInteger MOD)
        {
            BigInteger[] array = new BigInteger[arr.Length];
            for (int i = 0; i < arr.Length; i++)
                array[i] = arr[i].ToModBigInteger(MOD);
            return array;
        }
    }
}
