using UnityEngine;

namespace Playcraft
{
    public class LerpPositionArray : MonoBehaviour
    {
        [SerializeField] LerpPosition[] elements;
        
        public void Input(float value)
        {
            foreach (var element in elements)
                element.Input(value);
        }
    }
}
