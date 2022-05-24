using UnityEngine;

namespace ZMD
{
    public class GetDuration : MonoBehaviour
    {
        float startTime;
        [SerializeField] FloatEvent Output = default;

        public void Begin()
        {
            startTime = Time.time;
        }
        
        public void End()
        {
            Output.Invoke(Time.time - startTime);
            Begin();
        }
    }
}
