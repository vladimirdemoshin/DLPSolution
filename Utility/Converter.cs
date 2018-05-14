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
        public static BigInteger[][] ToTwoDimensionalBigIntegerArray(List<List<BigInteger>> list)
        {
            var array = new BigInteger[list.Count][];
            int i = 0, j;
            foreach(var l in list)
            {
                array[i] = new BigInteger[l.Count];
                j = 0;
                foreach (var n in l)
                    array[i][j++] = n;
                i++;
            }
            return array;
        }
        public static ModRationalNumber[] ToModRationalNumberArray(BigInteger[] arr, BigInteger mod)
        {
            ModRationalNumber[] array = new ModRationalNumber[arr.Length];
            for (int i = 0; i < arr.Length; i++)
                array[i] = new ModRationalNumber(arr[i],1,mod);
            return array;
        }
        public static ModRationalNumber[][] ToTwoDimensionalModRationalNumberArray(BigInteger[][] arr, BigInteger mod)
        {
            ModRationalNumber[][] array = new ModRationalNumber[arr.Length][];
            for (int i = 0; i < arr.Length; i++)
                array[i] = Converter.ToModRationalNumberArray(arr[i], mod);
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

        public static string ToString(BigInteger[] array)
        {
            var temp = new StringBuilder("");
            var divider = " ";
            foreach (var item in array)
            {
                temp.Append(item.ToString());
                temp.Append(divider);
            }
            temp.Remove(temp.Length - 1, 1);
            return temp.ToString();
        }
    }
}
