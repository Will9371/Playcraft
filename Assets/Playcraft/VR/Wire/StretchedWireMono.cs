using UnityEngine;

namespace Playcraft
{
    public class StretchedWireMono : MonoBehaviour
    {
        public Transform start, end;
        public StretchedWire process;
        
        void Update() { Stretch(); }
        
        void OnValidate() 
        {
            if (start) process.start = start;
            if (end) process.end = end;
            process.self = transform;
            
            Stretch(); 
        }

        public void Stretch() { process.Stretch(); }
    }
}
