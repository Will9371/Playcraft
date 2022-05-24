using UnityEngine;
using System.Collections.Generic;
using Object = UnityEngine.Object;

// Consider rename if becomes more general than scene management
namespace ZMD
{
    public class GlobalScenes : Singleton<GlobalScenes>
    {
        public GridInfo info;
        public GridActions actions;

        [HideInInspector] public List<Object> active = new List<Object>();
        
        void Start() { Add(info.start); }
        
        public void Enter(Object center)
        {
            var binding = info.GetBinding(center);
            if (binding == null) return;
            
            foreach (var adjacentScene in binding.adjacent)
                Add(adjacentScene);
                
            for (int i = active.Count - 1; i >= 0; i--)
                if (active[i] != center && !binding.adjacent.Contains(active[i]))
                    Remove(active[i]);
        }
        
        void Add(Object value)
        {
            if (active.Contains(value)) return;
            actions.Add(value);
            active.Add(value);
        }
        
        void Remove(Object value)
        {
            if (!active.Contains(value)) return;
            actions.Remove(value);
            active.Remove(value);
        }
    }
}
