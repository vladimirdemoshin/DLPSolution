using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Numerics;

namespace Utility
{
    public struct DLPInput
    {
        public BigInteger g;
        public BigInteger h;
        public BigInteger p;
        public BigInteger order;
        public DLPInput(BigInteger g, BigInteger h, BigInteger p, BigInteger order)
        {
            this.g = g;
            this.h = h;
            this.p = p;
            this.order = order;
        }
    }
}
