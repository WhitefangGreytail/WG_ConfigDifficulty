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
        /// <param name="c"></param>
        void setParams(double a, double c);


        /// <summary>
        ///
        /// </summary>
        /// <param name="a"></param>
        /// <param name="c"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        double calculateReturnValue(double input);
        int calculateReturnValue(int input);
    }
}
