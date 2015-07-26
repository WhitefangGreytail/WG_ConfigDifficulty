using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Xml;


namespace WG_ConfigDifficulty
{
    class XML_VersionOne
    {
        private const string econName = "economy";
        private const string demandName = "demand";
        private const string levelUpName = "levelUp";

        // The 
        private bool[] mathToDefault = { false, false, false, false, false, false, false };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="doc"></param>
        public bool[] readXML(XmlDocument doc)
        {
            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {
                if (node.Name.Equals(econName))
                {
                    readEconomyNode(node);
                }
                else if (node.Name.Equals(demandName))
                {
                    readDemandNode(node);
                }
                else if (node.Name.Equals(levelUpName))
                {
                    readLevelUpNode(node);
                }
            }

            return mathToDefault;
        }

        private void readEconomyNode(XmlNode node)
        {
            throw new NotImplementedException();
        }

        private void readDemandNode(XmlNode node)
        {
            throw new NotImplementedException();
        }

        private void readLevelUpNode(XmlNode node)
        {
            // Probably do nothing for a while
        }


        public void writeXML(string fileLocation)
        {
            XmlDocument xmlDoc = new XmlDocument();

            XmlNode rootNode = xmlDoc.CreateElement("WG_ConfigDifficulty");
            XmlAttribute attribute = xmlDoc.CreateAttribute("version");
            attribute.Value = "1";
            rootNode.Attributes.Append(attribute);
            xmlDoc.AppendChild(rootNode);

            XmlNode econNode = xmlDoc.CreateElement(econName);
            XmlNode demandNode = xmlDoc.CreateElement(demandName);
            XmlNode levelUpNode = xmlDoc.CreateElement(levelUpName);

            // Economynode
            makeEconomyNode(xmlDoc, econNode, "construct", DataStore.calcObjects[DataStore.CONSTRUCT]);
            makeEconomyNode(xmlDoc, econNode, null, DataStore.calcObjects[DataStore.MAINT]);
            makeEconomyNode(xmlDoc, econNode, null, DataStore.calcObjects[DataStore.RELOC]);
            makeEconomyNode(xmlDoc, econNode, null, DataStore.calcObjects[DataStore.REFUND]);

            // DemandNode
            makeDemandNode(xmlDoc, demandNode, null, DataStore.calcObjects[DataStore.DEMAND_RES]);
            makeDemandNode(xmlDoc, demandNode, null, DataStore.calcObjects[DataStore.DEMAND_COM]);
            makeDemandNode(xmlDoc, demandNode, null, DataStore.calcObjects[DataStore.DEMAND_IND]);

            // LevelNode
            // makeLevelUpNode(levelUpNode, null, ???)

            rootNode.AppendChild(econNode);
            rootNode.AppendChild(demandNode);

            if (File.Exists(fileLocation))
            {
                try
                {
                    if (File.Exists(fileLocation + ".bak"))
                    {
                        File.Delete(fileLocation + ".bak");
                    }
                    File.Move(fileLocation, fileLocation + ".bak");
                }
                catch (Exception e)
                {
                    Debugging.panelMessage(e.Message);
                }
            }

            try
            {
                xmlDoc.Save(fileLocation);
            }
            catch (Exception e)
            {
                Debugging.panelMessage(e.Message);
            }
        }

        public void makeEconomyNode(XmlDocument xmlDoc, XmlNode rootNode, String name, WGCD_Math a)
        {
            XmlNode node = xmlDoc.CreateElement(name);

            XmlAttribute attribute = xmlDoc.CreateAttribute("type");
            attribute.Value = Convert.ToString(getMathType(a));
            node.Attributes.Append(attribute);

            // TODO - repeat for a and c
        }

        public void makeDemandNode(XmlDocument xmlDoc, XmlNode rootNode, String name, WGCD_Math a)
        {
            XmlNode node = xmlDoc.CreateElement(name);

            XmlAttribute attribute = xmlDoc.CreateAttribute("type");
            attribute.Value = Convert.ToString(getMathType(a));
            node.Attributes.Append(attribute);

            // TODO - repeat for a and c
        }

        public void makeLevelUpNode(XmlDocument xmlDoc, XmlNode rootNode, String name)
        {
            // Probably do nothing for a while
        }

        /// <summary>
        /// Gets the name of the math type for the 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public String getMathType(WGCD_Math type)
        {
            // Default is off
            MODIFIER mod = MODIFIER.off;

            if (type is Logarithmic)
            {
                mod = MODIFIER.logarithmic;
            }
            else if (type is Sigmoid)
            {
                mod = MODIFIER.sigmoid;
            }
            else if (type is Linear)
            {
                mod = MODIFIER.linear;
            }
            else if (type is Percentage)
            {
                mod = MODIFIER.percentage;
            }

            return mod.ToString("G").ToLowerInvariant();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="w"></param>
        /// <returns></returns>
        public WGCD_Math getMathObject(String type, WG_MathParam w)
        {
            if (type.Equals(MODIFIER.logarithmic.ToString("G"), StringComparison.CurrentCultureIgnoreCase))
            {
                return new Logarithmic(w);
            }
            else if (type.Equals(MODIFIER.sigmoid.ToString("G"), StringComparison.CurrentCultureIgnoreCase))
            {
                return new Sigmoid(w);
            }
            else if (type.Equals(MODIFIER.linear.ToString("G"), StringComparison.CurrentCultureIgnoreCase))
            {
                return new Linear(w);
            }
            else if (type.Equals(MODIFIER.percentage.ToString("G"), StringComparison.CurrentCultureIgnoreCase))
            {
                return new Percentage(w);
            }
            else if (type.Equals(MODIFIER.off.ToString("G"), StringComparison.CurrentCultureIgnoreCase))
            {
                return new Off(w);
            }
            else
            {
                // Log error here
                Debugging.panelWarning("Math type passed ( " + type + " ) which does not match any existing types. Defaulting to linear");
                return new Linear(w);
            }
        }
    }
}
