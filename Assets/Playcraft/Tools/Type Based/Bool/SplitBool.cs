using UnityEngine;
using UnityEngine.Events;

namespace Playcraft
{
    public class SplitBool : MonoBehaviour
    {
        [SerializeField] UnityEvent OnTrue;
        [SerializeField] UnityEvent OnFalse;
        
        public void Input(bool value)
        {
            if (value) OnTrue.Invoke();
            else OnFalse.Invoke();
        }
    }
}
