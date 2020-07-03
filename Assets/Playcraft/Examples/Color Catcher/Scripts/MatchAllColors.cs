using UnityEngine;

namespace Playcraft.Examples.ColorCatcher
{
    public class MatchAllColors : MonoBehaviour
    {
        [SerializeField] GetBlockerData data = default;
        
        public void Set(int index)
        {
            var item = data.GetSlider(index);

            foreach (var component in data.components)
                component.Set(item.data);
        }

        public void Clear()
        {                    
            foreach (var component in data.components)
                component.Reset(); 
        }
    }      
}
