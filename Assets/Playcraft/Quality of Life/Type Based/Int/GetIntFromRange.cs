using UnityEngine;

// REFACTOR: multiple responsibilities
namespace Playcraft
{
    public class GetIntFromRange : MonoBehaviour
    {
        #pragma warning disable 0649
        [SerializeField] int minimum;
        [SerializeField] int maximum;
        [SerializeField] IntEvent Output;
        
        // REFACTOR
        [SerializeField] bool disableRepeat;
        [SerializeField] bool disableSpecific;
        [SerializeField] int specificDisable;
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
        
        // REFACTOR
        public void Randomize()
        {
            var disabled = disableRepeat ? index : specificDisable;
        
            index = disableRepeat || disableSpecific ? 
                RandomStatics.RandomNoRepeat(minimum, maximum + 1, disabled) : 
                Random.Range(minimum, maximum + 1);
                
            Output.Invoke(index);
        }
    }
}
