using UnityEngine;

namespace Playcraft
{
    public class CycleColor : MonoBehaviour
    {
        #pragma warning disable 0649
        [SerializeField] Color[] colors;
        [SerializeField] ColorEvent Color; 
        #pragma warning restore 0649
        
        int index;
        
        public void Cycle(int change)
        {
            index += change;
            
            while (index >= colors.Length) index -= colors.Length;
            while (index < 0) index += colors.Length;
            
            Set(index);
        }
        
        public void Set(int index)
        {
            if (index >= colors.Length) return;
            this.index = index;
            Color.Invoke(colors[index]);
        }
        
        public void Randomize()
        {
            Set(Random.Range(0, colors.Length));
        }
    }
}