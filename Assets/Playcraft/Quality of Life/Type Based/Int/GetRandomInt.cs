using UnityEngine;

namespace Playcraft
{
    public class GetRandomInt : MonoBehaviour
    {
        #pragma warning disable 0649
        [SerializeField] int minimum;
        [SerializeField] int maximum;
        [SerializeField] IntEvent Output;
        #pragma warning restore 0649
        
        public void Input()
        {
            Output.Invoke(Random.Range(minimum, maximum + 1));
        }
    }
}
