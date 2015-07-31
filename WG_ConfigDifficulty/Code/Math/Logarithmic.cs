using System;
using System.Collections.Generic;
using System.Xml;


namespace WG_ConfigDifficulty
{
    class Logarithmic : WGCD_Math
    {
        public static string NAME = "logarithmic";
        double a;
        double b;

        Dictionary<int, int> logMap = new Dictionary<int,int>(100);

        public Logarithmic()
        {
        }

        public override void setDefaults()
        {
            a = 1.0;
            b = 400.0; // Base of 300 so costs don't explode. Using a base of 100, I can just scrape by.
        }

        public override void readXML(XmlNode node)
        {
            a = XMLHelper.takeParam(node, "multiplier", 1.0);
            b = Math.Abs(XMLHelper.takeParam(node, "base", 400.0));  // Take absolute because a base that is negative is not defined
        }

        public override XmlNode generateXML(XmlDocument xmlDoc, string elementName)
        {
            XmlNode node = xmlDoc.CreateElement(elementName);

            XmlAttribute attribute = xmlDoc.CreateAttribute("type");
            attribute.Value = Convert.ToString(NAME);
            node.Attributes.Append(attribute);

            attribute = xmlDoc.CreateAttribute("multiplier");
            attribute.Value = Convert.ToString(a);
            node.Attributes.Append(attribute);

            attribute = xmlDoc.CreateAttribute("base");
            attribute.Value = Convert.ToString(b);
            node.Attributes.Append(attribute);

            return node;
        }

        public override int calculateReturnValue(int input)
        {
            int output = 0;

            if (input >= 0) // Stop imaginary numbers
            {
                if (!logMap.TryGetValue(input, out output))
                {
                    output = addToMap(input);
                }
            }
            return output;
        }


        private int addToMap(int input)
        {
            int output = (int) (input * a * Math.Log(input, b));
            logMap.Add(input, output);
            return output;
        }
    }
}
