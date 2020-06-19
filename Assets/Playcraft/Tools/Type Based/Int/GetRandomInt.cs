using UnityEngine;

namespace Playcraft
{
    public class GetRandomInt : MonoBehaviour
    {
        [SerializeField] int minimum;
        [SerializeField] int maximum;
        [SerializeField] IntEvent Output;
        
        public void Input()
        {
            Output.Invoke(Random.Range(minimum, maximum + 1));
        }
    }
}
