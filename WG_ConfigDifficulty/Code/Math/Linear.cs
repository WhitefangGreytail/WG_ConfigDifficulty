using System;
using System.Collections.Generic;
using System.Xml;


namespace WG_ConfigDifficulty
{
    class Linear : WGCD_Math
    {
        public static string NAME = "linear";
        double a;
        double b;

        public Linear()
        {
            a = 1.2;
            b = 0.0;
        }

        public override void setDefaults()
        {
            a = 1.2;
            b = 0.0;
        }

        public override void readXML(XmlNode node)
        {
            a = XMLHelper.takeParam(node, "a", 1.2);
            b = XMLHelper.takeParam(node, "b", 0.0);
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

        public override int calculateReturnValue(int input)
        {
            return (int) ((a * input) + b);
        }
    }
}
