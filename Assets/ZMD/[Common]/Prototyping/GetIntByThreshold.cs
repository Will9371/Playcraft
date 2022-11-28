using UnityEngine;

namespace ZMD
{
    public class GetIntByThreshold : MonoBehaviour
    {
        [SerializeField] int[] minimums;
        [SerializeField] IntEvent Output;
        [SerializeField] bool startOutputAtOne;
        [SerializeField] bool ignoreRepeat;
        
        int priorResult = -1;
        
        public void SetMinimums(int[] value) { minimums = value; }
        
        public void GetIndexByValue(int value)
        {
            var offset = startOutputAtOne ? 1 : 0;
            var result = priorResult;
        
            for (int i = minimums.Length - 1; i >= 0; i--)
            {
                if (value >= minimums[i])
                {
                    result = i + offset;
                    break;
                }
            }
                    
            if (!ignoreRepeat || result != priorResult)        
                Output.Invoke(result);
                
            priorResult = result;
        }
    }
}
