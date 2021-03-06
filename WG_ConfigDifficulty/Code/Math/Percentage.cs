﻿using System;
using System.Collections.Generic;
using System.Xml;


namespace WG_ConfigDifficulty
{
    class Percentage : WGCD_Math
    {
        public static string NAME = "percentage";
        double a;

        public Percentage(double a)
        {
            this.a = a / 100.0;
        }

        public override void setDefaults()
        {
            a = 1.0;
        }

        public override void readXML(XmlNode node)
        {
            a = (XMLHelper.takeParam(node, "a", 100.0) / 100.0);
        }

        public override XmlNode generateXML(XmlDocument xmlDoc, string elementName)
        {
            XmlNode node = xmlDoc.CreateElement(elementName);

            XmlAttribute attribute = xmlDoc.CreateAttribute("type");
            attribute.Value = Convert.ToString(NAME);
            node.Attributes.Append(attribute);

            attribute = xmlDoc.CreateAttribute("a");
            attribute.Value = Convert.ToString(a * 100.0);
            node.Attributes.Append(attribute);

            return node;
        }

        public override int calculateReturnValue(int input)
        {
            return (int) (a * input);
        }
    }
}
