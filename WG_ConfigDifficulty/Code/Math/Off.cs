using System;
using System.Xml;


namespace WG_ConfigDifficulty
{
    class Off : WGCD_Math
    {
        public static string NAME = "off";

        public Off()
        {
        }

        public override void setDefaults()
        {
        }

        public override void readXML(XmlNode node)
        {
        }

        public override XmlNode generateXML(XmlDocument xmlDoc, string elementName)
        {
            XmlNode node = xmlDoc.CreateElement(elementName);

            XmlAttribute attribute = xmlDoc.CreateAttribute("type");
            attribute.Value = Convert.ToString(NAME);
            node.Attributes.Append(attribute);

            return node;
        }

        public override double calculateReturnValue(double input)
        {
            return input;
        }

        public override int calculateReturnValue(int input)
        {
            return input;
        }
    }
}
