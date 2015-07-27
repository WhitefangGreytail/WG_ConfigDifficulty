using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Xml;
using ICities;
using UnityEngine;
using ColossalFramework.Plugins;


namespace WG_ConfigDifficulty
{
    public class LoadingExtension : LoadingExtensionBase
    {
        public const String XML_FILE = "WG_ConfigDifficulty.xml";
        public String currentFileLocation = ColossalFramework.IO.DataLocation.localApplicationData + Path.DirectorySeparatorChar + XML_FILE;

        public override void OnLevelUnloading()
        {
            try
            {
                XML_VersionOne xml = new XML_VersionOne();
                xml.writeXML(currentFileLocation);
            }
            catch (Exception e)
            {
                Debugging.panelMessage(e.Message);
            }
        }

        public override void OnCreated(ILoading loading)
        {
            // To overwrite the game defaults which EconomyExtensionBase is used before OnLevelLoaded is called
            readFromXML();
            Debugging.panelMessage("Loaded!");
        }

        public override void OnLevelLoaded(LoadMode mode)
        {
            if (mode == LoadMode.LoadGame || mode == LoadMode.NewGame)
            {
//                readFromXML();
            }
        }


        /// <summary>
        ///
        /// </summary>
        private void readFromXML()
        {
            bool fileAvailable = File.Exists(currentFileLocation);
            bool[] mathToDefault = null;

            if (fileAvailable)
            {
                // Load in from XML - Designed to be flat file for ease
                XmlDocument doc = new XmlDocument();
                try
                {
                    doc.Load(currentFileLocation);

                    XML_VersionOne reader = new XML_VersionOne();
                    mathToDefault = reader.readXML(doc);
                }
                catch (Exception e)
                {
                    // Game will now use defaults
                    Debugging.panelMessage(e.Message);
                }
            }
            else
            {
                Debugging.panelMessage("Configuration file not found. Will output new file to : " + currentFileLocation);
            }

            doMathDefaults(mathToDefault);
        }

        /// <summary>
        /// Sets the defaults to what the game currently have. We'll have other XML defaults
        /// </summary>
        /// <param name="mathToDefault"></param>
        private void doMathDefaults(bool[] mathToDefault)
        {
            if (mathToDefault == null)
            {
                mathToDefault = new bool[7];
                for (int i = 0; i < mathToDefault.Length; i++)
                {
                    mathToDefault[i] = false;
                }
            }

            WG_MathParam defaultParam = new WG_MathParam(1.0, 0.0);
            if (!mathToDefault[DataStore.CONSTRUCT])
            {
                DataStore.calcObjects[DataStore.CONSTRUCT] = new Linear();
                DataStore.calcObjects[DataStore.CONSTRUCT].setDefaults();
            }

            if (!mathToDefault[DataStore.MAINT])
            {
                DataStore.calcObjects[DataStore.MAINT] = new Linear();
                DataStore.calcObjects[DataStore.MAINT].setDefaults();
            }

            if (!mathToDefault[DataStore.RELOC])
            {
                DataStore.calcObjects[DataStore.RELOC] = new Percentage();
                //new WG_MathParam(20.0, 0.0)
            }
            // FIXME TODO

            if (!mathToDefault[DataStore.REFUND])
            {
                DataStore.calcObjects[DataStore.REFUND] = new Percentage();
                //new WG_MathParam(75.0, 0.0)
            }

            if (!mathToDefault[DataStore.DEMAND_RES])
            {
                DataStore.calcObjects[DataStore.DEMAND_RES] = new Linear();
                DataStore.calcObjects[DataStore.DEMAND_RES].setDefaults();
            }

            if (!mathToDefault[DataStore.DEMAND_COM])
            {
                DataStore.calcObjects[DataStore.DEMAND_COM] = new Linear();
                DataStore.calcObjects[DataStore.DEMAND_COM].setDefaults();
            }

            if (!mathToDefault[DataStore.DEMAND_IND])
            {
                DataStore.calcObjects[DataStore.DEMAND_IND] = new Linear();
                DataStore.calcObjects[DataStore.DEMAND_IND].setDefaults();
            }
        }
    }
}
