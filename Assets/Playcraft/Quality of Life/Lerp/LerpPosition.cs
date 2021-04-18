using UnityEngine;

namespace Playcraft
{
    public class LerpPosition : MonoBehaviour
    {
        [SerializeField] Transform self;
        [SerializeField] int defaultIndex;
        [SerializeField] bool useLocal = true;
        public Vector3[] positions;

        public Vector3 start
        {
            get => process.start;
            set => process.start = value;
        }
        
        public Vector3 end
        {
            get => process.end;
            set => process.end = value;
        } 
                
        Lerp_Position _process;
        Lerp_Position process
        {
            get
            {
                if (_process == null) Initialize();
                return _process;
            }
        }
                
        void Initialize()
        {
            if (!self) self = transform;
            _process = new Lerp_Position(self, positions, defaultIndex, useLocal);            
        }
        
        // Move between internally-stored positions
        public void SetDestination(int newIndex) { process.SetDestination(newIndex); }
        
        // Move towards externally defined location
        public void SetDestination(Vector3 destination) { process.SetDestination(destination); }
        
        Vector3 position;

        // Call continuously to move over time
        public void Input(float percent) { process.Input(percent); }
        
        public void SetDestinations(Vector3Array input) { SetDestinations(input.values); }
        public void SetDestinations(Vector3[] input) { process.SetDestinations(input); }
    }
}
