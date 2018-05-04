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

        #region Methods
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
            var rand = new BigIntegerRandom();
            BigInteger t;
            while (true)
            {
                var x = rand.Next(0, P - 1);
                t = (x * (x * x + A) + B).ModPositive(P);
                if (BigIntegerExtension.JacobiSymbol(t, P) == -1)
                    continue;
                return new AffinePoint(x, BigIntegerExtension.Sqrt(t).ModPositive(P), this);
            }
        }
        public object Clone()
        {
            return new EllipticCurve(A, B, C, P);
        }
        #endregion

    }
}
