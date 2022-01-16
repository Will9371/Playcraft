using UnityEngine;

namespace Playcraft
{
    public class SwapPositions : MonoBehaviour
    {
        public void Swap(IntArray data) { Swap(data.values[0], data.values[1]); }

        public void Swap(int aIndex, int bIndex)
        {
            var a = transform.GetChild(aIndex);
            var b = transform.GetChild(bIndex);
        
            var aPosition = a.position;
            var bPosition = b.position;
            
            a.position = bPosition;
            b.position = aPosition;
            
            if (aIndex > bIndex)
            {
                a.SetSiblingIndex(bIndex);
                b.SetSiblingIndex(aIndex);
            }
            else
            {
                b.SetSiblingIndex(aIndex);
                a.SetSiblingIndex(bIndex);
            }
        }
        
        public void Cycle(bool clockwise)
        {
            var count = transform.childCount;
            
            if (clockwise)
            {
                var last = transform.GetChild(count - 1);
                last.SetSiblingIndex(0);
            }
            else
            {
                var first = transform.GetChild(0);
                first.SetSiblingIndex(count - 1);         
            }
        }
    }
}
