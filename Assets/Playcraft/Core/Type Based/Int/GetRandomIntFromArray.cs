using UnityEngine;

namespace Playcraft
{
    public class GetRandomIntFromArray : MonoBehaviour
    {
        [SerializeField] int[] values;
        [SerializeField] bool preventRepeat;
        [SerializeField] IntEvent Output;
        
        int priorIndex;
        
        public void Input() 
        {
            var index = preventRepeat ? 
                RandomStatics.RandomNoRepeat(0, values.Length, priorIndex) : 
                Random.Range(0, values.Length);
                
            Output.Invoke(values[index]);
            priorIndex = index; 
        }
    }
}