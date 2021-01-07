// Credit: Game Dev Guide
// Source: https://www.youtube.com/watch?v=5roZtuqZyuw

using System;

// Customize for your project and remove from namespace
namespace Playcraft.Examples.Saving
{
    [Serializable]
    public class PlayerProfile
    {
        public string playerName;
        public int currency;
        public int experience;
    }
}
