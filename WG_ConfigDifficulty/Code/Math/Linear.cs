using System;
using System.Collections.Generic;


namespace WG_ConfigDifficulty
{
    class Linear : WGCD_Math
    {
        double a;
        double c;

        public Linear(WG_MathParam param)
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
            return (a * input) + c;
        }

        public int calculateReturnValue(int input)
        {
            return (int) ((a * input) + c);
        }
    }
}
