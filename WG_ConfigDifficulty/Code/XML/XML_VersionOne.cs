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

        public const string CONSTRUCT = "construct";
        public const string MAINT = "maint";
        public const string RELOC = "relocation";
        public const string REFUND = "refund";
        public const string DEMAND_RES = "demand_res";
        public const string DEMAND_COM = "demand_com";
        public const string DEMAND_IND = "demand_ind";

        // The array stating which math has been loaded in
        private bool[] defaultMath = { true, true, true, true, true, true, true };

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

            return defaultMath;
        }

        private void readEconomyNode(XmlNode rootNode)
        {
            foreach (XmlNode node in rootNode.ChildNodes)
            {
                try
                {
                    // Get the index of the element
                    string nodeName = node.Name;
                    int mathIndex = -1;

                    string type = "";
                    XmlAttribute attribute = node.Attributes["type"];
                    if (attribute != null)
                    {
                        type = attribute.InnerText;
                    }
                    WGCD_Math math = getMathObject(type);
                    math.readXML(node); // Initialise itself

                    switch (nodeName.ToLower())
                    {
                        case CONSTRUCT:
                            mathIndex = DataStore.CONSTRUCT;
                            break;
                        case MAINT:
                            mathIndex = DataStore.MAINT;
                            break;
                        case RELOC:
                            mathIndex = DataStore.RELOC;
                            break;
                        case REFUND:
                            mathIndex = DataStore.REFUND;
                            break;
                        case DEMAND_RES:
                            mathIndex = DataStore.DEMAND_RES;
                            break;
                        case DEMAND_COM:
                            mathIndex = DataStore.DEMAND_COM;
                            break;
                        case DEMAND_IND:
                            mathIndex = DataStore.DEMAND_IND;
                            break;
                        default:
                            Debugging.panelMessage("Unknown node name detected: " + nodeName + ", ignoring.");
                            return;
                    }

                    if (math is Off)
                    {
                        // Do nothing, this is universally accepted
                    }
                    else if (mathIndex == DataStore.RELOC || mathIndex == DataStore.REFUND)
                    {
                        // Allow percentage, based off the construction cost
                        if (!(math is Percentage))
                        {
                            mathIndex = -1;
                        }
                    }
                    else if (mathIndex == DataStore.CONSTRUCT || mathIndex == DataStore.MAINT)
                    {
                        // Allow linear, log, 
                        if (!(math is Linear || math is Logarithmic))
                        {
                            mathIndex = -1;
                        }
                    }
                    else if  (mathIndex == DataStore.DEMAND_RES || mathIndex == DataStore.DEMAND_COM || mathIndex == DataStore.DEMAND_IND)
                    {
                        // Allow linear, sigmoid
                        if (!(math is Linear || math is Sigmoid))
                        {
                            mathIndex = -1;
                        }
                    }
                    // No else case because the switch already detected it for us

                    if (mathIndex >= 0)
                    {
                        DataStore.calcObjects[mathIndex] = math;
                        defaultMath[mathIndex] = false;
                    }
                }
                catch
                {
                    // defaultMath
                }
            } // end for
        }

        private void readDemandNode(XmlNode rootNode)
        {
            // They are the same, so pass it through
            readEconomyNode(rootNode);
        }

        private void readLevelUpNode(XmlNode rootNode)
        {
            // Probably do nothing until the full release
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="w"></param>
        /// <returns></returns>
        public WGCD_Math getMathObject(String type)
        {
            if (type.Equals(Linear.NAME, StringComparison.CurrentCultureIgnoreCase))
            {
                return new Linear();
            }
            else if (type.Equals(Logarithmic.NAME, StringComparison.CurrentCultureIgnoreCase))
            {
                return new Logarithmic();
            }
            else if (type.Equals(Percentage.NAME, StringComparison.CurrentCultureIgnoreCase))
            {
                return new Percentage(100.0);
            }
            else if (type.Equals(Sigmoid.NAME, StringComparison.CurrentCultureIgnoreCase))
            {
                return new Sigmoid();
            }
            else if (type.Equals(Off.NAME, StringComparison.CurrentCultureIgnoreCase))
            {
                return new Off();
            }
            else
            {
                Debugging.panelMessage("Math type passed ( " + type + " ) which does not match any existing types. Defaulting to linear");
                return new Off();
            }
        }


        public void writeXML(string fileLocation)
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlComment comment = xmlDoc.CreateComment("Math type templates (copy paste as required)\n" +
                                                      "type=\"linear\" a=\"1\" b=\"0\"\n" +
                                                      "type=\"logarithmic\" a=\"1\" b=\"10\"\n" +
                                                      "type=\"percentage\" a=\"20\"\n" +
                                                      "type=\"sigmoid\" a=\"1\" b=\"0\"\n" +
                                                      "type=\"off\"\n");
            xmlDoc.AppendChild(comment);

            XmlAttribute attribute = xmlDoc.CreateAttribute("version");
            attribute.Value = "1";
            XmlNode rootNode = xmlDoc.CreateElement("WG_ConfigDifficulty");
            rootNode.Attributes.Append(attribute);
            xmlDoc.AppendChild(rootNode);

            // Economynode
            XmlNode econNode = xmlDoc.CreateElement(econName);
            econNode.AppendChild(DataStore.calcObjects[DataStore.CONSTRUCT].generateXML(xmlDoc, CONSTRUCT));
            econNode.AppendChild(DataStore.calcObjects[DataStore.MAINT].generateXML(xmlDoc, MAINT));
            econNode.AppendChild(DataStore.calcObjects[DataStore.RELOC].generateXML(xmlDoc, RELOC));
            econNode.AppendChild(DataStore.calcObjects[DataStore.REFUND].generateXML(xmlDoc, REFUND));

            // DemandNode
            XmlNode demandNode = xmlDoc.CreateElement(demandName);
            demandNode.AppendChild(DataStore.calcObjects[DataStore.DEMAND_RES].generateXML(xmlDoc, DEMAND_RES));
            demandNode.AppendChild(DataStore.calcObjects[DataStore.DEMAND_COM].generateXML(xmlDoc, DEMAND_COM));
            demandNode.AppendChild(DataStore.calcObjects[DataStore.DEMAND_IND].generateXML(xmlDoc, DEMAND_IND));

            // LevelNode
            // XmlNode levelUpNode = xmlDoc.CreateElement(levelUpName);
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

        public void makeLevelUpNode(XmlDocument xmlDoc, XmlNode rootNode, String name)
        {
            // Probably do nothing for a while
        }
    }
}
