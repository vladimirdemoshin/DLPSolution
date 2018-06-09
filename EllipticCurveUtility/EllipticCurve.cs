using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Numerics;
using Utility;

namespace EllipticCurveUtility
{
    //эллиптическая кривая в канонической форме Вейрштрасса с характеристикой > 3
    public class EllipticCurve : ICloneable
    {
        #region Properites
        public BigInteger A { get; set; }
        public BigInteger B { get; set; }
        public BigInteger C { get; set; }
        public BigInteger P { get; set; } // простое число отлично от 2 и 3
        #endregion

        #region Constructors
        public static bool operator ==(EllipticCurve E1, EllipticCurve E2)
        {
            return (E1.A == E2.A && E1.B == E2.B && E1.C == E2.C && E1.P == E2.P);
        }
        public static bool operator !=(EllipticCurve E1, EllipticCurve E2)
        {
            return !(E1 == E2);
        }
        #endregion

        #region Constructors
        public EllipticCurve()
        {
            //write some init
        }
        public EllipticCurve(BigInteger A, BigInteger B)
        {
            this.A = A;
            this.B = B;
            C = 0;
        }
        public EllipticCurve(BigInteger A, BigInteger B, BigInteger P)
            : this(A, B)
        {
            this.P = P;
        }
        public EllipticCurve(BigInteger A, BigInteger B, BigInteger C, BigInteger P)
            : this(A, B, P)
        {
            this.C = C;
        }

        #endregion

        #region Static Methods


        ////Функция нахождения порядка кривой в афинных координатах
        //public static BigInteger FindOrder(EllipticCurve E)
        //{
        //    var Z = E.P;
        //    var rand = new Random();
        //    var startPoint = BasePoint.RandomPoint<PointAffine>(E);
        //    // Найдем границы по теореме Хассе
        //    var leftBorder = Z + 1 - 2 * (FieldsOperations.Sqrt(Z));
        //    var rightBorder = Z + 1 + 2 * (FieldsOperations.Sqrt(Z));
        //    for (int c = 0; c < NUMBER_OF_ATTEMPTS; c++)
        //    {
        //        //Сгенерируем точку на кривой и найдем ее порядок
        //        var P = BasePoint.RandomPoint<PointAffine>(E);
        //        var M = E.FindPointOrder(P);
        //        startPoint = P;
        //        if (M == 0)
        //            continue;
        //        int countOfDevided = 0;
        //        var order = BigInteger.Zero;
        //        //Найдем число делителей порядка точки в границах
        //        for (var i = leftBorder; i < rightBorder; )
        //        {
        //            if (i % M == 0)
        //            {
        //                order = i;
        //                i += M;
        //                countOfDevided++;
        //                if (countOfDevided > 1)
        //                    break;
        //            }
        //            else
        //            {
        //                i++;
        //            }
        //        }
        //        //Если оно равно единице, то порядок кривой - делитель порядка точки
        //        if (countOfDevided == 1)
        //        {
        //            E.Order = order;
        //            return order;
        //        }
        //    }
        //    return 0;
        //}

        public static BigInteger FindPointOrder(AffinePoint P)
        {
            var point = P;
            //var rand = new Random();
            var rand = new BigIntegerRandom();
            BigInteger M = 0;
            //Создаем список точек
            var A = new List<AffinePoint>();
            var Q = (P.E.P + 1) * point;
            A.Add(new AffinePoint());
            A[0].X = 0;
            A[0].Y = 0;
            A[0].Z = 1;
            A[0].E = P.E;
            A.Add(point);
            var max = BigInteger.One;
            int count = 0;
            for (; count < P.E.P; count++)
            {
                M = 0;
                // генерерируем число m
                var m = BigIntegerExtension.Sqrt(BigIntegerExtension.Sqrt(P.E.P)) + 1 + rand.Next(0, P.E.P);
                if (m > max)
                {
                    //дозаполняем список
                    for (BigInteger i = 0; i < m - max; i++)
                    {
                        A.Add(point + A[A.Count - 1]);
                    }
                    max = m;
                }
                var k = -m - 1;
                int j = 0;
                bool flag = true;
                var twoMP = (2 * m) * point;
                var temp = Q + (k * twoMP);
                k++;
                //вычисляем параметры k и j
                for (; k < m && flag; k++)
                {
                    if (k < 0 && k != -m)
                        temp = temp - twoMP;
                    else
                        temp = temp + twoMP;

                    for (j = 0; j < A.Count && flag; j++)
                    {
                        if (temp == A[j])
                        {
                            j = -j;
                            flag = false;
                            break;
                        }
                        if (temp == (-A[j]))
                        {
                            flag = false;
                            break;
                        }
                    }
                }
                k--;
                M = (P.E.P + 1 + 2 * m * k + j);
                if ((M * point) == P.E.GetInfiniteAffinePoint() && M != 0)
                {
                    break;
                }
            }
            if (count == P.E.P)
            {
                return 0;
            }
            // Раскладываем число M ро-методом Полларда
            List<BigInteger> factorsM;
            try
            {
                factorsM = BigIntegerExtension.PollardsAlg(BigInteger.Abs(M));
            }
            catch (Exception)
            {
                return 0;
            }
            // Проверяем простые делители числа M
            for (int i = 0; i < factorsM.Count; )
            {
                BigInteger temp = M / factorsM[i];
                if (M != factorsM[i] && M % factorsM[i] == 0 && (temp * point) == P.E.GetInfiniteAffinePoint())
                {
                    M = temp;
                    i = 0;
                }
                else
                    i++;
            }
            //P.Order = M;
            return M;
        }




        public static EllipticCurve GenerateEllipticCurve(BigInteger p)
        {
            BigIntegerRandom rand = new BigIntegerRandom();
            BigInteger a, b;
            bool condition;
            do
            {
                a = rand.Next(0, p);
                b = rand.Next(0, p);
                condition = (4 * BigInteger.ModPow(a, 3, p) + 27 * BigInteger.ModPow(b, 2, p) % p) != 0 && a != 0 && b != 0;
            } while (!condition);
            return new EllipticCurve(a, b, p);
        }

        #endregion


        #region Methods
        public bool IsOnCurve(AffinePoint P)
        {
            var x = P.X;
            var t = (x * (x * x + A) + B).ModPositive(this.P);
            if (BigIntegerExtension.JacobiSymbol(t, this.P) == -1) return false;
            return true;
        }
        public AffinePoint GetInfiniteAffinePoint()
        {
            return new AffinePoint(0, 1, 0, this);
        }
        //неособая кривая если характеристика отлична от 2 и 3 и выполняется условие для a и b
        public bool IsNotSpecial()
        {
            var condition = 4 * A * A * A + 27 * B * B - 18 * A * B * C - A * A * C * C + 4 * B * C * C * C;
            return condition != 0 && P != 2 && P != 3;
        }
        public BigInteger HasseTheorem()
        {
            return P + 1 + (BigIntegerExtension.Sqrt(P) + 1);
        }
        public AffinePoint GetRandomAffinePoint()
        {
            BigIntegerRandom rand = new BigIntegerRandom();
            var start = rand.Next(0, P);
            var x = start;
            BigInteger t;
            do
            {
                t = (x * (x * x + A) + B).ModPositive(P);
                if (BigIntegerExtension.JacobiSymbol(t, P) != -1)
                    return new AffinePoint(x, BigIntegerExtension.Sqrt(t).ModPositive(P), this);
                if (x > P - 1) x = -1;
                x++;
            }
            while (x != start);
            return GetInfiniteAffinePoint();
        }
        public object Clone()
        {
            return new EllipticCurve(A, B, C, P);
        }

        public override string ToString()
        {
            return String.Format("y^2 = x^3 + {0} * x + {1}  (mod {2})", A, B, P);
        }
        #endregion

    }
}
