using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace WG_ConfigDifficulty
{
    interface WGCD_Math
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        void setParams(double a, double b);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        void getParams(out double a, out double b);


        /// <summary>
        ///
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        double calculateReturnValue(double input);
        int calculateReturnValue(int input);
    }
}
