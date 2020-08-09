using UnityEngine;
using UnityEngine.Events;

namespace Playcraft
{
    public class SplitBool : MonoBehaviour
    {
        #pragma warning disable 0649
        [SerializeField] UnityEvent OnTrue;
        [SerializeField] UnityEvent OnFalse;
        #pragma warning restore 0649
        
        public void Input(bool value)
        {
            if (value) OnTrue.Invoke();
            else OnFalse.Invoke();
        }
    }
}
