// Credit: Game Dev Guide
// Source: https://www.youtube.com/watch?v=5roZtuqZyuw

using System;
using UnityEngine;

// Customize for your project and remove from namespace
namespace Playcraft.Saving
{
    [Serializable]
    public class Profile
    {
        public string playerName;
        public int currency;
        public int experience;
        
        // Requires Json.Net for Unity
        //[SerializeField, JsonProperty]
        //public SavedProperties savedProperties = new SavedProperties();
    }
}

