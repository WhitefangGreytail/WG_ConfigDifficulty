using System;


namespace WG_ConfigDifficulty
{
    class Off : WGCD_Math
    {
        public Off(WG_MathParam param)
        {
        }

        public void setParams(double a, double b)
        {
        }

        public void getParams(out double a, out double b)
        {
            a = 0;
            b = 0;
        }

        public double calculateReturnValue(double input)
        {
            return input;
        }

        public int calculateReturnValue(int input)
        {
            return input;
        }
    }
}
