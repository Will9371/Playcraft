using UnityEngine;

namespace Playcraft
{
    public class GetGameObjectFromArray : MonoBehaviour
    {
        #pragma warning disable 0649
        [SerializeField] GameObject[] elements;
        [SerializeField] GameObjectEvent Output;
        #pragma warning restore 0649
        
        public void Input(int value)
        {
            Output.Invoke(elements[value]);
        }
    }
}
