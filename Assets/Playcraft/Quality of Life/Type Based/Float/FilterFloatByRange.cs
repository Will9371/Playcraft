using UnityEngine;
using UnityEngine.Events;

namespace Playcraft
{
    public class FilterFloatByRange : MonoBehaviour
    {
        #pragma warning disable 0649
        [SerializeField] Vector2 range;
        [SerializeField] UnityEvent OnSuccess;
        [SerializeField] UnityEvent OnFail;
        #pragma warning restore 0649
        
        public void Input(float value)
        {
            if (value >= range.x && value <= range.y)
                OnSuccess.Invoke();
            else
                OnFail.Invoke();
        }
    }
}
