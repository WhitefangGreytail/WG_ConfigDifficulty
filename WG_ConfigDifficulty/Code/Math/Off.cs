using System;


namespace WG_ConfigDifficulty
{
    class Off : WGCD_Math
    {
        public Off(WG_MathParam param)
        {
        }

        public void setParams(double a, double c)
        {
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
