using System;
using System.Text;
using ICities;

namespace WG_ConfigDifficulty
{
    public class ConfigDifficulty : IUserMod
    {
        public string Name
        {
            get { return "WG Configurable Difficulty"; }
        }

        public string Description
        {
            get { return "Allows configuration of the game's economy, demand levels."; }
        }
    }
}
