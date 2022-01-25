using UnityEngine;

namespace ZMD.PredictiveMovement
{
    public class ThiefCursor : MonoBehaviour
    {
        public RespondToCustomTag treasureIds;
        public RespondToCustomTag guardIds;

        public float cooldown = 0.5f;
        float lastCaughtTime;
        float lastStealTime;
        
        public DisplayText text;
        public float displayTextTime = 1.5f;
        
        bool recentlyCaught => Time.time - lastCaughtTime < cooldown;
        bool recentlyStolen => Time.time - lastStealTime < cooldown;

        void OnTriggerEnter2D(Collider2D other)
        {
            if (guardIds.Input(other))
                Caught();
            else if (treasureIds.Input(other))
                Steal();
        }
        
        void Caught()
        {
            if (recentlyCaught) return;
            lastCaughtTime = Time.time;
            text.DisplayForTime("Caught!", displayTextTime);
        }
        
        void Steal()
        {
            if (recentlyCaught || recentlyStolen) return;
            lastStealTime = Time.time;
            Invoke(nameof(Verdict), cooldown);
        }
        
        void Verdict()
        {
            if (recentlyCaught) return;
            text.DisplayForTime("Steal!", displayTextTime);
        }
    }
}
