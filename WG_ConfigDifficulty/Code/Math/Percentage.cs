using System;
using System.Collections.Generic;


namespace WG_ConfigDifficulty
{
    class Percentage : WGCD_Math
    {
        double a;

        public Percentage(WG_MathParam param)
        {
            this.a = param.multiplier / 100.0;
        }

        public void setParams(double a, double c)
        {
            this.a = a / 100.0;
        }

        public double calculateReturnValue(double input)
        {
            return (a * input);
        }

        public int calculateReturnValue(int input)
        {
            return (int) (a * input);
        }
    }
}
