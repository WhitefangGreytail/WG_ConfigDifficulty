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
using System.Diagnostics;

namespace WG_ConfigDifficulty
{
    public class LoadingExtension : LoadingExtensionBase
    {
        public const String XML_FILE = "WG_ConfigDifficulty.xml";
        public String currentFileLocation = ColossalFramework.IO.DataLocation.localApplicationData + Path.DirectorySeparatorChar + XML_FILE;
        bool hasLoaded = false;
        Stopwatch sw = new Stopwatch();

        public override void OnLevelUnloading()
        {
            try
            {
                XML_VersionOne xml = new XML_VersionOne();
                xml.writeXML(currentFileLocation);
            }
            catch (Exception e)
            {
                Debugging.panelWarning(e.Message);
            }
        }

        public override void OnCreated(ILoading loading)
        {
            // To overwrite the game defaults which EconomyExtensionBase is used before OnLevelLoaded is called
            sw = Stopwatch.StartNew();
            readFromXML();
            sw.Stop();
            hasLoaded = true;
        }

        public override void OnLevelLoaded(LoadMode mode)
        {
            // Do nothing because loading from the main menu or from a different city is required
            if (hasLoaded)
            { 
                // To print notification, because I don't think OnCreated has access to a panel yet
                Debugging.panelMessage("Loaded in " + sw.ElapsedMilliseconds + " ms.");
                hasLoaded = false;
            }
        }


        /// <summary>
        ///
        /// </summary>
        private void readFromXML()
        {
            bool fileAvailable = File.Exists(currentFileLocation);
            bool[] defaultMath = null;

            if (fileAvailable)
            {
                // Load in from XML - Designed to be flat file for ease
                XmlDocument doc = new XmlDocument();
                try
                {
                    doc.Load(currentFileLocation);

                    XML_VersionOne reader = new XML_VersionOne();
                    defaultMath = reader.readXML(doc);
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

            doMathDefaults(defaultMath);
        }

        /// <summary>
        /// Sets the defaults to what the game currently have to have a starting point
        /// </summary>
        /// <param name="defaultMath"></param>
        private void doMathDefaults(bool[] defaultMath)
        {
            if (defaultMath == null)
            {
                defaultMath = new bool[7];
                for (int i = 0; i < defaultMath.Length; i++)
                {
                    defaultMath[i] = true;
                }
            }

            if (defaultMath[DataStore.CONSTRUCT])
            {
                DataStore.calcObjects[DataStore.CONSTRUCT] = new Linear();
                DataStore.calcObjects[DataStore.CONSTRUCT].setDefaults();
            }

            if (defaultMath[DataStore.MAINT])
            {
                DataStore.calcObjects[DataStore.MAINT] = new Linear();
                DataStore.calcObjects[DataStore.MAINT].setDefaults();
            }

            if (defaultMath[DataStore.RELOC])
            {
                DataStore.calcObjects[DataStore.RELOC] = new Percentage(50.0);
            }

            if (defaultMath[DataStore.REFUND])
            {
                DataStore.calcObjects[DataStore.REFUND] = new Percentage(50.0);
            }

            if (defaultMath[DataStore.DEMAND_RES])
            {
                DataStore.calcObjects[DataStore.DEMAND_RES] = new Sigmoid();
                DataStore.calcObjects[DataStore.DEMAND_RES].setDefaults();
            }

            if (defaultMath[DataStore.DEMAND_COM])
            {
                DataStore.calcObjects[DataStore.DEMAND_COM] = new Sigmoid();
                DataStore.calcObjects[DataStore.DEMAND_COM].setDefaults();
            }

            if (defaultMath[DataStore.DEMAND_IND])
            {
                DataStore.calcObjects[DataStore.DEMAND_IND] = new Sigmoid();
                DataStore.calcObjects[DataStore.DEMAND_IND].setDefaults();
            }
        }
    }
}
