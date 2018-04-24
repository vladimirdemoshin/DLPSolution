using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Numerics;

namespace EllipticCurveUtility
{
    //эллиптическая кривая в канонической форме Вейрштрасса с характеристикой > 3
    public class EllipticCurve
    {
        #region Properites
        public BigInteger A { get; set; }
        public BigInteger B { get; set; }
        public BigInteger C { get; set; }
        /// <summary>
        /// Modulus of finite group
        /// </summary>
        public BigInteger P { get; set; }
        public BigInteger Order { get; set; }
        public BigInteger Char { get; set; }  //характеристика, char != 2,3
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

        #region Methods
        #endregion

    }
}
