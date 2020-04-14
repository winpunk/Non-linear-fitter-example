using System;
using System.Threading;
using CenterSpace.NMath.Core;
using CenterSpace.NMath.Analysis;
using System.Linq;

namespace ConsoleApp
{
    class Program
    {
        

        static void Main (string[] args)
        {
            double[] xRaw;
            double[] yRaw;

            getData(); //Funcion that loads xRaw and yRaw with data.
            FitData(xRaw, yRaw);

        }


        private DoubleVector FitData (double[] xDataRaw, double[] yDataRaw)
        {


            DoubleParameterizedFunction func = new Function();

            var f = new DoubleParameterizedDelegate(
            func.Evaluate);

            var fitter =
              new OneVariableFunctionFitter<TrustRegionMinimizer>(f);
            DoubleVector x = new DoubleVector(xDataRaw);
            DoubleVector y = new DoubleVector(yDataRaw);
            DoubleVector init = new DoubleVector((yDataRaw.Min()).ToString() + " " + 0.5 + " " + ((yDataRaw.Max() - yDataRaw.Min())).ToString());
            DoubleVector solution = fitter.Fit(x, y, init);



            double[] yData = new double[xDataRaw.Length * 600];
            double[] xData = new double[xDataRaw.Length * 600];

            for (int g = 0; g < yData.Length; g++)
            {
                double rads = ((xDataCourse[0] + g)) * 2 * Math.PI / 180;
                yData[g] = MyFunction(solution, (rads));
                xData[g] = xDataCourse[0] + g;

                Console.Writeline("x: " + xData[g] + "   y: " + yData[g]);
            }

            return solution;
        }
		
		public double MyFunction (DoubleVector p, double x)
        {
            return p[0] + p[2]*Math.Pow(Math.Sin(p[1] + x), 2);
        }

        public class Function : DoubleParameterizedFunction
        {
            public Function ()
            { }

            public override double Evaluate (DoubleVector p, double x)
            {
                double a = p[0];
                double b = p[1];
                double c = p[2];
                return a + c*Math.Sin(b + x) * Math.Sin(b + x);
            }

            public override void GradientWithRespectToParams (DoubleVector p,
              double x, ref DoubleVector grad)
            {
                double a = p[0];
                double b = p[1];
                double c = p[2];
                grad[0] = 1;
                grad[1] = 2 * c * Math.Sin(x + b) * Math.Cos(x + b);
                grad[2] = Math.Sin(x + b) * Math.Sin(x + b);
            }
        }
    }
}