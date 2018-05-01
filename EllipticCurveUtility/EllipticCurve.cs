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

        /// <summary>
        /// Modulus of finite group
        /// </summary>
        public BigInteger P { get; set; }
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
        public EllipticCurve(BigInteger A, BigInteger B, BigInteger P) : this(A,B)
        {
            this.P = P; 
        }
        public EllipticCurve(BigInteger A, BigInteger B, BigInteger C, BigInteger P) : this(A,B,P)
        {
            this.C = C;
        }
       
        #endregion

        #region Public Methods
        public BigInteger HasseTheorem()
        {
            return P + 1 + (BigIntegerExtension.Sqrt(P)+1);
        }
        #endregion

        #region Interface and Override Methods
        public object Clone()
        {
            return new EllipticCurve(A, B, C, P);
        }
        #endregion

    }
}
