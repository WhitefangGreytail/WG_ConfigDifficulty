using System;
using System.IO;
using ColossalFramework.Plugins;

namespace WG_ConfigDifficulty
{
    class Debugging
    {
        public const String MOD_NAME = "WG_ConfigDifficulty";

        // Write to WG log file
        public static void writeDebugToFile(String text)
        {
            using (FileStream fs = new FileStream(ColossalFramework.IO.DataLocation.localApplicationData + Path.DirectorySeparatorChar + "WG_log.log", FileMode.Append, FileAccess.Write))
            using (StreamWriter sw = new StreamWriter(fs))
            {
                sw.WriteLine(text);
            }
        }

        // Write a message to the panel
        public static void panelMessage(string text)
        {
            DebugOutputPanel.AddMessage(PluginManager.MessageType.Message, MOD_NAME + ": " + text);
        }


        // Write a warning to the panel
        public static void panelWarning(string text)
        {
            DebugOutputPanel.AddMessage(PluginManager.MessageType.Warning, MOD_NAME + ": " + text);
        }


        // Write an error to the panel
        public static void panelError(string text)
        {
            DebugOutputPanel.AddMessage(PluginManager.MessageType.Error, MOD_NAME + ": " + text);
        }

    }
}
