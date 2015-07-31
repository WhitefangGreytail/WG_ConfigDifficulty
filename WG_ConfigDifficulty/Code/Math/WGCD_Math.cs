using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Xml;


namespace WG_ConfigDifficulty
{
    abstract class WGCD_Math
    {
        public abstract void setDefaults();

        public abstract void readXML(XmlNode node);

        public abstract XmlNode generateXML(XmlDocument xmlDoc, string elementName);

        public abstract int calculateReturnValue(int input);
    }
}
