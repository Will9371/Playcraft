using UnityEngine;

namespace Playcraft
{
    public class LerpPositionArray : MonoBehaviour
    {
        [SerializeField] LerpPosition[] elements;
        
        public void Input(float value)
        {
            foreach (var element in elements)
                element.percent = value;
        }
        
        public void SetEndValues(Vector3[] values)
        {
            for (int i = 0; i < elements.Length; i++)
            {
                if (i >= values.Length) break;
                elements[i].end = values[i];
            }
        }
    }
}
