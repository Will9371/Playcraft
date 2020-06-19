using UnityEngine;

namespace Playcraft.Examples.ColorCatcher
{
    public class SwapWispBlockerData : MonoBehaviour
    {
        public void Swap(IntArray data) { Swap(data.values[0], data.values[1]); }

        public void Swap(int aIndex, int bIndex)
        {
            var a = transform.GetChild(aIndex).GetComponent<WispBlockerOverride>();
            var b = transform.GetChild(bIndex).GetComponent<WispBlockerOverride>();
            var aData = a.data;
            var bData = b.data;
            
            a.Set(bData);
            b.Set(aData);
        }
        
        public void Cycle(bool clockwise)
        {
            var count = transform.childCount;
            var components = new WispBlockerOverride[count];
            var data = new BlockerData[count];
            
            for (int i = 0; i < count; i++)
            {
                components[i] = transform.GetChild(i).GetComponent<WispBlockerOverride>();
                data[i] = components[i].data;
            }
            
            for (int i = 0; i < count; i++)
            {
                var from = i;
                
                if (clockwise)
                {
                    from--;
                    if (from < 0) 
                        from = count - 1;
                }
                else
                {
                    from++;
                    if (from >= count)
                        from = 0;
                }
                
                components[i].Set(data[from]);
            }
        }
    }
}
