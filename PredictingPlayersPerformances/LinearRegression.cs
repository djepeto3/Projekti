using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PredictingPlayersPerformances
{
    class LinearRegression
    {
        public double k { get; set; }
        public double n { get; set; }
        public bool fited { get; set; }

        public void fit(double[] x, double[] y)
        {
            fited = true;
            double sum_x_times_y = 0;
            double sum_x = 0; 
            double sum_y = 0;
            double sum_x_square = 0;

            for (int i = 0; i < x.Length; i++)
            {
                sum_x_times_y += x[i] * y[i];
                sum_x += x[i];
                sum_y += y[i];
                sum_x_square += x[i] * x[i];
            }


            this.k = (x.Length * sum_x_times_y - sum_x * sum_y) / (x.Length * sum_x_square - sum_x * sum_x);
            this.n = (sum_y - this.k * sum_x) / x.Length;
        }

        public double predict(double x)
        {
            return k * x + n;
        }
    }
}
