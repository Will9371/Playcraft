// Credit: Game Dev Guide
// Source: https://www.youtube.com/watch?v=5roZtuqZyuw

using System;
using System.Collections.Generic;
using UnityEngine;

// Customize for your project and remove from namespace
namespace Playcraft.Examples.Saving
{
    [Serializable]
    public class SaveData
    {
        #region Singleton
        static SaveData _current;
        public static SaveData current
        {
            get
            {
                if (_current == null)
                    _current = new SaveData();
                    
                return _current;
            }
            set
            {
                if (value != null)
                    _current = value;
            }
        }
        #endregion
        
        SaveData()
        {
            profile = new PlayerProfile();
            //shapes = new List<ShapeData>();
        }
        
        //public Action onLoadEvent;
        
        public PlayerProfile profile;
        
        public int sphereCount;
        public int cubeCount;
        
        //public List<ShapeData> shapes;
    }
}
