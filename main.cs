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
    }
}