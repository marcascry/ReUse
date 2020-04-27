using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ReUse
{
    public class MathTool
    {
        protected static Random GetRandom = new Random();
        public static int RandomInt(int min, int max)
        {
            if (max < min)
            {
                min += max;
                max = min - max;
                min = min - max;
            }
            int result = GetRandom.Next(min,max);
            return result;
            
        }

        public static double RandomDouble(double min, double max)
        {
            if (max < min)
            {
                min += max;
                max = min - max;
                min = min - max;
            }
            double result = GetRandom.NextDouble()*(max-min)+min;
            //Thread.Sleep(100);
            return result;

        }

    }
}
