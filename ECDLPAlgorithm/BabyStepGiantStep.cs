using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Numerics;
using EllipticCurveUtility;
using Utility;

namespace ECDLPAlgorithm
{
    public static class BabyStepGiantStep
    {
        public static BigInteger SolveDLP(ProjectivePoint P, ProjectivePoint Q)
        {
            var order = P.E.HasseTheorem();
            Console.WriteLine(order);
            var babySteps = new Dictionary<AffinePoint, BigInteger>();
            var m = BigIntegerExtension.Sqrt(order) + 1;
            AffinePoint babyStep;
            for (BigInteger i = 0; i < m; i++)
            {
                babyStep = (i * P).ToAffinePoint();
                babySteps.Add(babyStep, i);
                Console.WriteLine(babyStep);
            }
            AffinePoint temp;
            ProjectivePoint giantStep = (m * P);
            Console.WriteLine();
            Console.WriteLine(giantStep.ToAffinePoint());
            Console.WriteLine();
            for (BigInteger j = 0; j < m; j++)
            {
                temp = (Q - j * giantStep).ToAffinePoint();
                Console.WriteLine(temp);
                BigInteger i = -1;
                try
                {
                    i = babySteps[temp];
                }
                catch (KeyNotFoundException e) { }
                if (i != -1)
                    return BigIntegerExtension.ModPositive(i + j * m, order);
            }
            //make modification with m/2 and +-iP
            return -1;
        }
    }
}



  //public static class BabyStepGiantStep
  //  {
  //      public static BigInteger SolveDLP(AffinePoint P, AffinePoint Q)
  //      {
  //          var order = 699;//P.E.HasseTheorem();
  //          Console.WriteLine(order);
  //          var babySteps = new Dictionary<AffinePoint, BigInteger>();
  //          var m = BigIntegerExtension.Sqrt(order) + 1;

  //          AffinePoint babyStep;

  //          for (BigInteger i = 0; i < m; i++)
  //          {
  //              babyStep = i * P;
  //              babySteps.Add(babyStep, i);
  //              Console.WriteLine(babyStep);
  //          }
  //          AffinePoint giantStep = (m * P);
  //          Console.WriteLine();
  //          Console.WriteLine(giantStep);
  //          Console.WriteLine();
  //          AffinePoint temp;
  //          for (BigInteger j = 0; j < m; j++)
  //          {
  //              temp = Q - j * giantStep;
  //              Console.WriteLine(temp);
  //              BigInteger i = -1;
  //              try
  //              {
  //                  i = babySteps[temp];
  //              }
  //              catch (KeyNotFoundException e) { }
  //              if (i != -1)
  //                  return BigIntegerExtension.ModPositive(i + j * m, order);  //тут еще по модулю порядка эл кривой надо брать (хз как правильно сделать)
  //          }

  //          //make modification with m/2 and +-iP


  //          return -1;
  //      }
  //}