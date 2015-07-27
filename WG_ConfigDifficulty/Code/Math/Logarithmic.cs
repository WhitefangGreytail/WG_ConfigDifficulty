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
            b = 0.0;
        }

        public override void readXML(XmlNode node)
        {
            a = XMLHelper.takeParam(node, "a", 1.0);
            a = XMLHelper.takeParam(node, "b", 0.0);
        }

        public override XmlNode generateXML(XmlDocument xmlDoc, string elementName)
        {
            XmlNode node = xmlDoc.CreateElement(elementName);

            XmlAttribute attribute = xmlDoc.CreateAttribute("type");
            attribute.Value = Convert.ToString(NAME);
            node.Attributes.Append(attribute);

            attribute = xmlDoc.CreateAttribute("a");
            attribute.Value = Convert.ToString(a);
            node.Attributes.Append(attribute);

            attribute = xmlDoc.CreateAttribute("b");
            attribute.Value = Convert.ToString(b);
            node.Attributes.Append(attribute);

            return node;
        }

        public override double calculateReturnValue(double input)
        {
            // Take the log of the input, then scale by a, add b
            return (input * a * Math.Log(input, b));
        }

        public override int calculateReturnValue(int input)
        {
            int output = 0;

            if (!logMap.TryGetValue(input, out output))
            {
                output = addToMap(input);
            }

            return output;
        }


        private int addToMap(int input)
        {
            int output = (int) ((input * a * Math.Log10(input)) + b);
            logMap.Add(input, output);
            return output;
        }
    }
}
