using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ZMD
{
    public class GetIntFromRangeMono : MonoBehaviour
    {
        [SerializeField] GetIntFromRange process;
        [SerializeField] IntEvent Output;
        public void Randomize() { Output.Invoke(process.Randomize()); }
    }
    
    [Serializable]
    public class GetIntFromRange
    {
        [SerializeField] int minimum;
        [SerializeField] int maximum;
        
        [SerializeField] bool disableRepeat;
        [SerializeField] bool disableSpecific;
        [SerializeField] int specificDisable;
        
        int index;

        public int Randomize()
        {
            var disabled = disableRepeat ? index : specificDisable;
        
            index = disableRepeat || disableSpecific ? 
                RandomStatics.RandomNoRepeat(minimum, maximum + 1, disabled) : 
                Random.Range(minimum, maximum + 1);
                
            return index;
        }
    }
}
