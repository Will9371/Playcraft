using UnityEngine;

namespace Playcraft
{
    public class GetIntFromRange : MonoBehaviour
    {
        #pragma warning disable 0649
        [SerializeField] int minimum;
        [SerializeField] int maximum;
        [SerializeField] IntEvent Output;
        [SerializeField] bool allowRepeat;
        #pragma warning restore 0649
        
        int index;
        
        public void Cycle(bool reverse)
        {
            index = reverse ? index - 1 : index + 1;
            
            if (index > maximum)
                index = minimum;
            else if (index < minimum)
                index = maximum;
            
            Output.Invoke(index);
        }
        
        public void Randomize()
        {
            index = allowRepeat ? Random.Range(minimum, maximum + 1) : 
                RandomStatics.RandomNoRepeat(minimum, maximum + 1, index);
                
            Output.Invoke(index);
        }
    }
}
