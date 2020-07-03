using UnityEngine;

namespace Playcraft.Examples.ColorCatcher
{
    public class SwapWispBlockerData : MonoBehaviour
    {        
        #pragma warning disable 0649
        [SerializeField] GetBlockerData data;
        [SerializeField] float slideTime = 0.2f;
        #pragma warning restore 0649
        
        int count => data.count;
        public bool cooldown;        
                        
        
        private void BeginSlide(int fromIndex, int toIndex)
        {
            if (cooldown) return;
            var slider = data.GetSlider(fromIndex).move;
            var destination = data.GetSlider(toIndex);
            slider.BeginSlide(destination, slideTime);
        }
        
        public void EndSlide()
        {
            cooldown = false;
        }

        public void Swap(IntArray data) { Swap(data.values[0], data.values[1]); }
        private void Swap(int a, int b)
        {
            BeginSlide(a, b);
            BeginSlide(b, a);
            cooldown = true;
        }
        
        public void Cycle(bool clockwise)
        {            
            for (int i = 0; i < count; i++)
            {
                var original = i;                
                original = RangeMath.CycleInt(original, count - 1, !clockwise);
                BeginSlide(original, i);
            }
            
            cooldown = true;
        }
    }
}
