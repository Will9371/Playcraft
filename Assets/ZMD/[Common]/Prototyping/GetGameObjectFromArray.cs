using UnityEngine;

namespace ZMD
{
    public class GetGameObjectFromArray : MonoBehaviour
    {
        [SerializeField] GameObject[] elements;
        [SerializeField] GameObjectEvent Output;
        public void Input(int value) { Output.Invoke(elements[value]); }
    }
}
