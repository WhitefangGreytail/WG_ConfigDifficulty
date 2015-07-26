using System;
using System.Collections.Generic;


namespace WG_ConfigDifficulty
{
    class Linear : WGCD_Math
    {
        double a;
        double b;

        public Linear(WG_MathParam param)
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
            return (a * input) + b;
        }

        public int calculateReturnValue(int input)
        {
            return (int) ((a * input) + b);
        }
    }
}
