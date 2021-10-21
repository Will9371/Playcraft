using UnityEngine;

namespace Playcraft
{
    public class GetRandomInt : MonoBehaviour
    {
        [SerializeField] int minimum;
        [SerializeField] int maximum;
        [SerializeField] bool preventRepeat;
        [SerializeField] IntEvent Output;
        
        int prior;
        
        public void Input() 
        {
            var output = preventRepeat ? 
                RandomStatics.RandomNoRepeat(minimum, maximum, prior) : 
                Random.Range(minimum, maximum + 1);
                
            Output.Invoke(output);
            prior = output; 
        }
    }
}
