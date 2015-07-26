using System;
using System.Collections.Generic;


namespace WG_ConfigDifficulty
{
    class Logarithmic : WGCD_Math
    {
        double a;
        double c;

        Dictionary<int, int> logMap = new Dictionary<int,int>(100);

        public Logarithmic(WG_MathParam param)
        {
            this.a = param.multiplier;
            this.c = param.offset;
        }

        public void setParams(double a, double c)
        {
            this.a = a;
            this.c = c;
        }

        public double calculateReturnValue(double input)
        {
            // Take the log of the input, then scale by a, add c
            return (input * a * Math.Log10(input)) + c;
        }

        public int calculateReturnValue(int input)
        {
            int output = 0;

            if (!logMap.TryGetValue(input, out output))
            {
                output = addToMap(input);
            }

            return output;
        }


        private int addToMap(int input)
        {
            int output = (int) ((input * a * Math.Log10(input)) + c);
            logMap.Add(input, output);
            return output;
        }
    }
}
