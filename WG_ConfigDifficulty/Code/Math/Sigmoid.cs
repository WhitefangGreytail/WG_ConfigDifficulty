using System;
using System.Collections.Generic;


namespace WG_ConfigDifficulty
{
    // This is scaled to the 0 - 100 for demand
    class Sigmoid : WGCD_Math
    {
        double a;
        double c;
        Dictionary<int, int> sigMap = new Dictionary<int,int>(100);


        public Sigmoid(WG_MathParam param)
        {
            this.a = param.multiplier;
            this.c = param.offset;
            calculateArray();
        }


        public void setParams(double a, double c)
        {
            this.a = a;
            this.c = c;
            calculateArray();
        }


        public void calculateArray()
        {
            // Preload
            sigMap.Clear();
            for (int i = -10; i <= 110; i++)
            {
                addToMap(i);
            }
        }


        public double calculateReturnValue(double input)
        {
            // Returns 0 to 100
            return (100 / (1 + Math.Exp(a * -0.1 * (input - 50)))) + c;
        }


        public int calculateReturnValue(int input)
        {
            int output = 0;

            if (!sigMap.TryGetValue(input, out output))
            {
                output = addToMap(input);
            }

            return output;
        }


        private int addToMap(int input)
        {
            int output = (int) ((100 / (1 + Math.Exp(a * -0.1 * (input - 50)))) + c);
            sigMap.Add(input, output);
            return output;
        }
    }
}
