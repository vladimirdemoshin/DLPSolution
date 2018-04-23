using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Numerics;
using System.Collections;

namespace DLPAlgorithm
{
    class BigStepGiantStep
    {
        public static int GiantStepBabyStep(int generator, int value, int order)
        {
            int m = (int)Math.Sqrt((double)order) + 1;
            int temp = (int)BigInteger.ModPow(generator, m, order);
            int gamma = temp;
            Hashtable hashtable = new Hashtable();
            for (int i = 1; i <= m; i++)
            {
                hashtable.Add(gamma, i);
                gamma = (gamma * temp) % order;
            }
            temp = value * generator % order;
            for (int j = 1; j <= m; j++)
            {
                if (hashtable.ContainsKey(temp)) return (int)hashtable[temp] * m - j;
                temp = temp * generator % order;
            }
            return 0;
        }
    }
}
