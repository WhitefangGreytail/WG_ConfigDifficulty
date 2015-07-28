using System;
using System.Collections.Generic;
using System.Xml;


namespace WG_ConfigDifficulty
{
    // This is scaled to the 0 - 100 for demand
    class Sigmoid : WGCD_Math
    {
        public static string NAME = "sigmoid";
        double a;
        double b;
        Dictionary<int, int> sigMap = new Dictionary<int,int>(150);


        public Sigmoid()
        {
        }


        public override void setDefaults()
        {
            a = 1.0;
            b = 0.0;
            calculateArray();
        }

        public override void readXML(XmlNode node)
        {
            a = XMLHelper.takeParam(node, "a", 1.0);
            b = XMLHelper.takeParam(node, "b", 0.0);
            calculateArray();
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
            // Returns 0 to 100
            return (100 / (1 + Math.Exp(a * -0.1 * (input - 50)))) + b;
        }


        public override int calculateReturnValue(int input)
        {
            int output = 0;

            if (!sigMap.TryGetValue(input, out output))
            {
                output = addToMap(input);
            }

            return output;
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

        private int addToMap(int input)
        {
            int output = (int) ((100 / (1 + Math.Exp(a * -0.1 * (input - 50)))) + b);
            sigMap.Add(input, output);
            return output;
        }
    }
}
