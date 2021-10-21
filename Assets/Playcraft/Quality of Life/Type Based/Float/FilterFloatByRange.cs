using UnityEngine;
using UnityEngine.Events;

namespace Playcraft
{
    public class FilterFloatByRange : MonoBehaviour
    {
        [SerializeField] Vector2 range;
        [SerializeField] UnityEvent OnSuccess;
        [SerializeField] UnityEvent OnFail;
        
        public void Input(float value)
        {
            if (value >= range.x && value <= range.y)
                OnSuccess.Invoke();
            else
                OnFail.Invoke();
        }
    }
}
