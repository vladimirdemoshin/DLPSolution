////using System;
////using System.Collections.Generic;
////using System.Linq;
////using System.Text;
////using System.Threading.Tasks;
////using System.Numerics;

////using Utility;
////using EllipticCurveUtility;

////namespace ECDLPAlgorithm
////{
////    public static class RhoPollard
////    {
////        public static BigInteger SolveDLP(AffinePoint P, AffinePoint Q)
////        {
////            var R = P;
////            BigInteger a = 1, b = 0;
////            var R2 = R;
////            BigInteger a2 = a, b2 = b;
////            var MOD = P.E.P;
////            for (int i = 1; i < MOD; i++)
////            {
////                R = f(Q, P, R, ref a, ref b);
////                R2 = f(Q, P, R2, ref a2, ref b2);
////                R2 = f(Q, P, R2, ref a2, ref b2); 
////                if (R == R2)
////                {
////                    Console.WriteLine(R);
////                    Console.WriteLine(R2);
////                    break;
////                }
////            }
////            return (a2 - a) * BigIntegerExtension.ModInverse(b - b2, MOD);         
////        }

////        private static AffinePoint f(AffinePoint Q, AffinePoint P, AffinePoint R, ref BigInteger a, ref BigInteger b)
////        {
////            int set = chooseSet(R);
////            if (set == 0)
////            {
////                a = a;
////                b = b + 1;
////                return Q + R;
////            }
////            else if (set == 1)
////            {
////                a = 2 * a;
////                b = 2 * b;
////                return 2 * R;
////            }
////            else
////            {
////                a = a + 1;
////                b = b;
////                return P + R;
////            }
////        }

////        private static int chooseSet(AffinePoint R)
////        {
////            var x = R.X;
////            int countSet = 3;
////            return (int)x % countSet;
////        }
////    }
////}
