using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Numerics;

namespace EllipticCurveUtility
{
    public abstract class BasePoint
    {
        #region Properties
        public BigInteger X { get; set; }
        public BigInteger Y { get; set; }
        public BigInteger Z { get; set; }
        #endregion

        #region Constructors
        public BasePoint()
        {
            X = 0;
            Y = 0;
            Z = 1;
        }
        public BasePoint(BigInteger X, BigInteger Y)
        {
            this.X = X;
            this.Y = Y;
            Z = 1;
        }
        public BasePoint(BigInteger X, BigInteger Y, BigInteger Z)
        {
            this.X = X;
            this.Y = Y;
            this.Z = Z;
        }
        #endregion
    }
}



//#region Operators
//public abstract BasePoint operator -(BasePoint P, BasePoint Q);
//public abstract BasePoint operator +(BasePoint P, BasePoint Q);

//public abstract BasePoint operator *(BigInteger k, BasePoint P);

//#endregion