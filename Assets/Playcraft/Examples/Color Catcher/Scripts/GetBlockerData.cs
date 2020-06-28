using UnityEngine;

namespace Playcraft.Examples.ColorCatcher
{
    public class GetBlockerData : MonoBehaviour
    {
        public WispBlockerOverride[] components;
        public int count => components.Length;

        public WispBlockerOverride GetSlider(int index)
        {
            WispBlockerOverride slider = components[0];
            
            foreach (var item in components)
                if (item.data.index == index)
                    slider = item;
                    
            return slider;
        }
    }
}
