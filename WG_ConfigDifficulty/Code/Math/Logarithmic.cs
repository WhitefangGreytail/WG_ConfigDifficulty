using System;
using System.Collections.Generic;


namespace WG_ConfigDifficulty
{
    class Logarithmic : WGCD_Math
    {
        double a;
        double b;

        Dictionary<int, int> logMap = new Dictionary<int,int>(100);

        public Logarithmic(WG_MathParam param)
        {
            this.a = param.multiplier;
            this.b = param.offset;
        }

        public void setParams(double a, double b)
        {
            this.a = a;
            this.b = b;
        }

        public void getParams(out double a, out double b)
        {
            a = this.a;
            b = this.b;
        }

        public double calculateReturnValue(double input)
        {
            // Take the log of the input, then scale by a, add b
            return (input * a * Math.Log10(input)) + b;
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
            int output = (int) ((input * a * Math.Log10(input)) + b);
            logMap.Add(input, output);
            return output;
        }
    }
}
