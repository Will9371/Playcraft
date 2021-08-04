using UnityEngine;
using UnityEngine.Events;

namespace Playcraft
{
    public class RangedRepeatingTimedEvent : MonoBehaviour
    {
        [SerializeField] Vector2 range;
        [SerializeField] UnityEvent Output;
        [SerializeField] bool beginOnEnable;
        [SerializeField] bool endOnDisable;
        
        void OnEnable() { if (beginOnEnable) Begin(); }
        void OnDisable() { if (endOnDisable) End(); }
        
        public void Begin() { Invoke(nameof(Act), RandomTime()); }
        public void End() { CancelInvoke(nameof(Act)); }
        
        void Act() 
        { 
            Output.Invoke();
            Invoke(nameof(Act), RandomTime());
        }
        
        float RandomTime() { return Random.Range(range.x, range.y); }    
    }
}
