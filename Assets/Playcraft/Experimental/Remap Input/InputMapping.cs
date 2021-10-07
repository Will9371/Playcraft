using System;
using UnityEngine;

namespace Playcraft
{
    public class InputMapping : MonoBehaviour
    {
        [SerializeField] InputMap[] maps;
        
        public KeyCode[] GetKeys(SO actionId)
        {
            foreach (var map in maps)
                if (map.actionId == actionId)
                    return map.keys;
                    
            return null;
        }
        
        void Start() { RefreshAll(); }
        
        public Action<SO, KeyCode[]> onRemap;

        void RefreshAll()
        {
            foreach (var map in maps)
                Remap(map.actionId, map.keys);
        }
        
        public void Remap(SO actionId, KeyCode[] keys) { onRemap.Invoke(actionId, keys); }

        [Serializable]
        public class InputMap
        {
            public SO actionId;
            public KeyCode[] keys;
        }
    }
}
